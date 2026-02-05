namespace GerenciadorDeAluguel.Application.DTOs;

/// <summary>
/// Data transfer object for creating a new reservation.
/// </summary>
/// <param name="PropertyId">Unique identifier of the property to reserve.</param>
/// <param name="ClientId">Unique identifier of the client making the reservation.</param>
/// <param name="CheckIn">Check-in date.</param>
/// <param name="CheckOut">Check-out date.</param>
public sealed record CreateReservationCommandDTO(
    Guid PropertyId,
    Guid ClientId,
    DateOnly CheckIn,
    DateOnly CheckOut
    );
