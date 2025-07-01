using AgendamentoServicos.Core.Interfaces.Services;
using AgendamentoServicos.Core.Model;
using Microsoft.EntityFrameworkCore;

namespace AgendamentoServicos.Core.Services;

public class ProfessionalService(Context context) : IProfessionalService
{
    public async Task<IEnumerable<Professional>> GetAll()
    {
        return await context.Professionals.AsNoTracking().ToListAsync();
    }

    public async Task<Professional?> GetById(int id)
    {
        return await context.Professionals.FindAsync(id);
    }

    public async Task<Professional> Create(string name, string specialty, string? cellphone, string? email)
    {
        var professional = new Professional(name, specialty, cellphone, email);

        context.Professionals.Add(professional);
        await context.SaveChangesAsync();
        return professional;
    }

    public async Task<Professional> Update(int id, string name, string specialty, string? cellphone, string? email)
    {
        var professional = await context.Professionals.FindAsync(id);
        if (professional is null)
            throw new KeyNotFoundException("Profissional não encontrado.");

        professional.Update(name, specialty, cellphone, email);

        context.Professionals.Update(professional);
        await context.SaveChangesAsync();
        return professional;
    }

    public async Task<bool> Delete(int id)
    {
        var professional = await context.Professionals.FindAsync(id);
        if (professional is null)
            throw new KeyNotFoundException("Profissional não encontrado.");

        context.Professionals.Remove(professional);
        return await context.SaveChangesAsync() > 0;
    }
}