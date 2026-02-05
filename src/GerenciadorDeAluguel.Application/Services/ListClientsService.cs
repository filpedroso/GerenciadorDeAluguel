using GerenciadorDeAluguel.Application.Ports;
using GerenciadorDeAluguel.Domain.Entities;

namespace GerenciadorDeAluguel.Application.Services;

public class ListClientsService
{
    private readonly IClientRepository _clientRepo;

    public ListClientsService(IClientRepository clientRepo)
    {
        _clientRepo = clientRepo ?? throw new ArgumentNullException(nameof(clientRepo));
    }

    public async Task<IEnumerable<Client>> GetAllAsync(CancellationToken ct = default)
    {
        return await _clientRepo.GetAllAsync(ct);
    }
}