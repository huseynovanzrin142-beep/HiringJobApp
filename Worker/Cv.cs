namespace FinalProject;
public class Cv
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string? MajorName { get; set; }
    public string? WorkerFullName { get; set; }
    public string? WorkerEmail { get; set; }
    public int VacancyId { get; set; }         
    public string? EmployerEmail { get; set; }
    public AcademicRecord AcademicRecord { get; set; } = new AcademicRecord();
    public List<WorkExperience> WorkExperiences { get; set; } = new List<WorkExperience>();
    public Skill Skills { get; set; } = new Skill();

    public bool IsExperienceDuplicate(WorkExperience experience)
    {
        foreach (var anyExperience in WorkExperiences)
            if (anyExperience.Equals(experience) == true) return true;
        return false;
    }

    public void AddNewExperience(WorkExperience experience)
    {
        if (IsExperienceDuplicate(experience) == true)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"Duplicate entry detected: This experience is already present in your list.");
            Console.ResetColor();
        }
        else
        {
            Console.ForegroundColor = ConsoleColor.Green;
            WorkExperiences.Add(experience);
            Console.WriteLine($"Experience successfully added to your CV.");
            Console.ResetColor();
        }
    }

    public void ShowExperiences()
    {
        foreach (var experience in WorkExperiences)
        {
            Console.WriteLine(experience.ToString());
        }
    }

    public void HandleAddExperience(ValidationService validationService, InputService inputService)
    {
        WorkExperience experience = new WorkExperience();
        experience.SetExperience(validationService, inputService);
        AddNewExperience(experience);
    }

    public void SetCv(ValidationService validationService, InputService inputService, Worker worker)
    {
        Console.ForegroundColor = ConsoleColor.DarkCyan;

        MajorName = validationService.GetValidInput(
            "\nEnter Major Name: ",
            inputService,
            validationService.StartCheckName
        );
        WorkerFullName = validationService.GetValidInput(
            "\nEnter Major Name: ",
            inputService,
            validationService.StartCheckName
        );

        Console.WriteLine("\nEnter Academic Record: ");
        AcademicRecord.SetAcademicRecord(validationService, inputService);

        Console.WriteLine("\nEnter Work Experience:");
        HandleAddExperience(validationService, inputService);

        Console.WriteLine("\nEnter Soft Skill:");
        Skills.HandleAddSoftSkill(validationService, inputService);

        Console.WriteLine("\nEnter Foreign Language:");
        Skills.HandleAddLanguage(validationService, inputService);

        Console.WriteLine("\nEnter Programming Language");
        Skills.HandleAddProgrammingLanguage(validationService, inputService);

        Console.ResetColor();
    }

    public void ShowOneCv()
    {
        Console.ForegroundColor = ConsoleColor.Magenta;

        Console.WriteLine($"Major: {MajorName}");
        Console.WriteLine("Academic Record:");
        Console.WriteLine(AcademicRecord.ToString());

        Console.WriteLine("Work Experiences:");
        ShowExperiences();

        Console.WriteLine("Skills:");
        Skills.ShowSoftSkill();
        Skills.ShowLanguage();
        Skills.ShowProgrammingLanguage();

        Console.ResetColor();
    }

    public override string ToString()
    {
        return $"Id: {Id} | Major: {MajorName} | Candidate: {WorkerFullName}";
    }
}
