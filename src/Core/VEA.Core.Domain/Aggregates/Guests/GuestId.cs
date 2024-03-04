using VEA.Core.Domain.Common.Bases;
using VEA.Core.Tools.OperationResult;

namespace VEA.Core.Domain.Aggregates.Guests;

public class GuestId : ValueObject<Guid>
{
    public GuestId(Guid value) : base(value){}
    
    public static Result<GuestId> New()
    {
        var instance = new GuestId(Guid.NewGuid());
        return Result<GuestId>.Success(instance);
    }
}