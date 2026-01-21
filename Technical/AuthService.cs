namespace FinalProject.Technical;

public class AuthService
{

    public void LogIn<T>(JsonHandler jsonHandler, T anyPerson)
    {
        if (IsUserRegistered<T>(jsonHandler, anyPerson) == true)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"Welcome {anyPerson.ToString()}!!!");
            Console.ResetColor();
        }
        else
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("User not found. Please ensure your credentials are correct.");
            Console.ResetColor();
        }
    }
    public bool IsUserRegistered<T>(JsonHandler jsonHandler, T anyPerson)
    {
        var people = jsonHandler.GetJsonDeserializerElement<List<T>>() ?? new List<T>();
        return people.Any(person => person != null && person.Equals(anyPerson));
    }

    public void SignUp<T>(JsonHandler jsonHandler, List<T> people, T anyPerson)
    {
        if (IsUserRegistered<T>(jsonHandler, anyPerson) == false)
        {
            people.Add(anyPerson);
            jsonHandler.GetJsonSerializerList<T>(people);

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Registration completed successfully!");
            Console.ResetColor();
        }
        else
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("This user already exists. Please try another username or log in.");
            Console.ResetColor();
        }
    }
  
}