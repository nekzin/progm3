using System;
using System.Collections.Generic;

interface IChatMediator
{
    void AddUser(User user);
    void RemoveUser(User user);
    void SendMessage(string message, User sender, string receiverName);
}
class ChatMediator : IChatMediator
{
    private readonly List<User> _users = new();

    public void AddUser(User user)
    {
        _users.Add(user);
        Console.WriteLine($"{user.Name} присаидинилсо к чяту.");
    }

    public void RemoveUser(User user)
    {
        _users.Remove(user);
        Console.WriteLine($"{user.Name} вышыл из чята.");
    }

    public void SendMessage(string message, User sender, string receiverName)
    {
        var receiver = _users.Find(u => u.Name == receiverName);
        if (receiver != null)
        {
            receiver.ReceiveMessage(message, sender.Name);
        }
        else
        {
            Console.WriteLine($"Палучятил {receiverName} ни найдин.");
        }
    }
}
class User
{
    public string Name { get; }
    private readonly IChatMediator _mediator;
    private readonly List<string> _messageHistory = new();

    public User(string name, IChatMediator mediator)
    {
        Name = name;
        _mediator = mediator;
    }

    public void SendMessage(string message, string receiverName)
    {
        Console.WriteLine($"{Name} атправляит саапщенее тля {receiverName}: \"{message}\"");
        _mediator.SendMessage(message, this, receiverName);
    }

    public void ReceiveMessage(string message, string senderName)
    {
        string formattedMessage = $"{senderName}: {message}";
        _messageHistory.Add(formattedMessage);
        Console.WriteLine($"{Name} палучиль саапщенее: {formattedMessage}");
    }

    public void ShowMessageHistory()
    {
        Console.WriteLine($"Естроея саапщени тля {Name}:");
        foreach (var message in _messageHistory)
        {
            Console.WriteLine(message);
        }
    }
}
class Program
{
    static void Main(string[] args)
    {
        IChatMediator chatMediator = new ChatMediator();

        User alice = new("Гарик", chatMediator);
        User bob = new("Инокенти", chatMediator);
        User charlie = new("Шабит Хапаев", chatMediator);

        chatMediator.AddUser(alice);
        chatMediator.AddUser(bob);
        chatMediator.AddUser(charlie);

        alice.SendMessage("Превет, Шабит ХАпаеф!", "Шабит Хапаев");
        bob.SendMessage("Превет, ГАрек!", "Гарик");
        charlie.SendMessage("Фсем Перевет!", "Инокенти");

        alice.ShowMessageHistory();
        bob.ShowMessageHistory();
        charlie.ShowMessageHistory();

        chatMediator.RemoveUser(charlie);

        alice.SendMessage("Куда пропал Семен Лобков?", "Семен Лобков");
    }
}
