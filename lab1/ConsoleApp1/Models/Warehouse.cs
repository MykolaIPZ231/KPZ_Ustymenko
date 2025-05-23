using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.Models
{
    internal class Warehouse
    {
        private readonly List<WarehouseItem> _items = new List<WarehouseItem>();

        public void AddProduct(Product product, int quantity, DateTime date)
        {
            var existingItem = _items.Find(item => item.Product.Name == product.Name && item.Product.Unit == product.Unit);

            if (existingItem != null)
            {
                existingItem.Quantity += quantity;
                existingItem.LastDeliveryData = date;
            }
            else
            {
                _items.Add(new WarehouseItem(product, quantity, date));
            }
        }

        public void RemoveProduct(Product product, int quantity)
        {
            var item = _items.Find(i =>  i.Product.Name == product.Name && i.Product.Unit == product.Unit);

            if (item == null)
            {
                throw new ArgumentException("товар не знайдено");
            }
            if (item.Quantity < quantity)
            {
                throw new ArgumentException("недостатня кількість");
            }

            item.Quantity -= quantity;
            if(item.Quantity == 0)
            {
                _items.Remove(item);
            }
        }
        public List<WarehouseItem> GetInventory() => new List<WarehouseItem>(_items);

    }
}
