using System;
using System.Diagnostics;
using System.Linq;

namespace BridgeLabz.AlgorithmRuntimeAnalysis
{
    // Compare Linear Search vs Binary Search (with sorting cost) on different dataset sizes
    public static class SearchTargetLargeDataset
    {
        static int LinearSearch(int[] arr, int target)
        {
            for (int i = 0; i < arr.Length; i++) if (arr[i] == target) return i;
            return -1;
        }

        static int BinarySearch(int[] arr, int target)
        {
            int l = 0, r = arr.Length - 1;
            while (l <= r)
            {
                int mid = (l + r) / 2;
                if (arr[mid] == target) return mid;
                if (arr[mid] < target) l = mid + 1;
                else r = mid - 1;
            }
            return -1;
        }

        static void Bench(int n, int repeats = 5)
        {
            var rnd = new Random(42);
            var arr = Enumerable.Range(0, n).Select(_ => rnd.Next()).ToArray();
            int target = arr[n / 2]; // a value likely present

            // Linear search benchmark
            long linearTicks = 0;
            for (int i = 0; i < repeats; i++)
            {
                var copy = arr; // already randomized
                var sw = Stopwatch.StartNew();
                LinearSearch(copy, target);
                sw.Stop(); linearTicks += sw.ElapsedTicks;
            }

            // Binary search benchmark: includes sort cost
            long sortAndSearchTicks = 0;
            for (int i = 0; i < repeats; i++)
            {
                var copy = (int[])arr.Clone();
                var sw = Stopwatch.StartNew();
                Array.Sort(copy); // O(N log N)
                BinarySearch(copy, target);
                sw.Stop(); sortAndSearchTicks += sw.ElapsedTicks;
            }

            double tickToMs = 1000.0 / Stopwatch.Frequency;
            Console.WriteLine($"N={n}");
            Console.WriteLine($"  Linear search (avg): {linearTicks / repeats * tickToMs:F4} ms");
            Console.WriteLine($"  Sort + Binary search (avg): {sortAndSearchTicks / repeats * tickToMs:F4} ms");
            Console.WriteLine();
        }

        public static void Main()
        {
            Console.WriteLine("Search performance comparison — Linear vs Binary (includes sort)");
            Bench(1_000);
            Bench(10_000);
            Bench(1_000_000);
            Console.WriteLine("Note: Binary costs include sorting; if data is already sorted, binary search alone is far faster.");
        }
    }
}
