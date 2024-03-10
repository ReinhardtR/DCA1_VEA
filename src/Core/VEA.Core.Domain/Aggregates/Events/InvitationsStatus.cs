using VEA.Core.Domain.Common.Bases;

namespace VEA.Core.Domain.Aggregates.Events;

public class InvitationsStatus(int id, string name) : Enumeration(id, name)
{
    public static readonly InvitationsStatus Pending = new(1, "Pending");
    public static readonly InvitationsStatus Declined = new(2, "Declined");
    public static readonly InvitationsStatus Accepted = new(3, "Accepted");
}