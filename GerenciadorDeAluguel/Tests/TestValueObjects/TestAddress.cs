using GerenciadorDeAluguel.Domain.ValueObjects;
using Xunit;

namespace GerenciadorDeAluguel.Tests
{
    public class TestAddress
    {
        [Fact]
        public void ShouldCreateAddressWithValidData()
        {
            var street = "Janete Clair";
            var number = "242";
            var zipCode = "31565-400";
            var city = "Belo Horizonte";
            var state = "Minas Gerais";

            var address = new Address(street, number, zipCode, city, state);

            Assert.Equal(street, address.Street);
            Assert.Equal(number, address.Number);
            Assert.Equal(zipCode, address.ZipCode);
            Assert.Equal(city, address.City);
            Assert.Equal(state, address.State);
            Assert.Null(address.Complement);
        }

        [Fact]
        public void ShouldCreateAddressWithComplement()
        {
            var address = new Address("Janete Clair", "242", "31565-400", "Belo Horizonte", "MG", "Apt 101");

            Assert.Equal("Apt 101", address.Complement);
        }

        [Fact]
        public void ShouldNotAllowEmptyStreet()
        {
            Assert.Throws<ArgumentException>(() => new Address("", "242", "31565-400", "Belo Horizonte", "MG"));
            Assert.Throws<ArgumentException>(() => new Address("   ", "242", "31565-400", "Belo Horizonte", "MG"));
            Assert.Throws<ArgumentException>(() => new Address(null!, "242", "31565-400", "Belo Horizonte", "MG"));
        }

        [Fact]
        public void ShouldNotAllowEmptyNumber()
        {
            Assert.Throws<ArgumentException>(() => new Address("Janete Clair", "", "31565-400", "Belo Horizonte", "MG"));
            Assert.Throws<ArgumentException>(() => new Address("Janete Clair", "   ", "31565-400", "Belo Horizonte", "MG"));
            Assert.Throws<ArgumentException>(() => new Address("Janete Clair", null!, "31565-400", "Belo Horizonte", "MG"));
        }

        [Fact]
        public void ShouldNotAllowEmptyZipCode()
        {
            Assert.Throws<ArgumentException>(() => new Address("Janete Clair", "242", "", "Belo Horizonte", "MG"));
            Assert.Throws<ArgumentException>(() => new Address("Janete Clair", "242", "   ", "Belo Horizonte", "MG"));
            Assert.Throws<ArgumentException>(() => new Address("Janete Clair", "242", null!, "Belo Horizonte", "MG"));
        }

        [Fact]
        public void ShouldNotAllowEmptyCity()
        {
            Assert.Throws<ArgumentException>(() => new Address("Janete Clair", "242", "31565-400", "", "MG"));
            Assert.Throws<ArgumentException>(() => new Address("Janete Clair", "242", "31565-400", "   ", "MG"));
            Assert.Throws<ArgumentException>(() => new Address("Janete Clair", "242", "31565-400", null!, "MG"));
        }

        [Fact]
        public void ShouldNotAllowEmptyState()
        {
            Assert.Throws<ArgumentException>(() => new Address("Janete Clair", "242", "31565-400", "Belo Horizonte", ""));
            Assert.Throws<ArgumentException>(() => new Address("Janete Clair", "242", "31565-400", "Belo Horizonte", "   "));
            Assert.Throws<ArgumentException>(() => new Address("Janete Clair", "242", "31565-400", "Belo Horizonte", null!));
        }
    }
}
