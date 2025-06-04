using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace task2
{
    interface IMediator
    {
        void Notify(object sender, string eventName);
        void RegisterAircraft(Aircraft aircraft);
        void RegisterRunway(Runway runway);
    }

    class CommandCentre : IMediator
    {
        private List<Runway> _runways = new List<Runway>();
        private List<Aircraft> _aircrafts = new List<Aircraft>();

        public void RegisterAircraft(Aircraft aircraft)
        {
            _aircrafts.Add(aircraft);
        }

        public void RegisterRunway(Runway runway)
        {
            _runways.Add(runway);
        }

        public void Notify(object sender, string eventName)
        {
            if (sender is Aircraft aircraft && eventName == "Land")
            {
                bool foundFreeRunway = false;
                foreach (var runway in _runways)
                {
                    if (runway.IsBusyWithAircraft == null)
                    {
                        Console.WriteLine($"CommandCentre: Assigning runway {runway.Id} to {aircraft.Name}");
                        runway.IsBusyWithAircraft = aircraft;
                        runway.HighLightRed();
                        aircraft.CurrentRunway = runway;
                        foundFreeRunway = true;
                        break;
                    }
                }

                if (!foundFreeRunway)
                {
                    Console.WriteLine($"CommandCentre: No free runway available for {aircraft.Name}");
                }
            }
            else if (sender is Aircraft aircraftTakeOff && eventName == "TakeOff")
            {
                if (aircraftTakeOff.CurrentRunway != null)
                {
                    Console.WriteLine($"CommandCentre: Clearing runway {aircraftTakeOff.CurrentRunway.Id}");
                    aircraftTakeOff.CurrentRunway.IsBusyWithAircraft = null;
                    aircraftTakeOff.CurrentRunway.HighLightGreen();
                    aircraftTakeOff.CurrentRunway = null;
                }
                else
                {
                    Console.WriteLine($"CommandCentre: {aircraftTakeOff.Name} is not on any runway");
                }
            }
        }
    }

    class Aircraft
    {
        public string Name { get; }
        public Runway CurrentRunway { get; set; }
        private IMediator _mediator;

        public Aircraft(string name, IMediator mediator)
        {
            Name = name;
            _mediator = mediator;
        }

        public void Land()
        {
            Console.WriteLine($"Aircraft {Name} requests landing.");
            _mediator.Notify(this, "Land");
        }

        public void TakeOff()
        {
            Console.WriteLine($"Aircraft {Name} requests takeoff.");
            _mediator.Notify(this, "TakeOff");
        }
    }

    class Runway
    {
        public Guid Id { get; } = Guid.NewGuid();
        public Aircraft IsBusyWithAircraft { get; set; }

        public void HighLightRed()
        {
            Console.WriteLine($"Runway {Id} is busy!");
        }

        public void HighLightGreen()
        {
            Console.WriteLine($"Runway {Id} is free!");
        }
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            var mediator = new CommandCentre();

            var runway1 = new Runway();
            var runway2 = new Runway();

            mediator.RegisterRunway(runway1);
            mediator.RegisterRunway(runway2);

            Console.WriteLine("Rammstein airbase");
            var aircraft1 = new Aircraft("panavia tornado 45+76", mediator);
            var aircraft2 = new Aircraft("eurofighter 31+46", mediator);
            var aircraft3 = new Aircraft("eurofighter 30+69", mediator);

            mediator.RegisterAircraft(aircraft1);
            mediator.RegisterAircraft(aircraft2);
            mediator.RegisterAircraft(aircraft3);

            Console.WriteLine("\n= Landing tests =\n");
            aircraft1.Land();
            aircraft2.Land();
            aircraft3.Land();

            Console.WriteLine("\n= Takeoff tests =\n");
            aircraft1.TakeOff();
            aircraft3.Land();

            Console.WriteLine("\n= Final status =\n");
            Console.WriteLine($"Runway1 busy: {runway1.IsBusyWithAircraft?.Name ?? "none"}");
            Console.WriteLine($"Runway2 busy: {runway2.IsBusyWithAircraft?.Name ?? "none"}");
        }
    }
}
