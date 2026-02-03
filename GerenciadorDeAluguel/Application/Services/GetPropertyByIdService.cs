using GerenciadorDeAluguel.Application.Ports;
using GerenciadorDeAluguel.Domain.Entities;

namespace GerenciadorDeAluguel.Application.Services;

public class GetPropertyByIdService
{
    private readonly IPropertyRepository _propertyRepo;

    public GetPropertyByIdService(IPropertyRepository propertyRepo)
    {
        _propertyRepo = propertyRepo ?? throw new ArgumentNullException(nameof(propertyRepo));
    }

    public async Task<Property> GetByIdAsync(Guid propertyId, CancellationToken ct = default)
    {
        var property = await _propertyRepo.GetByIdAsync(propertyId, ct)
            ?? throw new InvalidOperationException($"Property '{propertyId}' not found.");

        return property;
    }
}