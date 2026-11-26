using System;
using System.Collections.Generic;

namespace Task2_2
{
    public enum Rarity
    {
        Common,
        Rare,
        Legendary
    }

    public interface IUsableItem
    {
        string Name { get; }
        string Description { get; }
        Rarity Rarity { get; }
        void Use(Player player);
    }

    public class Player
    {
        public string Name;
        public int HP;
        public int Damage;

        public Player(string name, int hp, int damage)
        {
            Name = name;
            HP = hp;
            Damage = damage;
        }

        public void Heal(int amount)
        {
            HP += amount;
        }

        public void IncreaseDamage(int amount)
        {
            Damage += amount;
        }
    }

    public class HealthPotion : IUsableItem
    {
        public string Name => "Зелье здоровья";
        public string Description => "Восстанавливает 50 HP";
        public Rarity Rarity => Rarity.Common;

        public void Use(Player player)
        {
            player.Heal(50);
            Console.WriteLine("Вы выпили зелье и восстановили 50 HP");
        }
    }

    public class Sword : IUsableItem
    {
        public string Name => "Стальной меч";
        public string Description => "Увеличивает урон на 10";
        public Rarity Rarity => Rarity.Rare;

        public void Use(Player player)
        {
            player.IncreaseDamage(10);
            Console.WriteLine("Вы экипировали меч, урон увеличен на 10");
        }
    }

    public class AncientScroll : IUsableItem
    {
        public string Name => "Древний свиток";
        public string Description => "Наносит 20 урона, но дает +30 к урону";
        public Rarity Rarity => Rarity.Legendary;

        public void Use(Player player)
        {
            player.HP -= 20;
            if (player.HP < 0)
            {
                player.HP = 0;
            }
            player.IncreaseDamage(30);
            Console.WriteLine("Свиток проклял вас на 20 урона, но увеличил ваш урон на 30");
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Player p = new Player("Герой", 60, 15);

            List<IUsableItem> items = new List<IUsableItem>();
            items.Add(new HealthPotion());
            items.Add(new Sword());
            items.Add(new AncientScroll());

            for (int i = 0; i < items.Count; i++)
            {
                items[i].Use(p);
            }

            Console.WriteLine();
            Console.WriteLine($"Игрок: {p.Name}");
            Console.WriteLine($"HP: {p.HP}");
            Console.WriteLine($"Урон: {p.Damage}");
            if (p.HP > 0)
            {
                Console.WriteLine("Состояние: жив");
            }
            else
            {
                Console.WriteLine("Состояние: мёртв");
            }
        }
    }
}