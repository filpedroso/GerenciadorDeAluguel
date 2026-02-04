using GerenciadorDeAluguel.Application.Ports;
using GerenciadorDeAluguel.Domain.Enums;
using GerenciadorDeAluguel.Domain.Entities;

namespace GerenciadorDeAluguel.Application.Services;

public class CheckPropertyAvailabilityService
{
    private readonly IPropertyRepository _propertyRepo;

    public CheckPropertyAvailabilityService(IPropertyRepository propertyRepo)
    {
        _propertyRepo = propertyRepo ?? throw new ArgumentNullException(nameof(propertyRepo));
    }

    public async Task<bool> IsAvailableAsync(Guid propertyId, CancellationToken ct = default)
    {
        var property = await _propertyRepo.GetByIdAsync(propertyId, ct)
            ?? throw new InvalidOperationException($"Property with ID '{propertyId}' was not found.");

        return property.Status == PropertyStatus.Available;
    }
}
