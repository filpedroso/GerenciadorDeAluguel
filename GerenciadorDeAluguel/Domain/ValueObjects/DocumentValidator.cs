namespace GerenciadorDeAluguel.Domain.ValueObjects
{
    internal static class DocumentValidator
    {
        /// <summary>
        /// Validates a CPF (Cadastro de Pessoas Físicas) using modulo 11 algorithm.
        /// </summary>
        /// <param name="normalizedValue">11-digit normalized string (digits only)</param>
        /// <returns>True if valid CPF, false otherwise</returns>
        public static bool ValidateCpf(string cpf)
        {
        if (string.IsNullOrEmpty(cpf) || cpf.Length != 11)
            return false;

        // Only digits
        for (int i = 0; i < 11; i++)
            if (cpf[i] < '0' || cpf[i] > '9')
                return false;

        // Reject all-same-digit CPFs (common invalids)
        if (cpf.AsSpan().IndexOfAnyExcept(cpf[0]) == -1)
            return false;

        static int CalcDigit(ReadOnlySpan<char> digits, int startWeight)
        {
            int sum = 0;
            for (int i = 0; i < digits.Length; i++)
                sum += (digits[i] - '0') * (startWeight - i);

            int remainder = sum % 11;
            return remainder < 2 ? 0 : 11 - remainder;
        }

        int dv1 = CalcDigit(cpf.AsSpan(0, 9), startWeight: 10);
        if (cpf[9] - '0' != dv1)
            return false;

        int dv2 = CalcDigit(cpf.AsSpan(0, 10), startWeight: 11);
        if (cpf[10] - '0' != dv2)
            return false;

        return true;
        }

        /// <summary>
        /// Validates an alphanumeric CNPJ using modulo 11 algorithm (2026+ format).
        /// </summary>
        /// <param name="normalizedValue">14-character normalized string (12 alphanumeric + 2 numeric check digits)</param>
        /// <returns>True if valid CNPJ, false otherwise</returns>
        public static bool ValidateCnpj(string normalizedValue)
        {
        if (string.IsNullOrWhiteSpace(normalizedValue) || normalizedValue.Length != 14)
            return false;

        // CNPJ alfanumérico: 12 primeiros [0-9A-Z], 2 últimos dígitos [0-9]
        // Normalize to uppercase to match ASCII table in the guideline
        normalizedValue = normalizedValue.ToUpperInvariant();

        for (int i = 0; i < 12; i++)
        {
            char c = normalizedValue[i];
            bool ok = (c >= '0' && c <= '9') || (c >= 'A' && c <= 'Z');
            if (!ok) return false;
        }

        if (!(normalizedValue[12] >= '0' && normalizedValue[12] <= '9')) return false;
        if (!(normalizedValue[13] >= '0' && normalizedValue[13] <= '9')) return false;

        static int CharToValue(char c)
        {
            // spec: use ASCII decimal and subtract 48
            // '0'..'9' => 0..9 ; 'A'..'Z' => 17..42
            return (int)c - 48;
        }

        static int CalcDv(ReadOnlySpan<char> chars, ReadOnlySpan<int> weights)
        {
            int sum = 0;
            for (int i = 0; i < weights.Length; i++)
                sum += CharToValue(chars[i]) * weights[i];

            int remainder = sum % 11;
            return remainder < 2 ? 0 : 11 - remainder;
        }

        ReadOnlySpan<int> w1 = new[] { 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
        ReadOnlySpan<int> w2 = new[] { 6, 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };

        int dv1 = CalcDv(normalizedValue.AsSpan(0, 12), w1);
        if (normalizedValue[12] - '0' != dv1)
            return false;

        // For DV2 we use the first 12 chars + DV1 as the 13th char
        Span<char> first13 = stackalloc char[13];
        normalizedValue.AsSpan(0, 12).CopyTo(first13);
        first13[12] = (char)('0' + dv1);

        int dv2 = CalcDv(first13, w2);
        if (normalizedValue[13] - '0' != dv2)
            return false;

        return true;
        }
    }
}
