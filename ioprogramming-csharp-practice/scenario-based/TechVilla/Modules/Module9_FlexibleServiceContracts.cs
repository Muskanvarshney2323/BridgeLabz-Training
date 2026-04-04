using System;
using TechVilla.Services;
using TechVilla.Interfaces;

namespace TechVilla.Modules
{
    public static class Module9_FlexibleServiceContracts
    {
        public static void Demo()
        {
            Console.WriteLine("Module 9: Flexible Service Contracts");

            IBookable b = new ProviderService("ProviderA");
            b.Book("Alice");

            if (b is ICancellable c)
            {
                c.Cancel();
            }

            if (b is ITrackable t)
            {
                Console.WriteLine(t.GetStatus());
            }
        }
    }
}
