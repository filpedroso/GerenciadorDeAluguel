using GerenciadorDeAluguel.Domain.Entities;
using GerenciadorDeAluguel.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;

namespace GerenciadorDeAluguel.Infrastructure;

public class ApplicationDbContext : DbContext
{
    public DbSet<Property> Properties { get; set; } = null!;
    public DbSet<Client> Clients { get; set; } = null!;
    public DbSet<Reservation> Reservations { get; set; } = null!;

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder mb)
    {
        mb.Entity<Client>(e =>
        {
            e.HasKey(c => c.Id);
            e.OwnsOne(c => c.DocumentNumber, d =>
            {
                d.Property(x => x.Value).HasColumnName("Document");
            });
        });

        mb.Entity<Property>(e =>
        {
            e.HasKey(p => p.Id);
            e.OwnsOne(p => p.Address, a =>
            {
                a.Property(x => x.Street).HasColumnName("Street");
                a.Property(x => x.Number).HasColumnName("Number");
                a.Property(x => x.ZipCode).HasColumnName("ZipCode");
                a.Property(x => x.City).HasColumnName("City");
                a.Property(x => x.State).HasColumnName("State");
                a.Property(x => x.Complement).HasColumnName("Complement");
            });

            e.OwnsOne(p => p.MonthlyRent, m =>
            {
                m.Property(x => x.Value).HasColumnName("MonthlyRent");
            });

            e.Property(p => p.Type);
            e.Property(p => p.Status);
        });

        mb.Entity<Reservation>(e =>
        {
            e.HasKey(r => r.Id);

            e.OwnsOne(r => r.Period, p =>
            {
                p.Property(x => x.Start).HasColumnName("PeriodStart");
                p.Property(x => x.End).HasColumnName("PeriodEnd");
            });

            // relations
            e.HasOne(r => r.Tenant).WithMany();
            e.HasOne(r => r.Property).WithMany();
        });
    }
}