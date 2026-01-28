using GerenciadorDeAluguel.Domain.ValueObjects;
using Xunit;

namespace GerenciadorDeAluguel.Tests
{
    public class TestMoney
    {
        [Fact]
        public void ShouldCreateWithPositiveValue()
        {
            var money = new Money(50.00m);
            Assert.Equal(50.00m, money.Value);
        }

        [Fact]
        public void ShouldNotAllowNegativeValue()
        {
            Assert.Throws<ArgumentException>(() => new Money(-50.00m));
        }

        [Fact]
        public void ShouldNotAllowZero()
        {
            Assert.Throws<ArgumentException>(() => new Money(0m));
        }

        [Fact]
        public void ShouldAddAndSubtract()
        {
            Money value1 = new Money(30.00m);
            Money value2 = new Money(25.00m);

            var sum = value1.Add(value2);
            Assert.Equal(55.00m, sum.Value);

            var subtraction = value1.Subtract(value2);
            Assert.Equal(5.00m, subtraction.Value);
        }

        [Fact]
        public void SubtractionResultingInNegativeShouldFail()
        {
            Money value1 = new Money(30.00m);
            Money value2 = new Money(25.00m);

            Assert.Throws<ArgumentException>(() => value2.Subtract(value1));
        }

        [Fact]
        public void ShouldMultiplyByFactor()
        {
            var money = new Money(100.00m);

            var result = money.Multiply(1.10m); // 10% adjustment
            Assert.Equal(110.00m, result.Value);
        }

        [Fact]
        public void MultiplicationByZeroOrNegativeShouldFail()
        {
            var money = new Money(100.00m);

            Assert.Throws<ArgumentException>(() => money.Multiply(0m));
            Assert.Throws<ArgumentException>(() => money.Multiply(-1m));
        }

        [Fact]
        public void ShouldDivide()
        {
            var money = new Money(120.00m);

            var result = money.Divide(4m); // divide into 4 installments
            Assert.Equal(30.00m, result.Value);
        }

        [Fact]
        public void DivisionByZeroShouldFail()
        {
            var money = new Money(100.00m);

            Assert.Throws<DivideByZeroException>(() => money.Divide(0m));
        }

        [Fact]
        public void DivisionByNegativeShouldFail()
        {
            var money = new Money(100.00m);

            Assert.Throws<ArgumentException>(() => money.Divide(-2m));
        }
    }
}
