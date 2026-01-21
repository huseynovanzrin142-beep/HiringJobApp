namespace FinalProject;

public class ValidationService
{
    private readonly AgeValidator _AgeValidator;
    private readonly EmailValidator _EmailValidator;
    private readonly FullNameValidator _FullNameValidator;
    private readonly NullOrEmptyValidator _NullOrEmptyValidator;
    private readonly PhoneValidator _PhoneValidator;
    private readonly SelectionValidator _SelectionValidator;
    private readonly int _MaxRange;
    private readonly PasswordValidator _PasswordValidator = new();
    private readonly TextValidator _TextValidator = new();
    private readonly DateValidator _DateValidator = new();

    public ValidationService(AgeValidator ageValidator, EmailValidator emailValidator, FullNameValidator fullNameValidator, NullOrEmptyValidator nullOrEmptyValidator, PhoneValidator phoneValidator, SelectionValidator selectionValidators, int maxRange)
    {
        _AgeValidator = ageValidator;
        _EmailValidator = emailValidator;
        _FullNameValidator = fullNameValidator;
        _NullOrEmptyValidator = nullOrEmptyValidator;
        _PhoneValidator = phoneValidator;
        _MaxRange = maxRange;
        _SelectionValidator = selectionValidators;
    }

    public string? StartCheckDate(string? input)
    {
        try
        {
            _NullOrEmptyValidator.CheckNullOrEmpty(input!);
            _DateValidator.HandleValidators(input!);
            return input;
        }
        catch (ArgumentNullException e) { return HandleError(e.Message); }
        catch (ArgumentOutOfRangeException e) { return HandleError(e.Message); }
        catch (FormatException e) { return HandleError(e.Message); }
    }

    public string? StartCheckText(string? input)
    {
        try
        {
            _NullOrEmptyValidator.CheckNullOrEmpty(input!);
            _TextValidator.HandleValidators(input!);
            return input;
        }
        catch (ArgumentNullException e) { return HandleError(e.Message); }
        catch (InvalidLengthException e) { return HandleError(e.Message); }
    }

    public string? StartCheckPassword(string? input)
    {
        try
        {
            _NullOrEmptyValidator.CheckNullOrEmpty(input!);
            _PasswordValidator.HandleValidators(input!);
            return input;
        }
        catch (ArgumentNullException e) { return HandleError(e.Message); }
        catch (FormatException e) { return HandleError(e.Message); }
    }

    public string? StartCheckEmail(string? input)
    {
        try
        {
            _NullOrEmptyValidator.CheckNullOrEmpty(input!);
            _EmailValidator.HandleValidators(input!);
            return input;
        }
        catch (ArgumentNullException e) { return HandleError(e.Message); }
        catch (InvalidEmailException e) { return HandleError(e.Message); }
        catch (InvalidDomainException e) { return HandleError(e.Message); }
        catch (FormatException e) { return HandleError(e.Message); }
    }

    public string? StartCheckName(string? input)
    {
        try
        {
            _NullOrEmptyValidator.CheckNullOrEmpty(input!);
            _FullNameValidator.HandleValidators(input!);
            return input;
        }
        catch (ArgumentNullException e) { return HandleError(e.Message); }
        catch (InvalidLengthException e) { return HandleError(e.Message); }
        catch (FormatException e) { return HandleError(e.Message); }
    }

    public string? StartCheckAge(string? input)
    {
        try
        {
            _NullOrEmptyValidator.CheckNullOrEmpty(input!);
            _AgeValidator.HandleValidators(input!);
            return input;
        }
        catch (ArgumentNullException e) { return HandleError(e.Message); }
        catch (InvalidAgeException e) { return HandleError(e.Message); }
        catch (FormatException e) { return HandleError(e.Message); }
    }

    public string? StartCheckPhone(string? input)
    {
        try
        {
            _NullOrEmptyValidator.CheckNullOrEmpty(input!);
            _PhoneValidator.HandleValidators(input!);
            return input;
        }
        catch (ArgumentNullException e) { return HandleError(e.Message); }
        catch (InvalidPhoneException e) { return HandleError(e.Message); }
        catch (InvalidLengthException e) { return HandleError(e.Message); }
        catch (FormatException e) { return HandleError(e.Message); }
    }

    public string? StartCheckSelection(string? input)
    {
        try
        {
            _NullOrEmptyValidator.CheckNullOrEmpty(input!);
            _SelectionValidator.HandleValidators(input!);
            return input;
        }
        catch (ArgumentNullException e) { return HandleError(e.Message); }
        catch (ArgumentOutOfRangeException e) { return HandleError(e.Message); }
        catch (FormatException e) { return HandleError(e.Message); }
    }

    public string GetValidInput(string message, InputService inputService, Func<string?, string?> checkMethod)
    {
        while (true)
        {
            Console.WriteLine(message);
            string? input = inputService.GetDataFromUser();
            string? result = checkMethod(input);

            if (result != null) return result;

            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Please try again...");
            Console.ResetColor();
        }
    }

    private string? HandleError(string message)
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine(message);
        Console.ResetColor();
        return null;
    }
}
