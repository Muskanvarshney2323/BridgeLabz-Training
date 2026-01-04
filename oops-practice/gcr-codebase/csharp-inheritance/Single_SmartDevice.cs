using System;

namespace csharp_inheritance
{
    class Device
    {
        public string DeviceId { get; set; }
        public string Status { get; set; }

        public Device(string id, string status)
        {
            DeviceId = id;
            Status = status;
        }

        public virtual void DisplayStatus()
        {
            Console.WriteLine($"Device {DeviceId}: {Status}");
        }
    }

    class Thermostat : Device
    {
        public double TemperatureSetting { get; set; }

        public Thermostat(string id, string status, double temp) : base(id, status)
        {
            TemperatureSetting = temp;
        }

        public override void DisplayStatus()
        {
            Console.WriteLine($"Thermostat {DeviceId}: {Status}, Temp: {TemperatureSetting}°C");
        }
    }

    class Program
    {
        static void Main()
        {
            var d = new Thermostat("T100", "Online", 22.5);
            d.DisplayStatus();
        }
    }
}