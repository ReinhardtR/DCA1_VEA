namespace VEA.Core.Tools.OperationResult;

public class Error
{
    public ErrorType ErrorType { get; }
    public int ErrorCode { get; }
    public string ErrorMessage { get; }

    public Error(ErrorType errorType, int errorCode, string errorMessage)
    {
        ErrorType = errorType;
        ErrorCode = errorCode;
        ErrorMessage = errorMessage;
    }
}