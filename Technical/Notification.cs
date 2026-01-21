
namespace FinalProject;

public class Notification
{
    public string? Message { get; set; }
    public bool IsRead { get; set; }
    public DateTime CreateTime { get; set; } = DateTime.Now;
    public Person? Receiver { get; set; }

    public Notification(string message, Person person)
    {
        Message = message;
        Receiver = person;
    }

    public override string ToString()
    {
        Console.ForegroundColor = ConsoleColor.Yellow;
        var surname = Receiver?.Surname ?? "Unknown";
        var name = Receiver?.Name ?? "Unknown";
        return $"[{CreateTime:HH:mm}] Notification for {surname} {name}: {Message}";
    }
}

public static class NotificationHandler
{
    public static event Action<Notification>? OnNotificationCreated;

    public static void RaiseNotification(string message, Person receiver)
    {
        var notification = new Notification(message, receiver);
        receiver.Notifications.Add(notification);
        OnNotificationCreated?.Invoke(notification);
    }
}