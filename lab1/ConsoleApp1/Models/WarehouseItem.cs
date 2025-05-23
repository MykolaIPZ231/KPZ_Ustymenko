using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.Models
{
    internal class WarehouseItem
    {
        public Product Product { get; }
        public int Quantity { get; set; }
        public DateTime LastDeliveryData { get; set; }

        public WarehouseItem(Product product, int quantity, DateTime lastDeliveryData)
        {
            Product = product;
            Quantity = quantity;
            LastDeliveryData = lastDeliveryData;
        }

        public override string ToString()
        {
            return $"{Product.Name} ({Quantity} {Product.Unit}). востаннє доставлений - {LastDeliveryData:yyyy-MM-dd}";
        }
    }
}
