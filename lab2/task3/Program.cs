using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace task3
{
    public sealed class authenticator
    {
        private static readonly Lazy<authenticator> _instanse = new Lazy<authenticator>(() => new authenticator());

        public static authenticator instance => _instanse.Value;

        private authenticator()
        {
            Console.WriteLine("аутентифікатор створено");
        }

        public void authenticate(string user)
        {
            Console.WriteLine($"користувачa {user} аутентифіковано");
        }
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            authenticator qwe = authenticator.instance;
            authenticator asd = authenticator.instance;

            Console.WriteLine($"qwe == asd - {object.ReferenceEquals(asd,asd)}");

            qwe.authenticate("qwe");
            asd.authenticate("asd");
        }
    }
}
