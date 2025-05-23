using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.Models
{
    internal class Product
    {
        public string Name { get; }
        public string Unit { get; }
        public Money Price { get; private set; }

        public Product(string name, string unit, Money price)
        {
            Name = name;
            Unit = unit;
            Price = price;
        }

        public void DecreasePrice(Money amount)
        {
            if (Price.Currency != amount.Currency)
            {
                throw new ArgumentException("валюти не співпадають");
            }
            Price = Price.Subtract(amount);
        }
    }
}
