using GerenciadorDeAluguel.Domain.Entities;
using GerenciadorDeAluguel.Domain.Enums;
using GerenciadorDeAluguel.Domain.ValueObjects;
using Xunit;

namespace GerenciadorDeAluguel.Tests.TestDomainObjects
{
    public class TestDocument
    {
        [Fact]
        public void ShouldCreateWithValidCpf()
        {
            var cpf = "08903320611";
            var document = new Document(cpf);
            Assert.Equal(cpf, document.Value);
            Assert.True(document.IsCpf);
            Assert.False(document.IsCnpj);
        }

        [Fact]
        public void ShouldCreateWithValidCpfFormatted()
        {
            var cpfFormatted = "089.033.206-11";
            var document = new Document(cpfFormatted);
            Assert.Equal("08903320611", document.Value);
            Assert.True(document.IsCpf);
            Assert.False(document.IsCnpj);
        }

        [Fact]
        public void ShouldCreateWithValidCnpj()
        {
            var cnpj = "42960849000319";
            var document = new Document(cnpj);
            Assert.Equal(cnpj, document.Value);
            Assert.True(document.IsCnpj);
            Assert.False(document.IsCpf);
        }

        [Fact]
        public void ShouldCreateWithValidCnpjFormatted()
        {
            var cnpjFormatted = "42.960.849/0003-19";
            var document = new Document(cnpjFormatted);
            Assert.Equal("42960849000319", document.Value); // Should normalize
            Assert.True(document.IsCnpj);
            Assert.False(document.IsCpf);
        }

        [Fact]
        public void ShouldNotAllowInvalidCpf()
        {
            var invalidCpf = "1234567890"; // Invalid CPF
            Assert.Throws<ArgumentException>(() => new Document(invalidCpf));
        }

        [Fact]
        public void ShouldNotAllowInvalidCnpj()
        {
            var invalidCnpj = "1234567800010"; // Invalid CNPJ
            Assert.Throws<ArgumentException>(() => new Document(invalidCnpj));
        }

        [Fact]
        public void ShouldNotAllowEmptyOrNull()
        {
            Assert.Throws<ArgumentException>(() => new Document(""));
            Assert.Throws<ArgumentException>(() => new Document(null!));
        }
    }
}
