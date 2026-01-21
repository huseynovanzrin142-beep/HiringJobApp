namespace FinalProject;

public class NullOrEmptyValidator
{
    public NullOrEmptyValidator CheckNullOrEmpty(string input)
    {
        if (string.IsNullOrWhiteSpace(input))
            throw new ArgumentNullException(null, "Input cannot be null or empty!");
        return this;
    }
}
