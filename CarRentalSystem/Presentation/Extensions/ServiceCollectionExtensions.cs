namespace CarRentalSystem.Presentation.Extensions;

using CarRentalSystem.Application.Services;
using CarRentalSystem.Application.UseCases.RentCar;
using CarRentalSystem.Application.UseCases.ReturnCar;
using CarRentalSystem.Application.UseCases.GetCars;
using CarRentalSystem.Application.UseCases.GetCarById;
using CarRentalSystem.Application.UseCases.GetAvailableCars;
using CarRentalSystem.Application.UseCases.GetLoyaltyPoints;
using CarRentalSystem.Application.UseCases.GetRental;
using CarRentalSystem.Application.UseCases.GetCustomerRentals;
using CarRentalSystem.Application.UseCases.CreateCar;
using CarRentalSystem.Application.UseCases.CreateCustomer;
using CarRentalSystem.Domain.Interfaces;
using CarRentalSystem.Infrastructure.Repositories;
using CarRentalSystem.Infrastructure.Persistence;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;

/// <summary>
/// Extensiones para registrar servicios y configurar endpoints.
/// </summary>
public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Registra todos los servicios necesarios para la aplicación.
    /// </summary>
    public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
    {
        // Obtener connectionString desde appsettings.json (requerido)
        var connectionString = configuration.GetConnectionString("CarRentalDb")
            ?? throw new InvalidOperationException("Connection string 'CarRentalDb' no encontrada en las settings.");

        // Configurar DbContext con InMemory database
        services.AddDbContext<CarRentalDbContext>(options =>
            options.UseInMemoryDatabase(connectionString));

        // Registrar domain services (Singleton)
        services.AddSingleton<IPricingDomainService, PricingDomainService>();

        // Registrar repositories (Scoped) - usando EF Core
        services.AddScoped<ICarRepository, EfCarRepository>();
        services.AddScoped<ICustomerRepository, EfCustomerRepository>();
        services.AddScoped<IRentalRepository, EfRentalRepository>();

        // Registrar application usecases (Scoped)
        services.AddScoped<IRentCarUseCase, RentCarUseCase>();
        services.AddScoped<IReturnCarUseCase, ReturnCarUseCase>();
        services.AddScoped<IGetCarsUseCase, GetCarsUseCase>();
        services.AddScoped<IGetCarByIdUseCase, GetCarByIdUseCase>();
        services.AddScoped<IGetAvailableCarsUseCase, GetAvailableCarsUseCase>();
        services.AddScoped<IGetLoyaltyPointsUseCase, GetLoyaltyPointsUseCase>();
        services.AddScoped<IGetRentalUseCase, GetRentalUseCase>();
        services.AddScoped<IGetCustomerRentalsUseCase, GetCustomerRentalsUseCase>();
        services.AddScoped<ICreateCarUseCase, CreateCarUseCase>();
        services.AddScoped<ICreateCustomerUseCase, CreateCustomerUseCase>();

        return services;
    }
}
