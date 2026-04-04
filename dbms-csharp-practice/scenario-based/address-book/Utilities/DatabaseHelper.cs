using AddressBookSystem.Models;
using System.Data;
using System.Reflection;

namespace AddressBookSystem.Utilities
{
    public class DatabaseHelper
    {
        private string _connectionString;

        public DatabaseHelper(string connectionString = "DefaultConnection")
        {
            _connectionString = connectionString;
        }

        public async Task<List<Person>> ReadFromDatabaseAsync(string addressBookName)
        {
            List<Person> persons = new List<Person>();

            try
            {
                // Simulating asynchronous database read
                // In a real scenario, you would use ADO.NET or EF Core here
                await Task.Delay(100); // Simulate async database operation

                Console.WriteLine($"Successfully loaded contacts from database for Address Book: {addressBookName}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error reading from database: {ex.Message}");
            }

            return persons;
        }

        public async Task WriteToDatabaseAsync(string addressBookName, List<Person> persons)
        {
            try
            {
                // Simulating asynchronous database write
                // In a real scenario, you would use ADO.NET or EF Core here
                await Task.Delay(100); // Simulate async database operation

                Console.WriteLine($"Successfully saved {persons.Count} contacts to database for Address Book: {addressBookName}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error writing to database: {ex.Message}");
            }
        }

        public static string GetSimpleTypeInfo(Type type)
        {
            return type.Name;
        }

        public static object? GetPropertyValue(object obj, string propertyName)
        {
            PropertyInfo? property = obj.GetType().GetProperty(propertyName, 
                BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);
            
            return property?.GetValue(obj);
        }

        public static void SetPropertyValue(object obj, string propertyName, object value)
        {
            PropertyInfo? property = obj.GetType().GetProperty(propertyName, 
                BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);
            
            property?.SetValue(obj, value);
        }
    }
}
