namespace CarRentalSystem.Tests.UseCases;

using System.Threading;
using System.Threading.Tasks;
using CarRentalSystem.Application.DTOs;
using CarRentalSystem.Application.Errors;
using CarRentalSystem.Application.UseCases.RentCar;
using CarRentalSystem.Domain.Entities;
using CarRentalSystem.Domain.Interfaces;
using CarRentalSystem.Domain.ValueObjects;
using FluentAssertions;
using Moq;
using NUnit.Framework;


/// <summary>
/// Tests unitarios para RentCarUseCase.
/// </summary>
[TestFixture]
public class RentCarUseCaseTests
{
    private Mock<ICarRepository> _carRepositoryMock = null!;
    private Mock<ICustomerRepository> _customerRepositoryMock = null!;
    private Mock<IRentalRepository> _rentalRepositoryMock = null!;
    private Mock<IPricingDomainService> _pricingServiceMock = null!;
    private RentCarUseCase _useCase = null!;

    [SetUp]
    public void Setup()
    {
        _carRepositoryMock = new Mock<ICarRepository>();
        _customerRepositoryMock = new Mock<ICustomerRepository>();
        _rentalRepositoryMock = new Mock<IRentalRepository>();
        _pricingServiceMock = new Mock<IPricingDomainService>();

        _useCase = new RentCarUseCase(
            _carRepositoryMock.Object,
            _customerRepositoryMock.Object,
            _rentalRepositoryMock.Object,
            _pricingServiceMock.Object
        );
    }

    [Test]
    public async Task ExecuteAsync_WithValidRequest_ShouldRentCarSuccessfully()
    {
        // Arrange
        var customerId = "customer-1";
        var carId = "car-1";
        var days = 5;

        var customer = Customer.Create(customerId, "Juan Pérez");
        var car = Car.Create(carId, "Toyota Camry", CarType.Small);

        _customerRepositoryMock.Setup(r => r.GetByIdAsync(customerId, It.IsAny<CancellationToken>()))
            .ReturnsAsync(customer);

        _carRepositoryMock.Setup(r => r.GetByIdAsync(carId, It.IsAny<CancellationToken>()))
            .ReturnsAsync(car);

        _pricingServiceMock.Setup(p => p.CalculateRentalPrice(car.Type, days))
            .Returns(new Money(250.00m));

        var request = new RentCarRequest(customerId, carId, days);

        // Act
        var result = await _useCase.ExecuteAsync(request);

        // Assert
        result.IsSuccess.Should().BeTrue();
        result.Data.Should().NotBeNull();
        result.Data!.RentalPrice.Should().Be(250.00m);

        _carRepositoryMock.Verify(
            r => r.GetByIdAsync(carId, It.IsAny<CancellationToken>()),
            Times.Once);

        _customerRepositoryMock.Verify(
            r => r.GetByIdAsync(customerId, It.IsAny<CancellationToken>()),
            Times.Once);
    }

    [Test]
    public async Task ExecuteAsync_WithNonExistentCar_ShouldReturnNotFoundError()
    {
        var customerId = "customer-1";
        var carId = "invalid-car";
        var days = 5;

        var customer = Customer.Create(customerId, "Juan Pérez");

        _customerRepositoryMock.Setup(r => r.GetByIdAsync(customerId, It.IsAny<CancellationToken>()))
            .ReturnsAsync(customer);

        _carRepositoryMock.Setup(r => r.GetByIdAsync(carId, It.IsAny<CancellationToken>()))
            .ReturnsAsync((Car?)null);

        var request = new RentCarRequest(customerId, carId, days);

        var result = await _useCase.ExecuteAsync(request);

        result.IsSuccess.Should().BeFalse();
        result.Error.Should().BeOfType<NotFoundError>();
    }

    [Test]
    public async Task ExecuteAsync_WithNonExistentCustomer_ShouldReturnNotFoundError()
    {
        var customerId = "invalid-customer";
        var carId = "car-1";
        var days = 5;

        _customerRepositoryMock.Setup(r => r.GetByIdAsync(customerId, It.IsAny<CancellationToken>()))
            .ReturnsAsync((Customer?)null);

        var request = new RentCarRequest(customerId, carId, days);

        var result = await _useCase.ExecuteAsync(request);

        result.IsSuccess.Should().BeFalse();
        result.Error.Should().BeOfType<NotFoundError>();
    }
}
