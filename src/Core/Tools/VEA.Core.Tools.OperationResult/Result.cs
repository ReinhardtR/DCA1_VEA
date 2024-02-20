namespace VEA.Core.Tools.OperationResult;

public class Result : Result<Void>
{
  public Result() : base() { }
  public Result(string errorMessage) : base(errorMessage) { }

  public static Result Success() => new();
}

public class Result<T>
{
  public T Payload { get; } = default!;
  public string ErrorMessage { get; } = null!;
  public bool IsSuccess => string.IsNullOrEmpty(ErrorMessage);
  public bool IsFailure => !IsSuccess;

  protected Result(T payload) => Payload = payload!;
  protected Result() => Payload = default!;
  protected Result(string errorMessage) => ErrorMessage = errorMessage;

  public static Result<T> Success(T payload) => new(payload);
  public static Result<Void> Success() => new(new Void());
  public static Result<T> Failure(string errorMessage) => new(errorMessage);
}
