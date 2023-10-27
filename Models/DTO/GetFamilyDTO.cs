
namespace FamilySync.Registry.Models.DTO;

public class GetFamilyDTO
{
    public Guid Id { get; set; }
    public string Name { get; set; } = default!;
    public List<GetFamilyMemberDTO> FamilyMembers { get; set; } = new();
}