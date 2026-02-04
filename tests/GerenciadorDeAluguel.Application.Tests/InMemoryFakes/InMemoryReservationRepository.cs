using GerenciadorDeAluguel.Application.Ports;
using GerenciadorDeAluguel.Domain.Entities;

namespace GerenciadorDeAluguel.Tests.InMemoryFakes;

public class InMemoryReservationRepository : IReservationRepository
{
    private readonly Dictionary<Guid, Reservation> _store = new();

    public Task SaveAsync(Reservation reservation, CancellationToken ct = default)
    {
        _store[reservation.Id] = reservation;
        return Task.CompletedTask;
    }

    public Task<Reservation?> GetByIdAsync(Guid reservationId, CancellationToken ct = default)
    {
        var reservation = _store.TryGetValue(reservationId, out var r) ? r : null;
        return Task.FromResult(reservation);
    }

    public int Count => _store.Count;

    public Task<IReadOnlyList<Reservation>> GetAllAsync(CancellationToken ct = default)
    {
        IReadOnlyList<Reservation> list = _store.Values.ToList();
        return Task.FromResult(list);
    }

    public void Clear() => _store.Clear();
}
