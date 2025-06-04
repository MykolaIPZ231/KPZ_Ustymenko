using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace task1
{
    abstract class SupportHandler
    {
        protected SupportHandler nextHandler;
        public void SetNextHandler(SupportHandler handler)
        {
            nextHandler = handler;
        }
        public abstract bool HandleRequest();
    }

    class lvl1SupportHandler : SupportHandler
    {
        public override bool HandleRequest()
        {
            Console.WriteLine("\nlvl 1 support (y/n)");
            while (true)
            {
                string input = Console.ReadLine();
                if (input == "y")
                {
                    Console.WriteLine(">>>>lvl1 support");
                    return true;
                }
                else if (input == "n")
                {
                    return nextHandler?.HandleRequest() ?? false;
                }
            }
        }
    }

    class lvl2SupportHandler : SupportHandler
    {
        public override bool HandleRequest()
        {
            Console.WriteLine("\nlvl2 support (y/n)");
            while (true)
            {
                string input = Console.ReadLine();
                if (input == "y")
                {
                    Console.WriteLine(">>>>lvl2 support");
                    return true;
                }
                else if (input == "n")
                {
                    return nextHandler?.HandleRequest() ?? false;
                }
            }
        }
    }

    class lvl3SupportHandler : SupportHandler
    {
        public override bool HandleRequest()
        {
            Console.WriteLine("\nlvl2 support (y/n)");
            while (true)
            {
                string input = Console.ReadLine();
                if (input == "y")
                {
                    Console.WriteLine(">>>>lvl3 support");
                    return true;
                }
                else if (input == "n")
                {
                    return nextHandler?.HandleRequest() ?? false;
                }
            }
        }
    }

    class lvl4SupportHandler : SupportHandler
    {
        public override bool HandleRequest()
        {
            Console.WriteLine("\nlvl2 support (y/n)");
            while (true)
            {
                string input = Console.ReadLine();
                if (input == "y")
                {
                    Console.WriteLine(">>>>lvl4 support");
                    return true;
                }
                else if (input == "n")
                {
                    return nextHandler?.HandleRequest() ?? false;
                }
            }
        }
    }

    class SupportSystem
    {
        private SupportHandler firstHandler;
        public SupportSystem(SupportHandler handler)
        {
            firstHandler = handler;
        }
        public void ProcessRequest()
        {
            while (true)
            {
                if (firstHandler.HandleRequest())
                {
                    break;
                }
                Console.WriteLine("chose err, try again");
            }

            Console.WriteLine("problem will be fixed soon");
        }
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            var handler1 = new lvl1SupportHandler();
            var handler2 = new lvl2SupportHandler();
            var handler3 = new lvl3SupportHandler();
            var handler4 = new lvl4SupportHandler();

            handler1.SetNextHandler(handler2);
            handler2.SetNextHandler(handler3);
            handler3.SetNextHandler(handler4);

            var supportSystem = new SupportSystem(handler1);
            supportSystem.ProcessRequest();
        }
    }
}
