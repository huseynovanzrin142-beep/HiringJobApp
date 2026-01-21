
using System.Numerics;
using System.Xml.Linq;

namespace FinalProject;

public class Vacancy
{
    public int Id { get; set; }
    public string? CompanyName { get; set; } = String.Empty;
    public string? AboutUs { get; set; } = String.Empty;
    public string? Position { get; set; } = String.Empty;
    public string? Description { get; set; } = String.Empty;
    public string? Responsibilities { get; set; } = String.Empty;
    public string? Location { get; set; } = String.Empty;
    public Requirement? Requirement { get; set; } = new Requirement();
    public double? Salary { get; set; }
    public string Deadline { get; set; } = string.Empty;
    public override string ToString()
    {
        return $@"Vacancy ID: {Id}
Company Name: {CompanyName}
About Us: {AboutUs}
Position: {Position}
Description: {Description}
Responsibilities: {Responsibilities}
Location: {Location}
Requirement: {Requirement.ToString()}
Salary: {Salary}
DeadLine: {Deadline}";
   
    }
    public void SetVacancy(ValidationService validationService, InputService inputService)
    {
        CompanyName = validationService.GetValidInput("Enter Company Name:", inputService, validationService.StartCheckName);

        AboutUs = validationService.GetValidInput("Enter About Us (min 10 chars):", inputService, validationService.StartCheckText);

        Position = validationService.GetValidInput("Enter Position:", inputService, validationService.StartCheckName);

        Description = validationService.GetValidInput("Enter Description (min 10 chars):", inputService, validationService.StartCheckText);

        Responsibilities = validationService.GetValidInput("Enter Responsibilities (min 10 chars):", inputService, validationService.StartCheckText);

        Location = validationService.GetValidInput("Enter Location (e.g. Baku, Azerbaijan):", inputService, validationService.StartCheckText);

        Console.WriteLine("\n--- Adding Requirements ---");
        Requirement!.SetRequirements(validationService, inputService);

        string salaryInput = validationService.GetValidInput("\nEnter Salary (0-700):", inputService, validationService.StartCheckSelection);
        Salary = double.Parse(salaryInput);


        Deadline = validationService.GetValidInput("Enter Deadline (dd.MM.yyyy):", inputService, validationService.StartCheckDate);
    }


}
