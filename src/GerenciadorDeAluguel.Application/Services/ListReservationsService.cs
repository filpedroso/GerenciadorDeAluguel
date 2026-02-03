using GerenciadorDeAluguel.Application.Ports;
using GerenciadorDeAluguel.Domain.Entities;

namespace GerenciadorDeAluguel.Application.Services;

public class ListReservationsService
{
    private readonly IReservationRepository _reservationRepository;

    public ListReservationsService(IReservationRepository reservationRepository)
    {
        _reservationRepository = reservationRepository ?? throw new ArgumentNullException(nameof(reservationRepository));
    }

    public async Task<IReadOnlyList<Reservation>> ListAsync(CancellationToken ct = default)
    {
        return await _reservationRepository.GetAllAsync(ct);
    }
}