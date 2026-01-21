
using System.Diagnostics.CodeAnalysis;
using System.Runtime.ConstrainedExecution;

namespace FinalProject;

public class ForeignLanguage
{
    public string? Name { get; set; } = string.Empty;
    public CEFR_Level? CEFR_ { get; set; }

    public string Proficiency => CEFR_ switch
    {
        CEFR_Level.A1 => "Beginner",
        CEFR_Level.A2 => "Elementary",
        CEFR_Level.B1 => "Intermediate",
        CEFR_Level.B2 => "Upper Intermediate",
        CEFR_Level.C1 => "Advanced",
        CEFR_Level.C2 => "Proficient",
        _ => "Unknown"
    };

    public string DisplayLevel => $"{CEFR_} - {Proficiency}";

    public override bool Equals(object? obj)
    {
        if (obj is ForeignLanguage fl)
        {

            return string.Equals(fl.Name, this.Name, StringComparison.OrdinalIgnoreCase) &&
                   fl.CEFR_ == this.CEFR_;
        }
        return false;
    }

    public override int GetHashCode()
    {

        return HashCode.Combine(Name?.ToLower().Trim(), CEFR_);
    }

    public override string ToString()
    {
        return $"Language: {Name}, Level: {DisplayLevel}";
    }
    public void SetForeignLanguage(ValidationService validationService, InputService inputService)
    {

        Name = validationService.GetValidInput("Enter Foreign Language Name: ", inputService, validationService.StartCheckName);

        Console.WriteLine("Enter CEFR level: ");
        Console.WriteLine("1.A1, 2.A2, 3.B1, 4.B2, 5.C1, 6.C2");


        string input = validationService.GetValidInput("Seçiminizi edin: ", inputService, validationService.StartCheckSelection);

        if (int.TryParse(input, out int choice) && choice >= 1 && choice <= 6)
        {
            CEFR_ = (CEFR_Level)(choice - 1);
        }
        else
        {
            Console.WriteLine("Invalid selection. Defaulting to Unknown.");
        }
    }


}

public struct ProgrammingLanguage
{
    public string? Name { get; set; } = String.Empty;
    public Technical_Level Technical_ { get; set; }
    public ProgrammingLanguage() { }
    public override string ToString()
    {
        return $@"Programming Language Name: {Name}
                  Technical Level: {Technical_}";
    }
    public override bool Equals(object? obj)
    {
        if (obj is ProgrammingLanguage pl)
        {
            return string.Equals(pl.Name, this.Name, StringComparison.OrdinalIgnoreCase);
        }
        return false;
        ;
    }
    public override int GetHashCode()
    {

        return HashCode.Combine(Name?.ToLower().Trim(), Technical_);
    }
    public void SetProgrammingLanguage(ValidationService validationService, InputService inputService)
    {
        Name = validationService.GetValidInput("Enter Programming Language Name: ", inputService, validationService.StartCheckName);

        Console.WriteLine("Enter Technical level: ");
        Console.WriteLine("1.Junior, 2.Middle, 3.Senior");

        string input = validationService.GetValidInput("Seçiminizi edin: ", inputService, validationService.StartCheckSelection);

        if (int.TryParse(input, out int choice) && choice >= 1 && choice <= 3)
        {
            Technical_ = (Technical_Level)(choice - 1);
        }
        else
        {
            Console.WriteLine("Invalid selection.");
        }
    }

}