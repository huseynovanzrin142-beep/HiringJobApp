using FinalProject;

public class Requirement
{
    public Dictionary<int, string> Requirements { get; set; } = new Dictionary<int, string>();
    public int KeyCounter { get; set; } = 0; 

    public void AddNewRequirements(string requirement)
    {
        KeyCounter++; 
        Requirements.Add(KeyCounter, requirement);
    }

    public void SetRequirements(ValidationService validationService, InputService inputService)
    {
        string choice;
        do
        {
            string reqText = validationService.GetValidInput(
                $"Enter Requirement #{KeyCounter + 1}:",
                inputService,
                validationService.StartCheckText
            );

            AddNewRequirements(reqText);

            Console.WriteLine("Add another requirement? (y/n)");
            choice = inputService.GetDataFromUser()?.ToLower() ?? "n";
        } while (choice == "y");
    }


}