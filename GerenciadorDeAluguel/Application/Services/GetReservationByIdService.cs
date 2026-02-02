using GerenciadorDeAluguel.Application.Ports;
using GerenciadorDeAluguel.Domain.Entities;

namespace GerenciadorDeAluguel.Application.Services;

public class GetReservationByIdService
{
    private readonly IReservationRepository _reservationRepo;

    public GetReservationByIdService(IReservationRepository reservationRepo)
    {
        _reservationRepo = reservationRepo ?? throw new ArgumentNullException(nameof(reservationRepo));
    }

    public async Task<Reservation> GetByIdAsync(Guid reservationId, CancellationToken ct = default)
    {
        var reservation = await _reservationRepo.GetByIdAsync(reservationId, ct)
            ?? throw new InvalidOperationException($"Reservation '{reservationId}' not found.");

        return reservation;
    }
}


// Automapper
// Migration
//