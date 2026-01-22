
namespace FinalProject;

using System.Text.RegularExpressions;

public class AgeValidator
{
    public void HandleValidators(string input)
    {
        if (!Regex.IsMatch(input, @"^\d+$"))
        {
            throw new FormatException("Age must consist of digits only!");
        }
        if (input.Length > 3)
        {
            throw new InvalidAgeException("Age cannot be more than 3 digits!");
        }
        CheckAge(input);
    }

    public AgeValidator CheckAge(string input)
    {
        int age = int.Parse(input);
        if (age < 0 || age > 100)
        {
            throw new InvalidAgeException("Age cannot be less than 0 or greater than 150.");
        }

        return this;
    }
}


