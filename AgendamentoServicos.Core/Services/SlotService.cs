using AgendamentoServicos.Core.Dtos;
using AgendamentoServicos.Core.Interfaces.Services;
using AgendamentoServicos.Core.Model;
using Microsoft.EntityFrameworkCore;

namespace AgendamentoServicos.Core.Services;

public class SlotService(Context context) : ISlotService
{
    public async Task<IEnumerable<SlotDto>> GetAll()
    {
        var ret = await context.Slots
            .Include(slot => slot.Customer)
            .Include(slot => slot.Professional)
            .Include(slot => slot.Service)
            .ToListAsync();

        return ret.Select(SlotDto.Create);
    }

    public async Task<IEnumerable<SlotDto>> GetAllAvailableSlots(int professionalId, int serviceId)
    {
        var ret = await context.Slots
            .Include(s => s.Professional)
            .Include(s => s.Service)
            .Where(s => s.Status == SlotStatus.Available && !s.CustomerId.HasValue)
            .ToListAsync();

        return ret.Select(SlotDto.Create);
    }

    public async Task<SlotDto> Schedule(ScheduleSlotDto dto)
    {
        var slot = await context.Slots
            .Include(s => s.Professional)
            .Include(s => s.Service)
            .FirstOrDefaultAsync(s => s.Id == dto.SlotId);
        
        if (slot is null)
            throw new KeyNotFoundException("Agendamento não encontrado.");
        
        if (slot.Status != SlotStatus.Available)
            throw new InvalidOperationException("Agendamento não disponível para agendamento.");
        
        var customer = await context.Customers.FindAsync(dto.CustomerId);
        if (customer is null)
            throw new ArgumentException("Cliente inválido.");

        slot.Schedule(customer);
        await context.SaveChangesAsync();

        return SlotDto.Create(slot);
    }

    public async Task<SlotDto?> GetById(int id)
    {
        var ret = await context.Slots
            .Include(slot => slot.Customer)
            .Include(slot => slot.Professional)
            .Include(slot => slot.Service)
            .FirstOrDefaultAsync(s => s.Id == id);

        if (ret is null)
            return null;

        return new SlotDto(
            ret.Id,
            ret.Date,
            ret.Status,
            ret.Description,
            ret.Customer is not null ? new CustomerInfo(ret.Customer.Id, ret.Customer.Name) : null,
            new ProfessionalInfo(ret.Professional.Id, ret.Professional.Name, ret.Professional.Specialty),
            new ServiceInfo(ret.Service.Id, ret.Service.Name, ret.Service.Duration, ret.Service.Value)
        );
    }

    public async Task<Slot> Create(CreateSlotDto dto)
    {
        var customer = await context.Customers.FindAsync(dto.CustomerId);
        if (customer is null)
            throw new ArgumentException("Cliente inválido.");
        
        var professional = await context.Professionals.FindAsync(dto.ProfessionalId);
        if (professional is null)
            throw new ArgumentException("Profissional inválido.");
        
        var service = await context.Services.FindAsync(dto.ServiceId);
        if (service is null)
            throw new ArgumentException("Serviço inválido.");

        var slot = new Slot(dto.Date, customer, professional, service, dto.Description);

        context.Slots.Add(slot);
        await context.SaveChangesAsync();
        return slot;
    }

    public async Task<Slot> Cancel(int id)
    {
        var slot = await context.Slots.FindAsync(id);
        if (slot is null)
            throw new KeyNotFoundException("Agendamento não encontrado.");
        
        slot.Cancel();
        
        context.Slots.Update(slot);
        await context.SaveChangesAsync();
        return slot;
    }

    public async Task<Slot> Complete(int id)
    {
        var slot = await context.Slots.FindAsync(id);
        if (slot is null)
            throw new KeyNotFoundException("Agendamento não encontrado.");

        slot.Complete();

        context.Slots.Update(slot);
        await context.SaveChangesAsync();
        return slot;
    }
}