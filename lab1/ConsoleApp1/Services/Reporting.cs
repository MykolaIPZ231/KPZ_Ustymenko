using ConsoleApp1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.Services
{
    internal class Reporting
    {
        private readonly Warehouse _warehouse;

        public Reporting(Warehouse warehouse)
        {
            _warehouse = warehouse;
        }

        public void RegisterIncoming(Product product, int quantity, DateTime date)
        {
            _warehouse.AddProduct(product, quantity, date);
            Console.WriteLine($"Прибуткова накладна: Додано {quantity} {product.Unit} {product.Name} ({date:yyyy-MM-dd})");
        }

        public void RegisterOutgoing(Product product, int quantity, DateTime date)
        {
            try
            {
                _warehouse.RemoveProduct(product, quantity);
                Console.WriteLine($"Видаткова накладна: Відвантажено {quantity} {product.Unit} {product.Name} ({date:yyyy-MM-dd})");
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine($"Помилка: {ex.Message}");
            }
        }

        public void GenerateInventoryReport()
        {
            Console.WriteLine("Звіт про інвентаризацію:");
            foreach (var item in _warehouse.GetInventory())
            {
                Console.WriteLine($"- {item.Product.Name}: {item.Quantity} {item.Product.Unit}, " +
                                  $"Ціна: {item.Product.Price.Display()}, " +
                                  $"Остання доставка: {item.LastDeliveryData:yyyy-MM-dd}");
            }
        }
    }
}
