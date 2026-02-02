using GerenciadorDeAluguel.Domain.Entities;

namespace GerenciadorDeAluguel.Application.Ports;

public interface IPropertyRepository
{
    Task<Property?> GetByIdAsync(Guid propertyId, CancellationToken ct = default);
}
