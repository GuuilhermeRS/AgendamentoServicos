namespace AgendamentoServicos.Core.Dtos;

public record CreateSlotDto(
    int CustomerId,
    int ProfessionalId,
    int ServiceId,
    DateTime Date,
    string? Description
);