using System;
namespace SmartHomeAutomation
{
    class AC : Appliance
    {
        public AC(string name) : base(name) { }

        public override void TurnOn()
        {
            Console.WriteLine($"{Name} AC is cooling ❄️");
        }

        public override void TurnOff()
        {
            Console.WriteLine($"{Name} AC is turned off");
        }
    }
}
