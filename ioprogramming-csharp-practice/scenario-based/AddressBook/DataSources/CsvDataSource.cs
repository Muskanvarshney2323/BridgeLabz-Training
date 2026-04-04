using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using AddressBook.Interfaces;
using AddressBook.Models;

namespace AddressBook.DataSources
{
    /// <summary>
    /// CSV Data Source implementation
    /// UC10: Read/Write Address Book with Persons Contact as CSV File
    /// </summary>
    public class CsvDataSource : IDataSource
    {
        private readonly string _dataPath;

        public CsvDataSource(string dataPath = "./Data")
        {
            _dataPath = dataPath;
            if (!Directory.Exists(_dataPath))
            {
                Directory.CreateDirectory(_dataPath);
            }
        }

        public string GetSourceName()
        {
            return "CSV File";
        }

        public bool IsAvailable()
        {
            return Directory.Exists(_dataPath);
        }

        public void Write(List<Contact> contacts, string addressBookName)
        {
            try
            {
                string filePath = Path.Combine(_dataPath, $"{addressBookName}.csv");

                using (StreamWriter writer = new StreamWriter(filePath, false, Encoding.UTF8))
                {
                    // Write header
                    writer.WriteLine("FirstName,LastName,Email,PhoneNumber,Address,City,State,Zip");

                    // Write data
                    foreach (Contact contact in contacts)
                    {
                        string line = $"{EscapeCsvField(contact.FirstName)}," +
                                    $"{EscapeCsvField(contact.LastName)}," +
                                    $"{EscapeCsvField(contact.Email)}," +
                                    $"{EscapeCsvField(contact.PhoneNumber)}," +
                                    $"{EscapeCsvField(contact.Address)}," +
                                    $"{EscapeCsvField(contact.City)}," +
                                    $"{EscapeCsvField(contact.State)}," +
                                    $"{EscapeCsvField(contact.Zip)}";
                        writer.WriteLine(line);
                    }
                }

                Console.WriteLine($"Successfully saved {contacts.Count} contacts to CSV: {filePath}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error writing to CSV: {ex.Message}");
                throw;
            }
        }

        public List<Contact> Read(string addressBookName)
        {
            List<Contact> contacts = new List<Contact>();

            try
            {
                string filePath = Path.Combine(_dataPath, $"{addressBookName}.csv");

                if (!File.Exists(filePath))
                {
                    Console.WriteLine($"CSV file not found: {filePath}");
                    return contacts;
                }

                using (StreamReader reader = new StreamReader(filePath, Encoding.UTF8))
                {
                    string headerLine = reader.ReadLine(); // Skip header
                    string line;

                    while ((line = reader.ReadLine()) != null)
                    {
                        if (string.IsNullOrWhiteSpace(line))
                            continue;

                        var fields = ParseCsvLine(line);
                        if (fields.Count >= 8)
                        {
                            Contact contact = new Contact(
                                fields[0],
                                fields[1],
                                fields[2],
                                fields[3],
                                fields[4],
                                fields[5],
                                fields[6],
                                fields[7]
                            );
                            contacts.Add(contact);
                        }
                    }
                }

                Console.WriteLine($"Successfully loaded {contacts.Count} contacts from CSV: {filePath}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error reading from CSV: {ex.Message}");
                throw;
            }

            return contacts;
        }

        private string EscapeCsvField(string field)
        {
            if (string.IsNullOrEmpty(field))
                return "";

            if (field.Contains(",") || field.Contains("\"") || field.Contains("\n"))
            {
                return "\"" + field.Replace("\"", "\"\"") + "\"";
            }

            return field;
        }

        private List<string> ParseCsvLine(string line)
        {
            List<string> fields = new List<string>();
            StringBuilder currentField = new StringBuilder();
            bool insideQuotes = false;

            for (int i = 0; i < line.Length; i++)
            {
                char c = line[i];

                if (c == '"')
                {
                    if (insideQuotes && i + 1 < line.Length && line[i + 1] == '"')
                    {
                        currentField.Append('"');
                        i++;
                    }
                    else
                    {
                        insideQuotes = !insideQuotes;
                    }
                }
                else if (c == ',' && !insideQuotes)
                {
                    fields.Add(currentField.ToString());
                    currentField.Clear();
                }
                else
                {
                    currentField.Append(c);
                }
            }

            fields.Add(currentField.ToString());
            return fields;
        }
    }
}
