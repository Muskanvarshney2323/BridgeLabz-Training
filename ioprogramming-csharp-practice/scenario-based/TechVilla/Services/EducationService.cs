using System;
namespace TechVilla.Services
{
    public class EducationService : Service
    {
        public EducationService(string name) : base(name) { }

        public override void Register()
        {
            base.Register();
            Console.WriteLine($"Education plan created for {Name}");
        }
    }
}
