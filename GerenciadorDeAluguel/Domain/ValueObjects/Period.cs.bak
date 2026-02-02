namespace GerenciadorDeAluguel.Domain.ValueObjects
{
    public record Period
    {
        public DateTime Start { get; }
        public DateTime End { get; }

        public Period(DateTime start, DateTime end)
        {
            if (start >= end)
                throw new ArgumentException("End date must be after start date");
            Start = start;
            End = end;
        }
    }
}
