using AddressBookSystem.Models;
using System.IO;

namespace AddressBookSystem.Utilities
{
    /// <summary>
    /// Stream-based operations for reading and writing contact data
    /// Demonstrates proper usage of Stream API without LINQ
    /// </summary>
    public class StreamOperations
    {
        /// <summary>
        /// Write persons to a stream in CSV format
        /// </summary>
        public static async Task WritePersonsToStreamAsync(Stream stream, List<Person> persons)
        {
            using (StreamWriter writer = new StreamWriter(stream, leaveOpen: true))
            {
                // Write header
                await writer.WriteLineAsync("FirstName,LastName,Address,City,State,Zip,PhoneNumber,Email");

                // Write data rows
                foreach (Person person in persons)
                {
                    string line = $"{EscapeValue(person.FirstName)}," +
                                  $"{EscapeValue(person.LastName)}," +
                                  $"{EscapeValue(person.Address)}," +
                                  $"{EscapeValue(person.City)}," +
                                  $"{EscapeValue(person.State)}," +
                                  $"{EscapeValue(person.Zip)}," +
                                  $"{EscapeValue(person.PhoneNumber)}," +
                                  $"{EscapeValue(person.Email)}";
                    await writer.WriteLineAsync(line);
                }
            }
        }

        /// <summary>
        /// Read persons from a stream in CSV format
        /// </summary>
        public static async Task<List<Person>> ReadPersonsFromStreamAsync(Stream stream)
        {
            List<Person> persons = new List<Person>();

            using (StreamReader reader = new StreamReader(stream, leaveOpen: true))
            {
                // Skip header line
                string? headerLine = await reader.ReadLineAsync();

                // Read data lines
                string? line;
                while ((line = await reader.ReadLineAsync()) != null)
                {
                    if (string.IsNullOrWhiteSpace(line))
                        continue;

                    Person? person = ParseCSVLine(line);
                    if (person != null)
                    {
                        persons.Add(person);
                    }
                }
            }

            return persons;
        }

        /// <summary>
        /// Write persons to file using FileStream
        /// </summary>
        public static async Task WritePersonsToFileAsync(string filePath, List<Person> persons)
        {
            try
            {
                string directory = Path.GetDirectoryName(filePath);
                if (!string.IsNullOrEmpty(directory) && !Directory.Exists(directory))
                {
                    Directory.CreateDirectory(directory);
                }

                using (FileStream fileStream = new FileStream(filePath, FileMode.Create, FileAccess.Write, FileShare.None, bufferSize: 4096, useAsync: true))
                {
                    await WritePersonsToStreamAsync(fileStream, persons);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error writing persons to file: {ex.Message}");
            }
        }

        /// <summary>
        /// Read persons from file using FileStream
        /// </summary>
        public static async Task<List<Person>> ReadPersonsFromFileAsync(string filePath)
        {
            List<Person> persons = new List<Person>();

            try
            {
                if (!File.Exists(filePath))
                {
                    return persons;
                }

                using (FileStream fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.Read, bufferSize: 4096, useAsync: true))
                {
                    persons = await ReadPersonsFromStreamAsync(fileStream);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error reading persons from file: {ex.Message}");
            }

            return persons;
        }

        /// <summary>
        /// Filter persons by a predicate using stream processing
        /// </summary>
        public static List<Person> FilterPersons(List<Person> persons, Func<Person, bool> predicate)
        {
            List<Person> filtered = new List<Person>();
            foreach (Person person in persons)
            {
                if (predicate(person))
                {
                    filtered.Add(person);
                }
            }
            return filtered;
        }

        /// <summary>
        /// Map persons using a transformation function
        /// </summary>
        public static List<T> MapPersons<T>(List<Person> persons, Func<Person, T> mapper)
        {
            List<T> mapped = new List<T>();
            foreach (Person person in persons)
            {
                mapped.Add(mapper(person));
            }
            return mapped;
        }

        /// <summary>
        /// Reduce persons to a single value
        /// </summary>
        public static T ReducePersons<T>(List<Person> persons, T initial, Func<T, Person, T> accumulator)
        {
            T result = initial;
            foreach (Person person in persons)
            {
                result = accumulator(result, person);
            }
            return result;
        }

        private static string EscapeValue(string value)
        {
            if (value.Contains(",") || value.Contains("\"") || value.Contains("\n"))
            {
                return $"\"{value.Replace("\"", "\"\"")}\"";
            }
            return value;
        }

        private static Person? ParseCSVLine(string line)
        {
            string[] parts = line.Split(',');
            if (parts.Length >= 8)
            {
                return new Person
                {
                    FirstName = parts[0].Trim(),
                    LastName = parts[1].Trim(),
                    Address = parts[2].Trim(),
                    City = parts[3].Trim(),
                    State = parts[4].Trim(),
                    Zip = parts[5].Trim(),
                    PhoneNumber = parts[6].Trim(),
                    Email = parts[7].Trim()
                };
            }
            return null;
        }
    }
}
