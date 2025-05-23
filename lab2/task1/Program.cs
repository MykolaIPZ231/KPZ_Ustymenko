using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace task1
{
    public interface ISubscription
    {
        decimal MonthlyFee { get; }
        int MinimumPeriod { get; }
        List<string> Channels { get; }
        List<string> Features { get; }
    }

    public class DomesticSubscription : ISubscription
    {
        public decimal MonthlyFee { get; }
        public int MinimumPeriod { get; }
        public List<string> Channels { get; }
        public List<string> Features { get; }

        public DomesticSubscription(decimal monthlyFee, int minimumPeriod, List<string> channels, List<string> features)
        {
            MonthlyFee = monthlyFee;
            MinimumPeriod = minimumPeriod;
            Channels = channels;
            Features = features;
        }
    }

    public class EducationalSubscription : ISubscription
    {
        public decimal MonthlyFee { get; }
        public int MinimumPeriod { get; }
        public List<string> Channels { get; }
        public List<string> Features { get; }

        public EducationalSubscription(decimal monthlyFee, int minimumPeriod, List<string> channels, List<string> features)
        {
            MonthlyFee = monthlyFee;
            MinimumPeriod = minimumPeriod;
            Channels = channels;
            Features = features;
        }
    }

    public class PremiumSubscription : ISubscription
    {
        public decimal MonthlyFee { get; }
        public int MinimumPeriod { get; }
        public List<string> Channels { get; }
        public List<string> Features { get; }

        public PremiumSubscription(decimal monthlyFee, int minimumPeriod, List<string> channels, List<string> features)
        {
            MonthlyFee = monthlyFee;
            MinimumPeriod = minimumPeriod;
            Channels = channels;
            Features = features;
        }
    }

    public enum SubscriptionType
    {
        Domestic,
        Educational,
        Premium
    }

    public interface ISubscriptionCreator
    {
        ISubscription CreateSubscription(SubscriptionType type);
    }

    public class WebSite : ISubscriptionCreator
    {
        public ISubscription CreateSubscription(SubscriptionType type)
        {
            switch (type)
            {
                case SubscriptionType.Domestic:
                    return new DomesticSubscription(12.99m, 12, new List<string> { "домашні канали" }, new List<string> { "підключення до 4 акаунтів", "HD якість" });
                case SubscriptionType.Educational:
                    return new EducationalSubscription(10.99m, 12, new List<string> { "домашній набір + документальні канали" }, new List<string> { "підключення до 3 акаунтів", "підтримка 24/7" });
                case SubscriptionType.Premium:
                    return new PremiumSubscription(24.99m, 6, new List<string> { "усі канали" }, new List<string> { "4К", "VIP підтримка" });
                default:
                    throw new ArgumentException("Invalid subscription type");
            }
        }
    }

    public class MobileApp : ISubscriptionCreator
    {
        public ISubscription CreateSubscription(SubscriptionType type)
        {
            switch (type)
            {
                case SubscriptionType.Domestic:
                    return new DomesticSubscription(12.99m, 12, new List<string> { "домашні канали" }, new List<string> { "підключення до 4 акаунтів", "HD якість" });
                case SubscriptionType.Educational:
                    return new EducationalSubscription(10.99m, 12, new List<string> { "домашній набір + документальні канали" }, new List<string> { "підключення до 3 акаунтів", "підтримка 24/7" });
                case SubscriptionType.Premium:
                    return new PremiumSubscription(24.99m, 6, new List<string> { "усі канали" }, new List<string> { "4К", "VIP підтримка" });
                default:
                    throw new ArgumentException("Invalid subscription type");
            }
        }
    }

    public class ManagerCall : ISubscriptionCreator
    {
        public ISubscription CreateSubscription(SubscriptionType type)
        {
            switch (type)
            {
                case SubscriptionType.Domestic:
                    return new DomesticSubscription(12.99m, 12, new List<string> { "домашні канали" }, new List<string> { "підключення до 4 акаунтів", "HD якість" });
                case SubscriptionType.Educational:
                    return new EducationalSubscription(10.99m, 12, new List<string> { "домашній набір + документальні канали" }, new List<string> { "підключення до 3 акаунтів", "підтримка 24/7" });
                case SubscriptionType.Premium:
                    return new PremiumSubscription(24.99m, 6, new List<string> { "усі канали" }, new List<string> { "4К", "VIP підтримка" });
                default:
                    throw new ArgumentException("Invalid subscription type");
            }
        }
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            ISubscriptionCreator webCreator = new WebSite();
            ISubscription domesticWeb = webCreator.CreateSubscription(SubscriptionType.Domestic);
            Console.WriteLine("домашня підписка через вебсайт");
            DisplaySubscriptionDetails(domesticWeb);

            ISubscriptionCreator mobileCreator = new MobileApp();
            ISubscription premiumMobile = mobileCreator.CreateSubscription(SubscriptionType.Premium);
            Console.WriteLine("\nпреміум підписка через мобільний додаток");
            DisplaySubscriptionDetails(premiumMobile);

            ISubscriptionCreator managerCreator = new ManagerCall();
            ISubscription educationalManager = managerCreator.CreateSubscription(SubscriptionType.Educational);
            Console.WriteLine("\nосвітня підписка через дзвінок менеджеру");
            DisplaySubscriptionDetails(educationalManager);
        }

        static void DisplaySubscriptionDetails(ISubscription subscription)
        {
            Console.WriteLine($"щомісячна плата: {subscription.MonthlyFee}");
            Console.WriteLine($"мінімальний період: {subscription.MinimumPeriod} months");
            Console.WriteLine("доступні канали: " + string.Join(", ", subscription.Channels));
            Console.WriteLine("особливості: " + string.Join(", ", subscription.Features));
        }
    }
}
