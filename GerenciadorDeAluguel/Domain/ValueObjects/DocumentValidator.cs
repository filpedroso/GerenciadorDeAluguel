namespace GerenciadorDeAluguel.Domain.ValueObjects
{
    internal static class DocumentValidator
    {
        public static bool ValidateCpf(string normalizedValue)
        {
            if (normalizedValue?.Length != 11)
                return false;

            var allTheSameChar = normalizedValue.All(c => c == normalizedValue[0]);
            if (allTheSameChar)
                return false;

            // Calculate first verification digit
            int sum = 0;
            int multiplier = 10;

            for (int i = 0; i < 9; i++)
            {
                if (!int.TryParse(normalizedValue[i].ToString(), out int digit))
                    return false;

                sum += digit * multiplier;
                multiplier--;
            }

            int remainder = sum % 11;
            int firstDigit = remainder < 2 ? 0 : 11 - remainder;

            // Validate first digit
            if (!int.TryParse(normalizedValue[9].ToString(), out int providedFirstDigit) ||
                providedFirstDigit != firstDigit)
                return false;

            // Calculate second verification digit
            sum = 0;
            multiplier = 11;

            for (int i = 0; i < 10; i++)
            {
                if (!int.TryParse(normalizedValue[i].ToString(), out int digit))
                    return false;

                sum += digit * multiplier;
                multiplier--;
            }

            remainder = sum % 11;
            int secondDigit = remainder < 2 ? 0 : 11 - remainder;

            // Validate second digit
            if (!int.TryParse(normalizedValue[10].ToString(), out int providedSecondDigit) ||
                providedSecondDigit != secondDigit)
                return false;

            return true;
        }

        public static bool ValidateCnpj(string normalizedValue)
        {
            // Early return for invalid length
            if (normalizedValue?.Length != 14)
                return false;

            // Reject all same characters pattern
            if (normalizedValue[0] == normalizedValue[1])
            {
                var allSame = normalizedValue.All(c => c == normalizedValue[0]);
                if (allSame)
                    return false;
            }

            // Validate that first 12 characters are alphanumeric and last 2 are numeric
            for (int i = 0; i < 12; i++)
            {
                char c = normalizedValue[i];
                if (!char.IsLetterOrDigit(c))
                    return false;
            }

            for (int i = 12; i < 14; i++)
            {
                if (!char.IsDigit(normalizedValue[i]))
                    return false;
            }

            // Calculate first verification digit using weights 2-9 for the first 12 characters
            int sum = 0;
            int[] weights = { 2, 3, 4, 5, 6, 7, 8, 9, 2, 3, 4, 5 };

            for (int i = 0; i < 12; i++)
            {
                int value = ConvertAlphanumericToValue(normalizedValue[i]);
                if (value == -1)
                    return false;

                sum += value * weights[i];
            }

            int remainder = sum % 11;
            int firstDigit = remainder < 2 ? 0 : 11 - remainder;

            // Validate first check digit
            if (!int.TryParse(normalizedValue[12].ToString(), out int providedFirstDigit) ||
                providedFirstDigit != firstDigit)
                return false;

            // Calculate second verification digit: include first check digit in calculation
            sum = 0;
            weights = new[] { 3, 4, 5, 6, 7, 8, 9, 2, 3, 4, 5, 6, 7 };

            // First 12 characters + first check digit
            for (int i = 0; i < 12; i++)
            {
                int value = ConvertAlphanumericToValue(normalizedValue[i]);
                sum += value * weights[i];
            }

            sum += firstDigit * weights[12];
            remainder = sum % 11;
            int secondDigit = remainder < 2 ? 0 : 11 - remainder;

            // Validate second check digit
            if (!int.TryParse(normalizedValue[13].ToString(), out int providedSecondDigit) ||
                providedSecondDigit != secondDigit)
                return false;

            return true;
        }

        private static int ConvertAlphanumericToValue(char c)
        {
            if (char.IsDigit(c))
                return c - '0';

            if (char.IsLetter(c))
            {
                char upper = char.ToUpperInvariant(c);
                if (upper >= 'A' && upper <= 'Z')
                    return (upper - 'A') + 10;
            }

            return -1;
        }

    }
}
