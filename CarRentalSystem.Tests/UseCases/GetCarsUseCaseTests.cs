using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using CarRentalSystem.Application.DTOs;
using CarRentalSystem.Application.UseCases.GetCars;
using CarRentalSystem.Core.Entities;
using CarRentalSystem.Core.Interfaces;
using FluentAssertions;
using Moq;
using NUnit.Framework;

namespace CarRentalSystem.Tests.UseCases;

/// <summary>
/// Tests unitarios para GetCarsUseCase.
/// </summary>
[TestFixture]
public class GetCarsUseCaseTests
{
    private Mock<ICarRepository> _carRepositoryMock = null!;
    private GetCarsUseCase _useCase = null!;

    [SetUp]
    public void Setup()
    {
        _carRepositoryMock = new Mock<ICarRepository>();
        _useCase = new GetCarsUseCase(_carRepositoryMock.Object);
    }

    [Test]
    public async Task ExecuteAsync_WithAvailableCars_ShouldReturnAllCars()
    {
        var cars = new List<Car>
        {
            Car.Create("1", "Toyota Camry", CarType.Small, 50.00m),
            Car.Create("2", "Honda CR-V", CarType.SUV, 75.00m),
            Car.Create("3", "BMW 7", CarType.Premium, 150.00m)
        };

        _carRepositoryMock.Setup(r => r.GetAllAsync(It.IsAny<CancellationToken>()))
            .ReturnsAsync(cars);

        var result = await _useCase.ExecuteAsync();

        result.IsSuccess.Should().BeTrue();
        result.Data.Should().HaveCount(3);
        result.Data!.Should().AllSatisfy(car => car.Should().NotBeNull());
    }

    [Test]
    public async Task ExecuteAsync_WithNoCars_ShouldReturnEmptyList()
    {
        _carRepositoryMock.Setup(r => r.GetAllAsync(It.IsAny<CancellationToken>()))
            .ReturnsAsync(new List<Car>());

        var result = await _useCase.ExecuteAsync();

        result.IsSuccess.Should().BeTrue();
        result.Data.Should().BeEmpty();
    }

    [Test]
    public async Task ExecuteAsync_ShouldCallRepositoryOnce()
    {
        _carRepositoryMock.Setup(r => r.GetAllAsync(It.IsAny<CancellationToken>()))
            .ReturnsAsync(new List<Car>());

        await _useCase.ExecuteAsync();

        _carRepositoryMock.Verify(
            r => r.GetAllAsync(It.IsAny<CancellationToken>()),
            Times.Once);
    }
}
