using System;

namespace Task2_3
{
    public interface IDamageCalculator
    {
        int CalculateDamage(int baseDamage, int targetArmor);
    }

    public class PhysicalDamageCalculator : IDamageCalculator
    {
        public int CalculateDamage(int baseDamage, int targetArmor)
        {
            int dmg = baseDamage - targetArmor / 2;
            if (dmg < 1)
            {
                dmg = 1;
            }
            return dmg;
        }
    }

    public class MagicDamageCalculator : IDamageCalculator
    {
        public int CalculateDamage(int baseDamage, int targetArmor)
        {
            int dmg = baseDamage - targetArmor / 4;
            if (dmg < 1)
            {
                dmg = 1;
            }
            if (targetArmor == 0)
            {
                dmg = dmg * 2;
            }
            return dmg;
        }
    }

    public class FireDamageCalculator : IDamageCalculator
    {
        public int CalculateDamage(int baseDamage, int targetArmor)
        {
            int dmg = baseDamage - targetArmor / 10;
            if (dmg < 1)
            {
                dmg = 1;
            }
            if (baseDamage > 50)
            {
                dmg += 10;
            }
            return dmg;
        }
    }

    public class Unit
    {
        public string Name;
        public int HP;
        public int Armor;

        public Unit(string name, int hp, int armor)
        {
            Name = name;
            HP = hp;
            Armor = armor;
        }
    }

    public class DamageSystem
    {
        private IDamageCalculator calculator;

        public DamageSystem(IDamageCalculator calculator)
        {
            this.calculator = calculator;
        }

        public void ApplyDamage(Unit target, int baseDamage)
        {
            int damage = calculator.CalculateDamage(baseDamage, target.Armor);
            target.HP -= damage;
            if (target.HP < 0)
            {
                target.HP = 0;
            }
            Console.WriteLine($"Юнит {target.Name} получил {damage} урона. Осталось HP: {target.HP}");
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Unit unit = new Unit("Орк", 200, 20);

            DamageSystem physSystem = new DamageSystem(new PhysicalDamageCalculator());
            DamageSystem magicSystem = new DamageSystem(new MagicDamageCalculator());
            DamageSystem fireSystem = new DamageSystem(new FireDamageCalculator());

            Console.WriteLine("Физический урон:");
            physSystem.ApplyDamage(unit, 60);

            Console.WriteLine("Магический урон:");
            magicSystem.ApplyDamage(unit, 60);

            Console.WriteLine("Огненный урон:");
            fireSystem.ApplyDamage(unit, 60);
        }
    }

    // Ответ на вопрос для размышления:
    // Чтобы добавить новый тип урона (например ледяной) и не менять класс DamageSystem,
    // нужно просто создать новый класс IceDamageCalculator, который реализует интерфейс
    // IDamageCalculator, и прописать там свою формулу в методе CalculateDamage.
    // Потом этот новый калькулятор просто передается в конструктор DamageSystem.
    // Саму систему менять не нужно, потому что она зависит от интерфейса, а не от конкретных классов.
}