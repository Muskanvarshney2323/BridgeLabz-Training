using System;

namespace TechVilla.Services
{
    public abstract class Service
    {
        public string Name { get; set; }
        public static int TotalServices { get; private set; }

        protected Service(string name)
        {
            this.Name = name;
            TotalServices++;
        }

        public virtual void Register()
        {
            Console.WriteLine($"Registering service {Name}");
        }

        public override string ToString() => $"{GetType().Name}: {Name}";

        public override bool Equals(object? obj)
        {
            return obj is Service s && s.GetType() == GetType() && s.Name == Name;
        }

        public override int GetHashCode() => HashCode.Combine(GetType(), Name);
    }
}
