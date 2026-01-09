using System;
namespace SmartHomeAutomation
{
    class Fan : Appliance
    {
        public Fan(string name) : base(name) { }

        public override void TurnOn()
        {
            Console.WriteLine($"{Name} fan is running 🌀");
        }

        public override void TurnOff()
        {
            Console.WriteLine($"{Name} fan is stopped");
        }
    }
}
