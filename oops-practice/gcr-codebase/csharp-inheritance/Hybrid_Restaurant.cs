using System;

namespace csharp_inheritance
{
    class Person
    {
        public string Name { get; set; }
        public int Id { get; set; }

        public Person(string name, int id)
        {
            Name = name;
            Id = id;
        }
    }

    interface Worker
    {
        void PerformDuties();
    }

    class Chef : Person, Worker
    {
        public Chef(string name, int id) : base(name, id)
        {
        }

        public void PerformDuties()
        {
            Console.WriteLine($"Chef {Name} (Id:{Id}) is cooking.");
        }
    }

    class Waiter : Person, Worker
    {
        public Waiter(string name, int id) : base(name, id)
        {
        }

        public void PerformDuties()
        {
            Console.WriteLine($"Waiter {Name} (Id:{Id}) is serving.");
        }
    }

    class Program
    {
        static void Main()
        {
            Worker[] workers = new Worker[]
            {
                new Chef("Gordon", 1),
                new Waiter("Alice", 2)
            };

            foreach (var w in workers)
            {
                w.PerformDuties();
            }
        }
    }
}