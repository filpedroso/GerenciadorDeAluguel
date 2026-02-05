namespace GerenciadorDeAluguel.Domain.ValueObjects
{
    /// <summary>
    /// Represents a date period with start and end dates.
    /// </summary>
    public record Period
    {
        public DateOnly Start { get; }
        public DateOnly End { get; }

        public Period(DateOnly start, DateOnly end)
        {
            if (start >= end)
                throw new ArgumentException("End date must be after start date");
            Start = start;
            End = end;
        }
    }
}
