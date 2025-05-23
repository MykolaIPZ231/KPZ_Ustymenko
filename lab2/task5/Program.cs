using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace task5
{
    public interface ICharacterBuilder
    {
        ICharacterBuilder setRace(string race);
        ICharacterBuilder setClass(string classC);
        ICharacterBuilder setArmor(string armor);
        ICharacterBuilder setWeapon(string weapon);
        ICharacterBuilder setDeed(string deed);
        object Build();
    }

    public class Hero
    {
        public string Race { get; set; }
        public string Class { get; set; }
        public string Armor { get; set; }
        public string Weapon { get; set; }
        public List<string> Deeds { get; } = new List<string>();

        public void DisplayInfo()
        {
            Console.WriteLine($"персонаж: {Race} - {Class}");
            Console.WriteLine($"обладунки: {Armor} | зброя: {Weapon}");
            Console.WriteLine("вчинки: " + string.Join(", ", Deeds));
        }
    }

    public class Enemy
    {
        public string Race { get; set; }
        public string Class { get; set; }
        public string Armor { get; set; }
        public string Weapon { get; set; }
        public List<string> Deeds { get; } = new List<string>();

        public void DisplayInfo()
        {
            Console.WriteLine($"персонаж: {Race} - {Class}");
            Console.WriteLine($"обладунки: {Armor} | зброя: {Weapon}");
            Console.WriteLine("вчинки: " + string.Join(", ", Deeds));
        }
    }

    public class HeroBuilder : ICharacterBuilder
    {
        private Hero _hero = new Hero();

        public ICharacterBuilder setRace(string race) { _hero.Race = race; return this; }
        public ICharacterBuilder setClass(string cls) { _hero.Class = cls; return this; }
        public ICharacterBuilder setArmor(string armor) { _hero.Armor = armor; return this; }
        public ICharacterBuilder setWeapon(string weapon) { _hero.Weapon = weapon; return this; }
        public ICharacterBuilder setDeed(string deed) { _hero.Deeds.Add(deed); return this; }

        public Hero Build() => _hero;
        object ICharacterBuilder.Build() => Build();
    }

    public class EnemyBuilder : ICharacterBuilder
    {
        private Enemy _enemy = new Enemy();

        public ICharacterBuilder setRace(string race) { _enemy.Race = race; return this; }
        public ICharacterBuilder setClass(string cls) { _enemy.Class = cls; return this; }
        public ICharacterBuilder setArmor(string armor) { _enemy.Armor = armor; return this; }
        public ICharacterBuilder setWeapon(string weapon) { _enemy.Weapon = weapon; return this; }
        public ICharacterBuilder setDeed(string deed) { _enemy.Deeds.Add(deed); return this; }

        public Enemy Build() => _enemy;
        object ICharacterBuilder.Build() => Build();
    }

    public class CharacterDirector
    {
        public Hero CreateHero(ICharacterBuilder builder)
        {
            return (Hero)builder
                .setRace("людина")
                .setClass("космодесантник")
                .setArmor("силова броня")
                .setWeapon("болтер та пиломеч")
                .setDeed("вбив варбоса")
                .setDeed("служба в караулі смерті")
                .Build();
        }

        public Enemy CreateEnemy(ICharacterBuilder builder)
        {
            return (Enemy)builder
                .setRace("орк")
                .setClass("варбос")
                .setArmor("віра в горку і морку")
                .setWeapon("все що під рукою")
                .setDeed("зробив багато постука")
                .setDeed("WAAAAGH")
                .Build();
        }
    }
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            var director = new CharacterDirector();
            var heroBuilder = new HeroBuilder();
            Hero hero = director.CreateHero(heroBuilder);
            var enemyBuilder = new EnemyBuilder();
            Enemy enemy = director.CreateEnemy(enemyBuilder);

            hero.DisplayInfo();
            Console.WriteLine("\n-------\n");
            enemy.DisplayInfo();
        }
    }
}
