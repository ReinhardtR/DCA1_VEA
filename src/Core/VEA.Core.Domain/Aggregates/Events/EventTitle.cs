using VEA.Core.Tools.OperationResult;

namespace VEA.Core.Domain;

public class EventTitle : ValueObject<string>
{
    private EventTitle(string value) : base(value)
    {
        
    }
    
    public static Result<EventTitle> Create(string title)
    {
        List<Error> errors = new List<Error>();
        if (string.IsNullOrWhiteSpace(title))
            errors.Add(EventErrors.TitleMustBeBetween3And75Characters());
        
        //Title has to be between 3 and 75 characters
        if (title?.Length is < 3 or > 75)
            errors.Add(EventErrors.TitleMustBeBetween3And75Characters());
        
        //If there are errors return a failure
        if (errors.Count > 0)
            return Result<EventTitle>.Failure(errors);
        
        //If there are no errors return a success
        return Result<EventTitle>.Success(new EventTitle(title));
    }
    
   //overide Object.equals
    public override bool Equals(object? obj)
    {
        if (obj is null) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != this.GetType()) return false;
        return Equals((EventTitle) obj);
    }

    
    
    
}