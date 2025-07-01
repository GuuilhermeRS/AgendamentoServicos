using AgendamentoServicos.Core.Model;

namespace AgendamentoServicos.Core.Interfaces.Services;

public interface IProfessionalService
{
    Task<IEnumerable<Professional>> GetAll();
    Task<Professional?> GetById(int id);
    Task<Professional> Create(string name, string specialty, string? cellphone, string? email);
    Task<Professional> Update(int id, string name, string specialty, string? cellphone, string? email);
    Task<bool> Delete(int id);
}