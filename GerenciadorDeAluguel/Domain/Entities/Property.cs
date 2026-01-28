using GerenciadorDeAluguel.Domain.Enums;
using GerenciadorDeAluguel.Domain.ValueObjects;

namespace GerenciadorDeAluguel.Domain.Entities
{
    public class Property
    {
        public Guid Id { get; }
        public Address Address { get; }
        public Money MonthlyRent { get; }
        public PropertyType Type { get; }
        public PropertyStatus Status { get; private set; }

        public Property(Address address, Money monthlyRent, PropertyType type)
        {
            if (address == null)
                throw new ArgumentNullException(nameof(address), "Address is required");
            if (monthlyRent == null)
                throw new ArgumentNullException(nameof(monthlyRent), "Monthly rent is required");

            Id = Guid.NewGuid();
            Address = address;
            MonthlyRent = monthlyRent;
            Type = type;
            Status = PropertyStatus.Available;
        }
    }
}
