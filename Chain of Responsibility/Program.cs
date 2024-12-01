public abstract class Handler
{
    protected Handler NextHandler;

    public void SetNextHandler(Handler nextHandler)
    {
        NextHandler = nextHandler;
    }

    public abstract void HandleRequest(Request request);
}
public class LogHandler : Handler
{
    public override void HandleRequest(Request request)
    {
        if (request.EventType == "Log")
        {
            Console.WriteLine($"Логгируем евент: {request.Content}");
        }
        else if (NextHandler != null)
        {
            NextHandler.HandleRequest(request);
        }
    }
}
public class EmailHandler : Handler
{
    public override void HandleRequest(Request request)
    {
        if (request.EventType == "Email")
        {
            Console.WriteLine($"Отправляем email: {request.Content}");
        }
        else if (NextHandler != null)
        {
            NextHandler.HandleRequest(request);
        }
    }
}

public class DatabaseHandler : Handler
{
    public override void HandleRequest(Request request)
    {
        if (request.EventType == "Database")
        {
            Console.WriteLine($"Сохраняем в бд: {request.Content}");
        }
        else if (NextHandler != null)
        {
            NextHandler.HandleRequest(request);
        }
    }
}
public class Request
{
    public string EventType { get; }
    public string Content { get; }

    public Request(string eventType, string content)
    {
        EventType = eventType;
        Content = content;
    }
}
class Program
{
    static void Main(string[] args)
    {
        // Создаем обработчики
        var logHandler = new LogHandler();
        var emailHandler = new EmailHandler();
        var databaseHandler = new DatabaseHandler();

        // Формируем цепочку обработчиков
        logHandler.SetNextHandler(emailHandler);
        emailHandler.SetNextHandler(databaseHandler);

        // Создаем запросы
        var logRequest = new Request("Log", "Log ыыыы");
        var emailRequest = new Request("Email", "емаил сообщение ");
        var databaseRequest = new Request("Database", "бд ыаввыв");
        var unknownRequest = new Request("Неизвестное", "данные  удалены");

        // Отправляем запросы по цепочке обработчиков
        logHandler.HandleRequest(logRequest);
        logHandler.HandleRequest(emailRequest);
        logHandler.HandleRequest(databaseRequest);
        logHandler.HandleRequest(unknownRequest);
    }
}