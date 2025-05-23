using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.Models
{
    internal class Money
    {
        private int _dollars;
        private int _cents;
        private string _currency;

        public int Dollars => _dollars;
        public int Cents => _cents;
        public string Currency => _currency;

        public Money(int dollars, int cents, string currency)
        {
            if (dollars <0 || cents <0)
            {
                throw new ArgumentException("err, сума від'ємна");
            }
            _dollars = dollars;
            _cents = cents;
            _currency = currency;

            Normalize();
        }

        private void Normalize()
        {
            _dollars += _cents / 100;
            _cents %= 100;
        }

        public string Display()
        {
            return $"{_currency} {_dollars}.{_cents:00}";
        }

        public void SetAmount(int dollars, int cents)
        {
            if (dollars < 0 || cents < 0)
            {
                throw new ArgumentException("Сума не може бути від'ємною.");
            }

            _dollars = dollars;
            _cents = cents;
            Normalize();
        }

        public Money Subtract(Money other)
        {
            if(Currency != other.Currency)
            {
                throw new ArgumentException("валюти не співпадають");
            }

            int totalCents = (_dollars * 100 + _cents) - (other.Dollars * 100 + other.Cents);
            if (totalCents < 0)
            {
                throw new ArgumentException("недостатньо коштів");
            }
            return new Money(totalCents / 100, totalCents % 100, Currency);
        }
    }
}
