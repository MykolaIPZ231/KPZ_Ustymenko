using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace task2
{
    public interface ILaptop { string GetDetails(); }
    public interface INetbook { string GetDetails(); }
    public interface IEbook { string GetDetails(); }
    public interface ISmartphone { string GetDetails(); }

    public interface IDeviceFactory
    {
        ILaptop CreateLaptop();
        INetbook CreateNetbook();
        IEbook CreateEbook();
        ISmartphone CreateSmartphone();
    }

    public class xiaomiFactory : IDeviceFactory
    {
        public ILaptop CreateLaptop() => new xiaomiLaptop();
        public INetbook CreateNetbook() => new xiaomiNetbook();
        public IEbook CreateEbook() => new xiaomiEbook();
        public ISmartphone CreateSmartphone() => new xiaomiSmartphone();
    }

    public class xiaomiLaptop : ILaptop { public string GetDetails() => "Xiaomi Laptop"; }
    public class xiaomiNetbook : INetbook { public string GetDetails() => "Xiaomi Netbook"; }
    public class xiaomiEbook : IEbook { public string GetDetails() => "Xiaomi Ebook"; }
    public class xiaomiSmartphone : ISmartphone { public string GetDetails() => "Xiaomi Smartphone"; }

    public class appleFactory : IDeviceFactory
    {
        public ILaptop CreateLaptop() => new appleLaptop();
        public INetbook CreateNetbook() => new appleNetbook();
        public IEbook CreateEbook() => new appleEbook();
        public ISmartphone CreateSmartphone() => new appleSmartphone();
    }

    public class appleLaptop : ILaptop { public string GetDetails() => "Apple Laptop"; }
    public class appleNetbook : INetbook { public string GetDetails() => "Apple Netbook"; }
    public class appleEbook : IEbook { public string GetDetails() => "Apple Ebook"; }
    public class appleSmartphone : ISmartphone { public string GetDetails() => "Apple Smartphone"; }

    public class samsungFactory : IDeviceFactory
    {
        public ILaptop CreateLaptop() => new samsungLaptop();
        public INetbook CreateNetbook() => new samsungNetbook();
        public IEbook CreateEbook() => new samsungEbook();
        public ISmartphone CreateSmartphone() => new samsungSmartphone();
    }

    public class samsungLaptop : ILaptop { public string GetDetails() => "Samsung Laptop"; }
    public class samsungNetbook : INetbook { public string GetDetails() => "Samsung Netbook"; }
    public class samsungEbook : IEbook { public string GetDetails() => "Samsung Ebook"; }
    public class samsungSmartphone : ISmartphone { public string GetDetails() => "Samsung Smartphone"; }


    internal class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            IDeviceFactory xiaomiFactory = new xiaomiFactory();
            Console.WriteLine("xiaomi");
            Console.WriteLine(xiaomiFactory.CreateSmartphone().GetDetails());
            Console.WriteLine(xiaomiFactory.CreateNetbook().GetDetails());

            IDeviceFactory appleFactory = new appleFactory();
            Console.WriteLine("\napple");
            Console.WriteLine(appleFactory.CreateLaptop().GetDetails());
            Console.WriteLine(appleFactory.CreateEbook().GetDetails());

            IDeviceFactory samsungFactory = new samsungFactory();
            Console.WriteLine("\nsamsung");
            Console.WriteLine(samsungFactory.CreateSmartphone().GetDetails());
            Console.WriteLine(samsungFactory.CreateEbook().GetDetails());
        }
    }
}
