using GerenciadorDeAluguel.Domain.Entities;

namespace GerenciadorDeAluguel.Application.Ports;

public interface IReservationRepository
{
    Task<Reservation?> GetByIdAsync(Guid reservationId, CancellationToken ct = default);
    Task SaveAsync(Reservation reservation, CancellationToken ct = default);
}
