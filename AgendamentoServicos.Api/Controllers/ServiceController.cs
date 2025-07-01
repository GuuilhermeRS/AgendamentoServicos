using AgendamentoServicos.Api.Dtos;
using AgendamentoServicos.Core.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace AgendamentoServicos.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class ServiceController(IServiceService service) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var services = await service.GetAll();
        return Ok(services);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id)
    {
        var result = await service.GetById(id);
        if (result is null)
            return NotFound();

        return Ok(result);
    }
    
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateServiceDto dto)
    {
        try
        {
            var result = await service.Create(dto.Name, dto.Description, dto.DurationInMinutes, dto.Value);
            return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    /// <summary>
    /// Atualiza os dados de um serviço existente.
    /// </summary>
    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, [FromBody] CreateServiceDto dto)
    {
        try
        {
            var result = await service.Update(id, dto.Name, dto.Description, dto.DurationInMinutes, dto.Value);
            return Ok(result);
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

    /// <summary>
    /// Exclui um serviço.
    /// </summary>
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