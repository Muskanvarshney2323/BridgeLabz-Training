using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text.Json;

[Serializable]
public class Employee
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Department { get; set; }
    public decimal Salary { get; set; }

    public Employee(int id, string name, string department, decimal salary)
    {
        Id = id;
        Name = name;
        Department = department;
        Salary = salary;
    }

    public override string ToString()
    {
        return $"ID: {Id}, Name: {Name}, Department: {Department}, Salary: Rs.{Salary}";
    }
}

/// <summary>
/// Problem 4: Serialization - Save and Retrieve an Object
/// Stores a list of employees using both Binary and JSON serialization.
/// Deserializes and displays the employees from the file.
/// </summary>
class SerializationProgram
{
    static void Main(string[] args)
    {
        Console.WriteLine("╔════════════════════════════════════════════════════╗");
        Console.WriteLine("║    Serialization - Save and Retrieve Objects      ║");
        Console.WriteLine("╚════════════════════════════════════════════════════╝\n");

        string binaryFile = "employees.bin";
        string jsonFile = "employees.json";

        try
        {
            // Create sample employee list
            List<Employee> employees = new List<Employee>
            {
                new Employee(101, "Rajesh Kumar", "IT", 50000),
                new Employee(102, "Priya Singh", "HR", 45000),
                new Employee(103, "Amit Patel", "Finance", 55000),
                new Employee(104, "Neha Sharma", "IT", 52000),
                new Employee(105, "Vikram Gupta", "Sales", 48000)
            };

            Console.WriteLine("=== Original Employee List ===");
            foreach (var emp in employees)
            {
                Console.WriteLine(emp);
            }

            // Serialize to Binary file
            Console.WriteLine($"\n=== Serializing to Binary File: {binaryFile} ===");
            SerializeToBinary(binaryFile, employees);
            Console.WriteLine("✓ Employees serialized to binary file successfully!");

            // Deserialize from Binary file
            Console.WriteLine("\n=== Deserializing from Binary File ===");
            List<Employee> deserializedFromBinary = DeserializeFromBinary(binaryFile);
            Console.WriteLine("✓ Employees deserialized from binary file:");
            foreach (var emp in deserializedFromBinary)
            {
                Console.WriteLine(emp);
            }

            // Serialize to JSON file
            Console.WriteLine($"\n=== Serializing to JSON File: {jsonFile} ===");
            SerializeToJson(jsonFile, employees);
            Console.WriteLine("✓ Employees serialized to JSON file successfully!");

            // Deserialize from JSON file
            Console.WriteLine("\n=== Deserializing from JSON File ===");
            List<Employee> deserializedFromJson = DeserializeFromJson(jsonFile);
            Console.WriteLine("✓ Employees deserialized from JSON file:");
            foreach (var emp in deserializedFromJson)
            {
                Console.WriteLine(emp);
            }

            Console.WriteLine("\n✓ Serialization and Deserialization completed successfully!");
        }
        catch (FileNotFoundException ex)
        {
            Console.WriteLine($"✗ File not found: {ex.Message}");
        }
        catch (IOException ex)
        {
            Console.WriteLine($"✗ IO Exception: {ex.Message}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"✗ Error: {ex.Message}");
        }
        finally
        {
            // Cleanup
            if (File.Exists(binaryFile)) File.Delete(binaryFile);
            if (File.Exists(jsonFile)) File.Delete(jsonFile);
        }
    }

    static void SerializeToBinary(string filePath, List<Employee> employees)
    {
        using (FileStream fs = new FileStream(filePath, FileMode.Create))
        {
#pragma warning disable SYSLIB0011
            BinaryFormatter formatter = new BinaryFormatter();
            formatter.Serialize(fs, employees);
#pragma warning restore SYSLIB0011
        }
    }

    static List<Employee> DeserializeFromBinary(string filePath)
    {
        List<Employee> employees = new List<Employee>();

        using (FileStream fs = new FileStream(filePath, FileMode.Open))
        {
#pragma warning disable SYSLIB0011
            BinaryFormatter formatter = new BinaryFormatter();
            employees = (List<Employee>)formatter.Deserialize(fs);
#pragma warning restore SYSLIB0011
        }

        return employees;
    }

    static void SerializeToJson(string filePath, List<Employee> employees)
    {
        var options = new JsonSerializerOptions { WriteIndented = true };
        string jsonContent = JsonSerializer.Serialize(employees, options);
        File.WriteAllText(filePath, jsonContent);
    }

    static List<Employee> DeserializeFromJson(string filePath)
    {
        string jsonContent = File.ReadAllText(filePath);
        List<Employee> employees = JsonSerializer.Deserialize<List<Employee>>(jsonContent);
        return employees;
    }
}
