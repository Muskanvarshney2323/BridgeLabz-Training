using System;
using System.Diagnostics;
using System.Linq;

namespace BridgeLabz.AlgorithmRuntimeAnalysis
{
    // Compare Bubble Sort, Merge Sort, and Quick Sort on different dataset sizes
    public static class SortingLargeDataEfficiently
    {
        static void BubbleSort(int[] arr)
        {
            int n = arr.Length;
            for (int i = 0; i < n - 1; i++)
                for (int j = 0; j < n - i - 1; j++)
                    if (arr[j] > arr[j + 1])
                    {
                        int t = arr[j]; arr[j] = arr[j + 1]; arr[j + 1] = t;
                    }
        }

        static int[] MergeSort(int[] arr)
        {
            if (arr.Length <= 1) return arr;
            int mid = arr.Length / 2;
            var left = MergeSort(arr.Take(mid).ToArray());
            var right = MergeSort(arr.Skip(mid).ToArray());
            return Merge(left, right);
        }

        static int[] Merge(int[] a, int[] b)
        {
            int i = 0, j = 0; var res = new int[a.Length + b.Length]; int k = 0;
            while (i < a.Length && j < b.Length)
                res[k++] = (a[i] <= b[j]) ? a[i++] : b[j++];
            while (i < a.Length) res[k++] = a[i++];
            while (j < b.Length) res[k++] = b[j++];
            return res;
        }

        static void QuickSort(int[] arr, int lo, int hi)
        {
            if (lo >= hi) return;
            int p = Partition(arr, lo, hi);
            QuickSort(arr, lo, p - 1);
            QuickSort(arr, p + 1, hi);
        }

        static int Partition(int[] arr, int lo, int hi)
        {
            int pivot = arr[hi];
            int i = lo;
            for (int j = lo; j < hi; j++)
                if (arr[j] < pivot) { (arr[i], arr[j]) = (arr[j], arr[i]); i++; }
            (arr[i], arr[hi]) = (arr[hi], arr[i]);
            return i;
        }

        static void Bench(int n, int repeats = 3)
        {
            Console.WriteLine($"--- Sorting benchmark N={n} ---");
            var rnd = new Random(42);
            var original = Enumerable.Range(0, n).Select(_ => rnd.Next()).ToArray();

            // Bubble sort: skip if n too large
            if (n <= 10_000)
            {
                long ticks = 0;
                for (int i = 0; i < repeats; i++)
                {
                    var a = (int[])original.Clone(); var sw = Stopwatch.StartNew();
                    BubbleSort(a);
                    sw.Stop(); ticks += sw.ElapsedTicks;
                }
                Console.WriteLine($"BubbleSort (avg): {ticks / repeats * 1000.0 / Stopwatch.Frequency:F4} ms");
            }
            else
            {
                Console.WriteLine("BubbleSort: Unfeasible for this N (skipped)");
            }

            // Merge sort (our impl)
            long mergeTicks = 0;
            for (int i = 0; i < repeats; i++)
            {
                var a = (int[])original.Clone(); var sw = Stopwatch.StartNew();
                var sorted = MergeSort(a);
                sw.Stop(); mergeTicks += sw.ElapsedTicks;
            }
            Console.WriteLine($"MergeSort (avg): {mergeTicks / repeats * 1000.0 / Stopwatch.Frequency:F4} ms");

            // Quick sort (in-place)
            long quickTicks = 0;
            for (int i = 0; i < repeats; i++)
            {
                var a = (int[])original.Clone(); var sw = Stopwatch.StartNew();
                QuickSort(a, 0, a.Length - 1);
                sw.Stop(); quickTicks += sw.ElapsedTicks;
            }
            Console.WriteLine($"QuickSort (avg): {quickTicks / repeats * 1000.0 / Stopwatch.Frequency:F4} ms");
            Console.WriteLine();
        }

        public static void Main()
        {
            Console.WriteLine("Sorting algorithms: Bubble vs Merge vs Quick");
            Bench(1_000);
            Bench(10_000);
            Bench(1_000_000);
            Console.WriteLine("Expected: Bubble is impractical for large N; Merge & Quick perform well.");
        }
    }
}
