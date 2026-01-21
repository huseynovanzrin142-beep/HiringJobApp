using FinalProject.Technical;
namespace FinalProject;

class Program
{
    static void Main(string[] args)
    {
        AgeValidator ageValidator = new AgeValidator();
        EmailValidator emailValidator = new EmailValidator();
        FullNameValidator fullNameValidator = new FullNameValidator();
        InputService inputService = new InputService(); 
        NullOrEmptyValidator   nullOrNullOrEmptyValidator = new NullOrEmptyValidator();
        PhoneValidator phoneValidator = new PhoneValidator();
        SelectionValidator selectionValidator   = new SelectionValidator(4);
        int maxRange = 4;
        ValidationService validator = new ValidationService(ageValidator, emailValidator, fullNameValidator, nullOrNullOrEmptyValidator, phoneValidator, selectionValidator, maxRange);
        InputService input = new InputService();
        AuthService authService = new AuthService();

        JsonHandler workerJson = new JsonHandler { FilePath = "workers.json" };
        JsonHandler employerJson = new JsonHandler { FilePath = "employers.json" };


        AppEngine engine = new AppEngine(
            validator,
            workerJson,
            employerJson,
            authService,
            input
        );


    engine.Run();
    }
}
