using FinalProject.Technical;

namespace FinalProject;

public class Employer : Person
{
    public List<Vacancy> Vacancies { get; set; } = new List<Vacancy>();
    public List<Guid> IncomingCvs { get; set; } = new List<Guid>();

    public List<Notification> Notifications { get; set; } = new List<Notification>();

    public Employer() : base() { }
    public Employer(string name, string surname, string city, string phone, int age, string email, string password)
        : base(name, surname, city, phone, age)
    {
        this.Email = email;
        this.Password = password;
    }

    public void HandleAddVacancy(ValidationService vs, InputService isrv, JsonHandler employerJson, Employer employer)
    {
        Vacancy newVacancy = new Vacancy();
        newVacancy.SetVacancy(vs, isrv);
        newVacancy.Id = Vacancies.Count == 0 ? 1 : Vacancies.Max(v => v.Id) + 1;
        AddNewVacancy(newVacancy);

        List<Employer> allEmployers = employerJson.GetJsonDeserializerList<Employer>() ?? new();

        var currentMe = allEmployers.FirstOrDefault(e => e.Email == this.Email);

        if (currentMe != null)
        {
            currentMe.Vacancies = this.Vacancies;
        }
        else
        {
            allEmployers.Add(this);
        }

        employerJson.GetJsonSerializerList(allEmployers);
    }


    public void AddNewVacancy(Vacancy vacancy)
    {
        Vacancies.Add(vacancy);
    }

    public void AcceptCandidate(Guid cvId, JsonHandler workerJson, JsonHandler employerJson)
    {
        List<Worker> allWorkers = workerJson.GetJsonDeserializerList<Worker>() ?? new();
        var targetWorker = allWorkers.FirstOrDefault(w => w.Cv.Any(cv => cv.Id == cvId));

        if (targetWorker != null)
        {
            targetWorker.Notifications ??= new List<Notification>();
            targetWorker.Notifications.Add(new Notification("Congratulations! Your CV has been accepted.", targetWorker));
            workerJson.GetJsonSerializerList(allWorkers);
        }

        List<Employer> allEmployers = employerJson.GetJsonDeserializerList<Employer>() ?? new();
        var currentEmployer = allEmployers.FirstOrDefault(e => e.Email == this.Email);

        if (currentEmployer != null && currentEmployer.IncomingCvs != null)
        {
            currentEmployer.IncomingCvs.Remove(cvId);
            employerJson.GetJsonSerializerList(allEmployers);
        }

        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine("Candidate accepted successfully.");
        Console.ResetColor();
    }


    public void RejectCandidate(Guid cvId, JsonHandler workerJson, JsonHandler employerJson)
    {
        List<Worker> allWorkers = workerJson.GetJsonDeserializerList<Worker>() ?? new();
        var targetWorker = allWorkers.FirstOrDefault(w => w.Cv.Any(cv => cv.Id == cvId));

        if (targetWorker != null)
        {
            targetWorker.Notifications ??= new List<Notification>();
            targetWorker.Notifications.Add(new Notification("We regret to inform you that your CV was rejected.", targetWorker));
            workerJson.GetJsonSerializerList(allWorkers);
        }

        List<Employer> allEmployers = employerJson.GetJsonDeserializerList<Employer>() ?? new();
        var currentEmployer = allEmployers.FirstOrDefault(e => e.Email == this.Email);

        if (currentEmployer != null && currentEmployer.IncomingCvs != null)
        {
            currentEmployer.IncomingCvs.Remove(cvId);
            employerJson.GetJsonSerializerList(allEmployers);
        }

        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine("Candidate rejected.");
        Console.ResetColor();
    }


    public void UpdateEmployerData(JsonHandler employerJson)
    {
        List<Employer> allEmps = employerJson.GetJsonDeserializerList<Employer>() ?? new();

        var me = allEmps.FirstOrDefault(e => e.Email == this.Email);
        if (me != null)
        {
            me.IncomingCvs = this.IncomingCvs;
            employerJson.GetJsonSerializerList(allEmps);
        }
    }


    public void ViewMyVacancy()
    {
        if (Vacancies.Count == 0)
        {
            Console.WriteLine("You have no active vacancies.");
            return;
        }
        foreach (var vacancy in Vacancies)
        {
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine(vacancy.ToString());
            Console.ResetColor();
        }
    }

    public void ViewIncomingCvs(JsonHandler employerJson, List<Worker> allWorkers)
    {
        List<Employer> allEmployers = employerJson.GetJsonDeserializerList<Employer>() ?? new();

        var currentMe = allEmployers.FirstOrDefault(e => e.Email == this.Email);

        if (currentMe == null)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Employer not found in JSON.");
            Console.ResetColor();
            return;
        }

        if (currentMe.IncomingCvs == null || currentMe.IncomingCvs.Count == 0)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("No incoming CVs found.");
            Console.ResetColor();
            return;
        }

        foreach (var cvId in currentMe.IncomingCvs)
        {
            Cv foundCv = allWorkers
                .SelectMany(w => w.Cv)
                .FirstOrDefault(cv => cv.Id == cvId);

            if (foundCv != null)
            {
                Console.ForegroundColor = ConsoleColor.Magenta;
                Console.WriteLine($"=== Incoming CV ===");
                Console.WriteLine(foundCv.ToString());
                Console.ResetColor();
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"CV not found for ID: {cvId}");
                Console.ResetColor();
            }
        }
    }



}
