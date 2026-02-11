using System;
using System.Collections.Generic;

namespace StackQueueProblems
{

    public class CircularTourProblem
    {
        public class PetrolPump
        {
            public int Petrol { get; set; }
            public int Distance { get; set; }

            public PetrolPump(int petrol, int distance)
            {
                Petrol = petrol;
                Distance = distance;
            }

            public override string ToString()
            {
                return $"(Petrol: {Petrol}, Distance: {Distance})";
            }
        }

               public static int FindStartingPoint(PetrolPump[] pumps)
        {
            if (pumps == null || pumps.Length == 0)
                return -1;

            int start = 0;
            int totalPetrol = 0;
            int totalDistance = 0;
            int currentPetrol = 0;

            for (int i = 0; i < pumps.Length; i++)
            {
                totalPetrol += pumps[i].Petrol;
                totalDistance += pumps[i].Distance;
                currentPetrol += pumps[i].Petrol - pumps[i].Distance;

               
                if (currentPetrol < 0)
                {
                    start = i + 1;
                    currentPetrol = 0;
                }
            }

            // If total petrol is less than total distance, no solution exists
            return totalPetrol >= totalDistance ? start : -1;
        }

    
        public static bool VerifyTour(PetrolPump[] pumps, int start)
        {
            if (pumps == null || pumps.Length == 0 || start < 0 || start >= pumps.Length)
                return false;

            int petrol = 0;
            int n = pumps.Length;

            for (int i = 0; i < n; i++)
            {
                int index = (start + i) % n;
                petrol += pumps[index].Petrol - pumps[index].Distance;

                if (petrol < 0)
                    return false;
            }

            return true;
        }
        public static int FindStartingPointQueue(PetrolPump[] pumps)
        {
            if (pumps == null || pumps.Length == 0)
                return -1;

            Queue<int> queue = new Queue<int>();
            for (int i = 0; i < pumps.Length; i++)
            {
                queue.Enqueue(i);
            }

            int petrol = 0;
            int lastStart = 0;

            while (queue.Count > 0)
            {
                int pump = queue.Dequeue();
                petrol += pumps[pump].Petrol - pumps[pump].Distance;

                if (petrol < 0)
                {
                    petrol = 0;
                    lastStart = pump + 1;
                }
                else
                {
                    queue.Enqueue(pump);
                }
            }

            return VerifyTour(pumps, lastStart) ? lastStart : -1;
        }
    }

    // Example Usage
    public class CircularTourProblemExample
    {
        public static void Main()
        {
            // Example 1: Solution exists
            CircularTourProblem.PetrolPump[] pumps1 = new CircularTourProblem.PetrolPump[]
            {
                new CircularTourProblem.PetrolPump(4, 6),
                new CircularTourProblem.PetrolPump(6, 5),
                new CircularTourProblem.PetrolPump(7, 3),
                new CircularTourProblem.PetrolPump(4, 5)
            };

            int start1 = CircularTourProblem.FindStartingPoint(pumps1);
            Console.WriteLine("Example 1:");
            Console.WriteLine("Pumps: [4,6] [6,5] [7,3] [4,5]");
            Console.WriteLine($"Starting Point: {start1}"); // Output: 1
            Console.WriteLine($"Verification: {CircularTourProblem.VerifyTour(pumps1, start1)}");

            Console.WriteLine("\n" + new string('-', 50) + "\n");

            // Example 2: No solution exists
            CircularTourProblem.PetrolPump[] pumps2 = new CircularTourProblem.PetrolPump[]
            {
                new CircularTourProblem.PetrolPump(1, 5),
                new CircularTourProblem.PetrolPump(2, 5),
                new CircularTourProblem.PetrolPump(3, 5),
                new CircularTourProblem.PetrolPump(4, 10)
            };

            int start2 = CircularTourProblem.FindStartingPoint(pumps2);
            Console.WriteLine("Example 2:");
            Console.WriteLine("Pumps: [1,5] [2,5] [3,5] [4,10]");
            Console.WriteLine($"Starting Point: {start2}"); // Output: -1 (no solution)

            Console.WriteLine("\n" + new string('-', 50) + "\n");

            // Example 3: Another valid case
            CircularTourProblem.PetrolPump[] pumps3 = new CircularTourProblem.PetrolPump[]
            {
                new CircularTourProblem.PetrolPump(5, 1),
                new CircularTourProblem.PetrolPump(2, 5),
                new CircularTourProblem.PetrolPump(1, 2),
                new CircularTourProblem.PetrolPump(3, 4)
            };

            int start3 = CircularTourProblem.FindStartingPoint(pumps3);
            Console.WriteLine("Example 3:");
            Console.WriteLine("Pumps: [5,1] [2,5] [1,2] [3,4]");
            Console.WriteLine($"Starting Point: {start3}");
            Console.WriteLine($"Verification: {CircularTourProblem.VerifyTour(pumps3, start3)}");
        }
    }
}
