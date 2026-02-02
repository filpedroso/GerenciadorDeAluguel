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

    public Reservation? GetById(Guid id)
    {
        return _store.TryGetValue(id, out var reserv) ? reserv : null;
    }

    public int Count => _store.Count;

    public IReadOnlyList<Reservation> GetAll() => _store.Values.ToList();

    public void Clear() => _store.Clear();
}
