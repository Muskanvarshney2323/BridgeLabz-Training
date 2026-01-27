using System;
using System.Collections.Generic;
using System.Reflection;

namespace CollectionAnnotations.Advanced
{
    /// <summary>
    /// Advanced Level - Problem 7: Implement a Custom Caching System with CacheResult
    /// Define CacheResult to store method return values and avoid repeated execution.
    /// </summary>
    /// 
    // Custom CacheResult Attribute
    [AttributeUsage(AttributeTargets.Method)]
    public class CacheResultAttribute : Attribute
    {
        public int DurationSeconds { get; set; }

        public CacheResultAttribute(int durationSeconds = 300)
        {
            DurationSeconds = durationSeconds;
        }
    }

    // Cache entry with expiration
    public class CacheEntry
    {
        public object Value { get; set; }
        public DateTime ExpirationTime { get; set; }

        public bool IsExpired
        {
            get { return DateTime.Now > ExpirationTime; }
        }
    }

    // Method result cache
    public static class MethodResultCache
    {
        private static Dictionary<string, CacheEntry> _cache = new Dictionary<string, CacheEntry>();

        public static void Set(string key, object value, int durationSeconds)
        {
            _cache[key] = new CacheEntry
            {
                Value = value,
                ExpirationTime = DateTime.Now.AddSeconds(durationSeconds)
            };
        }

        public static object Get(string key)
        {
            if (_cache.ContainsKey(key))
            {
                CacheEntry entry = _cache[key];
                if (!entry.IsExpired)
                {
                    return entry.Value;
                }
                else
                {
                    _cache.Remove(key);
                }
            }
            return null;
        }

        public static bool Has(string key)
        {
            if (_cache.ContainsKey(key))
            {
                if (_cache[key].IsExpired)
                {
                    _cache.Remove(key);
                    return false;
                }
                return true;
            }
            return false;
        }

        public static void Clear()
        {
            _cache.Clear();
        }

        public static void DisplayCache()
        {
            Console.WriteLine("\n--- Current Cache ---");
            if (_cache.Count == 0)
            {
                Console.WriteLine("Cache is empty");
                return;
            }

            foreach (var kvp in _cache)
            {
                string status = kvp.Value.IsExpired ? "EXPIRED" : "VALID";
                Console.WriteLine($"Key: {kvp.Key}, Value: {kvp.Value.Value}, Status: {status}");
            }
        }
    }

    // Data service with cached methods
    class DataService
    {
        [CacheResult(300)] // Cache for 300 seconds (5 minutes)
        public int CalculateSum(int[] numbers)
        {
            Console.WriteLine("[Processing] Calculating sum of numbers...");
            System.Threading.Thread.Sleep(1000); // Simulate expensive operation

            int sum = 0;
            foreach (int num in numbers)
            {
                sum += num;
            }
            return sum;
        }

        [CacheResult(600)] // Cache for 600 seconds
        public double CalculateAverage(int[] numbers)
        {
            Console.WriteLine("[Processing] Calculating average of numbers...");
            System.Threading.Thread.Sleep(800);

            if (numbers.Length == 0)
                return 0;

            int sum = 0;
            foreach (int num in numbers)
            {
                sum += num;
            }
            return (double)sum / numbers.Length;
        }

        [CacheResult(120)] // Cache for 120 seconds
        public string FetchDataFromDatabase(string query)
        {
            Console.WriteLine($"[Processing] Fetching data from database: {query}");
            System.Threading.Thread.Sleep(1500);

            return $"Result for query: {query}";
        }

        [CacheResult(900)]
        public int[] SortLargeArray(int[] numbers)
        {
            Console.WriteLine("[Processing] Sorting large array...");
            System.Threading.Thread.Sleep(2000);

            Array.Sort(numbers);
            return numbers;
        }

        public string NoCacheMethod(string input)
        {
            Console.WriteLine($"[Processing] No caching: {input}");
            System.Threading.Thread.Sleep(500);
            return $"Result: {input}";
        }
    }

    class CacheResultDemo
    {
        static void Main()
        {
            Console.WriteLine("=== CacheResult Attribute - Advanced Level ===\n");

            // Example 1: Basic caching
            Console.WriteLine("Example 1: Basic Method Caching\n");
            BasicCaching();

            // Example 2: Cache with multiple parameters
            Console.WriteLine("\n\nExample 2: Caching Different Inputs\n");
            MultipleInputsCaching();

            // Example 3: Compare cached vs non-cached
            Console.WriteLine("\n\nExample 3: Performance Comparison\n");
            PerformanceComparison();

            // Example 4: Display cache state
            Console.WriteLine("\n\nExample 4: Cache State\n");
            DisplayCacheState();

            // Example 5: Cache expiration
            Console.WriteLine("\n\nExample 5: Cache Expiration Simulation\n");
            CacheExpirationDemo();
        }

        static void BasicCaching()
        {
            DataService service = new DataService();
            int[] data = { 1, 2, 3, 4, 5 };

            // Create cache key
            string cacheKey = GenerateCacheKey(nameof(DataService.CalculateSum), data);

            // First call - will compute
            Console.WriteLine("First call:");
            int result1 = CachedInvoke<int>(service, nameof(DataService.CalculateSum), data, cacheKey);
            Console.WriteLine($"Result: {result1}\n");

            // Second call - will use cache
            Console.WriteLine("Second call (should use cache):");
            int result2 = CachedInvoke<int>(service, nameof(DataService.CalculateSum), data, cacheKey);
            Console.WriteLine($"Result: {result2}");
        }

