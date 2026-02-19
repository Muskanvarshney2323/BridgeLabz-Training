using System;
namespace TechVilla.Services
{
    public class HealthcareService : Service
    {
        public HealthcareService(string name) : base(name) { }

        public override void Register()
        {
            base.Register();
            Console.WriteLine($"Healthcare specifics applied for {Name}");
        }
    }
}
