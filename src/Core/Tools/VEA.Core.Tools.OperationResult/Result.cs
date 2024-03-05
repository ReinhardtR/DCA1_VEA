namespace VEA.Core.Tools.OperationResult;

public class Result
{
    public List<Error> Errors { get; } = [];
    public bool IsFailure => Errors.Count != 0;

    protected Result() { }
    protected Result(List<Error> errors) => Errors = errors;

    public static Result Success() => new();
    public static Result<T> Success<T>(T payload) => new(payload);

    public static Result Failure(List<Error> errors) => new(errors);
    public static Result Failure(Error error) => new([error]);
    public static Result<T> Failure<T>(List<Error> errors) => new(errors);
    public static Result<T> Failure<T>(Error error) => new([error]);

    public static ResultValidator Validator() => new();

    public static Result Merge(params Result[] results)
    {
        List<Error> errors = [];
        foreach (var result in results) errors.AddRange(result.Errors);
        return errors.Count == 0 ? Success() : Failure(errors);
    }
}

public class Result<T> : Result
{
    public T Payload { get; } = default!;
    public Result(T payload) => Payload = payload;
    public Result(List<Error> errors) : base(errors) { }
}

public sealed class ResultValidator
{
    private readonly List<Error> _errors = [];

    public ResultValidator Assert(Func<bool> condition, Error error)
    {
        if (condition()) return this;
        _errors.Add(error);
        return this;
    }

    public ResultValidator Assert(bool condition, Error error)
    {
        if (condition) return this;
        _errors.Add(error);
        return this;
    }

    public Result Validate() => _errors.Count == 0 ? Result.Success() : Result.Failure(_errors);
    public Result<T> Validate<T>(T payload) => _errors.Count == 0 ? Result.Success(payload) : Result.Failure<T>(_errors);
}