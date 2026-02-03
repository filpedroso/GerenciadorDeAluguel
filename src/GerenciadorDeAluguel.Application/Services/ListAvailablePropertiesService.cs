using GerenciadorDeAluguel.Application.Ports;
using GerenciadorDeAluguel.Domain.Entities;
using GerenciadorDeAluguel.Domain.Enums;

namespace GerenciadorDeAluguel.Application.Services;

public class ListAvailablePropertiesService
{
    private readonly IPropertyRepository _propertyRepository;

    public ListAvailablePropertiesService(IPropertyRepository propertyRepository)
    {
        _propertyRepository = propertyRepository ?? throw new ArgumentNullException(nameof(propertyRepository));
    }

    public async Task<IReadOnlyList<Property>> ListAvailableAsync(CancellationToken ct = default)
    {
        var all = await _propertyRepository.GetAllAsync(ct);
        var available = all.Where(p => p.Status == PropertyStatus.Available).ToList();
        return available;
    }
}