        static void MultipleInputsCaching()
        {
            DataService service = new DataService();

            // First query
            string query1 = "SELECT * FROM Users";
            string cacheKey1 = GenerateCacheKey(nameof(DataService.FetchDataFromDatabase), query1);

            Console.WriteLine("Query 1:");
            string result1 = CachedInvoke<string>(service, nameof(DataService.FetchDataFromDatabase), 
                                                  query1, cacheKey1);
            Console.WriteLine($"Result: {result1}\n");

            // Second query (different)
            string query2 = "SELECT * FROM Orders";
            string cacheKey2 = GenerateCacheKey(nameof(DataService.FetchDataFromDatabase), query2);

            Console.WriteLine("Query 2 (different):");
            string result2 = CachedInvoke<string>(service, nameof(DataService.FetchDataFromDatabase), 
                                                  query2, cacheKey2);
            Console.WriteLine($"Result: {result2}\n");

            // First query again (should use cache)
            Console.WriteLine("Query 1 again (cached):");
            string result1Again = CachedInvoke<string>(service, nameof(DataService.FetchDataFromDatabase), 
                                                       query1, cacheKey1);
            Console.WriteLine($"Result: {result1Again}");
        }

        static void PerformanceComparison()
        {
            DataService service = new DataService();
            int[] data = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
            string cacheKey = GenerateCacheKey(nameof(DataService.CalculateAverage), data);

            // Without cache
            Console.WriteLine("Without Caching (3 calls):");
            var watch = System.Diagnostics.Stopwatch.StartNew();
            
            var avg1 = service.CalculateAverage(data);
            Console.WriteLine($"Call 1: {avg1}");
            
            var avg2 = service.CalculateAverage(data);
            Console.WriteLine($"Call 2: {avg2}");
            
            var avg3 = service.CalculateAverage(data);
            Console.WriteLine($"Call 3: {avg3}");
            
            watch.Stop();
            Console.WriteLine($"Total time: {watch.ElapsedMilliseconds}ms\n");

            // With cache
            MethodResultCache.Clear();
            Console.WriteLine("With Caching (3 calls):");
            watch.Restart();
            
            var cachedAvg1 = CachedInvoke<double>(service, nameof(DataService.CalculateAverage), 
                                                  data, cacheKey);
            Console.WriteLine($"Call 1: {cachedAvg1}");
            
            var cachedAvg2 = CachedInvoke<double>(service, nameof(DataService.CalculateAverage), 
                                                  data, cacheKey);
            Console.WriteLine($"Call 2: {cachedAvg2} (from cache)");
            
            var cachedAvg3 = CachedInvoke<double>(service, nameof(DataService.CalculateAverage), 
                                                  data, cacheKey);
            Console.WriteLine($"Call 3: {cachedAvg3} (from cache)");
            
            watch.Stop();
            Console.WriteLine($"Total time: {watch.ElapsedMilliseconds}ms");
        }

        static void DisplayCacheState()
        {
            DataService service = new DataService();
            
            int[] nums = { 1, 2, 3 };
            string key1 = GenerateCacheKey(nameof(DataService.CalculateSum), nums);
            CachedInvoke<int>(service, nameof(DataService.CalculateSum), nums, key1);

            string query = "SELECT * FROM Data";
            string key2 = GenerateCacheKey(nameof(DataService.FetchDataFromDatabase), query);
            CachedInvoke<string>(service, nameof(DataService.FetchDataFromDatabase), query, key2);

            MethodResultCache.DisplayCache();
        }

        static void CacheExpirationDemo()
        {
            DataService service = new DataService();
            int[] data = { 5, 10, 15 };
            
            // Create short-lived cache (2 seconds)
            string cacheKey = GenerateCacheKey(nameof(DataService.CalculateSum), data);
            var attr = typeof(DataService).GetMethod(nameof(DataService.CalculateSum))
                                         .GetCustomAttribute<CacheResultAttribute>();

            Console.WriteLine("Demonstrating cache with short expiration (simulated):\n");
            
            // First call
            int result1 = CachedInvoke<int>(service, nameof(DataService.CalculateSum), data, cacheKey, 2);
            Console.WriteLine($"Result: {result1}");
            Console.WriteLine("Cache stored\n");

            // Immediate second call - uses cache
            Console.WriteLine("Calling again immediately (should use cache):");
            int result2 = CachedInvoke<int>(service, nameof(DataService.CalculateSum), data, cacheKey, 2);
            Console.WriteLine($"Result: {result2}\n");

            // After expiration
            Console.WriteLine("Waiting for cache to expire...");
            System.Threading.Thread.Sleep(3000);
            
            Console.WriteLine("Calling after expiration (should recompute):");
            int result3 = CachedInvoke<int>(service, nameof(DataService.CalculateSum), data, cacheKey, 2);
            Console.WriteLine($"Result: {result3}");
        }

        static T CachedInvoke<T>(DataService service, string methodName, object parameter, 
                                string cacheKey, int durationSeconds = 300)
        {
            // Check cache
            if (MethodResultCache.Has(cacheKey))
            {
                Console.WriteLine("[Cache Hit] Returning cached result");
                return (T)MethodResultCache.Get(cacheKey);
            }

            // Invoke method
            var method = typeof(DataService).GetMethod(methodName);
            var result = (T)method.Invoke(service, new object[] { parameter });

            // Store in cache
            MethodResultCache.Set(cacheKey, result, durationSeconds);

            return result;
        }

        static string GenerateCacheKey(string methodName, params object[] parameters)
        {
            string paramKey = string.Empty;
            
            foreach (var param in parameters)
            {
                if (param is int[] array)
                    paramKey += string.Join(",", array) + "|";
                else
                    paramKey += param.ToString() + "|";
            }

            return $"{methodName}:{paramKey}";
        }
    }
}
