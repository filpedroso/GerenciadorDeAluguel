using GerenciadorDeAluguel.Application.DTOs;
using GerenciadorDeAluguel.Application.Ports;
using GerenciadorDeAluguel.Domain.Entities;
using GerenciadorDeAluguel.Domain.Enums;
using GerenciadorDeAluguel.Domain.ValueObjects;

namespace GerenciadorDeAluguel.Application.Services;

public class RegisterPropertyService
{
    private readonly IPropertyRepository _propertyRepository;
    private readonly IClientRepository _clientRepository;

    public RegisterPropertyService(IPropertyRepository propertyRepository, IClientRepository clientRepository)
    {
        _propertyRepository = propertyRepository;
        _clientRepository = clientRepository ?? throw new ArgumentNullException(nameof(clientRepository));
    }

    public async Task<Guid> ExecuteAsync(RegisterPropertyDTO dto, CancellationToken ct = default)
    {
        ValidateDTO(dto);

        var address = new Address(
            dto.Street,
            dto.Number,
            dto.ZipCode,
            dto.City,
            dto.State
        );

        var monthlyRent = new Money(dto.MonthlyRent);

        // 1. Load owner
        if (dto.OwnerId == Guid.Empty)
            throw new ArgumentException("OwnerId is required", nameof(dto.OwnerId));

        var owner = await _clientRepository.GetByIdAsync(dto.OwnerId, ct);
        if (owner is null)
            throw new InvalidOperationException($"Owner with ID '{dto.OwnerId}' was not found.");

        // 2. Create property and persist
        var property = new Property(
            owner,
            address,
            monthlyRent,
            dto.Type
        );

        await _propertyRepository.SaveAsync(property, ct);

        return property.Id;
    }

    private void ValidateDTO(RegisterPropertyDTO dto)
    {
        if (dto == null)
            throw new ArgumentNullException(nameof(dto));

        if (string.IsNullOrWhiteSpace(dto.Street))
            throw new ArgumentException("Street is required", nameof(dto.Street));

        if (string.IsNullOrWhiteSpace(dto.Number))
            throw new ArgumentException("Number is required", nameof(dto.Number));

        if (string.IsNullOrWhiteSpace(dto.City))
            throw new ArgumentException("City is required", nameof(dto.City));

        if (string.IsNullOrWhiteSpace(dto.State))
            throw new ArgumentException("State is required", nameof(dto.State));

        if (string.IsNullOrWhiteSpace(dto.ZipCode))
            throw new ArgumentException("ZipCode is required", nameof(dto.ZipCode));

        if (dto.MonthlyRent <= 0)
            throw new ArgumentException("Monthly rent must be greater than zero", nameof(dto.MonthlyRent));
    }
}
