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
    
    // override object.Equals
    public override bool Equals(object? obj)
    {
        if (obj == null || GetType() != obj.GetType())
        {
            return false;
        }
        Error e = (Error)obj;
        return ErrorType == e.ErrorType && ErrorCode == e.ErrorCode && ErrorMessage == e.ErrorMessage;
    }
}