using FamilySync.Registry.Models.DTO;
using FamilySync.Registry.Services;
using Microsoft.AspNetCore.Mvc;

namespace FamilySync.Registry.Controllers;

[Route("api/v1/[controller]")]
[ApiController]
public class FamilyController : Controller
{
    private readonly IFamilyService _service;

    public FamilyController(IFamilyService service)
    {
        _service = service;
    }
    
    [HttpPost]
    [ProducesResponseType(typeof(GetFamilyDTO), StatusCodes.Status201Created)]
    public async Task<ActionResult<GetFamilyDTO>> Post(PostFamilyDTO dto)
    {
        var result = await _service.Create(dto);

        return CreatedAtAction(nameof(Get), new { id = result.Id }, result);
    }

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(GetFamilyDTO), StatusCodes.Status200OK)]
    public async Task<ActionResult<GetFamilyDTO>> Get([FromRoute] Guid id)
    {
        var result = await _service.Get(id);

        return Ok(result);
    }
    
    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(GetFamilyDTO), StatusCodes.Status200OK)]
    public async Task<ActionResult<GetFamilyDTO>> Update(PostFamilyDTO dto, [FromRoute] Guid id)
    {
        var result = await _service.Update(dto, id);

        return Ok(result);
    }
    
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> Delete ([FromRoute] Guid id)
    {
        await _service.Delete(id);

        return Ok();
    }
}
