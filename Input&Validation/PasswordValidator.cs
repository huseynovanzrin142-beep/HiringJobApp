namespace FinalProject;

using System.Text.RegularExpressions;

public class PasswordValidator
{
    public void HandleValidators(string input)
    {
        if (string.IsNullOrWhiteSpace(input) || input.Length < 8)
            throw new FormatException("Password must be at least 8 characters long!");
        if (!Regex.IsMatch(input, @"\d"))
            throw new FormatException("Password must contain at least one digit!");

        CheckPassword(input);
    }

    public PasswordValidator CheckPassword(string input)
    {
        return this;
    }
}
