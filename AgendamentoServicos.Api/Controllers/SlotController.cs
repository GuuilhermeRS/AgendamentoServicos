using AgendamentoServicos.Core.Dtos;
using AgendamentoServicos.Core.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace AgendamentoServicos.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class SlotController(ISlotService service) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var slots = await service.GetAll();
        return Ok(slots);
    }
    
    [HttpGet, Route("Available")]
    public async Task<IActionResult> GetAllAvailable([FromQuery] int professionalId, [FromQuery] int serviceId)
    {
        var slots = await service.GetAllAvailableSlots(professionalId, serviceId);
        return Ok(slots);
    }
    
    [HttpPost, Route("Schedule")]
    public async Task<IActionResult> Schedule([FromBody] ScheduleSlotDto dto)
    {
        try
        {
            var slot = await service.Schedule(dto);
            return Ok(slot);
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(ex.Message);
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id)
    {
        var slot = await service.GetById(id);
        if (slot is null)
            return NotFound();

        return Ok(slot);
    }
    
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateSlotDto dto)
    {
        try
        {
            var slot = await service.Create(dto);
            var resultDto = await service.GetById(slot.Id);
            return CreatedAtAction(nameof(GetById), new { id = slot.Id }, resultDto);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (Exception ex)
        {
            return StatusCode(500, "Ocorreu um erro interno.");
        }
    }

    [HttpPut("{id:int}/cancel")]
    public async Task<IActionResult> Cancel(int id)
    {
        try
        {
            var slot = await service.Cancel(id);
            return Ok(slot);
        }
        catch (KeyNotFoundException)
        {
            return NotFound();
        }
        catch(InvalidOperationException ex)
        {
            return BadRequest(ex.Message);
        }
    }
    
    [HttpPut("{id:int}/complete")]
    public async Task<IActionResult> Complete(int id)
    {
        try
        {
            var slot = await service.Complete(id);
            return Ok(slot);
        }
        catch (KeyNotFoundException)
        {
            return NotFound();
        }
    }
}