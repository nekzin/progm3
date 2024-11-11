public interface INotifier
{
    void Send(string message);
}
public class Notifier : INotifier
{
    private readonly string _emailAddress;

    public Notifier(string emailAddress)
    {
        _emailAddress = emailAddress;
    }

    public virtual void Send(string message)
    {
        Console.WriteLine($"Отправка емаил на почьту: {_emailAddress}: {message}");
        
    }
}
public abstract class NotifierDecorator : INotifier
{
    protected readonly INotifier _notifier;

    public NotifierDecorator(INotifier notifier)
    {
        _notifier = notifier;
    }

    public virtual void Send(string message)
    {
        _notifier.Send(message);
    }
}
public class SmsNotifier : NotifierDecorator
{
    private readonly string _phoneNumber;

    public SmsNotifier(INotifier notifier, string phoneNumber) : base(notifier)
    {
        _phoneNumber = phoneNumber;
    }

    public override void Send(string message)
    {
        base.Send(message);
        Console.WriteLine($"Отправка смс на номер: {_phoneNumber}: {message}");
        
    }
}
public class FacebookNotifier : NotifierDecorator
{
    private readonly string _facebookAccount;

    public FacebookNotifier(INotifier notifier, string facebookAccount) : base(notifier)
    {
        _facebookAccount = facebookAccount;
    }

    public override void Send(string message)
    {
        base.Send(message);
        Console.WriteLine($"Отправка сообщения на фейсбук профил {_facebookAccount}: {message}");
        
    }
}
class Program
{
    static void Main()
    {
        
        INotifier notifier = new Notifier("admin@example.com");

       
        notifier = new SmsNotifier(notifier, "+123456789");

        
        notifier = new FacebookNotifier(notifier, "admin_facebook_account");

        
        notifier.Send("важновое уведомление");
    }
}

