using GerenciadorDeAluguel.Domain.Entities;
using GerenciadorDeAluguel.Domain.Enums;
using GerenciadorDeAluguel.Domain.ValueObjects;
using Xunit;

namespace GerenciadorDeAluguel.Tests.TestDomainObjects
{
    public class TestProperty
    {
        public Address CreateValidAddress() =>
            new Address("Rua do Amendoim", "2222", "31321-123", "Belo Horizonte", "MG");
        public Client CreateValidClient() =>
            new Client("Renato Manfredini", "rr@dti.com.br", "31997979797", "08908908911");

        [Fact]
        public void ShouldCreatePropertyWithValidData()
        {
            var address = CreateValidAddress();
            var owner = CreateValidClient();
            var monthlyRent = new Money(900.00m);

            var property = new Property(owner, address, monthlyRent, PropertyType.House);

            Assert.Equal(address, property.Address);
            Assert.Equal(monthlyRent, property.MonthlyRent);
            Assert.Equal(PropertyType.House, property.Type);
            Assert.Equal(PropertyStatus.Available, property.Status);
            Assert.NotEqual(Guid.Empty, property.Id);
        }

        [Fact]
        public void ShouldDefaultStatusToAvailable()
        {
            var property = new Property(CreateValidClient(), CreateValidAddress(), new Money(1000m), PropertyType.Apartment);

            Assert.Equal(PropertyStatus.Available, property.Status);
        }

        [Fact]
        public void ShouldRequireAddress()
        {
            Assert.Throws<ArgumentNullException>(() => new Property(CreateValidClient(), null!, new Money(1000m), PropertyType.House));
        }

        [Fact]
        public void ShouldRequireMonthlyRent()
        {
            Assert.Throws<ArgumentNullException>(() => new Property(CreateValidClient(), CreateValidAddress(), null!, PropertyType.House));
        }
    }
}
