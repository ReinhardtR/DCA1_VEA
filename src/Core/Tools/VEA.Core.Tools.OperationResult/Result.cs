namespace VEA.Core.Tools.OperationResult;

// Result with no payload
public class Result
{
  public List<Error> Errors { get; } = [];
  public bool IsFailure => Errors.Count != 0;

  public Result() { }
  protected Result(List<Error> errors) => Errors = errors;

  public static Result Success() => new();

  public static Result Failure(List<Error> errors) => new(errors);
}

// Result with generic payload
public class Result<T> : Result
{
  public T Payload { get; } = default!;

  private Result(T payload) => Payload = payload;
  private Result(List<Error> errors) : base(errors) { }

  public static Result<T> Success(T payload) => new(payload);
  public new static Result<T> Failure(List<Error> errors) => new(errors);
}