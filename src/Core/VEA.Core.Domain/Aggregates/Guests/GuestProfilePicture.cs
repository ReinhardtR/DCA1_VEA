using VEA.Core.Domain.Common.Bases;
using VEA.Core.Domain.Common.Values;
using VEA.Core.Tools.OperationResult;

namespace VEA.Core.Domain.Aggregates.Guests;

public class GuestProfilePicture : ValueObject<Url>
{
    public GuestProfilePicture(Url value) : base(value) {}
    
    public static Result<GuestProfilePicture> Create(Url value)
    {
        var validation = Validate(value);
        return validation.IsFailure 
            ? Result<GuestProfilePicture>.Failure(validation.Errors)
            : Result<GuestProfilePicture>.Success(new GuestProfilePicture(value));
    }

    private static Result Validate(Url value)
    {
        return Result.Success();
    }
}