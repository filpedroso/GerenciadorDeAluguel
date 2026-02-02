using GerenciadorDeAluguel.Application.Ports;
using GerenciadorDeAluguel.Domain.Entities;

namespace GerenciadorDeAluguel.Tests.InMemoryFakes;

public class InMemoryClientRepository : IClientRepository
{
    private readonly Dictionary<Guid, Client> _store = new();

    public Task<Client?> GetByIdAsync(Guid clientId, CancellationToken ct = default)
    {
        var client = _store.TryGetValue(clientId, out var c) ? c : null;
        return Task.FromResult(client);
    }

    public void Add(Client client)
    {
        _store[client.Id] = client;
    }

    public int Count => _store.Count;

    public void Clear() => _store.Clear();
}
