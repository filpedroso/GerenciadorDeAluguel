using GerenciadorDeAluguel.Domain.ValueObjects;
using Xunit;

namespace GerenciadorDeAluguel.Tests
{
    public class TestPeriod
    {
        [Fact]
        public void ShouldCreateWithValidPeriod()
        {
            var start = new DateTime(year: 2026, month: 02, day: 01);
            var end = new DateTime(year: 2026, month: 02, day: 28);

            var period = new Period(start, end);

            Assert.Equal(start, period.Start);
            Assert.Equal(end, period.End);
        }

        [Fact]
        public void ShouldNotAllowPeriodWithEndBeforeStart()
        {
            var start = new DateTime(year: 2026, month: 02, day: 01);
            var end = new DateTime(year: 2025, month: 01, day: 31);

            Assert.Throws<ArgumentException>(() => new Period(start, end));
        }
    }
}
