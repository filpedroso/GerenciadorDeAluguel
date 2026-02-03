using GerenciadorDeAluguel.Application.Ports;
using GerenciadorDeAluguel.Domain.Entities;

namespace GerenciadorDeAluguel.Tests.InMemoryFakes;

public class InMemoryPropertyRepository : IPropertyRepository
{
    private readonly Dictionary<Guid, Property> _store = new();

    public Task<Property?> GetByIdAsync(Guid propertyId, CancellationToken ct = default)
    {
        var property = _store.TryGetValue(propertyId, out var p) ? p : null;
        return Task.FromResult(property);
    }

    public Task SaveAsync(Property property, CancellationToken ct = default)
    {
        _store[property.Id] = property;
        return Task.CompletedTask;
    }

    public void Add(Property property) => _store[property.Id] = property;

    public int Count => _store.Count;

    public void Clear() => _store.Clear();
}
