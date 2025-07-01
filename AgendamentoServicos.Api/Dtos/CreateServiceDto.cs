namespace AgendamentoServicos.Api.Dtos;

public record CreateServiceDto(
    string Name,
    string? Description,
    int DurationInMinutes,
    int Value
);