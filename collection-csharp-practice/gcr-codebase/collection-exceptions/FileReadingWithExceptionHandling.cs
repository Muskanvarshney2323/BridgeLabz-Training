using System;
using System.IO;

namespace CollectionExceptions
{
    class FileReadingWithExceptionHandling
    {
        static void Main()
        {
            string fileName = "data.txt";

            try
            {
                // Attempt to read the file
                string[] lines = File.ReadAllLines(fileName);

                // Print the contents if file exists
                Console.WriteLine("File contents:");
                foreach (string line in lines)
                {
                    Console.WriteLine(line);
                }
            }
            catch (IOException)
            {
                // Handle IOException when file does not exist
                Console.WriteLine("File not found");
            }
            catch (Exception ex)
            {
                // Handle any other unexpected exceptions
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
        }
    }
}
