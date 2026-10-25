using SimplePay.Communication.Enums;

namespace SimplePay.Application.Validators;

internal static class DocumentValidator
{
    public static bool IsValid(string document, UserType type)
    {
        var digitsOnly = new string(document.Where(char.IsDigit).ToArray());

        return type switch
        {
            UserType.Common => IsValidCpf(digitsOnly),
            UserType.Shopkeeper => IsValidCnpj(digitsOnly),
            _ => false
        };
    }

    private static bool IsValidCpf(string cpf)
    {
        if (cpf.Length != 11 || AllDigitsEqual(cpf))
            return false;

        var firstCheckDigit = CalculateCheckDigit(cpf[..9], [10, 9, 8, 7, 6, 5, 4, 3, 2]);
        var secondCheckDigit = CalculateCheckDigit(cpf[..9] + firstCheckDigit, [11, 10, 9, 8, 7, 6, 5, 4, 3, 2]);

        return cpf == cpf[..9] + firstCheckDigit + secondCheckDigit;
    }

    private static bool IsValidCnpj(string cnpj)
    {
        if (cnpj.Length != 14 || AllDigitsEqual(cnpj))
            return false;

        var firstCheckDigit = CalculateCheckDigit(cnpj[..12], [5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2]);
        var secondCheckDigit = CalculateCheckDigit(cnpj[..12] + firstCheckDigit, [6, 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2]);

        return cnpj == cnpj[..12] + firstCheckDigit + secondCheckDigit;
    }

    private static bool AllDigitsEqual(string digits) => digits.Distinct().Count() == 1;

    private static int CalculateCheckDigit(string digits, int[] weights)
    {
        var sum = digits.Select((character, index) => (character - '0') * weights[index]).Sum();
        var remainder = sum % 11;

        return remainder < 2 ? 0 : 11 - remainder;
    }
}