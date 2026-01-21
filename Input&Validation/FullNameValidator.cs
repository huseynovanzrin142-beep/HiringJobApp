namespace FinalProject;

using System.Text.RegularExpressions;

public class FullNameValidator
{
    public void HandleValidators(string input)
    {
        CheckFullName(input);
    }

    public FullNameValidator CheckFullName(string Input)
    {
        if (Input!.Length < 2)
            throw new InvalidLengthException("Name or Surname must be at least 2 characters long!");
        if (!Regex.IsMatch(Input, @"^[a-zA-ZİçöüğıəÇÖÜĞİƏ\s]+$"))
            throw new FormatException("Name or Surname must contain only letters!");

        return this;
    }
}
