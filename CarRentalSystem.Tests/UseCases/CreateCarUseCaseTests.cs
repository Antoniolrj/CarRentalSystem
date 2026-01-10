using System.Threading;
using System.Threading.Tasks;
using CarRentalSystem.Application.DTOs;
using CarRentalSystem.Application.UseCases.CreateCar;
using CarRentalSystem.Core.Entities;
using CarRentalSystem.Core.Interfaces;
using FluentAssertions;
using Moq;
using NUnit.Framework;

namespace CarRentalSystem.Tests.UseCases;

/// <summary>
/// Tests unitarios para CreateCarUseCase.
/// </summary>
[TestFixture]
public class CreateCarUseCaseTests
{
    private Mock<ICarRepository> _carRepositoryMock = null!;
    private CreateCarUseCase _useCase = null!;

    [SetUp]
    public void Setup()
    {
        _carRepositoryMock = new Mock<ICarRepository>();
        _useCase = new CreateCarUseCase(_carRepositoryMock.Object);
    }

    [Test]
    public async Task ExecuteAsync_WithValidRequest_ShouldCreateCarSuccessfully()
    {
        // Arrange
        var request = new CreateCarRequest(
            Model: "Toyota Camry",
            Type: "Small"
        );

        var result = await _useCase.ExecuteAsync(request);

        result.IsSuccess.Should().BeTrue();
        result.Data.Should().NotBeNull();
        result.Data!.Model.Should().Be("Toyota Camry");
        result.Data!.Type.Should().Be("Small");
        result.Data!.IsAvailable.Should().BeTrue();

        _carRepositoryMock.Verify(
            r => r.AddAsync(It.IsAny<Car>(), It.IsAny<CancellationToken>()),
            Times.Once);
    }

    [Test]
    public async Task ExecuteAsync_ShouldGenerateUniqueId()
    {
        var request1 = new CreateCarRequest(
            Model: "Honda CR-V",
            Type: "SUV"
        );

        var request2 = new CreateCarRequest(
            Model: "Honda CR-V",
            Type: "Premium"
        );

        var result1 = await _useCase.ExecuteAsync(request1);
        var result2 = await _useCase.ExecuteAsync(request2);

        result1.IsSuccess.Should().BeTrue();
        result2.IsSuccess.Should().BeTrue();
        result1.Data!.Id.Should().NotBe(result2.Data!.Id);
    }

    [Test]
    public async Task ExecuteAsync_WithPremiumCar_ShouldCreateSuccessfully()
    {
        var request = new CreateCarRequest(
            Model: "BMW 7 Series",
            Type: "Premium"
        );

        var result = await _useCase.ExecuteAsync(request);

        result.IsSuccess.Should().BeTrue();
        result.Data!.Type.Should().Be("Premium");
    }
}
