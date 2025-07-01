using System.Text.Json;
using AgendamentoServicos.Core;
using AgendamentoServicos.Core.Interfaces.Services;
using AgendamentoServicos.Core.Services;

namespace AgendamentoServicos.Api;

public abstract class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddControllers()
            .AddJsonOptions(options => options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase);
        builder.Services.AddOpenApi();

        builder.Services.AddScoped<Context>();
        builder.Services.AddScoped<ICustomerService, CustomerService>();
        builder.Services.AddScoped<IProfessionalService, ProfessionalService>();
        builder.Services.AddScoped<IServiceService, ServiceService>();
        builder.Services.AddScoped<ISlotService, SlotService>();

        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        var app = builder.Build();
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
            app.MapOpenApi();
        }

        app.UseHttpsRedirection();
        app.UseAuthorization();
        app.MapControllers();

        app.Run();
    }
}