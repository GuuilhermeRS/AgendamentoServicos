using AgendamentoServicos.Core.Interfaces.Services;
using AgendamentoServicos.Core.Model;
using Microsoft.EntityFrameworkCore;

namespace AgendamentoServicos.Core.Services;

public class ServiceService(Context context) : IServiceService
{
    public async Task<IEnumerable<Service>> GetAll()
    {
        return await context.Services.AsNoTracking().ToListAsync();
    }

    public async Task<Service?> GetById(int id)
    {
        return await context.Services.FindAsync(id);
    }

    public async Task<Service> Create(string name, string? description, int durationInMinutes, int value)
    {
        // Usando o construtor que definimos anteriormente
        var service = new Service(name, description, durationInMinutes, value);

        context.Services.Add(service);
        await context.SaveChangesAsync();
        return service;
    }

    public async Task<Service> Update(int id, string name, string? description, int durationInMinutes, int value)
    {
        var service = await context.Services.FindAsync(id);
        if (service is null)
            throw new KeyNotFoundException("Serviço não encontrado.");

        // Usando o método de update encapsulado no próprio objeto
        service.Update(name, description, durationInMinutes, value);

        context.Services.Update(service);
        await context.SaveChangesAsync();
        return service;
    }

    public async Task<bool> Delete(int id)
    {
        var service = await context.Services.FindAsync(id);
        if (service is null)
            throw new KeyNotFoundException("Serviço não encontrado.");

        context.Services.Remove(service);
        return await context.SaveChangesAsync() > 0;
    }
}