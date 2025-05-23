using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace task4
{
    public class Virus : ICloneable
    {
        public double Weight { get; set; }
        public int Age { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public Virus[] Children {  get; set; }

        public Virus(double weight, int age, string name, string description, Virus[] children)
        {
            Weight = weight;
            Age = age;
            Name = name;
            Description = description;
            Children = children;
        }

        public object Clone()
        {
            Virus[] clonedChild = new Virus[Children.Length];
            for(int i = 0; i < Children.Length; i++)
            {
                clonedChild[i] = (Virus)Children[i].Clone();
            }
            return new Virus(Weight, Age, Name, Description, clonedChild);
        }

        public void PrintInfo(string pref = "")
        {
            Console.WriteLine($"{pref}ім'я - {Name}" + $"\nвік - {Age}" + $"\nвага - {Weight}" + $"\nвірус - {Description}\n");
            foreach(var child in Children)
            {
                child.PrintInfo(pref);
            }
        }
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Virus grandchild = new Virus(40, 9, "онук", "flu", Array.Empty<Virus>());
            Virus child = new Virus(80 ,36, "син", "covid-19", new Virus[] {grandchild});
            Virus parent = new Virus(84, 60, "батько", "rotaviral enteritis", new Virus[] { child });
            Console.WriteLine("орігінал");
            parent.PrintInfo();

            Virus clonedParent = (Virus)parent.Clone();
            parent.Children[0].Name = "qwe";
            Console.WriteLine("оригінал зі зміненим ім'ям сина");
            parent.PrintInfo();

            Console.WriteLine("клон");
            clonedParent.PrintInfo();
        }
    }
}
