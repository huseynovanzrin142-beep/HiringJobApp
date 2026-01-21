
using FinalProject.Technical;

namespace FinalProject;

public class Worker : Person
{
    public List<Cv> Cv { get; set; } = new List<Cv>();
    public Worker() : base() { }

    public Worker(string name, string surname, string city, string phone, int age, string email, string password)
        : base(name, surname, city, phone, age)
    {
        this.Email = email;
        this.Password = password;
    }
    public void HandleAddCv(ValidationService validationService, InputService inputService)
    {
        Cv cv = new Cv();
        cv.SetCv(validationService, inputService, this);
        AddNewCv(cv);
    }

    public void AddNewCv(Cv cv)
    {
        Cv.Add(cv);
    }

    public void ShowCv()
    {
        if (Cv.Count == 0)
        {
            Console.WriteLine("No CVs found in your profile.");
            return;
        }

        foreach (var anyCv in Cv)
        {

            anyCv.ShowOneCv();
        }
    }

    public void BrowseVacancies(JsonHandler employerJson, List<Employer> employers)
    {
        employers = employerJson.GetJsonDeserializerList<Employer>() ?? new();

        if (employers.Count > 0)
        {
            Console.WriteLine("\n--- Available Vacancies ---");
            int globalIndex = 1;

            foreach (var emp in employers)
            {
                foreach (var vac in emp.Vacancies)
                {
                    Console.WriteLine($"{globalIndex++}. {vac.ToString()}");
                }
            }
        }
        else
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("No employers or vacancies found in the system.");
            Console.ResetColor();
        }
    }
    public void SendCv(string vInput, string cInput, JsonHandler empJson, List<Employer> employers)
    {
        List<Employer> allEmployers = employers;

        if (!int.TryParse(vInput, out int targetVacIdx) ||
            !int.TryParse(cInput, out int targetCvIdx))
        {
            Console.WriteLine("Selection error: Please enter valid numbers.");
            return;
        }

        if (targetCvIdx < 1 || targetCvIdx > this.Cv.Count)
        {
            Console.WriteLine("CV not found in your list.");
            return;
        }

        var selectedCv = this.Cv[targetCvIdx - 1];

        int counter = 1;
        foreach (var emp in allEmployers)
        {
            foreach (var vac in emp.Vacancies)
            {
                if (counter++ == targetVacIdx)
                {
                    if (emp.IncomingCvs == null)
                        emp.IncomingCvs = new List<Guid>();

                    emp.IncomingCvs.Add(selectedCv.Id);

                    empJson.GetJsonSerializerList(allEmployers);

                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("\nApplication submitted successfully!");
                    Console.ResetColor();
                    return;
                }
            }
        }

        Console.WriteLine("Target vacancy not found.");
    }

}