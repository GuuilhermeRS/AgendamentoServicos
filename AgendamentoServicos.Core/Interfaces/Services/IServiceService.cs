using AgendamentoServicos.Core.Model;

namespace AgendamentoServicos.Core.Interfaces.Services;

public interface IServiceService
{
    Task<IEnumerable<Service>> GetAll();
    Task<Service?> GetById(int id);
    Task<Service> Create(string name, string? description, int durationInMinutes, int value);
    Task<Service> Update(int id, string name, string? description, int durationInMinutes, int value);
    Task<bool> Delete(int id);
}