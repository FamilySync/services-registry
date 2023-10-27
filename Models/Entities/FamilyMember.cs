namespace FamilySync.Registry.Models.Entities;

public class FamilyMember
{
    //TODO Implement Role
    public Guid MemberId { get; set; }
    public Guid FamilyId { get; set; }
    public string Name { get; set; } = default!;
    public Family Family { get; set; } = default!;
}