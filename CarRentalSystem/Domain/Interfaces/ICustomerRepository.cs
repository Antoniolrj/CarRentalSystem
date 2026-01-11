namespace CarRentalSystem.Domain.Interfaces;

using CarRentalSystem.Domain.Entities;

/// <summary>
/// Interfaz del repositorio para acceso a datos de clientes.
/// </summary>
public interface ICustomerRepository
{
    /// <summary>
    /// Obtiene un cliente por ID.
    /// </summary>
    Task<Customer?> GetByIdAsync(string customerId, CancellationToken cancellationToken = default);

    /// <summary>
    /// Obtiene todos los clientes.
    /// </summary>
    Task<IEnumerable<Customer>> GetAllAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// Añade un nuevo cliente.
    /// </summary>
    Task AddAsync(Customer customer, CancellationToken cancellationToken = default);

    /// <summary>
    /// Actualiza un cliente existente.
    /// </summary>
    Task UpdateAsync(Customer customer, CancellationToken cancellationToken = default);

    /// <summary>
    /// Verifica si existe un cliente con el ID especificado.
    /// </summary>
    Task<bool> ExistsAsync(string customerId, CancellationToken cancellationToken = default);
}
