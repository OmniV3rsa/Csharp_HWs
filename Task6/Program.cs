using System;
using System.IO;
using System.Text.Json;

namespace Task6
{
    public class GameSettings
    {
        public string PlayerName { get; set; }
        public int Difficulty { get; set; }
        public int MusicVolume { get; set; }
    }

    class Program
    {
        static void Main(string[] args)
        {
            string path = Path.Combine(AppContext.BaseDirectory, "settings.json");

            if (File.Exists(path))
            {
                string text = File.ReadAllText(path);
                GameSettings s = JsonSerializer.Deserialize<GameSettings>(text);

                Console.WriteLine($"Имя: {s.PlayerName}");
                Console.WriteLine($"Сложность: {s.Difficulty}");
                Console.WriteLine($"Громкость: {s.MusicVolume}");
            }
            else
            {
                Console.WriteLine("Файл настроек не найден. Создаём новые настройки.");

                GameSettings s = new GameSettings();

                Console.Write("Имя игрока: ");
                s.PlayerName = Console.ReadLine();

                Console.Write("Сложность (1-3): ");
                s.Difficulty = int.Parse(Console.ReadLine());

                Console.Write("Громкость (0-100): ");
                s.MusicVolume = int.Parse(Console.ReadLine());

                JsonSerializerOptions options = new JsonSerializerOptions();
                options.WriteIndented = true;

                string json = JsonSerializer.Serialize(s, options);
                File.WriteAllText(path, json);
            }
        }
    }
}