namespace CarRentalSystem.Infrastructure.Repositories;

using Microsoft.EntityFrameworkCore;
using CarRentalSystem.Core.Entities;
using CarRentalSystem.Core.Interfaces;
using CarRentalSystem.Infrastructure.Persistence;

/// <summary>
/// Implementación de ICarRepository usando Entity Framework Core.
/// </summary>
public class EfCarRepository : ICarRepository
{
    private readonly CarRentalDbContext _context;

    public EfCarRepository(CarRentalDbContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public async Task<Car?> GetByIdAsync(string id, CancellationToken cancellationToken = default)
    {
        return await _context.Cars.FirstOrDefaultAsync(c => c.Id == id, cancellationToken);
    }

    public async Task<IEnumerable<Car>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await _context.Cars.ToListAsync(cancellationToken);
    }

    public async Task<IEnumerable<Car>> GetAvailableAsync(CancellationToken cancellationToken = default)
    {
        return await _context.Cars
            .Where(c => c.IsAvailable)
            .ToListAsync(cancellationToken);
    }

    public async Task AddAsync(Car car, CancellationToken cancellationToken = default)
    {
        await _context.Cars.AddAsync(car, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(Car car, CancellationToken cancellationToken = default)
    {
        _context.Cars.Update(car);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task<bool> ExistsAsync(string id, CancellationToken cancellationToken = default)
    {
        return await _context.Cars.AnyAsync(c => c.Id == id, cancellationToken);
    }
}
