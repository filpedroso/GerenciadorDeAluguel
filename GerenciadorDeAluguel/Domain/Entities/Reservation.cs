using GerenciadorDeAluguel.Domain.ValueObjects;
using GerenciadorDeAluguel.Domain.Enums;

namespace GerenciadorDeAluguel.Domain.Entities
{
    public class Reservation
    {
        public Guid Id { get; }
        public Client Tenant { get; }
        public Property Property { get; }
        public Period Period { get; }
        public Money MonthlyRent { get; }

        public Reservation(Client tenant, Property property, Period period, Money monthlyRent)
        {
            if (tenant == null)
                throw new ArgumentNullException(nameof(tenant), "Reservation must have a tenant");
            if (property == null)
                throw new ArgumentNullException(nameof(property), "Reservation must have a property");
            if (period == null)
                throw new ArgumentNullException(nameof(period), "Reservation must have a period");
            if (monthlyRent == null)
                throw new ArgumentNullException(nameof(monthlyRent), "Reservation must have a monthly rent");
            if (property.Status != PropertyStatus.Available)
                throw new InvalidOperationException("Property is not available for reservation");

            Id = Guid.NewGuid();
            Tenant = tenant;
            Property = property;
            Period = period;
            MonthlyRent = monthlyRent;
        }
    }
}