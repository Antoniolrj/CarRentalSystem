namespace CarRentalSystem.Infrastructure.Persistence;

using Microsoft.EntityFrameworkCore;
using CarRentalSystem.Core.Entities;

/// <summary>
/// DbContext para Car Rental System. Aquí configuramos las entidades y sus relaciones con la base de datos.
/// </summary>
public class CarRentalDbContext : DbContext
{
    public DbSet<Car> Cars { get; set; }
    public DbSet<Customer> Customers { get; set; }
    public DbSet<Rental> Rentals { get; set; }

    public CarRentalDbContext(DbContextOptions<CarRentalDbContext> options)
        : base(options)
    {
    }

    /// <summary>
    /// Configura el modelo y las convenciones de EF Core.
    /// </summary>
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Configurar tabla Cars
        modelBuilder.Entity<Car>()
            .HasKey(c => c.Id);

        modelBuilder.Entity<Car>()
            .Property(c => c.Id)
            .HasMaxLength(50);

        modelBuilder.Entity<Car>()
            .Property(c => c.Model)
            .HasMaxLength(100)
            .IsRequired();

        modelBuilder.Entity<Car>()
            .Property(c => c.Type)
            .HasConversion<int>();

        modelBuilder.Entity<Car>()
            .Property(c => c.DailyPrice)
            .HasPrecision(10, 2);

        // Configurar tabla Customers
        modelBuilder.Entity<Customer>()
            .HasKey(c => c.Id);

        modelBuilder.Entity<Customer>()
            .Property(c => c.Id)
            .HasMaxLength(50);

        modelBuilder.Entity<Customer>()
            .Property(c => c.Name)
            .HasMaxLength(100)
            .IsRequired();

        // Configurar tabla Rentals
        modelBuilder.Entity<Rental>()
            .HasKey(r => r.Id);

        modelBuilder.Entity<Rental>()
            .Property(r => r.Id)
            .HasMaxLength(50);

        modelBuilder.Entity<Rental>()
            .Property(r => r.CustomerId)
            .HasMaxLength(50)
            .IsRequired();

        modelBuilder.Entity<Rental>()
            .Property(r => r.CarId)
            .HasMaxLength(50)
            .IsRequired();

        modelBuilder.Entity<Rental>()
            .Property(r => r.RentalPrice)
            .HasPrecision(10, 2);

        modelBuilder.Entity<Rental>()
            .Property(r => r.SurchargePrice)
            .HasPrecision(10, 2);
    }
}
