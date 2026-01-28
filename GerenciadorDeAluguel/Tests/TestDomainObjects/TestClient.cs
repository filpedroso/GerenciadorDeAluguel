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
    }
}
