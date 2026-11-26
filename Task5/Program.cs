using System;
using System.IO;
using System.Text.Json;

namespace Task5
{
    public class GameCharacter
    {
        public string Name { get; set; }
        public int Level { get; set; }
        public double Health { get; set; }
        public string[] Inventory { get; set; }
    }

    class Program
    {
        static void Main(string[] args)
        {
            GameCharacter c = new GameCharacter();
            c.Name = "Arthas";
            c.Level = 10;
            c.Health = 100.5;
            c.Inventory = new string[] { "Меч", "Щит", "Зелье" };

            JsonSerializerOptions options = new JsonSerializerOptions();
            options.WriteIndented = true;

            string json = JsonSerializer.Serialize(c, options);

            string path = Path.Combine(AppContext.BaseDirectory, "save.json");
            File.WriteAllText(path, json);

            string text = File.ReadAllText(path);
            GameCharacter c2 = JsonSerializer.Deserialize<GameCharacter>(text);

            Console.WriteLine("=== Загруженный персонаж ===");
            Console.WriteLine($"Имя: {c2.Name}");
            Console.WriteLine($"Уровень: {c2.Level}");
            Console.WriteLine($"Здоровье: {c2.Health}");
            Console.WriteLine($"Инвентарь: {string.Join(", ", c2.Inventory)}");
        }
    }
}