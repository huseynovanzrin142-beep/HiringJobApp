namespace FinalProject;

public class Skill : ISkillService
{
    public List<SoftSkill> SoftSkills { get; set; } = new List<SoftSkill>();
    public List<ForeignLanguage> Languages { get; set; } = new List<ForeignLanguage>();
    public List<ProgrammingLanguage>? ProgrammingLanguages { get; set; } = new List<ProgrammingLanguage>();

    public bool IsSkillDuplicate<T>(IEnumerable<T> anyCollection, T skill)
    {
        foreach (var anySkill in anyCollection)
            if (anySkill.Equals(skill) == true) return true;
        return false;
    }

    public void HandleAddSoftSkill(ValidationService vs, InputService isrv)
    {
        SoftSkill newSkill = new SoftSkill();
        newSkill.SetSoftSkill(vs, isrv);
        AddNewSkill(newSkill, SoftSkills);
    }

    public void HandleAddLanguage(ValidationService vs, InputService isrv)
    {
        ForeignLanguage fl = new ForeignLanguage();
        fl.SetForeignLanguage(vs, isrv);
        AddNewSkill(fl, Languages);
    }

    public void HandleAddProgrammingLanguage(ValidationService vs, InputService isrv)
    {
        ProgrammingLanguage pl = new ProgrammingLanguage();
        pl.SetProgrammingLanguage(vs, isrv);
        AddNewSkill(pl, ProgrammingLanguages!);
    }

    public void ShowSkills<T>(IEnumerable<T> skills, string title)
    {
        Console.WriteLine($"\n--- {title} ---");
        if (skills == null || !skills.Any())
        {
            Console.WriteLine("No skills added yet.");
            return;
        }
        foreach (var skill in skills)
        {
            Console.WriteLine(skill?.ToString());
        }
    }

    public void AddNewSkill<T>(T skill, ICollection<T> skills)
    {
        if (IsSkillDuplicate<T>(skills, skill) == true)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"Duplicate entry detected: This skill is already present in your list.");
            Console.ResetColor();
        }
        else
        {
            Console.ForegroundColor = ConsoleColor.Green;
            skills.Add(skill);
            Console.WriteLine($"Skill successfully added to your CV.");
            Console.ResetColor();
        }
    }

    public void ShowSoftSkill()
    {
        foreach (var skill in SoftSkills)
        {
            Console.WriteLine($"Skill : {skill.ToString()}");
        }
    }

    public void ShowLanguage()
    {
        foreach (var language in Languages)
        {
            Console.WriteLine($"Skill : {language.ToString()}");
        }
    }

    public void ShowProgrammingLanguage()
    {
        if (ProgrammingLanguages != null)
        {
            foreach (var pl in ProgrammingLanguages)
            {
                Console.WriteLine($"Skill : {pl.ToString()}");
            }
        }
    }
}

public interface ISkillService
{
    bool IsSkillDuplicate<T>(IEnumerable<T> anyCollection, T skill);
    void AddNewSkill<T>(T skill, ICollection<T> skills);
}

public class SoftSkill
{
    public string? Name { get; set; } = string.Empty;

    public override bool Equals(object? obj)
    {
        if (obj is SoftSkill ss)
        {
            return string.Equals(ss.Name, this.Name, StringComparison.OrdinalIgnoreCase);
        }
        else return false;
    }

    public override string ToString()
    {
        return $"Name: {Name}";
    }

    public void SetSoftSkill(ValidationService validationService, InputService inputService)
    {
        Name = validationService.GetValidInput(
            "Enter Soft Skill Name: ",
            inputService,
            validationService.StartCheckName
        );
    }
}
