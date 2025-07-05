using AgendamentoServicos.Core.Model;

namespace AgendamentoServicos.Core.Dtos;

public record SlotDto (
    int Id,
    DateTime Date,
    SlotStatus Status,
    string? Description,
    CustomerInfo Customer,
    ProfessionalInfo Professional,
    ServiceInfo Service);