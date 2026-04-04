using System;

namespace TechVilla.Services
{
    public class EmergencyService : Service
    {
        public EmergencyService(string name) : base(name) { }

        public override void Register()
        {
            Console.WriteLine($"EMERGENCY: quick register for {Name}");
        }
    }
}
