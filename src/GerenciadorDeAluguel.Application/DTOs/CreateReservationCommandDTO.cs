namespace GerenciadorDeAluguel.Application.DTOs;

public sealed record CreateReservationCommandDTO(
    Guid PropertyId,
    Guid ClientId,
    DateOnly CheckIn,
    DateOnly CheckOut
    );
