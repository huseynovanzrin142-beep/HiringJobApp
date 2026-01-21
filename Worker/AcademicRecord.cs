
using System.Numerics;
using System.Xml.Linq;

namespace FinalProject;

public class AcademicRecord
{
    public string? HighSchoolName { get; set; }
    public double HighSchoolGpa { get; set; }
    public string? UniversityName { get; set; }
    public double UniversityEntranceScore { get; set; }
    public double UniversityGpa { get; set; }
    public override string ToString()
    {
        return $@"High School Name: {HighSchoolName}
                  High School GPA:  {HighSchoolGpa}
                  University Entrance Score: {UniversityEntranceScore}
                  University Name: {UniversityName}
                  University GPA: {UniversityGpa}";
    }
    public void SetAcademicRecord(ValidationService validationService, InputService inputService)
    {
        Console.ForegroundColor = ConsoleColor.DarkCyan;
        HighSchoolName = validationService.GetValidInput("1.Enter High School Name:", inputService, validationService.StartCheckName);

        string hsGpaStr = validationService.GetValidInput("2.Enter High School GPA (0-100):", inputService, validationService.StartCheckSelection);
        HighSchoolGpa = double.Parse(hsGpaStr);

        UniversityName = validationService.GetValidInput("3.Enter University Name:", inputService, validationService.StartCheckName);

        string entranceScoreStr = validationService.GetValidInput("4.Enter University Entrance Score (0-700):", inputService, validationService.StartCheckSelection);
        UniversityEntranceScore = double.Parse(entranceScoreStr);

        string uniGpaStr = validationService.GetValidInput("5.Enter University GPA (0-100):", inputService, validationService.StartCheckSelection);
        UniversityGpa = double.Parse(uniGpaStr);
        Console.ResetColor();
    }


}
