using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace task2
{
    public abstract class Hero
    {
        public abstract Dictionary<string, int> GetStats();
    }

    public class Warrior : Hero
    {
        public override Dictionary<string, int> GetStats()
        {
            return new Dictionary<string, int>
                {
                    {"attack", 10 }, {"defense", 10}, {"hp", 15}
                };
        }
    }

    public class Barbarian : Hero
    {
        public override Dictionary<string, int> GetStats()
        {
            return new Dictionary<string, int>
                {
                    {"attack", 15 }, {"defense", 5}, {"hp", 20}
                };
        }
    }

    public class Rouge : Hero
    {
        public override Dictionary<string, int> GetStats()
        {
            return new Dictionary<string, int>
                {
                    {"attack", 5 }, {"defense", 15}, {"hp", 10}
                };
        }
    }

    public abstract class heroDecorator : Hero
    {
        protected Hero _hero;

        public heroDecorator(Hero hero)
        {
            _hero = hero;
        }

        public override Dictionary<string, int> GetStats()
        {
            return _hero.GetStats();
        }
    }

    public class weaponDecorator : heroDecorator
    {
        public weaponDecorator(Hero hero) : base(hero) { }

        public override Dictionary<string, int> GetStats()
        {
            var stats = base.GetStats();
            stats["attack"] += 5;
            return stats;
        }
    }

    public class armorDecorator : heroDecorator
    {
        public armorDecorator(Hero hero) : base(hero) { }

        public override Dictionary<string, int> GetStats()
        {
            var stats = base.GetStats();
            stats["defense"] += 3;
            return stats;
        }
    }

    public class artifactDecorator : heroDecorator
    {
        public artifactDecorator(Hero hero) : base(hero) { }

        public override Dictionary<string, int> GetStats()
        {
            var stats = base.GetStats();
            stats["hp"] += 5;
            return stats;
        }

        internal class Program
        {
            static void PrintStats(string title, Dictionary<string, int> stats)
            {
                Console.Write($"{title}: ");
                foreach (var kvp in stats)
                {
                    Console.WriteLine($"{kvp.Key}={kvp.Value}");
                }
                Console.WriteLine();
            }

            static void Main(string[] args)
            {
                Console.OutputEncoding = System.Text.Encoding.UTF8;

                Hero warrior = new Warrior();
                Hero barbarian = new Barbarian();
                Hero rouge = new Rouge();

                Hero warriorWithWeapon = new weaponDecorator(warrior);
                Hero warriorFull = new armorDecorator(new artifactDecorator(warriorWithWeapon));
                PrintStats("воїн", warrior.GetStats());
                PrintStats("воїн з легендарною зброєю", warriorWithWeapon.GetStats());
                PrintStats("воїн у повному спорядженні", warriorFull.GetStats());

                Hero barbarianWithWeapon = new weaponDecorator(barbarian);
                Hero barbarianFull = new artifactDecorator(barbarianWithWeapon);
                PrintStats("варвар", barbarian.GetStats());
                PrintStats("варвар з легендарною сокирою", barbarianWithWeapon.GetStats());
                PrintStats("варвар у повному спорядженні", barbarianFull.GetStats());

                Hero rogueSkilled = new armorDecorator(rouge);
                Hero rogueFull = new artifactDecorator(rogueSkilled);
                PrintStats("розбійник", rouge.GetStats());
                PrintStats("досвідчений розбійник", rogueSkilled.GetStats());
                PrintStats("лідер банди розбійників", rogueFull.GetStats());
            }
        }
    }
}
