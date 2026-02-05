using GerenciadorDeAluguel.Application.Ports;
using GerenciadorDeAluguel.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace GerenciadorDeAluguel.Infrastructure.EfRepositories;

public class EfPropertyRepository : IPropertyRepository
{
    private readonly ApplicationDbContext _db;

    public EfPropertyRepository(ApplicationDbContext db)
    {
        _db = db ?? throw new ArgumentNullException(nameof(db));
    }

    public async Task<Property?> GetByIdAsync(Guid propertyId, CancellationToken ct = default)
    {
        return await _db.Properties
            .Include(p => p.Owner)
            .FirstOrDefaultAsync(p => p.Id == propertyId, ct);
    }

    public async Task<IReadOnlyList<Property>> GetAllAsync(CancellationToken ct = default)
    {
        return await _db.Properties
            .AsNoTracking()
            .Include(p => p.Owner)
            .ToListAsync(ct);
    }

    public async Task SaveAsync(Property property, CancellationToken ct = default)
    {
        var exists = await _db.Properties.FindAsync(new object[] { property.Id }, ct);
        if (exists is null)
            await _db.Properties.AddAsync(property, ct);
        else
            _db.Entry(exists).CurrentValues.SetValues(property);

        await _db.SaveChangesAsync(ct);
    }
}