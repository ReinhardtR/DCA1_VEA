namespace VEA.Core.Tools.OperationResult;

public class None() {}

public class Result<T>
{
  public T Payload { get; } = default!;
  public string ErrorMessage { get; } = null!;
  public bool IsFailure => !string.IsNullOrEmpty(ErrorMessage);

  protected Result() { }
  protected Result(T payload) => Payload = payload;
  protected Result(string errorMessage) => ErrorMessage = errorMessage;

  public static Result<T> Success() => new();
  public static Result<T> Success(T payload) => new(payload);
  public static Result<T> Failure(string errorMessage) => new(errorMessage);
}
