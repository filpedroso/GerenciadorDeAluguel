using GerenciadorDeAluguel.Domain.Entities;
using GerenciadorDeAluguel.Domain.ValueObjects;
using GerenciadorDeAluguel.Domain.Enums;
using Xunit;

namespace GerenciadorDeAluguel.Tests.TestDomainObjects
{
    public class TestReservation
    {
        private Client CreateValidClient() =>
            new Client("Maria Souza", "maria@email.com", "31988887777", "98765432100");

        private Client CreateValidOwner() =>
            new Client("João Dono", "joao@email.com", "31988889999", "12345678901");

        private Property CreateValidProperty() =>
            new Property(
                CreateValidOwner(),
                new Address("Rua das Laranjeiras", "100", "31234-567", "Belo Horizonte", "MG"),
                new Money(1200.00m),
                PropertyType.Apartment
            );

        private Period CreateValidPeriod() =>
            new Period(new DateTime(2026, 2, 1), new DateTime(2026, 2, 28));

        [Fact]
        public void ShouldCreateReservationWithValidData()
        {
            var client = CreateValidClient();
            var property = CreateValidProperty();
            var period = CreateValidPeriod();
            var rent = new Money(1200.00m);

            var reservation = new Reservation(client, property, period, rent);

            Assert.Equal(client, reservation.Tenant);
            Assert.Equal(property, reservation.Property);
            Assert.Equal(period, reservation.Period);
            Assert.Equal(rent, reservation.MonthlyRent);
            Assert.NotEqual(Guid.Empty, reservation.Id);
        }

        [Fact]
        public void ShouldRequireClient()
        {
            Assert.Throws<ArgumentNullException>(() => new Reservation(null!, CreateValidProperty(), CreateValidPeriod(), new Money(1200.00m)));
        }

        [Fact]
        public void ShouldRequireProperty()
        {
            Assert.Throws<ArgumentNullException>(() => new Reservation(CreateValidClient(), null!, CreateValidPeriod(), new Money(1200.00m)));
        }

        [Fact]
        public void ShouldRequirePeriod()
        {
            Assert.Throws<ArgumentNullException>(() => new Reservation(CreateValidClient(), CreateValidProperty(), null!, new Money(1200.00m)));
        }

        [Fact]
        public void ShouldRequireMonthlyRent()
        {
            Assert.Throws<ArgumentNullException>(() => new Reservation(CreateValidClient(), CreateValidProperty(), CreateValidPeriod(), null!));
        }

        [Fact]
        public void ShouldNotAllowReservationForUnavailableProperty()
        {
            var client = CreateValidClient();
            var property = new Property(
                CreateValidOwner(),
                new Address("Rua das Laranjeiras", "100", "31234-567", "Belo Horizonte", "MG"),
                new Money(1200.00m),
                PropertyType.Apartment
            );
            // Simulate property not available
            typeof(Property).GetProperty("Status")!.SetValue(property, PropertyStatus.Rented);

            Assert.Throws<InvalidOperationException>(() => new Reservation(client, property, CreateValidPeriod(), new Money(1200.00m)));
        }
    }
}
