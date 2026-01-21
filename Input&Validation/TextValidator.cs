namespace FinalProject;

public class TextValidator
{
    public void HandleValidators(string input)
    {
        CheckText(input);
    }

    public TextValidator CheckText(string input)
    {
        if (string.IsNullOrWhiteSpace(input))
            throw new ArgumentNullException(null, "Text field cannot be empty!");

        if (input.Trim().Length < 10)
            throw new InvalidLengthException("Description is too short! Please provide at least 10 characters.");

        if (input.Length > 500)
            throw new InvalidLengthException("Description is too long! Maximum 500 characters allowed.");

        return this;
    }
}
