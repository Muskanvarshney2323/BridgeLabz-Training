using System;
using TechVilla.Interfaces;

namespace TechVilla.Services
{
    public abstract class PartialService : Service, IBookable
    {
        protected PartialService(string name) : base(name) { }

        // Provide a default implementation for booking, leave tracking to concrete classes
        public virtual bool Book(string byWhom)
        {
            Console.WriteLine($"{Name} booked by {byWhom}");
            return true;
        }
    }
}
