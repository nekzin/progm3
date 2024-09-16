using System;
using System.Collections.Generic;
using System.Net;
using System.Threading;

public class Servers //ленивый
{
    private static readonly object _lock = new object();//для многопоточности
    private static readonly Lazy<Servers> _instance = new Lazy<Servers>(() => new Servers());//ленивая инициталзция == экземпляр создается только при первом обращениии к инстанс

    private readonly Dictionary<string, bool> _servers = new Dictionary<string, bool>(); //словарь для хранения серверов

    private Servers() { }

    public static Servers Instance => _instance.Value;

    public bool AddServer(string server)//добавляет сервер в список
    {
        if (string.IsNullOrEmpty(server))
            return false;
        //проверяет чтобы сервер начинался с хттп или хттпс
        if (!server.StartsWith("http://", StringComparison.OrdinalIgnoreCase) && !server.StartsWith("https://", StringComparison.OrdinalIgnoreCase))
            return false;

        lock (_lock)
        {
            if (_servers.ContainsKey(server))//проверка на дубликат
                return false;

            _servers.Add(server, true);//добавление
            return true;
        }
    }
    //список хттп серверов
    public List<string> GetHttpServers()
    {
        lock (_lock) //для синхронизации доступа к словарю серверс(многопоточност)
        {
            return _servers.Where(s => s.Key.StartsWith("http://", StringComparison.OrdinalIgnoreCase)).Select(s => s.Key).ToList();
        }
    }
    //список хттпс серверов
    public List<string> GetHttpsServers()
    {
        lock (_lock) //для синхронизации доступа к словарю серверс
        {
            return _servers.Where(s => s.Key.StartsWith("https://", StringComparison.OrdinalIgnoreCase)).Select(s => s.Key).ToList();
        }
    }
}
public class EagerServers //жадный т.е. без ленивой инициализации и здесь нет потокобезопасности тк нет lock
{
    private static readonly EagerServers _instance = new EagerServers();//ранняя инициализация синглтон экземпляра

    private readonly Dictionary<string, bool> _servers = new Dictionary<string, bool>();

    public static EagerServers Instance => _instance;

    public bool AddServer(string server)
    {
        if (string.IsNullOrEmpty(server))
            return false;

        if (!server.StartsWith("http://", StringComparison.OrdinalIgnoreCase) && !server.StartsWith("https://", StringComparison.OrdinalIgnoreCase))
            return false;

        if (_servers.ContainsKey(server))
            return false;

        _servers.Add(server, true);
        return true;
    }

    public List<string> GetHttpServers()
    {
        return _servers.Where(s => s.Key.StartsWith("http://", StringComparison.OrdinalIgnoreCase)).Select(s => s.Key).ToList();
    }

    public List<string> GetHttpsServers()
    {
        return _servers.Where(s => s.Key.StartsWith("https://", StringComparison.OrdinalIgnoreCase)).Select(s => s.Key).ToList();
    }
}
public class Program
{
    public static void Main()
    {
        var servers = Servers.Instance;
        //добавление серверов и вывод результата - нельзя добавить дубликат
        Console.WriteLine(servers.AddServer("http://example.com")); // true
        Console.WriteLine(servers.AddServer("https://example.com")); // true
        Console.WriteLine(servers.AddServer("http://example.com")); // false
        //вывод добавленных серверов
        Console.WriteLine(string.Join(", ", servers.GetHttpServers())); // http://example.com
        Console.WriteLine(string.Join(", ", servers.GetHttpsServers())); // https://example.com
    }
}