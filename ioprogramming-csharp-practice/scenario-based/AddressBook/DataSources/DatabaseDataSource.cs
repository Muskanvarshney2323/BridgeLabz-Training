using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using AddressBook.Interfaces;
using AddressBook.Models;

namespace AddressBook.DataSources
{
    /// <summary>
    /// Database Data Source implementation
    /// UC16: Save the AddressBook to Database
    /// Follows Open/Closed Principle - extensible for different database providers
    /// </summary>
    public class DatabaseDataSource : IDataSource
    {
        private readonly string _connectionString;

        public DatabaseDataSource(string connectionString = "Server=.;Database=AddressBookDB;Integrated Security=true;")
        {
            _connectionString = connectionString;
        }

        public string GetSourceName()
        {
            return "SQL Database";
        }

        public bool IsAvailable()
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
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
                Console.WriteLine($"Saving to Database: {addressBookName}");

                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    connection.Open();

                    // Create tables if they don't exist
                    CreateTablesIfNotExist(connection);

                    // Delete existing records for this address book (if updating)
                    using (SqlCommand deleteCmd = new SqlCommand(
                        "DELETE FROM Contacts WHERE AddressBookId IN (SELECT Id FROM AddressBooks WHERE Name = @Name)", 
                        connection))
                    {
                        deleteCmd.Parameters.AddWithValue("@Name", addressBookName);
                        deleteCmd.ExecuteNonQuery();
                    }

                    // Get or create address book
                    int addressBookId = GetOrCreateAddressBook(connection, addressBookName);

                    // Insert contacts
                    foreach (Contact contact in contacts)
                    {
                        using (SqlCommand insertCmd = new SqlCommand(
                            "INSERT INTO Contacts (AddressBookId, FirstName, LastName, Email, PhoneNumber, Address, City, State, Zip) " +
                            "VALUES (@AddressBookId, @FirstName, @LastName, @Email, @PhoneNumber, @Address, @City, @State, @Zip)",
                            connection))
                        {
                            insertCmd.Parameters.AddWithValue("@AddressBookId", addressBookId);
                            insertCmd.Parameters.AddWithValue("@FirstName", contact.FirstName ?? "");
                            insertCmd.Parameters.AddWithValue("@LastName", contact.LastName ?? "");
                            insertCmd.Parameters.AddWithValue("@Email", contact.Email ?? "");
                            insertCmd.Parameters.AddWithValue("@PhoneNumber", contact.PhoneNumber ?? "");
                            insertCmd.Parameters.AddWithValue("@Address", contact.Address ?? "");
                            insertCmd.Parameters.AddWithValue("@City", contact.City ?? "");
                            insertCmd.Parameters.AddWithValue("@State", contact.State ?? "");
                            insertCmd.Parameters.AddWithValue("@Zip", contact.Zip ?? "");

                            insertCmd.ExecuteNonQuery();
                        }
                    }

                    Console.WriteLine($"Successfully saved {contacts.Count} contacts to database");
                }
            }
            catch (SqlException sqlEx)
            {
                Console.WriteLine($"Database error: {sqlEx.Message}");
                Console.WriteLine("Make sure SQL Server is running and the connection string is correct.");
                throw;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error writing to database: {ex.Message}");
                throw;
            }
        }

        public List<Contact> Read(string addressBookName)
        {
            List<Contact> contacts = new List<Contact>();

            try
            {
                Console.WriteLine($"Loading from Database: {addressBookName}");

                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    connection.Open();

                    string query = @"
                        SELECT c.FirstName, c.LastName, c.Email, c.PhoneNumber, c.Address, c.City, c.State, c.Zip
                        FROM Contacts c
                        INNER JOIN AddressBooks ab ON c.AddressBookId = ab.Id
                        WHERE ab.Name = @Name";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Name", addressBookName);

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Contact contact = new Contact(
                                    reader["FirstName"].ToString(),
                                    reader["LastName"].ToString(),
                                    reader["Email"].ToString(),
                                    reader["PhoneNumber"].ToString(),
                                    reader["Address"].ToString(),
                                    reader["City"].ToString(),
                                    reader["State"].ToString(),
                                    reader["Zip"].ToString()
                                );
                                contacts.Add(contact);
                            }
                        }
                    }
                }

                Console.WriteLine($"Successfully loaded {contacts.Count} contacts from database");
            }
            catch (SqlException sqlEx)
            {
                Console.WriteLine($"Database error: {sqlEx.Message}");
                Console.WriteLine("Make sure SQL Server is running and the database exists.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error reading from database: {ex.Message}");
            }

            return contacts;
        }

        private void CreateTablesIfNotExist(SqlConnection connection)
        {
            string createTablesScript = @"
                IF NOT EXISTS (SELECT * FROM sys.tables WHERE name='AddressBooks')
                BEGIN
                    CREATE TABLE AddressBooks (
                        Id INT PRIMARY KEY IDENTITY(1,1),
                        Name NVARCHAR(100) UNIQUE NOT NULL,
                        CreatedAt DATETIME DEFAULT GETDATE()
                    )
                END

                IF NOT EXISTS (SELECT * FROM sys.tables WHERE name='Contacts')
                BEGIN
                    CREATE TABLE Contacts (
                        Id INT PRIMARY KEY IDENTITY(1,1),
                        AddressBookId INT NOT NULL,
                        FirstName NVARCHAR(50) NOT NULL,
                        LastName NVARCHAR(50) NOT NULL,
                        Email NVARCHAR(100),
                        PhoneNumber NVARCHAR(15),
                        Address NVARCHAR(200),
                        City NVARCHAR(50),
                        State NVARCHAR(50),
                        Zip NVARCHAR(10),
                        CreatedAt DATETIME DEFAULT GETDATE(),
                        FOREIGN KEY (AddressBookId) REFERENCES AddressBooks(Id)
                    )
                END";

            using (SqlCommand command = new SqlCommand(createTablesScript, connection))
            {
                try
                {
                    command.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Warning: Could not create tables: {ex.Message}");
                }
            }
        }

        private int GetOrCreateAddressBook(SqlConnection connection, string addressBookName)
        {
            // Try to get existing address book
            using (SqlCommand selectCmd = new SqlCommand(
                "SELECT Id FROM AddressBooks WHERE Name = @Name",
                connection))
            {
                selectCmd.Parameters.AddWithValue("@Name", addressBookName);
                object result = selectCmd.ExecuteScalar();

                if (result != null && int.TryParse(result.ToString(), out int id))
                {
                    return id;
                }
            }

            // Create new address book
            using (SqlCommand insertCmd = new SqlCommand(
                "INSERT INTO AddressBooks (Name) VALUES (@Name); SELECT SCOPE_IDENTITY()",
                connection))
            {
                insertCmd.Parameters.AddWithValue("@Name", addressBookName);
                object result = insertCmd.ExecuteScalar();

                if (result != null && int.TryParse(result.ToString(), out int newId))
                {
                    return newId;
                }
            }

            throw new Exception("Failed to create address book in database");
        }
    }
}
