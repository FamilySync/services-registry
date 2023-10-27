using FamilySync.Registry.Models.DTO;
using FamilySync.Registry.Services;
using Microsoft.AspNetCore.Mvc;

namespace FamilySync.Registry.Controllers;

[Route("api/v1/[controller]")]
[ApiController]
public class FamilyMemberController : Controller
{
    private readonly IFamilyMemberService _service;

    public FamilyMemberController(IFamilyMemberService service)
    {
        _service = service;
    }
    
    [HttpPost("{familyId}/members")]
    [ProducesResponseType(typeof(GetFamilyMemberDTO), StatusCodes.Status201Created)]
    public async Task<ActionResult<GetFamilyMemberDTO>> Post([FromRoute] Guid familyId, [FromBody] PostFamilyMemberDTO dto)
    {
        var result = await _service.Create(familyId, dto);

        return CreatedAtAction(nameof(Get), new { familyId, memberId = result.MemberId }, result);
    }

    [HttpGet("{familyId}")]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(List<GetFamilyMemberDTO>), StatusCodes.Status200OK)]
    public async Task<ActionResult<List<GetFamilyMemberDTO>>> GetAll([FromRoute] Guid familyId)
    {
        var result = await _service.GetAll(familyId);

        return Ok(result);
    }

    [HttpGet("{familyId}/{memberId}")]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(GetFamilyMemberDTO), StatusCodes.Status200OK)]
    public async Task<ActionResult<GetFamilyMemberDTO>> Get([FromRoute] Guid familyId, [FromRoute] Guid memberId)
    {
        var result = await _service.Get(memberId, familyId);

        return Ok(result);
    }
    
    [HttpDelete("{familyId}/{memberId}")]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> DeleteMember ([FromRoute] Guid familyId, [FromRoute] Guid memberId)
    {
        await _service.Delete(familyId, memberId);

        return Ok();
    }
}