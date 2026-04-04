using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using AddressBook.Interfaces;
using AddressBook.Models;

namespace AddressBook.DataSources
{
    /// <summary>
    /// JSON File Data Source implementation
    /// UC12: Read/Write the Address Book with Persons Contact as JSON File
    /// Manual JSON serialization without using external libraries
    /// </summary>
    public class JsonFileDataSource : IDataSource
    {
        private readonly string _dataPath;

        public JsonFileDataSource(string dataPath = "./Data")
        {
            _dataPath = dataPath;
            if (!Directory.Exists(_dataPath))
            {
                Directory.CreateDirectory(_dataPath);
            }
        }

        public string GetSourceName()
        {
            return "JSON File";
        }

        public bool IsAvailable()
        {
            return Directory.Exists(_dataPath);
        }

        public void Write(List<Contact> contacts, string addressBookName)
        {
            try
            {
                string filePath = Path.Combine(_dataPath, $"{addressBookName}.json");

                StringBuilder jsonBuilder = new StringBuilder();
                jsonBuilder.AppendLine("{");
                jsonBuilder.AppendLine($"  \"addressBook\": \"{EscapeJson(addressBookName)}\",");
                jsonBuilder.AppendLine($"  \"count\": {contacts.Count},");
                jsonBuilder.AppendLine("  \"contacts\": [");

                for (int i = 0; i < contacts.Count; i++)
                {
                    Contact contact = contacts[i];
                    jsonBuilder.AppendLine("    {");
                    jsonBuilder.AppendLine($"      \"firstName\": \"{EscapeJson(contact.FirstName)}\",");
                    jsonBuilder.AppendLine($"      \"lastName\": \"{EscapeJson(contact.LastName)}\",");
                    jsonBuilder.AppendLine($"      \"email\": \"{EscapeJson(contact.Email)}\",");
                    jsonBuilder.AppendLine($"      \"phoneNumber\": \"{EscapeJson(contact.PhoneNumber)}\",");
                    jsonBuilder.AppendLine($"      \"address\": \"{EscapeJson(contact.Address)}\",");
                    jsonBuilder.AppendLine($"      \"city\": \"{EscapeJson(contact.City)}\",");
                    jsonBuilder.AppendLine($"      \"state\": \"{EscapeJson(contact.State)}\",");
                    jsonBuilder.AppendLine($"      \"zip\": \"{EscapeJson(contact.Zip)}\"");
                    jsonBuilder.Append("    }");

                    if (i < contacts.Count - 1)
                        jsonBuilder.AppendLine(",");
                    else
                        jsonBuilder.AppendLine();
                }

                jsonBuilder.AppendLine("  ]");
                jsonBuilder.AppendLine("}");

                File.WriteAllText(filePath, jsonBuilder.ToString(), Encoding.UTF8);
                Console.WriteLine($"Successfully saved {contacts.Count} contacts to JSON: {filePath}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error writing to JSON: {ex.Message}");
                throw;
            }
        }

        public List<Contact> Read(string addressBookName)
        {
            List<Contact> contacts = new List<Contact>();

            try
            {
                string filePath = Path.Combine(_dataPath, $"{addressBookName}.json");

                if (!File.Exists(filePath))
                {
                    Console.WriteLine($"JSON file not found: {filePath}");
                    return contacts;
                }

                string jsonContent = File.ReadAllText(filePath, Encoding.UTF8);
                contacts = ParseJsonContacts(jsonContent);

                Console.WriteLine($"Successfully loaded {contacts.Count} contacts from JSON: {filePath}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error reading from JSON: {ex.Message}");
                throw;
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
                .Replace("\r", "\\r")
                .Replace("\t", "\\t");
        }

        private List<Contact> ParseJsonContacts(string jsonContent)
        {
            List<Contact> contacts = new List<Contact>();

            try
            {
                // Find contacts array
                int contactsStart = jsonContent.IndexOf("\"contacts\":");
                int arrayStart = jsonContent.IndexOf("[", contactsStart);
                int arrayEnd = jsonContent.LastIndexOf("]");

                if (arrayStart > 0 && arrayEnd > arrayStart)
                {
                    string contactsArray = jsonContent.Substring(arrayStart + 1, arrayEnd - arrayStart - 1);

                    // Split by contact objects
                    int braceCount = 0;
                    int objectStart = -1;

                    for (int i = 0; i < contactsArray.Length; i++)
                    {
                        if (contactsArray[i] == '{')
                        {
                            if (braceCount == 0)
                                objectStart = i;
                            braceCount++;
                        }
                        else if (contactsArray[i] == '}')
                        {
                            braceCount--;
                            if (braceCount == 0 && objectStart >= 0)
                            {
                                string contactJson = contactsArray.Substring(objectStart, i - objectStart + 1);
                                Contact contact = ParseSingleContact(contactJson);
                                if (contact != null)
                                    contacts.Add(contact);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error parsing JSON: {ex.Message}");
            }

            return contacts;
        }

        private Contact ParseSingleContact(string contactJson)
        {
            try
            {
                var fields = new Dictionary<string, string>();

                // Extract field values
                ExtractJsonField("firstName", contactJson, fields);
                ExtractJsonField("lastName", contactJson, fields);
                ExtractJsonField("email", contactJson, fields);
                ExtractJsonField("phoneNumber", contactJson, fields);
                ExtractJsonField("address", contactJson, fields);
                ExtractJsonField("city", contactJson, fields);
                ExtractJsonField("state", contactJson, fields);
                ExtractJsonField("zip", contactJson, fields);

                if (fields.ContainsKey("firstName") && fields.ContainsKey("lastName"))
                {
                    return new Contact(
                        UnescapeJson(fields["firstName"]),
                        UnescapeJson(fields["lastName"]),
                        UnescapeJson(fields.GetValueOrDefault("email", "")),
                        UnescapeJson(fields.GetValueOrDefault("phoneNumber", "")),
                        UnescapeJson(fields.GetValueOrDefault("address", "")),
                        UnescapeJson(fields.GetValueOrDefault("city", "")),
                        UnescapeJson(fields.GetValueOrDefault("state", "")),
                        UnescapeJson(fields.GetValueOrDefault("zip", ""))
                    );
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error parsing contact: {ex.Message}");
            }

            return null;
        }

        private void ExtractJsonField(string fieldName, string jsonObject, Dictionary<string, string> fields)
        {
            string searchPattern = $"\"{fieldName}\":";
            int startIndex = jsonObject.IndexOf(searchPattern);

            if (startIndex >= 0)
            {
                startIndex = jsonObject.IndexOf("\"", startIndex + searchPattern.Length) + 1;
                int endIndex = jsonObject.IndexOf("\"", startIndex);

                if (endIndex > startIndex)
                {
                    string value = jsonObject.Substring(startIndex, endIndex - startIndex);
                    fields[fieldName] = value;
                }
            }
        }

        private string UnescapeJson(string value)
        {
            if (string.IsNullOrEmpty(value))
                return "";

            return value
                .Replace("\\n", "\n")
                .Replace("\\r", "\r")
                .Replace("\\t", "\t")
                .Replace("\\\"", "\"")
                .Replace("\\\\", "\\");
        }
    }
}
