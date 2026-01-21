namespace FinalProject.Technical;

public class Menu
{
    public void RoleSelectionMenu()
    {
        Console.ForegroundColor = ConsoleColor.Blue;
        Console.WriteLine(@"1. Login as Worker
2. Login as Employer
3. Exit");
        Console.ResetColor();
    }
    public void AuthenticationMenu()
    {
        Console.ForegroundColor = ConsoleColor.Blue;
        Console.WriteLine(@"1. Log In
2. Sign Up
3. Back");
        Console.ResetColor();
    }
    public void WorkerDashboard()
    {
        Console.ForegroundColor = ConsoleColor.Blue;
        Console.WriteLine(@"1. View My CV
2. Create/Add CV
3. Browse Vacancies
4. Back");
        Console.ResetColor();
    }
    public void ActivePostingsBoard()
    {
        Console.ForegroundColor = ConsoleColor.Blue;
        Console.WriteLine(@"1. Apply for Vacancy
2. Back");
        Console.ResetColor();
    }
    public void EmployerDashboard()
    {
        Console.ForegroundColor = ConsoleColor.Blue;
        Console.WriteLine(@"1. View My Vacancies
2. Post a New Vacancy
3. View Incoming Applications (CVs)
4. Back");
        Console.ResetColor();
    }
    public void ApplicationReviewBoard()
    {
        Console.ForegroundColor = ConsoleColor.Blue;
        Console.WriteLine(@"1. Accept Candidate
2. Reject Candidate
3. Back");
        Console.ResetColor();
    }


}












