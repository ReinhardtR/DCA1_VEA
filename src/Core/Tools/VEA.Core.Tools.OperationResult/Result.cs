namespace VEA.Core.Tools.OperationResult;

public class None {}

public class Result<T>
{
  public T Payload { get; } = default!;
  public List<Error> Errors { get; } = new();
  public bool IsFailure => Errors.Any();

  protected Result() { }
  protected Result(T payload) => Payload = payload;
  protected Result(List<Error> errors) => Errors = errors;

  public static Result<T> Success() => new();
  public static Result<T> Success(T payload) => new(payload);
  public static Result<T> Failure(List<Error> errors) => new(errors);
}
