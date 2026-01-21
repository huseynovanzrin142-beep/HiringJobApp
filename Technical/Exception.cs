namespace FinalProject;

public class InvalidAgeException : Exception
{
    public InvalidAgeException(string msg) : base(msg) { }
}
public class InvalidPhoneException : Exception
{
    public InvalidPhoneException(string msg) : base(msg) { }
}

public class InvalidEmailException : Exception
{
    public InvalidEmailException(string msg) : base(msg) { }
}

public class InvalidDomainException : Exception
{
    public InvalidDomainException(string msg) : base(msg) { }
}

    public class InvalidLengthException : Exception
{
    public InvalidLengthException(string msg) : base(msg) { }
}