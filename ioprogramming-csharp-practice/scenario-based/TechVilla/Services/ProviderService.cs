using System;
using TechVilla.Interfaces;

namespace TechVilla.Services
{
    public class ProviderService : PartialService, ICancellable, ITrackable
    {
        public ProviderService(string name) : base(name) { }

        public void Cancel()
        {
            Console.WriteLine($"ProviderService {Name} cancelled.");
        }

        public string GetStatus()
        {
            return $"ProviderService {Name} is active";
        }
    }
}
