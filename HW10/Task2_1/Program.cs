using System;

namespace Task2_1
{
    public interface ICombatant
    {
        public string Name { get; }
        public int HP { get; set; }
        public bool IsDead { get; }
        public int Damage { get; }
        public void Attack(ICombatant target);
        public void TakeDamage(int amount);
    }

    public class Knight : ICombatant
    {
        public string Name { get; }
        public int HP { get; set; }
        public int Damage { get; }
        public bool IsDead => HP <= 0;

        public Knight(string name)
        {
            Name = name;
            HP = 150;
            Damage = 25;
        }

        public void Attack(ICombatant target)
        {
            if (IsDead)
            {
                Console.WriteLine($"Рыцарь {Name} мёртв и не может атаковать");
                return;
            }
            if (target.IsDead)
            {
                Console.WriteLine($"Цель {target.Name} уже мертва");
                return;
            }

            Console.WriteLine($"Рыцарь {Name} атакует!");
            target.TakeDamage(Damage);
        }

        public void TakeDamage(int amount)
        {
            HP -= amount;
            if (HP < 0)
            {
                HP = 0;
            }
            Console.WriteLine($"Рыцарь {Name} получил {amount} урона. Осталось HP: {HP}");
        }
    }

    public class Mage : ICombatant
    {
        public string Name { get; }
        public int HP { get; set; }
        public int Damage { get; }
        public bool IsDead => HP <= 0;

        public Mage(string name)
        {
            Name = name;
            HP = 80;
            Damage = 40;
        }

        public void Attack(ICombatant target)
        {
            if (IsDead)
            {
                Console.WriteLine($"Маг {Name} мёртв и не может атаковать");
                return;
            }
            if (target.IsDead)
            {
                Console.WriteLine($"Маг {Name} не может атаковать мёртвую цель");
                return;
            }

            Console.WriteLine($"Маг {Name} использует двойное заклинание!");
            target.TakeDamage(Damage);
            if (!target.IsDead)
            {
                target.TakeDamage(Damage);
            }
        }

        public void TakeDamage(int amount)
        {
            HP -= amount;
            if (HP < 0)
            {
                HP = 0;
            }
            Console.WriteLine($"Маг {Name} получил {amount} урона. Осталось HP: {HP}");
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Knight knight = new Knight("Артур");
            Mage mage = new Mage("Мерлин");

            while (!knight.IsDead && !mage.IsDead)
            {
                knight.Attack(mage);
                if (!mage.IsDead)
                {
                    mage.Attack(knight);
                }
            }

            if (!knight.IsDead)
            {
                Console.WriteLine($"Победил Рыцарь {knight.Name}!");
            }
            else
            {
                Console.WriteLine($"Победил Маг {mage.Name}!");
            }
        }
    }
}