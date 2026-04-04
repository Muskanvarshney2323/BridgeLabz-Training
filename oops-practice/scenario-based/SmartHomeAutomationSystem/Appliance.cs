using System;
namespace SmartHomeAutomation
{
    class Appliance : IControllable
    {
        public string Name { get; set; }

        public Appliance(string name)
        {
            Name = name;
        }

        public virtual void TurnOn()
        {
            Console.WriteLine($"{Name} appliance is turned ON");
        }

        public virtual void TurnOff()
        {
            Console.WriteLine($"{Name} appliance is turned OFF");
        }
    }
}
