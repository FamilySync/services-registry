namespace FamilySync.Registry.Models.DTO;

public class GetFamilyMemberDTO
{
    public Guid MemberId { get; set; }
    public string Name { get; set; } = default!;
}