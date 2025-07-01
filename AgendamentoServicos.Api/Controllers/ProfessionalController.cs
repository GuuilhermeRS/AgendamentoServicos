using AgendamentoServicos.Api.Dtos;
using AgendamentoServicos.Core.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace AgendamentoServicos.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class ProfessionalController(IProfessionalService service) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var professionals = await service.GetAll();
        return Ok(professionals);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id)
    {
        var professional = await service.GetById(id);
        if (professional is null)
            return NotFound();

        return Ok(professional);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateProfessionalDto dto)
    {
        try
        {
            var professional = await service.Create(dto.Name, dto.Specialty, dto.Cellphone, dto.Email);
            return CreatedAtAction(nameof(GetById), new { id = professional.Id }, professional);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, [FromBody] CreateProfessionalDto dto)
    {
        try
        {
            var professional = await service.Update(id, dto.Name, dto.Specialty, dto.Cellphone, dto.Email);
            return Ok(professional);
        }
        catch (KeyNotFoundException)
        {
            return NotFound();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        try
        {
            await service.Delete(id);
            return NoContent();
        }
        catch (KeyNotFoundException)
        {
            return NotFound();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}