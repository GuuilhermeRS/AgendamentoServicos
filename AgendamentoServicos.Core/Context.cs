using System.Reflection.Metadata;
using AgendamentoServicos.Core.Model;
using Microsoft.EntityFrameworkCore;

namespace AgendamentoServicos.Core;

public class Context(DbContextOptions<Context> options) : DbContext(options)
{
    internal DbSet<Customer> Customers { get; set; }
    internal DbSet<Professional> Professionals { get; set; }
    internal DbSet<Service> Services { get; set; }
    internal DbSet<Slot> Slots { get; set; }
    
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