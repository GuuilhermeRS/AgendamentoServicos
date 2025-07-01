namespace AgendamentoServicos.Api.Dtos;

public record CreateCustomerDto(
    string Name,
    string Cellphone,
    string Email);