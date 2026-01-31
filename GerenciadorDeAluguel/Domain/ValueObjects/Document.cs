namespace GerenciadorDeAluguel.Domain.ValueObjects
{
    public record Document
    {
        public string Value { get; }
        public bool IsCpf { get; }
        public bool IsCnpj { get; }

        public Document(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException("Cpf or Cnpj identifier is required", nameof(value));

            var normalized = Normalize(value);

            if (IsCpfFormat(normalized))
            {
                if (!DocumentValidator.ValidateCpf(normalized))
                    throw new ArgumentException("Invalid CPF", nameof(value));
                Value = normalized;
                IsCpf = true;
                IsCnpj = false;
            }
            else if (IsCnpjFormat(normalized))
            {
                if (!DocumentValidator.ValidateCnpj(normalized))
                    throw new ArgumentException("Invalid CNPJ", nameof(value));
                Value = normalized;
                IsCpf = false;
                IsCnpj = true;
            }
            else
                throw new ArgumentException("Value is neither valid CPF nor CNPJ", nameof(value));
        }
        private static string Normalize(string value) =>
            new string(value.Where(char.IsAsciiLetterOrDigit).ToArray());
        private static bool IsCpfFormat(string value) =>
            value.Length == 11;
        private static bool IsCnpjFormat(string value) =>
            value.Length == 14;
    }
}
