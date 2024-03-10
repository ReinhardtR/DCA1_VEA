﻿using VEA.Core.Domain.Common.Bases;
using VEA.Core.Tools.OperationResult;

namespace VEA.Core.Domain.Aggregates.Events;

public class EventTitle : ValueObject<string>
{
    private EventTitle(string value) : base(value) { }

    private const int MinLength = 3;
    private const int MaxLength = 75;

    public static Result<EventTitle> Create(string value)
    {
        var validation = Result.Validator()
            .Assert(string.IsNullOrWhiteSpace(value), EventErrors.Title.TitleMustBeBetween3And75Characters())
            .Assert(value?.Length is < MinLength or > MaxLength, EventErrors.Title.TitleMustBeBetween3And75Characters())
            .Validate();
        return validation.IsFailure
            ? Result.Failure<EventTitle>(validation.Errors)
            : Result.Success(new EventTitle(value!));
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