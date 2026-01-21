

namespace FinalProject
{
    public class WorkExperience
    {
        public string? Role { get; set; }
        public string? JobTitle { get; set; }
        public string? Description { get; set; }    
        public EmploymentHistory? EmploymentHistory { get; set; } = new EmploymentHistory();
        public string CompanyName { get; set; }
        public override string ToString()
        {
            return $@"{CompanyName} 
                      {JobTitle} : {Role} - {EmploymentHistory} :
                      {Description}";
        }
        public void SetExperience(ValidationService validationService, InputService inputService)
        {
            Console.WriteLine("\n--- Add Work Experience ---");


            CompanyName = validationService.GetValidInput("Enter Company Name:", inputService, validationService.StartCheckName);


            Role = validationService.GetValidInput("Enter Your Role (e.g. Senior Developer):", inputService, validationService.StartCheckName);


            JobTitle = validationService.GetValidInput("Enter Job Title:", inputService, validationService.StartCheckName);


            Description = validationService.GetValidInput("Enter Brief Job Description:", inputService, validationService.StartCheckName);


            Console.WriteLine("Enter Employment Period Details:");
            EmploymentHistory.SetEmploymentHistory(validationService, inputService);
        }

    }
}
