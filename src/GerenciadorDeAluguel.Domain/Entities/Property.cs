using GerenciadorDeAluguel.Domain.Enums;
using GerenciadorDeAluguel.Domain.ValueObjects;

namespace GerenciadorDeAluguel.Domain.Entities
{
    public class Property
    {
        public Guid Id { get; private set; }
        public Client Owner { get; private set; }
        public Address Address { get; private set; }
        public Money MonthlyRent { get; private set; }
        public PropertyType Type { get; private set; }
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

        public void ChangeStatus(PropertyStatus newStatus)
        {
            Status = newStatus;
        }

        #pragma warning disable CS8618
        private Property() { }  // EF Core only
        #pragma warning restore CS8618
    }
}
