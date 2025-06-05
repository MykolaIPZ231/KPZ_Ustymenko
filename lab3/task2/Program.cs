using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace task2
{
    public abstract class Hero
    {
        public string Name { get; protected set; }
        public int Attack { get; protected set; }
        public int Defense { get; protected set; }
        public int Health { get; protected set; }
        public double DodgeChance { get; protected set; }

        public virtual void DisplayStats()
        {
            Console.WriteLine($"\n{Name} характеристики:");
            Console.WriteLine($"атака: {Attack}");
            Console.WriteLine($"захист: {Defense}");
            Console.WriteLine($"здоров'я: {Health}");
            Console.WriteLine($"шанс ухилення: {DodgeChance * 100}%");
        }
    }

    public class Warrior : Hero
    {
        public Warrior(string name)
        {
            Name = name;
            Attack = 20;
            Defense = 15;
            Health = 100;
            DodgeChance = 0.1;
        }
    }

    public class Berserk : Hero
    {
        public Berserk(string name)
        {
            Name = name;
            Attack = 25;
            Defense = 10;
            Health = 90;
            DodgeChance = 0.05;
        }
    }

    public class Dodger : Hero
    {
        public Dodger(string name)
        {
            Name = name;
            Attack = 15;
            Defense = 12;
            Health = 85;
            DodgeChance = 0.3;
        }
    }

    public abstract class HeroDecorator : Hero
    {
        protected Hero _hero;

        protected HeroDecorator(Hero hero)
        {
            _hero = hero;
        }

        public override void DisplayStats()
        {
            _hero.DisplayStats();
        }
    }

    public class SwordDecorator : HeroDecorator
    {
        public SwordDecorator(Hero hero) : base(hero)
        {
            Name = _hero.Name;
            Attack = _hero.Attack + 8;
            Defense = _hero.Defense;
            Health = _hero.Health;
            DodgeChance = _hero.DodgeChance;
        }

        public override void DisplayStats()
        {
            base.DisplayStats();
            Console.WriteLine("-> меч (+8 атаки)");
        }
    }

    public class ShieldDecorator : HeroDecorator
    {
        public ShieldDecorator(Hero hero) : base(hero)
        {
            Name = _hero.Name;
            Attack = _hero.Attack;
            Defense = _hero.Defense + 7;
            Health = _hero.Health + 20;
            DodgeChance = _hero.DodgeChance - 0.05;
        }

        public override void DisplayStats()
        {
            base.DisplayStats();
            Console.WriteLine("-> щит (+7 захист, +20 здоров'я, -5% шанс ухилення)");
        }
    }

    public class ArtefactDecorator : HeroDecorator
    {
        public ArtefactDecorator(Hero hero) : base(hero)
        {
            Name = _hero.Name;
            Attack = _hero.Attack;
            Defense = _hero.Defense + 5;
            Health = _hero.Health + 15;
            DodgeChance = _hero.DodgeChance;
        }

        public override void DisplayStats()
        {
            base.DisplayStats();
            Console.WriteLine("-> таємничий артефакт (+15 захист, +15 здоров'я)");
        }
    }


    internal class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Hero warrior = new Warrior("воїн");
            Hero berserk = new Berserk("берсерк");
            Hero dodger = new Dodger("ловкач");

            Console.WriteLine("= базові характеристики =");
            warrior.DisplayStats();
            berserk.DisplayStats();
            dodger.DisplayStats();

            Console.WriteLine("= екіпіровані герої =");

            Hero equippedWarrior = new SwordDecorator(warrior);
            equippedWarrior = new ShieldDecorator(equippedWarrior);
            equippedWarrior = new ArtefactDecorator(equippedWarrior);
            equippedWarrior.DisplayStats();

            Hero equippedBerserk = new SwordDecorator(berserk);
            equippedBerserk = new SwordDecorator(equippedBerserk);
            equippedBerserk = new ArtefactDecorator(equippedBerserk);
            equippedBerserk.DisplayStats();

            Hero equippedDodger = new ArtefactDecorator(dodger);
            equippedDodger = new ShieldDecorator(equippedDodger);
            equippedDodger.DisplayStats();
        }
    }
}
