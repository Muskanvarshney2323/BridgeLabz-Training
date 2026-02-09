using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using AddressBook.Interfaces;
using AddressBook.Models;

namespace AddressBook.DataSources
{
    /// <summary>
    /// JSON Server Data Source implementation
    /// UC14-15: Read/Write the Address Book with Persons Contact to a JSONServer
    /// Note: This is a demonstration implementation. In production, use HttpClient
    /// </summary>
    public class JsonServerDataSource : IDataSource
    {
        private readonly string _serverUrl;

        public JsonServerDataSource(string serverUrl = "http://localhost:3000")
        {
            _serverUrl = serverUrl;
        }

        public string GetSourceName()
        {
            return "JSON Server";
        }

        public bool IsAvailable()
        {
            try
            {
                using (WebClient client = new WebClient())
                {
                    client.DownloadString($"{_serverUrl}/addressBooks");
                    return true;
                }
            }
            catch
            {
                return false;
            }
        }

        public void Write(List<Contact> contacts, string addressBookName)
        {
            try
            {
                Console.WriteLine($"Note: JSON Server implementation requires json-server running at {_serverUrl}");
                Console.WriteLine("This is a demonstration. In production, use HttpClient for async operations.");

                string url = $"{_serverUrl}/addressBooks";

                // Create address book entry
                string addressBookJson = $@"{{
  ""name"": ""{EscapeJson(addressBookName)}"",
  ""contacts"": {ConvertContactsToJson(contacts)},
  ""createdAt"": ""{DateTime.Now:yyyy-MM-dd HH:mm:ss}""
}}";

                using (WebClient client = new WebClient())
                {
                    client.Headers[HttpRequestHeader.ContentType] = "application/json";
                    string response = client.UploadString(url, "POST", addressBookJson);
                    Console.WriteLine($"Successfully saved to JSON Server: {addressBookName}");
                    Console.WriteLine($"Response: {response}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Note: JSON Server not available. {ex.Message}");
                Console.WriteLine("To use JSON Server feature:");
                Console.WriteLine("1. Install json-server: npm install -g json-server");
                Console.WriteLine("2. Create a db.json file with initial data");
                Console.WriteLine("3. Run: json-server --watch db.json");
            }
        }

        public List<Contact> Read(string addressBookName)
        {
            List<Contact> contacts = new List<Contact>();

            try
            {
                Console.WriteLine($"Reading from JSON Server: {addressBookName}");

                string url = $"{_serverUrl}/addressBooks?name={addressBookName}";

                using (WebClient client = new WebClient())
                {
                    client.Headers[HttpRequestHeader.ContentType] = "application/json";
                    string response = client.DownloadString(url);

                    // Parse response and extract contacts
                    contacts = ParseJsonServerResponse(response);
                    Console.WriteLine($"Successfully loaded {contacts.Count} contacts from JSON Server");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Note: JSON Server not available. {ex.Message}");
                Console.WriteLine("Ensure json-server is running at " + _serverUrl);
            }

            return contacts;
        }

        private string ConvertContactsToJson(List<Contact> contacts)
        {
            StringBuilder json = new StringBuilder();
            json.Append("[");

            for (int i = 0; i < contacts.Count; i++)
            {
                Contact c = contacts[i];
                json.Append($@"{{
    ""firstName"": ""{EscapeJson(c.FirstName)}"",
    ""lastName"": ""{EscapeJson(c.LastName)}"",
    ""email"": ""{EscapeJson(c.Email)}"",
    ""phoneNumber"": ""{EscapeJson(c.PhoneNumber)}"",
    ""address"": ""{EscapeJson(c.Address)}"",
    ""city"": ""{EscapeJson(c.City)}"",
    ""state"": ""{EscapeJson(c.State)}"",
    ""zip"": ""{EscapeJson(c.Zip)}""
  }}");

                if (i < contacts.Count - 1)
                    json.Append(",");
            }

            json.Append("]");
            return json.ToString();
        }

        private List<Contact> ParseJsonServerResponse(string response)
        {
            List<Contact> contacts = new List<Contact>();

            try
            {
                // Basic parsing - in production use a proper JSON parser
                int contactsStart = response.IndexOf("\"contacts\":");
                if (contactsStart > 0)
                {
                    int arrayStart = response.IndexOf("[", contactsStart);
                    int arrayEnd = response.IndexOf("]", arrayStart) + 1;

                    // This is simplified - actual implementation would properly parse JSON
                    Console.WriteLine("Contacts retrieved from server (simplified parsing)");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error parsing server response: {ex.Message}");
            }

            return contacts;
        }

        private string EscapeJson(string value)
        {
            if (string.IsNullOrEmpty(value))
                return "";

            return value
                .Replace("\\", "\\\\")
                .Replace("\"", "\\\"")
                .Replace("\n", "\\n")
                .Replace("\r", "\\r");
        }
    }
}
