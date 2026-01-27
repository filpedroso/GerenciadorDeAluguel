using GerenciadorDeAluguel.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace GerenciadorDeAluguel.Tests
{
    public class  TestDinheiro
    {
        [Fact]
        public void DeveCriarComValorPositivo()
        {
            var dinheiro = new Dinheiro(50.00m);
            Assert.Equal(50.00m, dinheiro.Valor);
        }

        [Fact]
        public void NaoDevePermitirValorNegativo()
        {
            Assert.Throws<ArgumentException>(() => new Dinheiro(-50.00m));
        }

        [Fact]
        public void NaoPermitirZero()
        {
            Assert.Throws<ArgumentException>(() => new Dinheiro(0m));
        }

        [Fact]
        public void DeveSomarESubtrair()
        {
            Dinheiro valor1 = new Dinheiro(30.00m);
            Dinheiro valor2 = new Dinheiro(25.00m);

            var soma = valor1.Somar(valor2);
            Assert.Equal(55.00m, soma.Valor);

            var subtracao = valor1.Subtrair(valor2);
            Assert.Equal(5.00m, subtracao.Valor);
        }

        [Fact]
        public void SubtraçaoResultNegativoDeveFalhar()
        {
            Dinheiro valor1 = new Dinheiro(30.00m);
            Dinheiro valor2 = new Dinheiro(25.00m);

            Assert.Throws<ArgumentException>(() => valor2.Subtrair(valor1));
        }

        [Fact]
        public void DeveMultiplicarPorFator()
        {
            var dinheiro = new Dinheiro(100.00m);

            var resultado = dinheiro.Multiplicar(1.10m); // reajuste de 10%
            Assert.Equal(110.00m, resultado.Valor);
        }

        [Fact]
        public void MultiplicacaoPorZeroOuNegativoDeveFalhar()
        {
            var dinheiro = new Dinheiro(100.00m);

            Assert.Throws<ArgumentException>(() => dinheiro.Multiplicar(0m));
            Assert.Throws<ArgumentException>(() => dinheiro.Multiplicar(-1m));
        }

        [Fact]
        public void DeveDividir()
        {
            var dinheiro = new Dinheiro(120.00m);

            var resultado = dinheiro.Dividir(4m); // dividir em 4 parcelas
            Assert.Equal(30.00m, resultado.Valor);
        }

        [Fact]
        public void DivisaoPorZeroDeveFalhar()
        {
            var dinheiro = new Dinheiro(100.00m);

            Assert.Throws<DivideByZeroException>(() => dinheiro.Dividir(0m));
        }

        [Fact]
        public void DivisaoPorNegativoDeveFalhar()
        {
            var dinheiro = new Dinheiro(100.00m);

            Assert.Throws<ArgumentException>(() => dinheiro.Dividir(-2m));
        }
    }
}
