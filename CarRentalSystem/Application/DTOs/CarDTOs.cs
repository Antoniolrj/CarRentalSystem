namespace CarRentalSystem.Application.DTOs;

using CarRentalSystem.Core.Entities;
using System.ComponentModel.DataAnnotations;


/// <summary>
/// DTO para solicitar la creación de un nuevo auto.
/// </summary>
public sealed record CreateCarRequest(
    [Required(ErrorMessage = "El modelo es requerido")]
    [StringLength(100, MinimumLength = 1, ErrorMessage = "El modelo debe tener entre 1 y 100 caracteres")]
    string Model,

    [Required(ErrorMessage = "El tipo de auto es requerido")]
    [EnumDataType(typeof(CarType), ErrorMessage = "El tipo de auto no es válido")]
    string Type,

    [Range(0.01, 9999999.99, ErrorMessage = "El precio diario debe ser mayor a 0")]
    decimal DailyPrice)
{
    public CarType GetCarType() => Enum.Parse<CarType>(Type);
}

/// <summary>
/// DTO para la respuesta al crear un nuevo auto.
/// </summary>
public sealed record CreateCarResponse(
    string Id,
    string Model,
    string Type,
    decimal DailyPrice,
    bool IsAvailable);

/// <summary>
/// Extension para convertir Car a CarDto.
/// Principio: Mantener conversiones centralizadas.
/// </summary>
public static class CarDtoExtensions
{
    public static CreateCarResponse ToCreateResponse(this Car car)
    {
        return new CreateCarResponse(
            car.Id,
            car.Model,
            car.Type.ToString(),
            car.DailyPrice,
            car.IsAvailable);
    }
}
