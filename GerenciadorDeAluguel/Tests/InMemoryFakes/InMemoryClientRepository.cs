using GerenciadorDeAluguel.Application.Ports;
using GerenciadorDeAluguel.Domain.Entities;
using GerenciadorDeAluguel.Domain.ValueObjects;

namespace GerenciadorDeAluguel.Tests.InMemoryFakes;

public class InMemoryClientRepository : IClientRepository
{
    private readonly Dictionary<Guid, Client> _clients = new();
    private readonly Dictionary<string, Client> _clientsByDocument = new();

    public Task<Client?> GetByIdAsync(Guid clientId, CancellationToken ct = default)
    {
        _clients.TryGetValue(clientId, out var client);
        return Task.FromResult(client);
    }

    public Task<Client?> GetByDocumentAsync(Document document, CancellationToken ct = default)
    {
        _clientsByDocument.TryGetValue(document.Value, out var client);
        return Task.FromResult(client);
    }

    public Task SaveAsync(Client client, CancellationToken ct = default)
    {
        _clients[client.Id] = client;
        _clientsByDocument[client.DocumentNumber.Value] = client;
        return Task.CompletedTask;
    }

    public void Add(Client client)
    {
        _clients[client.Id] = client;
    }

    public int Count => _clients.Count;

    public void Clear() => _clients.Clear();
}
