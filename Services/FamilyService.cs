using FamilySync.Core.Abstractions.Exceptions;
using FamilySync.Registry.Models.DTO;
using FamilySync.Registry.Models.Entities;
using FamilySync.Registry.Persistence;
using Microsoft.EntityFrameworkCore;

namespace FamilySync.Registry.Services;

public interface IFamilyService
{
    public Task<GetFamilyDTO> Create(PostFamilyDTO dto);
    public Task<GetFamilyDTO> Get(Guid id);
    public Task<GetFamilyDTO> Update(PostFamilyDTO dto, Guid id);
    public Task Delete(Guid id);
}

public class FamilyService : IFamilyService
{
    private readonly RegistryContext _context;
    
    public FamilyService(RegistryContext context)
    {
        _context = context;
    }
    
    public async Task<GetFamilyDTO> Create(PostFamilyDTO dto)
    {
        //TODO: Implement Mapster!
        var family = new Family
        {
            Name = dto.Name
        };

        _context.Families.Add(family);
        await _context.SaveChangesAsync();

        //TODO: Implement Mapster!
        return new GetFamilyDTO
        {
            Id = family.Id, Name = family.Name
        };
    }
    public async Task<GetFamilyDTO> Get(Guid id)
    {
        var family = await _context.Families.Include(x => x.FamilyMembers)
            .SingleOrDefaultAsync(x => x.Id == id);

        if (family == default)
        {
            throw new NotFoundException(typeof(Family), id.ToString());
        }

        //TODO: Implement Mapster!
        var members =
            family.FamilyMembers.Select(member => new GetFamilyMemberDTO
            {
                MemberId = member.MemberId,
                Name = member.Name
            }).ToList();

        //TODO: Implement Mapster!
        var dto = new GetFamilyDTO
        {
            Id = id,
            Name = family.Name,
            FamilyMembers = members
        };

        return dto;
    }
    public async Task<GetFamilyDTO> Update(PostFamilyDTO dto, Guid id)
    {
        var family = await _context.Families
            .Include(x => x.FamilyMembers)
            .SingleOrDefaultAsync(x => x.Id == id);

        if (family == default)
        {
            throw new NotFoundException(typeof(Family), id.ToString());
        }

        // Update the properties of the existing tracked family
        family.Name = dto.Name;

        _context.Families.Update(family);
        await _context.SaveChangesAsync();
        
        //TODO: Implement Mapster!
        var members =
            family.FamilyMembers.Select(member => new GetFamilyMemberDTO
            {
                MemberId = member.MemberId,
                Name = member.Name
            }).ToList();

        // Return the updated family using the same instance
        return new GetFamilyDTO
        {
            Id = family.Id,
            Name = family.Name,
            FamilyMembers = members
        };
    }
    public async Task Delete(Guid id)
    {
        var deleted = await _context.Families
            .Where(x => x.Id == id)
            .ExecuteDeleteAsync();

        if (deleted == 0)
        {
            throw new NotFoundException(typeof(Family), id.ToString());
        }
    }
}