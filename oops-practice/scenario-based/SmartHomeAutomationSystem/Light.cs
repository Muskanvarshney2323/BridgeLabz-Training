using System;
namespace SmartHomeAutomation
{
    class Light : Appliance
    {
        public Light(string name) : base(name) { }

        public override void TurnOn()
        {
            Console.WriteLine($"{Name} light is glowing 💡");
        }

        public override void TurnOff()
        {
            Console.WriteLine($"{Name} light is switched off");
        }
    }
}
