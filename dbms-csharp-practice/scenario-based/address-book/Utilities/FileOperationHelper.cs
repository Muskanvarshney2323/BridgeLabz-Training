using AddressBookSystem.Models;
using System.Text.Json.Serialization;
using System.Text.Json;
using System.Net.Http;
using System.Net.Http.Json;

namespace AddressBookSystem.Utilities
{
    public class FileOperationHelper
    {
        public static async Task<List<Person>> ReadCSVAsync(string filePath)
        {
            List<Person> persons = new List<Person>();

            try
            {
                if (!File.Exists(filePath))
                    return persons;

                string[] lines = await File.ReadAllLinesAsync(filePath);

                foreach (var line in lines.Skip(1))
                {
                    if (string.IsNullOrWhiteSpace(line)) continue;

                    string[] parts = line.Split(',');
                    if (parts.Length >= 8)
                    {
                        persons.Add(new Person
                        {
                            FirstName = parts[0].Trim(),
                            LastName = parts[1].Trim(),
                            Address = parts[2].Trim(),
                            City = parts[3].Trim(),
                            State = parts[4].Trim(),
                            Zip = parts[5].Trim(),
                            PhoneNumber = parts[6].Trim(),
                            Email = parts[7].Trim()
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error reading CSV: {ex.Message}");
            }

            return persons;
        }

        public static async Task WriteCSVAsync(string filePath, List<Person> persons)
        {
            try
            {
                string directory = Path.GetDirectoryName(filePath);
                if (!string.IsNullOrEmpty(directory) && !Directory.Exists(directory))
                {
                    Directory.CreateDirectory(directory);
                }

                List<string> lines = new List<string>
                {
                    "FirstName,LastName,Address,City,State,Zip,PhoneNumber,Email"
                };

                foreach (var person in persons)
                {
                    lines.Add($"{person.FirstName},{person.LastName},{person.Address},{person.City}," +
                             $"{person.State},{person.Zip},{person.PhoneNumber},{person.Email}");
                }

                await File.WriteAllLinesAsync(filePath, lines);
                Console.WriteLine("Successfully saved to CSV file.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error writing CSV: {ex.Message}");
            }
        }

        public static async Task<List<Person>> ReadJSONAsync(string filePath)
        {
            List<Person> persons = new List<Person>();

            try
            {
                if (!File.Exists(filePath))
                    return persons;

                string json = await File.ReadAllTextAsync(filePath);
                var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                persons = JsonSerializer.Deserialize<List<Person>>(json, options) ?? new List<Person>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error reading JSON file: {ex.Message}");
            }

            return persons;
        }

        public static async Task WriteJSONAsync(string filePath, List<Person> persons)
        {
            try
            {
                string directory = Path.GetDirectoryName(filePath);
                if (!string.IsNullOrEmpty(directory) && !Directory.Exists(directory))
                {
                    Directory.CreateDirectory(directory);
                }

                var options = new JsonSerializerOptions { WriteIndented = true };
                string json = JsonSerializer.Serialize(persons, options);
                await File.WriteAllTextAsync(filePath, json);
                Console.WriteLine("Successfully saved to JSON file.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error writing JSON file: {ex.Message}");
            }
        }

        public static async Task<List<Person>> ReadFromJsonServerAsync(string serverUrl)
        {
            List<Person> persons = new List<Person>();

            try
            {
                using (HttpClient client = new HttpClient())
                {
                    HttpResponseMessage response = await client.GetAsync(serverUrl);
                    if (response.IsSuccessStatusCode)
                    {
                        string jsonContent = await response.Content.ReadAsStringAsync();
                        var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                        persons = JsonSerializer.Deserialize<List<Person>>(jsonContent, options) ?? new List<Person>();
                        Console.WriteLine("Successfully loaded from JSON Server.");
                    }
                    else
                    {
                        Console.WriteLine($"Error fetching from server: {response.StatusCode}");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error reading from JSON Server: {ex.Message}");
            }

            return persons;
        }

        public static async Task WriteToJsonServerAsync(string serverUrl, List<Person> persons)
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    var options = new JsonSerializerOptions { WriteIndented = true };
                    string json = JsonSerializer.Serialize(persons, options);

                    StringContent content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");
                    HttpResponseMessage response = await client.PostAsync(serverUrl, content);
 
                    if (response.IsSuccessStatusCode)
                    {
                        Console.WriteLine("Successfully saved to JSON Server.");
                    }
                    else
                    {
                        Console.WriteLine($"Error saving to server: {response.StatusCode}");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error writing to JSON Server: {ex.Message}");
            }
        }
    }
}
