using AgendamentoServicos.Core.Model;

namespace AgendamentoServicos.Core.Dtos;

public record SlotDto(
    int Id,
    DateTime Date,
    SlotStatus Status,
    string? Description,
    CustomerInfo? Customer,
    ProfessionalInfo Professional,
    ServiceInfo Service)
{
    public static SlotDto Create(Slot slot)
    {
        return new SlotDto(
            slot.Id,
            slot.Date,
            slot.Status,
            slot.Description,
            slot.Customer is not null ? new CustomerInfo(slot.Customer.Id, slot.Customer.Name) : null,
            new ProfessionalInfo(slot.Professional.Id, slot.Professional.Name, slot.Professional.Specialty),
            new ServiceInfo(slot.Service.Id, slot.Service.Name, slot.Service.Duration, slot.Service.Value)
        );
    }
}