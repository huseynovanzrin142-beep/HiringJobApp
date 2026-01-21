namespace FinalProject;

using System.Text.RegularExpressions;

public class PhoneValidator
{
    public void HandleValidators(string input)
    {
        if (Regex.IsMatch(input, @"\d"))
        {
            CheckPhone(input);
        }
    }

    public PhoneValidator CheckPhone(string Input)
    {
        Input = Input.Replace(" ", "");
        if (string.IsNullOrWhiteSpace(Input) || Input.Length < 13)
            throw new InvalidLengthException("Telefon nömrəsi (+994)XXXXXXXXX formatında olmalıdır!");

        if (!Input.StartsWith("(+994)"))
            throw new InvalidPhoneException("Telefon nömrəsi (+994) ilə başlamalıdır!");

        string remainingPart = Input.Substring(6);

        if (!Regex.IsMatch(remainingPart, @"^\d+$"))
            throw new FormatException("Telefon nömrəsinin (+994)-dən sonrakı hissəsi yalnız rəqəmlərdən ibarət olmalıdır!");

        return this;
    }

}

