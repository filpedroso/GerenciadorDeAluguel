namespace GerenciadorDeAluguel.Domain.ValueObjects
{
    public record Money
    {
        public decimal Value { get; }
        public Money(decimal value)
        {
            if (value <= 0)
                throw new ArgumentException("Value must be greater than zero", nameof(value));
            Value = value;
        }

        public Money Add(Money other)
        {
            return new Money(Value + other.Value);
        }

        public Money Subtract(Money other)
        {
            return new Money(Value - other.Value);
        }

        public Money Multiply(decimal factor)
        {
            return new Money(Value * factor);
        }

        public Money Divide(decimal divisor)
        {
            if (divisor == 0)
                throw new DivideByZeroException("Cannot divide by zero.");
            return new Money(Value / divisor);
        }
    }
}
