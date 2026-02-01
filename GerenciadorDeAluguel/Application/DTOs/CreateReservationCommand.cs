namespace GerenciadorDeAluguel.Application.DTOs;

public sealed record CreateReservationCommand(
    Guid PropertyId,
    Guid ClientId,
    DateOnly CheckIn,
    DateOnly CheckOut
    );
