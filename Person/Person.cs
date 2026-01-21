

namespace FinalProject
{
    public abstract class Person
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string? Name { get; set; }
        public string? Surname { get; set; }
        public string? City { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
        public string? Phone { get; set; }
        public int? Age { get; set; }

        public List<Notification> Notifications { get; set; } = new List<Notification>();

        public Person() { }
        public Person(string name, string surname, string city, string phone, int age)
        {
            Name = name;
            Surname = surname;
            City = city;
            Phone = phone;
            Age = age;
        }

        public override string ToString()
        {
            return $"{Surname} {Name}";
        }

        public override bool Equals(object? obj)
        {
            if (obj is not Person other)
                return false;
            return string.Equals(this.Email, other.Email, StringComparison.OrdinalIgnoreCase) &&
                   this.Password == other.Password;
        }

        public void SetData(ValidationService validationService, InputService inputService)
        {
            Console.ForegroundColor = ConsoleColor.DarkCyan;

            Name = validationService.GetValidInput("Enter Name:", inputService, validationService.StartCheckName);
            Surname = validationService.GetValidInput("Enter Surname:", inputService, validationService.StartCheckName);
            Email = validationService.GetValidInput("Enter Email (@gmail.com):", inputService, validationService.StartCheckEmail);
            Password = validationService.GetValidInput("Create Password (min 8 chars, 1 digit):", inputService, validationService.StartCheckPassword);
            Phone = validationService.GetValidInput("Enter Phone:", inputService, validationService.StartCheckPhone);

            string ageInput = validationService.GetValidInput("Enter Age:", inputService, validationService.StartCheckAge);
            Age = int.Parse(ageInput);

            Console.ResetColor();
        }
    }
}

