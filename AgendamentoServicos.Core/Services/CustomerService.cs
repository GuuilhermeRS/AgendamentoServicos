using AgendamentoServicos.Core.Dtos;
using AgendamentoServicos.Core.Interfaces.Services;
using AgendamentoServicos.Core.Model;
using Microsoft.EntityFrameworkCore;

namespace AgendamentoServicos.Core.Services;

public class CustomerService(Context context) : ICustomerService
{
    public async Task<IEnumerable<Customer>> Get()
    {
        return await context.Customers.ToListAsync();
    }
    
    public async Task<Customer?> Get(int id)
    {
        return await context.Customers.FindAsync(id);
    }

    public async Task<Customer> Create(string name, string cellphone, string email)
    {
        var customer = new Customer(name, cellphone, email);
        context.Customers.Add(customer);
        await context.SaveChangesAsync();
        
        return customer;
    }

    public async Task<Customer> Update(int id, string name, string cellphone, string email)
    {
        var customer = await context.Customers.FindAsync(id);
        if (customer is null)
            throw new KeyNotFoundException();
        
        customer.Update(name, cellphone, email);
        context.Customers.Update(customer);
        await context.SaveChangesAsync();
        
        return customer;
    }

    public async Task<bool> Delete(int id)
    {
        var customer = await context.Customers.FindAsync(id);
        if (customer is null)
            throw new KeyNotFoundException();
        
        context.Customers.Remove(customer);
        return await context.SaveChangesAsync() > 0;
    }
}