using System;
using System.Text.Json;

namespace Task4
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
            Console.WriteLine(json);

            GameCharacter c2 = JsonSerializer.Deserialize<GameCharacter>(json);
            Console.WriteLine(c2.Name);
            Console.WriteLine(c2.Inventory.Length);
        }
    }
}