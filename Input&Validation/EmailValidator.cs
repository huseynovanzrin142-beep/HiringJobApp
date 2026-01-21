namespace FinalProject;

using System.Text.RegularExpressions;

public class EmailValidator
{
    public void HandleValidators(string input)
    {
        CheckEmail(input);
    }

    public EmailValidator CheckEmail(string Input)
    {
        if (!Regex.IsMatch(Input, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
            throw new FormatException("The email format is invalid! (Example: user@gmail.com)");

        if (Input.Length <= 12)
            throw new InvalidEmailException("Email must be at least 13 characters long!");
        if (!Input.EndsWith("@gmail.com"))
            throw new InvalidDomainException("Only @gmail.com addresses are accepted!");

        return this;
    }
}
