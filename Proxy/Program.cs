using System;
using System.Collections.Generic;

namespace Proxy
{
    public interface ISubject
    {
        string Request(string request);
    }

    public class RealSubject : ISubject
    {
        public string Request(string request)
        {
            Console.WriteLine($"RealSubject: Обрабатываю запрос '{request}'...");
            return $"Ответ на запрос '{request}'";
        }
    }
    public class Proxy : ISubject
    {
        private readonly RealSubject _realSubject;
        private readonly Dictionary<string, (string Response, DateTime CachedTime)> _cache = new();
        private readonly TimeSpan _cacheDuration = TimeSpan.FromMinutes(5);

        private readonly List<string> _authorizedUsers;

        private readonly string _currentUser;

        public Proxy(RealSubject realSubject, string currentUser)
        {
            _realSubject = realSubject;
            _currentUser = currentUser;

            _authorizedUsers = new List<string> { "Admin", "User1", "User2" };
        }

        public string Request(string request)
        {

            if (!CheckAccess())
            {
                return "Доступ запрещён. У вас нет прав на выполнение этого запроса.";
            }

            if (_cache.TryGetValue(request, out var cacheEntry))
            {
                if (DateTime.Now - cacheEntry.CachedTime < _cacheDuration)
                {
                    Console.WriteLine("Proxy: Возвращаю результат из кэша.");
                    return cacheEntry.Response;
                }

                Console.WriteLine("Proxy: Удаляю устаревший кэш.");
                _cache.Remove(request);
            }

            Console.WriteLine("Proxy: Передаю запрос RealSubject.");
            var response = _realSubject.Request(request);

            _cache[request] = (response, DateTime.Now);
            return response;
        }

        private bool CheckAccess()
        {
            Console.WriteLine($"Proxy: Проверяю права доступа для пользователя '{_currentUser}'.");
            return _authorizedUsers.Contains(_currentUser);
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            var realSubject = new RealSubject();

            var proxyAuthorized = new Proxy(realSubject, "Admin");

            var proxyUnauthorized = new Proxy(realSubject, "Guest");

            Console.WriteLine(proxyAuthorized.Request("Запрос 1"));
            Console.WriteLine(proxyAuthorized.Request("Запрос 1")); 

            Console.WriteLine(proxyUnauthorized.Request("Запрос 2"));

            System.Threading.Thread.Sleep(6000);

            Console.WriteLine(proxyAuthorized.Request("Запрос 1")); 
        }
    }
}
