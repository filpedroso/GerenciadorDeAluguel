using GerenciadorDeAluguel.Application.Ports;
using GerenciadorDeAluguel.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace GerenciadorDeAluguel.Infrastructure.EfRepositories;

public class EfReservationRepository : IReservationRepository
{
    private readonly ApplicationDbContext _db;

    public EfReservationRepository(ApplicationDbContext db)
    {
        _db = db ?? throw new ArgumentNullException(nameof(db));
    }

    public async Task<Reservation?> GetByIdAsync(Guid reservationId, CancellationToken ct = default)
    {
        return await _db.Reservations.FindAsync(new object[] { reservationId }, ct);
    }

    public async Task<IReadOnlyList<Reservation>> GetAllAsync(CancellationToken ct = default)
    {
        return await _db.Reservations.ToListAsync(ct);
    }

    public async Task SaveAsync(Reservation reservation, CancellationToken ct = default)
    {
        var exists = await _db.Reservations.FindAsync(new object[] { reservation.Id }, ct);
        if (exists is null)
            await _db.Reservations.AddAsync(reservation, ct);
        else
            _db.Entry(exists).CurrentValues.SetValues(reservation);

        await _db.SaveChangesAsync(ct);
    }
}