using GerenciadorDeAluguel.Domain.Enums;
using GerenciadorDeAluguel.Domain.ValueObjects;

namespace GerenciadorDeAluguel.Domain.Entities
{
    public class Property
    {
        public Guid Id { get; }
        public Client Owner { get; }
        public Address Address { get; }
        public Money MonthlyRent { get; }
        public PropertyType Type { get; }
        public PropertyStatus Status { get; private set; }

        public Property(Client owner, Address address, Money monthlyRent, PropertyType type)
        {
            if (owner == null)
                throw new ArgumentException(nameof(owner), "Must have an owner");
            if (address == null)
                throw new ArgumentNullException(nameof(address), "Address is required");
            if (monthlyRent == null)
                throw new ArgumentNullException(nameof(monthlyRent), "Monthly rent is required");

            Id = Guid.NewGuid();
            Owner = owner;
            Address = address;
            MonthlyRent = monthlyRent;
            Type = type;
            Status = PropertyStatus.Available;
        }
    }
}
