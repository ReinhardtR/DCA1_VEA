using VEA.Core.Domain.Common.Bases;

namespace VEA.Core.Domain.Aggregates.Events;

public class ParticipationId : ValueObject<Guid>
{
    public ParticipationId(Guid value) : base(value) { }
    
    public static ParticipationId New()
    {
        return new ParticipationId(Guid.NewGuid());
    }
}