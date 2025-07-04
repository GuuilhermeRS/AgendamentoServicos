using System.Text.Json;
using AgendamentoServicos.Core;
using AgendamentoServicos.Core.Interfaces.Services;
using AgendamentoServicos.Core.Services;
using Microsoft.EntityFrameworkCore;

namespace AgendamentoServicos.Api;

public abstract class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

        builder.Services.AddControllers()
            .AddJsonOptions(options => options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase);

        builder.Services.AddCors(options =>
        {
            options.AddPolicy("AllowAny",
                policy =>
                {
                    policy.AllowAnyOrigin()
                        .AllowAnyHeader()
                        .AllowAnyMethod();
                });
        });
        builder.Services.AddOpenApi();
        builder.Services.AddDbContext<Context>(options =>
        {
            options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
        });

        builder.Services.AddScoped<Context>();
        builder.Services.AddScoped<ICustomerService, CustomerService>();
        builder.Services.AddScoped<IProfessionalService, ProfessionalService>();
        builder.Services.AddScoped<IServiceService, ServiceService>();
        builder.Services.AddScoped<ISlotService, SlotService>();

        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        var app = builder.Build();
        app.UseCors("AllowAny");
        app.UseSwagger();
        app.UseSwaggerUI();
        if (app.Environment.IsDevelopment())
        {
            app.MapOpenApi();
        }

        app.UseHttpsRedirection();
        app.UseAuthorization();
        app.MapControllers();

        app.Run();
    }
}