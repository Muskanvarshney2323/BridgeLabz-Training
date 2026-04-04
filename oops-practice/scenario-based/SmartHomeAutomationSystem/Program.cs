using System;

namespace SmartHomeAutomation
{
    class Program
    {
        static void Main(string[] args)
        {
            IControllable[] devices =
            {
                new Light("Bedroom"),
                new Fan("Living Room"),
                new AC("Office")
            };

            foreach (IControllable device in devices)
            {
                device.TurnOn();
                device.TurnOff();
                Console.WriteLine();
            }
        }
    }
}
