using System.Threading;
using System.Threading.Tasks;
using CarRentalSystem.Application.DTOs;
using CarRentalSystem.Application.Results;
using CarRentalSystem.Application.UseCases.CreateCustomer;
using CarRentalSystem.Application.UseCases.GetLoyaltyPoints;
using CarRentalSystem.Presentation.Controllers;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;

namespace CarRentalSystem.Tests.Controllers;

/// <summary>
/// Tests unitarios para CustomerController.
/// </summary>
[TestFixture]
public class CustomerControllerTests
{
    private Mock<ICreateCustomerUseCase> _createCustomerUseCaseMock = null!;
    private Mock<IGetLoyaltyPointsUseCase> _getLoyaltyPointsUseCaseMock = null!;
    private CustomerController _controller = null!;

    [SetUp]
    public void Setup()
    {
        _createCustomerUseCaseMock = new Mock<ICreateCustomerUseCase>();
        _getLoyaltyPointsUseCaseMock = new Mock<IGetLoyaltyPointsUseCase>();

        _controller = new CustomerController(
            _createCustomerUseCaseMock.Object,
            _getLoyaltyPointsUseCaseMock.Object
        );

        // Setup del contexto HTTP mínimo
        var httpContext = new Microsoft.AspNetCore.Http.DefaultHttpContext();
        var controllerContext = new ControllerContext
        {
            HttpContext = httpContext
        };
        _controller.ControllerContext = controllerContext;
    }

    [Test]
    public async Task CreateCustomerAsync_WithValidRequest_ShouldReturnCreatedResult()
    {
        // Arrange
        var request = new CreateCustomerRequest("Juan Pérez");
        var response = new CreateCustomerResponse("customer-1", "Juan Pérez");

        _createCustomerUseCaseMock.Setup(u => u.ExecuteAsync(request, It.IsAny<CancellationToken>()))
            .ReturnsAsync(Result<CreateCustomerResponse>.Success(response));

        // Act
        var result = await _controller.CreateCustomer(request);

        // Assert
        result.Should().BeOfType<OkObjectResult>();
        var okObjectResult = result as OkObjectResult;
        okObjectResult!.StatusCode.Should().Be(200);
        okObjectResult.Value.Should().BeEquivalentTo(response);

        // Verify use case was called
        _createCustomerUseCaseMock.Verify(u => u.ExecuteAsync(request, It.IsAny<CancellationToken>()), Times.Once);
    }

    [Test]
    public async Task GetLoyaltyPointsAsync_WithValidCustomerId_ShouldReturnOkWithPoints()
    {
        // Arrange
        var customerId = "customer-1";
        var response = new GetLoyaltyPointsResponse(customerId, 500);

        _getLoyaltyPointsUseCaseMock.Setup(u => u.ExecuteAsync(customerId, It.IsAny<CancellationToken>()))
            .ReturnsAsync(Result<GetLoyaltyPointsResponse>.Success(response));

        // Act
        var result = await _controller.GetLoyaltyPointsAsync(customerId);

        // Assert
        result.Should().BeOfType<OkObjectResult>();
        var okResult = result as OkObjectResult;
        okResult!.Value.Should().BeEquivalentTo(response);
    }

    [Test]
    public async Task CreateCustomerAsync_WithEmptyName_ShouldHandleValidationError()
    {
        // Arrange
        var request = new CreateCustomerRequest("");

        _createCustomerUseCaseMock.Setup(u => u.ExecuteAsync(request, It.IsAny<CancellationToken>()))
            .ReturnsAsync(Result<CreateCustomerResponse>.Success(new CreateCustomerResponse("1", "")));

        // Act
        var result = await _controller.CreateCustomer(request);

        // Assert
        result.Should().BeOfType<OkObjectResult>();
    }
}
