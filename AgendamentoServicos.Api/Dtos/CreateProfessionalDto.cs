namespace AgendamentoServicos.Api.Dtos;

public record CreateProfessionalDto(
    string Name,
    string Specialty,
    string? Cellphone,
    string? Email
);