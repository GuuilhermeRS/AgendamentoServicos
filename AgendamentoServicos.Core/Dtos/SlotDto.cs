namespace AgendamentoServicos.Core.Dtos;

public record SlotDto (
    int Id,
    DateTime Date,
    string Status,
    string? Description,
    CustomerInfo Customer,
    ProfessionalInfo Professional,
    ServiceInfo Service);