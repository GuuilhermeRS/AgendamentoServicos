using System.Reflection.Metadata;
using AgendamentoServicos.Core.Model;
using Microsoft.EntityFrameworkCore;

namespace AgendamentoServicos.Core;

public class Context : DbContext
{
    private static string ConnectionString => "server=localhost;database=agendamento_db;uid=root;pwd=teste123;";
    
    public DbSet<Customer> Customers { get; set; }
    public DbSet<Professional> Professionals { get; set; }
    public DbSet<Service> Services { get; set; }
    public DbSet<Slot> Slots { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) =>
        optionsBuilder.UseMySql(ConnectionString, new MySqlServerVersion(new Version(8, 0, 20)));
    
    protected override void OnModelCreating(ModelBuilder modelBuilder) {
        modelBuilder.Entity<Customer>(entity =>
        {
            entity.ToTable("customer");
            
            entity.Property(c => c.CreatedAt).HasColumnName("created_at")
                .HasColumnType("datetime")
                .HasDefaultValueSql("CURRENT_TIMESTAMP");
        });

        modelBuilder.Entity<Professional>(entity =>
        {
            entity.ToTable("professional");

            entity.Property(c => c.CreatedAt).HasColumnName("created_at")
                .HasColumnType("datetime")
                .HasDefaultValueSql("CURRENT_TIMESTAMP");
        });

        modelBuilder.Entity<Service>(entity => entity.ToTable("service"));
        modelBuilder.Entity<Slot>(entity =>
        {
            entity.ToTable("slot");
            
            entity.Property(s => s.CustomerId).HasColumnName("customer_id");
            entity.Property(s => s.ProfessionalId).HasColumnName("professional_id");
            entity.Property(s => s.ServiceId).HasColumnName("service_id");
        });
    }
}