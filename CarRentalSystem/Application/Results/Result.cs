using CarRentalSystem.Application.Errors;

namespace CarRentalSystem.Application.Results;

/// <summary>
/// Clase base para resultados que no devuelven valor.
/// Patrón Result para manejar errores esperados sin excepciones.
/// </summary>
public class Result
{
    public bool IsSuccess { get; protected set; }
    public BaseError? Error { get; protected set; }

    protected Result(bool isSuccess, BaseError? error = null)
    {
        IsSuccess = isSuccess;
        Error = error;
    }

    public static Result Success() => new(true);
    public static Result Failure(BaseError error) => new(false, error);
}

/// <summary>
/// Resultado genérico que contiene un valor o un error.
/// </summary>
public class Result<T> : Result
{
    public T? Data { get; private set; }

    private Result(bool isSuccess, T? data = default, BaseError? error = null)
        : base(isSuccess, error)
    {
        Data = data;
    }

    public static Result<T> Success(T data) => new(true, data);
    public static new Result<T> Failure(BaseError error) => new(false, default, error);

    /// <summary>
    /// Conversión implícita de T a Result<T>.Success(data)
    /// Permite: return data; en lugar de return Result<T>.Success(data);
    /// </summary>
    public static implicit operator Result<T>(T data) => Success(data);

    /// <summary>
    /// Conversión implícita de BaseError a Result<T>.Failure(error)
    /// Permite: return new NotFoundError("..."); en lugar de return Result<T>.Failure(...);
    /// </summary>
    public static implicit operator Result<T>(BaseError error) => Failure(error);
}
