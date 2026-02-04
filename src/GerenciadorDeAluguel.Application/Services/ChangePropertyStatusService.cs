using GerenciadorDeAluguel.Application.Ports;
using GerenciadorDeAluguel.Domain.Enums;

namespace GerenciadorDeAluguel.Application.Services;

public class ChangePropertyStatusService
{
    private readonly IPropertyRepository _propertyRepository;

    public ChangePropertyStatusService(IPropertyRepository propertyRepository)
    {
        _propertyRepository = propertyRepository ?? throw new ArgumentNullException(nameof(propertyRepository));
    }

    public async Task ChangeStatusAsync(Guid propertyId, PropertyStatus newStatus, CancellationToken ct = default)
    {
        var property = await _propertyRepository.GetByIdAsync(propertyId, ct)
            ?? throw new InvalidOperationException($"Property with ID '{propertyId}' was not found.");

        property.ChangeStatus(newStatus);

        await _propertyRepository.SaveAsync(property, ct);
    }
}
