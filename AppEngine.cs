using FinalProject.Technical;
using Serilog;

namespace FinalProject;

public class AppEngine
{
    private readonly ValidationService _validator;
    private readonly InputService _input;
    private readonly JsonHandler _employerJson;
    private readonly JsonHandler _workerJson;
    public readonly AuthService _authService;

    public List<Worker> Workers = new();
    public List<Employer> Employers = new();

    private Person? _currentUser;
    private Menu Menu { get; set; } = new Menu();

    public AppEngine(ValidationService validator, JsonHandler workerJson, JsonHandler employerJson, AuthService authService, InputService input)
    {
        _validator = validator;
        _workerJson = workerJson;
        _employerJson = employerJson;
        _authService = authService;
        _input = input;

        RefreshData();
        Log.Information("AppEngine initialized successfully.");
    }

    private void RefreshData()
    {
        try
        {
            Workers = _workerJson.GetJsonDeserializerList<Worker>() ?? new List<Worker>();
            Employers = _employerJson.GetJsonDeserializerList<Employer>() ?? new List<Employer>();
            Log.Debug("Data refreshed from JSON files.");
        }
        catch (Exception ex)
        {
            Log.Error(ex, "Critical error during data refresh.");
        }
    }

    public void Run()
    {
        Log.Information("Application Run cycle started.");
        while (true)
        {
            NavigateByRole();
        }
    }

    public string GetRole()
    {
        Menu.RoleSelectionMenu();
        return _validator.GetValidInput("Please select your role:", _input, _validator.StartCheckSelection);
    }

    public string GetAuthChoice()
    {
        Menu.AuthenticationMenu();
        return _validator.GetValidInput("Choose an option:", _input, _validator.StartCheckSelection);
    }

    public void NavigateByRole()
    {
        string role = GetRole();
        if (role == "3")
        {
            Log.Information("Application exited by user.");
            Environment.Exit(0);
        }

        string authChoice = GetAuthChoice();
        if (authChoice == "3") return;

        if (authChoice == "1")
        {
            _currentUser = LoginProcess(role);
            if (_currentUser == null) return;

            UserPanel(role);
            _currentUser = null;
            Log.Information("User logged out.");
        }
        else if (authChoice == "2")
        {
            SignUpProcess(role);
        }
    }

    private Person? LoginProcess(string role)
    {
        RefreshData();
        string email = _validator.GetValidInput("Enter Email:", _input, _validator.StartCheckEmail);
        string password = _validator.GetValidInput("Enter Password:", _input, _validator.StartCheckPassword);

        Log.Information("Login attempt for email: {Email}", email);

        if (role == "1")
        {
            Worker loginWorker = new Worker { Email = email, Password = password };
            if (_authService.IsUserRegistered(_workerJson, loginWorker))
            {
                RefreshData();
                var realWorker = Workers.FirstOrDefault(w => w.Email.Equals(email, StringComparison.OrdinalIgnoreCase));

                if (realWorker != null)
                {
                    _authService.LogIn(_workerJson, realWorker);
                    return realWorker;
                }
            }
        }

        if (role == "2")
        {
            Employer loginEmployer = new Employer { Email = email, Password = password };
            if (_authService.IsUserRegistered(_employerJson, loginEmployer))
            {
                RefreshData();
                var listToSearch = Employers ?? new List<Employer>();

                var realEmployer = listToSearch.FirstOrDefault(e => e != null && e.Email != null &&
                                   e.Email.Equals(email, StringComparison.OrdinalIgnoreCase));

                if (realEmployer != null)
                {
                    _authService.LogIn(_employerJson, realEmployer);
                    return realEmployer;
                }
            }
        }


        Log.Warning("Failed login attempt for email: {Email}", email);
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine("Authentication failed: User not found!");
        Console.ResetColor();
        return null;
    }

    private void SignUpProcess(string role)
    {
        if (role == "1")
        {
            Worker newWorker = new Worker();
            newWorker.SetData(_validator, _input);
            _authService.SignUp(_workerJson, Workers, newWorker);
            Log.Information("New Worker registered: {Email}", newWorker.Email);
        }
        else
        {
            Employer newEmployer = new Employer();
            newEmployer.SetData(_validator, _input);
            _authService.SignUp(_employerJson, Employers, newEmployer);
            Log.Information("New Employer registered: {Email}", newEmployer.Email);
        }
        RefreshData();
    }

    private void UserPanel(string role)
    {
        while (true)
        {
            if (role == "1")
            {
                Menu.WorkerDashboard();
                if (WorkerActions() == "back") break;
            }
            else
            {
                Menu.EmployerDashboard();
                if (EmployerActions() == "back") break;
            }
        }
    }

