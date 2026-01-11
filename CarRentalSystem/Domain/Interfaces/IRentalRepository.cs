namespace CarRentalSystem.Domain.Interfaces;

using CarRentalSystem.Domain.Entities;

/// <summary>
/// Interfaz del repositorio para acceso a datos de alquileres.
/// </summary>
public interface IRentalRepository
{
    /// <summary>
    /// Obtiene un alquiler por ID.
    /// </summary>
    Task<Rental?> GetByIdAsync(string rentalId, CancellationToken cancellationToken = default);

    /// <summary>
    /// Obtiene todos los alquileres.
    /// </summary>
    Task<IEnumerable<Rental>> GetAllAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// Obtiene todos los alquileres de un cliente específico.
    /// </summary>
    Task<IEnumerable<Rental>> GetByCustomerIdAsync(string customerId, CancellationToken cancellationToken = default);

    /// <summary>
    /// Añade un nuevo alquiler.
    /// </summary>
    Task AddAsync(Rental rental, CancellationToken cancellationToken = default);

    /// <summary>
    /// Actualiza un alquiler existente.
    /// </summary>
    Task UpdateAsync(Rental rental, CancellationToken cancellationToken = default);

    /// <summary>
    /// Verifica si existe un alquiler con el ID especificado.
    /// </summary>
    Task<bool> ExistsAsync(string rentalId, CancellationToken cancellationToken = default);
}
