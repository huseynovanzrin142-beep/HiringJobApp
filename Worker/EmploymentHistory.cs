
using System.Data;

namespace FinalProject;

public class EmploymentHistory
{
    public string StartTime { get; set; } = string.Empty;
    public string EndTime { get; set; } = string.Empty;
    public override string ToString()
    {
        return $"{StartTime} - {EndTime}";
    }
    public void SetEmploymentHistory(ValidationService validationService, InputService inputService)
    {
        Console.ForegroundColor = ConsoleColor.DarkCyan;
        StartTime = validationService.GetValidInput("Enter Start Time: ", inputService, validationService.StartCheckDate);

        EndTime = validationService.GetValidInput("Enter Academic End Time: ", inputService, validationService.StartCheckDate);
        Console.ResetColor();
    }

}
