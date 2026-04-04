using System;
using System.IO;

/// <summary>
/// Problem 3: Read User Input from Console
/// Asks user for name, age, and favorite programming language.
/// Saves information to a file using StreamWriter.
/// </summary>
class UserInputProgram
{
    static void Main(string[] args)
    {
        Console.WriteLine("╔════════════════════════════════════════════════════╗");
        Console.WriteLine("║     Read User Input from Console and Save         ║");
        Console.WriteLine("╚════════════════════════════════════════════════════╝\n");

        string outputFile = "user_profile.txt";

        try
        {
            // Read user input
            Console.WriteLine("=== Please Enter Your Information ===\n");

            Console.Write("Enter your name: ");
            string name = Console.ReadLine();

            Console.Write("Enter your age: ");
            string ageInput = Console.ReadLine();

            if (!int.TryParse(ageInput, out int age))
            {
                Console.WriteLine("✗ Invalid age input. Setting default age to 0.");
                age = 0;
            }

            Console.Write("Enter your favorite programming language: ");
            string language = Console.ReadLine();

            // Validate inputs
            if (string.IsNullOrWhiteSpace(name))
            {
                Console.WriteLine("✗ Name cannot be empty!");
                return;
            }

            if (string.IsNullOrWhiteSpace(language))
            {
                Console.WriteLine("✗ Programming language cannot be empty!");
                return;
            }

            // Write to file using StreamWriter
            Console.WriteLine($"\n=== Saving to File: {outputFile} ===");
            using (StreamWriter writer = new StreamWriter(outputFile))
            {
                writer.WriteLine("╔════════════════════════════════════════════════════╗");
                writer.WriteLine("║             USER PROFILE INFORMATION               ║");
                writer.WriteLine("╚════════════════════════════════════════════════════╝\n");
                writer.WriteLine($"Name: {name}");
                writer.WriteLine($"Age: {age}");
                writer.WriteLine($"Favorite Language: {language}");
                writer.WriteLine($"Timestamp: {DateTime.Now}");
            }

            Console.WriteLine($"✓ Profile saved successfully to: {outputFile}\n");

            // Display saved content
            Console.WriteLine("=== Saved Content ===");
            using (StreamReader reader = new StreamReader(outputFile))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    Console.WriteLine(line);
                }
            }
        }
        catch (IOException ex)
        {
            Console.WriteLine($"✗ IO Exception: {ex.Message}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"✗ Error: {ex.Message}");
        }
    }
}
