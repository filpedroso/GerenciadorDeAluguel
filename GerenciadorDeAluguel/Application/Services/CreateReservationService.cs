using GerenciadorDeAluguel.Domain.Entities;
using GerenciadorDeAluguel.Domain.ValueObjects;

namespace GerenciadorDeAluguel.Application.Services
{
    public class CreateReservationService
    {
        public Reservation Execute(
            Client tenant,
            Property property,
            Period period,
            Money monthlyRent
            )
        {
            return new Reservation(tenant, property, period, monthlyRent);
        }
    }
}
