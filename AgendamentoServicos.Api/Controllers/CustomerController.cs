using AgendamentoServicos.Api.Dtos;
using AgendamentoServicos.Core.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace AgendamentoServicos.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class CustomerController(ICustomerService service) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var customers = await service.Get();
        return Ok(customers);
    }
    
    [HttpGet("{id:int}")]
    public async Task<IActionResult> Get(int id)
    {
        var customer = await service.Get(id);        
        if (customer is null)
            return NotFound();

        return Ok(customer);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateCustomerDto dto)
    {
        try
        {
            var customer = await service.Create(dto.Name, dto.Cellphone, dto.Email);
            return CreatedAtAction(nameof(Get), new { id = customer.Id }, customer);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, [FromBody] CreateCustomerDto dto)
    {
        try
        {
            var customer = await service.Update(id, dto.Name, dto.Cellphone, dto.Email);
            return Ok(customer);
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