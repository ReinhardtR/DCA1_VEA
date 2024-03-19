using VEA.Core.Domain.Common.Bases;
using VEA.Core.Tools.OperationResult;

namespace VEA.Core.Domain.Aggregates.Events;

public class InvitationId : ValueObject<Guid>
{
    public InvitationId(Guid value) : base(value) { }

    public static Result<InvitationId> New()
    {
        var instance = new InvitationId(Guid.NewGuid());
        return Result<InvitationId>.Success(instance);
    }
}