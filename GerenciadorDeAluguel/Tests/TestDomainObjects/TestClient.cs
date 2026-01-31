using GerenciadorDeAluguel.Domain.Entities;
using GerenciadorDeAluguel.Domain.ValueObjects;
using Xunit;

namespace GerenciadorDeAluguel.Tests.TestDomainObjects
{
    public class TestClient
    {
        [Fact]
        public void ShouldCreateWithValidAttributes()
        {
            var name = "Gilberto Gil";
            var email = "gil.gil@email.com";
            var phone = "31999998888";
            var document = "12345678900";

            var client = new Client(name, email, phone, document);

            Assert.Equal(name, client.Name);
            Assert.Equal(email, client.Email);
            Assert.Equal(phone, client.Phone);
            Assert.Equal(document, client.CpfOrCnpj);
            Assert.NotEqual(Guid.Empty, client.Id);
        }

        [Fact]
        public void ShouldRequireName()
        {
            Assert.Throws<ArgumentException>(() => new Client(null!, "a@b.com", "123", "12345678900"));
            Assert.Throws<ArgumentException>(() => new Client("", "a@b.com", "123", "12345678900"));
        }

        [Fact]
        public void ShouldRequireEmail()
        {
            Assert.Throws<ArgumentException>(() => new Client("João", null!, "123", "12345678900"));
            Assert.Throws<ArgumentException>(() => new Client("João", "", "123", "12345678900"));
        }

        [Fact]
        public void ShouldRequirePhone()
        {
            Assert.Throws<ArgumentException>(() => new Client("João", "a@b.com", null!, "12345678900"));
            Assert.Throws<ArgumentException>(() => new Client("João", "a@b.com", "", "12345678900"));
        }

        [Fact]
        public void ShouldRequireCpfOrCnpj()
        {
            Assert.Throws<ArgumentException>(() => new Client("João", "a@b.com", "123", null!));
            Assert.Throws<ArgumentException>(() => new Client("João", "a@b.com", "123", ""));
        }

        [Fact]
        public void ShouldAllowNullResidentialAddress()
        {
            var client = new Client("João", "a@b.com", "123", "12345678900");
            Assert.Null(client.ResidentialAddress);
        }

        [Fact]
        public void ShouldSetResidentialAddressWhenProvided()
        {
            var address = new Address("Rua X", "123", "12345-678", "Cidade", "ST");
            var client = new Client("João", "a@b.com", "123", "12345678900", address);
            Assert.Equal(address, client.ResidentialAddress);
        }
    }
}
