namespace GerenciadorDeAluguel.Domain.ValueObjects
{
    /// <summary>
    /// Represents a monetary value.
    /// </summary>
    public record Money
    {
        public decimal Value { get; }

        public Money(decimal value)
        {
            if (value <= 0)
                throw new ArgumentException("Value must be greater than zero", nameof(value));
            Value = value;
        }

        /// <summary>
        /// Adds another Money to this instance.
        /// </summary>
        /// <param name="other">The Money to add.</param>
        /// <returns>A new Money instance with the sum.</returns>
        public Money Add(Money other)
        {
            return new Money(Value + other.Value);
        }

        /// <summary>
        /// Subtracts another Money from this instance.
        /// </summary>
        /// <param name="other">The Money to subtract.</param>
        /// <returns>A new Money instance with the difference.</returns>
        public Money Subtract(Money other)
        {
            return new Money(Value - other.Value);
        }

        /// <summary>
        /// Multiplies this Money by a factor.
        /// </summary>
        /// <param name="factor">The multiplication factor.</param>
        /// <returns>A new Money instance with the product.</returns>
        public Money Multiply(decimal factor)
        {
            return new Money(Value * factor);
        }

        /// <summary>
        /// Divides this Money by a divisor.
        /// </summary>
        /// <param name="divisor">The divisor.</param>
        /// <returns>A new Money instance with the quotient.</returns>
        public Money Divide(decimal divisor)
        {
            if (divisor == 0)
                throw new DivideByZeroException("Cannot divide by zero.");
            return new Money(Value / divisor);
        }
    }
}
