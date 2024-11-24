using System;
using System.Collections.Generic;

namespace flyweight
{
    public class Character
    {
        
        public string Name { get; }
        public string Type { get; }
        public string Image { get; }

        public Character(string name, string type, string image)
        {
            Name = name;
            Type = type;
            Image = image;
        }
        public void DisplayInfo(ExtrinsicState extrinsicState)
        {
            Console.WriteLine($"Имя: {Name}, Тип: {Type}, Фото: {Image}, Уровень: {extrinsicState.Level}, Опыт: {extrinsicState.Experience}");
        }
    }
    public class ExtrinsicState
    {
        public int Level { get; set; }
        public int Experience { get; set; }

        public ExtrinsicState(int level, int experience)
        {
            Level = level;
            Experience = experience;
        }
    }

    public class CharacterFactory
    {
        private readonly Dictionary<string, Character> _characters = new();

        public Character GetCharacter(string name, string type, string image)
        {
            string key = $"{name}_{type}";
            if (!_characters.ContainsKey(key))
            {
                Console.WriteLine($"Создание нового персонажа: {name}, Тип: {type}");
                _characters[key] = new Character(name, type, image);
            }
            else
            {
                Console.WriteLine($"Пересоздание существующего персонажа: {name}, Тип: {type}");
            }

            return _characters[key];
        }
        public void ShowAllCharacters()
        {
            Console.WriteLine("Все созданные персонажи:");
            foreach (var key in _characters.Keys)
            {
                Console.WriteLine($" - {key}");
            }
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            CharacterFactory factory = new CharacterFactory();

            var warrior1 = factory.GetCharacter("Воин", "Ближник", "warrior.png");
            var warrior2 = factory.GetCharacter("Воин", "Ближник", "warrior.png"); 
            var mage = factory.GetCharacter("Маг", "Дальник", "mage.png");

            var warriorState1 = new ExtrinsicState(level: 5, experience: 1500);
            var warriorState2 = new ExtrinsicState(level: 10, experience: 3000);
            var mageState = new ExtrinsicState(level: 8, experience: 2500);

            warrior1.DisplayInfo(warriorState1);
            warrior2.DisplayInfo(warriorState2);
            mage.DisplayInfo(mageState);

            factory.ShowAllCharacters();
        }
    }
}
