using AgendamentoServicos.Core.Dtos;
using AgendamentoServicos.Core.Interfaces.Services;
using AgendamentoServicos.Core.Model;
using Microsoft.EntityFrameworkCore;

namespace AgendamentoServicos.Core.Services;

public class SlotService(Context context) : ISlotService
{
    // Método privado para reutilizar a lógica de projeção para DTO
    private IQueryable<SlotDto> GetSlotsAsDto()
    {
        return context.Slots
            .AsNoTracking()
            .Select(s => new SlotDto(
                s.Id,
                s.Date,
                s.Status.ToString(), // Converte o enum para string
                s.Description,
                new CustomerInfo(s.Customer.Id, s.Customer.Name),
                new ProfessionalInfo(s.Professional.Id, s.Professional.Name, s.Professional.Specialty),
                new ServiceInfo(s.Service.Id, s.Service.Name, s.Service.Duration, s.Service.Value)
            ));
    }

    public async Task<IEnumerable<SlotDto>> GetAll()
    {
        return await GetSlotsAsDto().ToListAsync();
    }

    public async Task<SlotDto?> GetById(int id)
    {
        return await GetSlotsAsDto().FirstOrDefaultAsync(s => s.Id == id);
    }

    public async Task<Slot> Create(CreateSlotDto dto)
    {
        // 1. Buscar as entidades relacionadas no banco de dados
        var customer = await context.Customers.FindAsync(dto.CustomerId);
        var professional = await context.Professionals.FindAsync(dto.ProfessionalId);
        var service = await context.Services.FindAsync(dto.ServiceId);

        // 2. Validar se todas as entidades foram encontradas
        if (customer is null || professional is null || service is null)
        {
            throw new ArgumentException("Cliente, Profissional ou Serviço inválido.");
        }

        // 3. Criar a entidade Slot usando o construtor, que garante a consistência
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