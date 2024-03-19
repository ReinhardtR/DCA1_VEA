using VEA.Core.Domain.Common.Bases;

namespace VEA.Core.Domain.Aggregates.Events;

public class InvitationStatus(int id, string name) : Enumeration(id, name)
{
    public static readonly InvitationStatus Pending = new(1, "Pending");
    public static readonly InvitationStatus Declined = new(2, "Declined");
    public static readonly InvitationStatus Accepted = new(3, "Accepted");
}