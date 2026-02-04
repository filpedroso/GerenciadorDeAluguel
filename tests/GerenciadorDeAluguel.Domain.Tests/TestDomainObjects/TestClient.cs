using GerenciadorDeAluguel.Domain.Entities;
using GerenciadorDeAluguel.Domain.ValueObjects;
using Xunit;
using static GerenciadorDeAluguel.Domain.Tests.Builders.TestDataBuilder;

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
            var document = CreateValidCpf();

            var client = new Client(name, email, phone, document);

            Assert.Equal(name, client.Name);
            Assert.Equal(email, client.Email);
            Assert.Equal(phone, client.Phone);
            Assert.Equal(document, client.DocumentNumber);
            Assert.NotEqual(Guid.Empty, client.Id);
        }

        [Fact]
        public void ShouldRequireName()
        {
            var document = CreateValidCpf();

            Assert.Throws<ArgumentException>(() =>
                new Client(null!, "a@b.com", "123", document)
            );
            Assert.Throws<ArgumentException>(() => new Client("", "a@b.com", "123", document));
        }

        [Fact]
        public void ShouldRequireEmail()
        {
            var document = CreateValidCpf();

            Assert.Throws<ArgumentException>(() => new Client("João", null!, "123", document));
            Assert.Throws<ArgumentException>(() => new Client("João", "", "123", document));
        }

        [Fact]
        public void ShouldRequirePhone()
        {
            var document = CreateValidCpf();

            Assert.Throws<ArgumentException>(() =>
                new Client("João", "a@b.com", null!, document)
            );
            Assert.Throws<ArgumentException>(() =>
                new Client("João", "a@b.com", "", document)
            );
        }

        [Fact]
        public void ShouldRequireCpfOrCnpj()
        {
            Assert.Throws<ArgumentNullException>(() => new Client("João", "a@b.com", "123", null!));
        }

        [Fact]
        public void ShouldAllowNullResidentialAddress()
        {
            var document = CreateValidCpf();
            
            var client = new Client("João", "a@b.com", "123", document);
            Assert.Null(client.ResidentialAddress);
        }

        [Fact]
        public void ShouldSetResidentialAddressWhenProvided()
        {
            var document = CreateValidCpf();

            var address = new Address("Rua X", "123", "12345-678", "Cidade", "ST");
            var client = new Client("João", "a@b.com", "123", document, address);
            Assert.Equal(address, client.ResidentialAddress);
        }
    }
}