    public string WorkerActions()
    {
        Worker currentWorker = (Worker)_currentUser!;
        string input = _validator.GetValidInput("Select an action:", _input, _validator.StartCheckSelection);

        if (input == "1") currentWorker.ShowCv();
        else if (input == "2")
        {
            currentWorker.HandleAddCv(_validator, _input);
            _workerJson.GetJsonSerializerList(Workers);
            Log.Information("CV added and Worker file updated for: {Email}", currentWorker.Email);
        }
        else if (input == "3") SendCv(currentWorker);
        else if (input == "4") return "back";

        return "continue";
    }

    public string EmployerActions()
    {
        Employer currentEmployer = (Employer)_currentUser!;
        string input = _validator.GetValidInput("Select an action:", _input, _validator.StartCheckSelection);

        if (input == "1") currentEmployer.ViewMyVacancy();
        else if (input == "2")
        {
            currentEmployer.HandleAddVacancy(_validator, _input, _employerJson, currentEmployer);
            _employerJson.GetJsonSerializerList(Employers);
            Log.Information("Vacancy added and Employer file updated for: {Email}", currentEmployer.Email);
        }
        else if (input == "3") ApplicationReview(currentEmployer);
        else if (input == "4") return "back";

        return "continue";
    }

    public void ApplicationReview(Employer employer)
    {
        List<Employer> allEmployers = _employerJson.GetJsonDeserializerList<Employer>() ?? new();
        var currentEmployer = allEmployers.FirstOrDefault(e => e.Email == employer.Email);

        if (currentEmployer == null)
        {
            Console.WriteLine("Employer not found.");
            return;
        }

        currentEmployer.ViewIncomingCvs(_employerJson, Workers);

        if (currentEmployer.IncomingCvs == null || currentEmployer.IncomingCvs.Count == 0)
            return;

        string idxStr = _validator.GetValidInput("Enter CV number to review:", _input, _validator.StartCheckSelection);
        int idx = int.Parse(idxStr) - 1;

        if (idx < 0 || idx >= currentEmployer.IncomingCvs.Count)
        {
            Console.WriteLine("Invalid selection.");
            return;
        }

        Guid cvId = currentEmployer.IncomingCvs[idx];
        List<Worker> allWorkers = _workerJson.GetJsonDeserializerList<Worker>() ?? new();
        Cv currentCv = allWorkers
            .SelectMany(w => w.Cv)
            .FirstOrDefault(cv => cv.Id == cvId);

        if (currentCv == null)
        {
            Console.WriteLine("CV not found.");
            return;
        }

        Console.Clear();
        Console.WriteLine(currentCv.ToString());

        Menu.ApplicationReviewBoard();
        string choice = _validator.GetValidInput("Final Decision:", _input, _validator.StartCheckSelection);

        if (choice == "1")
            currentEmployer.AcceptCandidate(cvId, _workerJson, _employerJson);
        else if (choice == "2")
            currentEmployer.RejectCandidate(cvId, _workerJson, _employerJson);

        _workerJson.GetJsonSerializerList(allWorkers);
        _employerJson.GetJsonSerializerList(allEmployers);
    }

    public void SendCv(Worker worker)
    {
        RefreshData();
        var currentWorker = Workers.FirstOrDefault(w => w.Email == worker.Email);

        if (currentWorker == null)
        {
            Console.WriteLine("Worker not found in data.");
            return;
        }

        Menu.ActivePostingsBoard();
        string choice = _validator.GetValidInput("Select an option:", _input, _validator.StartCheckSelection);

        if (choice == "1")
        {
            currentWorker.BrowseVacancies(_employerJson, Employers);
            string vIdx = _validator.GetValidInput("Enter Vacancy ID:", _input, _validator.StartCheckSelection);

            if (currentWorker.Cv == null || currentWorker.Cv.Count == 0)
            {
                Console.WriteLine("Action required: Please create a CV before applying!");
                return;
            }

            for (int i = 0; i < currentWorker.Cv.Count; i++)
                Console.WriteLine($"{i + 1}. {currentWorker.Cv[i].MajorName}");

            string cIdx = _validator.GetValidInput("Select CV ID to submit:", _input, _validator.StartCheckSelection);

            currentWorker.SendCv(vIdx, cIdx, _employerJson, Employers);

            _employerJson.GetJsonSerializerList(Employers);
            Log.Information("Worker {Email} applied to a vacancy.", currentWorker.Email);
        }
    }


}
