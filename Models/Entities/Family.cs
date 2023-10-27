namespace FamilySync.Registry.Models.Entities;

public class Family
{
    public Guid Id { get; set; }
    public string Name { get; set; } = default!;
    public List<FamilyMember> FamilyMembers { get; set; } = new();
}