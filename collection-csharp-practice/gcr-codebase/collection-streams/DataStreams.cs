using System;
using System.IO;

/// <summary>
/// Problem 7: Data Streams - Store and Retrieve Primitive Data
/// Stores student details (roll number, name, GPA) in a binary file
/// and retrieves it later using BinaryWriter and BinaryReader.
/// </summary>
class DataStreamsProgram
{
    const string BINARY_FILE = "students.dat";

    static void Main(string[] args)
    {
        Console.WriteLine("╔════════════════════════════════════════════════════╗");
        Console.WriteLine("║   Data Streams - Store and Retrieve Primitive Data║");
        Console.WriteLine("╚════════════════════════════════════════════════════╝\n");

        try
        {
            // Write student data to binary file
            Console.WriteLine("=== Writing Student Data to Binary File ===");
            WriteStudentData();
            Console.WriteLine($"✓ Data written to: {BINARY_FILE}\n");

            // Read student data from binary file
            Console.WriteLine("=== Reading Student Data from Binary File ===");
            ReadStudentData();
            Console.WriteLine("\n✓ Data retrieved successfully!");
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
            if (File.Exists(BINARY_FILE)) File.Delete(BINARY_FILE);
        }
    }

    static void WriteStudentData()
    {
        using (FileStream fs = new FileStream(BINARY_FILE, FileMode.Create, FileAccess.Write))
        using (BinaryWriter writer = new BinaryWriter(fs))
        {
            // Sample student data
            var students = new[]
            {
                (101, "Rajesh Kumar", 3.8),
                (102, "Priya Singh", 3.9),
                (103, "Amit Patel", 3.7),
                (104, "Neha Sharma", 3.6),
                (105, "Vikram Gupta", 3.5)
            };

            // Write total number of students
            writer.Write(students.Length);
            Console.WriteLine($"Writing {students.Length} student records...");

            // Write each student's data
            foreach (var (rollNumber, name, gpa) in students)
            {
                writer.Write(rollNumber);      // Write roll number (int)
                writer.Write(name);             // Write name (string)
                writer.Write(gpa);              // Write GPA (double)

                Console.WriteLine($"  ✓ Written - RollNo: {rollNumber}, Name: {name}, GPA: {gpa}");
            }
        }
    }

    static void ReadStudentData()
    {
        using (FileStream fs = new FileStream(BINARY_FILE, FileMode.Open, FileAccess.Read))
        using (BinaryReader reader = new BinaryReader(fs))
        {
            // Read total number of students
            int studentCount = reader.ReadInt32();
            Console.WriteLine($"Reading {studentCount} student records...\n");

            for (int i = 0; i < studentCount; i++)
            {
                // Read each student's data
                int rollNumber = reader.ReadInt32();
                string name = reader.ReadString();
                double gpa = reader.ReadDouble();

                Console.WriteLine($"  Student {i + 1}:");
                Console.WriteLine($"    Roll Number: {rollNumber}");
                Console.WriteLine($"    Name: {name}");
                Console.WriteLine($"    GPA: {gpa:F2}");
            }
        }
    }
}
