namespace CarRentalSystem.Infrastructure.Repositories;

using Microsoft.EntityFrameworkCore;
using CarRentalSystem.Domain.Entities;
using CarRentalSystem.Domain.Interfaces;
using CarRentalSystem.Infrastructure.Persistence;

/// <summary>
/// Implementación de IRentalRepository usando Entity Framework Core.
/// </summary>
public class EfRentalRepository : IRentalRepository
{
    private readonly CarRentalDbContext _context;

    public EfRentalRepository(CarRentalDbContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public async Task<Rental?> GetByIdAsync(string id, CancellationToken cancellationToken = default)
    {
        return await _context.Rentals.FirstOrDefaultAsync(r => r.Id == id, cancellationToken);
    }

    public async Task<IEnumerable<Rental>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await _context.Rentals.ToListAsync(cancellationToken);
    }

    public async Task<IEnumerable<Rental>> GetByCustomerIdAsync(string customerId, CancellationToken cancellationToken = default)
    {
        return await _context.Rentals
            .Where(r => r.CustomerId == customerId)
            .ToListAsync(cancellationToken);
    }

    public async Task AddAsync(Rental rental, CancellationToken cancellationToken = default)
    {
        await _context.Rentals.AddAsync(rental, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(Rental rental, CancellationToken cancellationToken = default)
    {
        _context.Rentals.Update(rental);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task<bool> ExistsAsync(string id, CancellationToken cancellationToken = default)
    {
        return await _context.Rentals.AnyAsync(r => r.Id == id, cancellationToken);
    }
}
