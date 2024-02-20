namespace VEA.Core.Tools.OperationResult;

public class Error
{
    public ErrorType ErrorType { get; set; }
    public int ErrorCode { get; set; }
    public string ErrorMessage { get; set; }

    public Error(ErrorType errorType, int errorCode, string errorMessage)
    {
        ErrorType = errorType;
        ErrorCode = errorCode;
        ErrorMessage = errorMessage;
    }
}