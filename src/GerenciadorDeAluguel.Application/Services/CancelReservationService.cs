using GerenciadorDeAluguel.Application.DTOs;
using GerenciadorDeAluguel.Application.Ports;
using GerenciadorDeAluguel.Domain.Entities;
using GerenciadorDeAluguel.Domain.ValueObjects;

namespace GerenciadorDeAluguel.Application.Services;

public class CancelReservationService
{
    private readonly IReservationRepository _reservationRepo;

    public CancelReservationService(IReservationRepository reservationRepo)
    {
        _reservationRepo =
            reservationRepo ??
                throw new ArgumentNullException(nameof(reservationRepo));
    }

    public async Task<Guid> CancelAsync(Guid id, CancellationToken ct = default)
    {
        // 1. Load and validate property exists
        var reservation = await _reservationRepo.GetByIdAsync(id, ct);
        if (reservation is null)
            throw new InvalidOperationException($"Reservation with ID '{id}' was not found.");

        reservation.Cancel();

        // 5. Persist the new reservation
        await _reservationRepo.SaveAsync(reservation, ct);

        // 6. Return the ID
        return reservation.Id;
    }
}
