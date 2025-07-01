using AgendamentoServicos.Core.Model;

namespace AgendamentoServicos.Core.Interfaces.Services;

public interface ICustomerService
{
    public Task<Customer?> Get(int id);
    public Task<IEnumerable<Customer>> Get();
    public Task<Customer> Create(string name, string cellphone, string email);
    public Task<Customer> Update(int id, string name, string cellphone, string email);
    public Task<bool> Delete(int id);
}