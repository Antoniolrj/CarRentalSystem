namespace CarRentalSystem.Application.UseCases.CreateCustomer;

using CarRentalSystem.Application.DTOs;
using CarRentalSystem.Application.Results;
using CarRentalSystem.Core.Entities;
using CarRentalSystem.Core.Interfaces;

/// <summary>
/// Implementación del caso de uso de crear un nuevo cliente.
/// </summary>
public class CreateCustomerUseCase : ICreateCustomerUseCase
{
    private readonly ICustomerRepository _customerRepository;

    public CreateCustomerUseCase(ICustomerRepository customerRepository)
    {
        _customerRepository = customerRepository;
    }

    /// <summary>
    /// Ejecuta el caso de uso de crear un nuevo cliente.
    /// </summary>
    public async Task<Result<CreateCustomerResponse>> ExecuteAsync(CreateCustomerRequest request, CancellationToken cancellationToken = default)
    {
        string customerId = Guid.NewGuid().ToString();

        var customer = Customer.Create(customerId, request.Name);

        await _customerRepository.AddAsync(customer, cancellationToken);

        return customer.ToCreateResponse();
    }
}
