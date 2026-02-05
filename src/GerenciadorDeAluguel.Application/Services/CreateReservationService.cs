using GerenciadorDeAluguel.Application.DTOs;
using GerenciadorDeAluguel.Application.Ports;
using GerenciadorDeAluguel.Domain.Entities;
using GerenciadorDeAluguel.Domain.ValueObjects;
using GerenciadorDeAluguel.Domain.Enums;

namespace GerenciadorDeAluguel.Application.Services;

public class CreateReservationService
{
    private readonly IClientRepository _clientRepo;
    private readonly IPropertyRepository _propertyRepo;
    private readonly IReservationRepository _reservationRepo;

    public CreateReservationService(
        IClientRepository clientRepo,
        IPropertyRepository propertyRepo,
        IReservationRepository reservationRepo
    )
    {
        _clientRepo = clientRepo ?? throw new ArgumentNullException(nameof(clientRepo));
        _propertyRepo = propertyRepo ?? throw new ArgumentNullException(nameof(propertyRepo));
        _reservationRepo = reservationRepo ?? throw new ArgumentNullException(nameof(reservationRepo));
    }

    public async Task<Guid> CreateAsync(CreateReservationCommandDTO command, CancellationToken ct = default)
    {
        // 1. Load and validate property exists
        var property = await _propertyRepo.GetByIdAsync(command.PropertyId, ct);
        if (property is null)
            throw new InvalidOperationException($"Property with ID '{command.PropertyId}' was not found.");

        // 2. Load and validate client exists
        var client = await _clientRepo.GetByIdAsync(command.ClientId, ct);
        if (client is null)
            throw new InvalidOperationException($"Client with ID '{command.ClientId}' was not found.");

        // 3. Build Period VO from DTO primitives
        var period = new Period(command.CheckIn, command.CheckOut);

        // 4. Create domain entity (business rules enforced here)
        var reservation = new Reservation(client, property, period, property.MonthlyRent);

        // 5. Change property status and persist it
        property.ChangeStatus(PropertyStatus.Rented);
        await _propertyRepo.SaveAsync(property, ct);

        // 6. Persist the new reservation
        await _reservationRepo.SaveAsync(reservation, ct);

        // 7. Return the ID
        return reservation.Id;
    }
}
