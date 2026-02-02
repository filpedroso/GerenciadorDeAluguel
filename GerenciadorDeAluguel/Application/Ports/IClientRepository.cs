using GerenciadorDeAluguel.Domain.Entities;

namespace GerenciadorDeAluguel.Application.Ports;

public interface IClientRepository
{
    Task<Client?> GetByIdAsync(Guid clientId, CancellationToken ct = default);
}
