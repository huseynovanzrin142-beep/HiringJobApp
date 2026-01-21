using System.Globalization;

public class DateValidator
{
    public void HandleValidators(string input)
    {
        CheckDate(input);
    }

    public DateValidator CheckDate(string input)
    {
        string[] formats = { "dd.MM.yyyy", "dd/MM/yyyy", "yyyy-MM-dd" };

        bool isValid = DateTime.TryParseExact(input, formats,
            CultureInfo.InvariantCulture,
            DateTimeStyles.None,
            out DateTime parsedDate);

        if (!isValid)
            throw new FormatException("Invalid date format! Please use dd.MM.yyyy (e.g., 21.01.2026).");

        return this;
    }

    public DateValidator CheckStartDate(string input)
    {
        CheckDate(input);

        DateTime parsedDate = DateTime.ParseExact(input, "dd.MM.yyyy", CultureInfo.InvariantCulture);

        if (parsedDate.Date < DateTime.Now.Date)
            throw new ArgumentOutOfRangeException(null, "The start date cannot be in the past! Please select a future date.");

        return this;
    }
}
