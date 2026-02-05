using GerenciadorDeAluguel.Application.Ports;
using GerenciadorDeAluguel.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace GerenciadorDeAluguel.Infrastructure.EfRepositories;

public class EfClientRepository : IClientRepository
{
    private readonly ApplicationDbContext _db;

    public EfClientRepository(ApplicationDbContext db)
    {
        _db = db ?? throw new ArgumentNullException(nameof(db));
    }

    public async Task<Client?> GetByIdAsync(Guid clientId, CancellationToken ct = default)
    {
        return await _db.Clients.FindAsync(new object[] { clientId }, ct);
    }

    public async Task<Client?> GetByDocumentAsync(Domain.ValueObjects.Document document, CancellationToken ct = default)
    {
        return await _db.Clients.FirstOrDefaultAsync(c => c.DocumentNumber.Value == document.Value, ct);
    }

    public async Task<IEnumerable<Client>> GetAllAsync(CancellationToken ct = default)
    {
        return await _db.Clients
        .ToListAsync(ct);
    }

    public async Task SaveAsync(Client client, CancellationToken ct = default)
    {
        var exists = await _db.Clients.FindAsync(new object[] { client.Id }, ct);
        if (exists is null)
            await _db.Clients.AddAsync(client, ct);
        else
            _db.Entry(exists).CurrentValues.SetValues(client);

        await _db.SaveChangesAsync(ct);
    }
}