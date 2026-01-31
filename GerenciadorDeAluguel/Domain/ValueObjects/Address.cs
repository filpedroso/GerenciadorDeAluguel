namespace GerenciadorDeAluguel.Domain.ValueObjects
{
    public record Address
    {
        public string Street { get; }
        public string Number { get; }
        public string ZipCode { get; }
        public string City { get; }
        public string State { get; }
        public string? Complement { get; }

        public Address(string street, string number, string zipCode, string city, string state, string? complement = null)
        {
            if (string.IsNullOrWhiteSpace(street))
                throw new ArgumentException("Street is required", nameof(street));
            if (string.IsNullOrWhiteSpace(number))
                throw new ArgumentException("Number is required", nameof(number));
            if (string.IsNullOrWhiteSpace(zipCode))
                throw new ArgumentException("ZipCode is required", nameof(zipCode));
            if (string.IsNullOrWhiteSpace(city))
                throw new ArgumentException("City is required", nameof(city));
            if (string.IsNullOrWhiteSpace(state))
                throw new ArgumentException("State is required", nameof(state));

            Street = street;
            Number = number;
            ZipCode = zipCode;
            City = city;
            State = state;
            Complement = complement;
        }
    }
}
