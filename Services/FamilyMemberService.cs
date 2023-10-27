using FamilySync.Core.Abstractions.Exceptions;
using FamilySync.Registry.Models.DTO;
using FamilySync.Registry.Models.Entities;
using FamilySync.Registry.Persistence;
using Microsoft.EntityFrameworkCore;
using Orleans.Runtime;

namespace FamilySync.Registry.Services;

public interface IFamilyMemberService
{
    Task<GetFamilyMemberDTO> Create(Guid familyId, PostFamilyMemberDTO dto);
    Task<GetFamilyMemberDTO> Get(Guid memberId, Guid familyId);
    Task<List<GetFamilyMemberDTO>> GetAll(Guid familyId);
    public Task Delete(Guid familyId, Guid memberId);
}

public class FamilyMemberService : IFamilyMemberService
{
    private readonly RegistryContext _context;

    public FamilyMemberService(RegistryContext context)
    {
        _context = context;
    }

    public async Task<GetFamilyMemberDTO> Create(Guid familyId, PostFamilyMemberDTO dto)
    {
        //TODO: Implement Mapster!
        var member = new FamilyMember
        {
            FamilyId = familyId,
            Name = dto.Name,
            MemberId = Guid.NewGuid()
        };

        if (!await _context.Families.AnyAsync(x => x.Id == member.FamilyId))
        {
            throw new NotFoundException(typeof(FamilyMember), member.FamilyId.ToString());
        }

        _context.Members.Add(member);
        await _context.SaveChangesAsync();

        //TODO: Implement Mapster!
        return new GetFamilyMemberDTO
        {
            MemberId = member.MemberId,
            Name = member.Name
        };    
    }

    public async Task<GetFamilyMemberDTO> Get(Guid memberId, Guid familyId)
    {
        var member = await _context.Members.FirstOrDefaultAsync(x =>
            x.MemberId == memberId && x.FamilyId == familyId);

        if (member == default)
        {
            throw new NotFoundException(typeof(FamilyMember), $"{familyId}:{memberId}");
        }

        //TODO: Implement Mapster!
        return new GetFamilyMemberDTO
        {
            MemberId = member.MemberId,
            Name = member.Name
        };
    }

    public async Task<List<GetFamilyMemberDTO>> GetAll(Guid familyId)
    {
        var members = await _context.Members.Where(x => x.FamilyId == familyId).ToListAsync();

        if (members.Count <= 0)
        {
            throw new NotFoundException(typeof(Family), familyId.ToString());
        }


        //TODO: REALLY NEED TO Implement Mapster!
        return members.Select(member => new GetFamilyMemberDTO { MemberId = member.MemberId, Name = member.Name}).ToList();
    }

    public async Task Delete(Guid familyId, Guid memberId)
    {
        var deleted = await _context.Members
            .Where(x => x.FamilyId == familyId && x.MemberId == memberId)
            .ExecuteDeleteAsync();

        if (deleted == 0)
        {
            throw new NotFoundException(typeof(FamilyMember), $"{familyId}:{memberId}");
        }
    }
}