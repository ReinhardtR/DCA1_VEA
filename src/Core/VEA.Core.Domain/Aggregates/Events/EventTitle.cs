﻿using VEA.Core.Domain.Common.Bases;
using VEA.Core.Tools.OperationResult;

namespace VEA.Core.Domain.Aggregates.Events;

public class EventTitle : ValueObject<string>
{
    private EventTitle(string value) : base(value) { }

    private const int MinLength = 3;
    private const int MaxLength = 75;

    public static Result<EventTitle> Create(string title)
    {
        var instance = new EventTitle(title);
        
        Result validation = Validate(title);
        if (validation.IsFailure)
            return Result<EventTitle>.Failure(validation.Errors);

        //If there are no errors return a success
        return Result<EventTitle>.Success(instance);
    }
    
    public static Result Validate(string value)
    {
        List<Error> errors = new List<Error>();
        if (string.IsNullOrWhiteSpace(value))
            errors.Add(EventErrors.Title.TitleMustBeBetween3And75Characters());
        
        //Title has to be between 3 and 75 characters
        if (MinLength > value?.Length || value?.Length > MaxLength)
            errors.Add(EventErrors.Title.TitleMustBeBetween3And75Characters());
        
        //If there are errors return a failure
        if (errors.Count > 0)
            return Result.Failure(errors);
        
        //If there are no errors return a success
        return Result.Success();
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