using GerenciadorDeAluguel.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace GerenciadorDeAluguel.Tests
{
    public class TestPeriodo
    {
        [Fact]
        public void DeveCriarComPeriodoValido()
        {
            var inicio = new DateTime(year: 2026, month: 02, day: 01);
            var fim = new DateTime(year: 2026, month: 02, day: 28);

            var periodo = new Periodo(inicio, fim);

            Assert.Equal(inicio, periodo.Inicio);
            Assert.Equal(fim, periodo.Fim);
        }

        [Fact]
        public void NaoDevePermitirPeriodoComFimAnteriorAInicio()
        {
            var inicio = new DateTime(year: 2026, month: 02, day: 01);
            var fim = new DateTime(year: 2025, month: 01, day: 31);

            Assert.Throws<ArgumentException>(() => new Periodo(inicio, fim));
        }
    }
}
