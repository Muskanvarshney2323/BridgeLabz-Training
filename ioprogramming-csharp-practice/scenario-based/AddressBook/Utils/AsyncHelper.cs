using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace AddressBook.Utils
{
    /// <summary>
    /// AsyncHelper for non-blocking IO operations
    /// UC17: Ensures IO operations don't block the main thread during CRUD operations
    /// Uses multithreading and delegates for async operations
    /// </summary>
    public class AsyncHelper
    {
        public delegate void AsyncOperation();
        public delegate T AsyncOperationWithReturn<T>();
        public delegate void AsyncCallback(bool success, string message);

        private static HashSet<int> _activeAsyncOperations = new HashSet<int>();
        private static object _lockObject = new object();

        /// <summary>
        /// Executes an asynchronous operation on a background thread
        /// </summary>
        public static void ExecuteAsync(AsyncOperation operation, AsyncCallback callback = null)
        {
            ThreadPool.QueueUserWorkItem(state =>
            {
                try
                {
                    operation?.Invoke();
                    callback?.Invoke(true, "Operation completed successfully");
                }
                catch (Exception ex)
                {
                    callback?.Invoke(false, $"Error: {ex.Message}");
                }
            });
        }

        /// <summary>
        /// Executes an asynchronous operation and returns a result
        /// </summary>
        public static void ExecuteAsync<T>(AsyncOperationWithReturn<T> operation, 
            Action<bool, string, T> callback = null)
        {
            ThreadPool.QueueUserWorkItem(state =>
            {
                try
                {
                    T result = operation != null ? operation() : default(T);
                    callback?.Invoke(true, "Operation completed successfully", result);
                }
                catch (Exception ex)
                {
                    callback?.Invoke(false, $"Error: {ex.Message}", default(T));
                }
            });
        }

        /// <summary>
        /// Waits for an asynchronous operation to complete
        /// </summary>
        public static void WaitForAsyncOperation(int timeoutMs = 30000)
        {
            Thread.Sleep(100); // Give thread time to start
            int elapsedMs = 0;
            while (_activeAsyncOperations.Count > 0 && elapsedMs < timeoutMs)
            {
                Thread.Sleep(100);
                elapsedMs += 100;
            }
        }

        /// <summary>
        /// Task-based async execution for modern async/await patterns
        /// </summary>
        public static async Task ExecuteAsyncTask(Func<Task> operation)
        {
            try
            {
                await operation();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Async operation failed: {ex.Message}");
            }
        }

        /// <summary>
        /// Task-based async execution with return value
        /// </summary>
        public static async Task<T> ExecuteAsyncTask<T>(Func<Task<T>> operation)
        {
            try
            {
                return await operation();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Async operation failed: {ex.Message}");
                return default(T);
            }
        }
    }
}
