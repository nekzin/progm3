using System;

public interface IWeapon
{
    void UseWeapon();
}

public class Sword : IWeapon
{
    public void UseWeapon()
    {
        Console.WriteLine("Используется меч: взмах мечом!");
    }
}

public class Bow : IWeapon
{
    public void UseWeapon()
    {
        Console.WriteLine("Используется лук: выстрел из лука!");
    }
}

public class Axe : IWeapon
{
    public void UseWeapon()
    {
        Console.WriteLine("Используется топор: удар топором!");
    }
}

public class Player
{
    private IWeapon _weapon;

    public Player(IWeapon weapon)
    {
        _weapon = weapon;
    }

    public void SetWeapon(IWeapon weapon)
    {
        _weapon = weapon;
    }

    public void Attack()
    {
        if (_weapon != null)
        {
            _weapon.UseWeapon();
        }
        else
        {
            Console.WriteLine("Игрок не вооружен!");
        }
    }
}

public class Game
{
    private Player _player;

    public Game()
    {
        _player = new Player(new Sword()); 
    }

    public void Start()
    {
        Console.WriteLine("Игра началась!");
        bool isPlaying = true;

        while (isPlaying)
        {
            Console.WriteLine("\nВыберите действие:");
            Console.WriteLine("1. Атаковать");
            Console.WriteLine("2. Сменить оружие");
            Console.WriteLine("3. Выйти из игры");

            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    _player.Attack();
                    break;
                case "2":
                    ChangeWeapon();
                    break;
                case "3":
                    isPlaying = false;
                    break;
                default:
                    Console.WriteLine("Неверный выбор. Попробуйте снова.");
                    break;
            }
        }

        Console.WriteLine("Игра завершена.");
    }

    private void ChangeWeapon()
    {
        Console.WriteLine("\nВыберите новое оружие:");
        Console.WriteLine("1. Меч");
        Console.WriteLine("2. Лук");
        Console.WriteLine("3. Топор");

        string choice = Console.ReadLine();

        switch (choice)
        {
            case "1":
                _player.SetWeapon(new Sword());
                Console.WriteLine("Игрок теперь использует меч.");
                break;
            case "2":
                _player.SetWeapon(new Bow());
                Console.WriteLine("Игрок теперь использует лук.");
                break;
            case "3":
                _player.SetWeapon(new Axe());
                Console.WriteLine("Игрок теперь использует топор.");
                break;
            default:
                Console.WriteLine("Неверный выбор. Оружие не изменено.");
                break;
        }
    }
}

class Program
{
    static void Main(string[] args)
    {
        Game game = new Game();
        game.Start();
    }
}