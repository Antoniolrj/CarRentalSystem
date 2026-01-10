namespace CarRentalSystem.Core.Interfaces;

using CarRentalSystem.Core.Entities;

/// <summary>
/// Interfaz del repositorio para acceso a datos de autos.
/// Principio SOLID - DIP: El dominio depende de abstracciones, no de implementaciones concretas.
/// </summary>
public interface ICarRepository
{
    /// <summary>
    /// Obtiene un auto por ID.
    /// </summary>
    Task<Car?> GetByIdAsync(string carId, CancellationToken cancellationToken = default);

    /// <summary>
    /// Obtiene todos los autos.
    /// </summary>
    Task<IEnumerable<Car>> GetAllAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// Obtiene solo los autos disponibles.
    /// Principio: Separación de consultas complejas en el repositorio.
    /// </summary>
    Task<IEnumerable<Car>> GetAvailableAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// Añade un nuevo auto.
    /// </summary>
    Task AddAsync(Car car, CancellationToken cancellationToken = default);

    /// <summary>
    /// Actualiza un auto existente.
    /// </summary>
    Task UpdateAsync(Car car, CancellationToken cancellationToken = default);

    /// <summary>
    /// Verifica si existe un auto con el ID especificado.
    /// </summary>
    Task<bool> ExistsAsync(string carId, CancellationToken cancellationToken = default);
}
