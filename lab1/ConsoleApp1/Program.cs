using ConsoleApp1.Models;
using ConsoleApp1.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace ConsoleApp1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            var price = new Money(10, 50, "UAH");
            Console.WriteLine($"Ціна: {price.Display()}");

            var apple = new Product("Яблука", "кг", new Money(20, 0, "UAH"));
            apple.DecreasePrice(new Money(5, 0, "UAH"));
            Console.WriteLine($"Нова ціна: {apple.Price.Display()}");

            var warehouse = new Warehouse();
            var reporting = new Reporting(warehouse);
            reporting.RegisterIncoming(apple, 100, new DateTime(2024, 5, 1));
            reporting.RegisterIncoming(apple, 50, new DateTime(2024, 5, 2));
            reporting.RegisterOutgoing(apple, 30, new DateTime(2024, 5, 3));
            reporting.GenerateInventoryReport();

            reporting.RegisterOutgoing(apple, 200, new DateTime(2024, 5, 4));
        }
    }
}
