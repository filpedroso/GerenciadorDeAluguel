using GerenciadorDeAluguel.Domain.Enums;
using GerenciadorDeAluguel.Domain.ValueObjects;

namespace GerenciadorDeAluguel.Domain.Entities
{
    /// <summary>
    /// Represents a reservation of a property by a client.
    /// </summary>
    public class Reservation
    {
        public Guid Id { get; private set; }
        public Client Tenant { get; private set; }
        public Property Property { get; private set; }
        public Period Period { get; private set; }
        public Money MonthlyRent { get; private set; }
        public bool IsCancelled { get; private set; }
        public DateTime? CancelledAt { get; private set; }

        public Reservation(Client tenant, Property property, Period period, Money monthlyRent)
        {
            if (tenant == null)
                throw new ArgumentNullException(nameof(tenant), "Reservation must have a tenant");
            if (property == null)
                throw new ArgumentNullException(
                    nameof(property),
                    "Reservation must have a property"
                );
            if (period == null)
                throw new ArgumentNullException(nameof(period), "Reservation must have a period");
            if (monthlyRent == null)
                throw new ArgumentNullException(
                    nameof(monthlyRent),
                    "Reservation must have a monthly rent"
                );
            if (property.Status != PropertyStatus.Available)
                throw new InvalidOperationException("Property is not available for reservation");

            Id = Guid.NewGuid();
            Tenant = tenant;
            Property = property;
            Period = period;
            MonthlyRent = monthlyRent;
        }

        /// <summary>
        /// Cancels the reservation.
        /// </summary>
        public void Cancel()
        {
            if (IsCancelled)
                throw new InvalidOperationException("Already cancelled.");
            IsCancelled = true;
            CancelledAt = DateTime.UtcNow;
        }

        #pragma warning disable CS8618
        private Reservation() { }  // EF Core only
        #pragma warning restore CS8618
    }
}
