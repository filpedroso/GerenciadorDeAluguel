using GerenciadorDeAluguel.Domain.Entities;
using GerenciadorDeAluguel.Domain.ValueObjects;

namespace GerenciadorDeAluguel.Application.Ports;

public interface IClientRepository
{
    Task<Client?> GetByIdAsync(Guid clientId, CancellationToken ct = default);
    Task<Client?> GetByDocumentAsync(Document document, CancellationToken ct = default);  // Changed
    Task SaveAsync(Client client, CancellationToken ct = default);
}
