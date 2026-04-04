using System;
using System.IO;

/// <summary>
/// Problem 5: ByteArray Stream - Convert Image to ByteArray
/// Reads an image file into a byte array, writes it back,
/// and verifies both files are identical.
/// </summary>
class ImageByteArrayProgram
{
    static void Main()
    {
        Console.WriteLine("╔════════════════════════════════════════════════════╗");
        Console.WriteLine("║   ByteArray Stream - Convert Image to ByteArray   ║");
        Console.WriteLine("╚════════════════════════════════════════════════════╝\n");

        string sourceImage = "source_image.bin";
        string destImage = "dest_image.bin";

        try
        {
            // Create sample binary image
            Console.WriteLine("Creating sample image...");
            CreateSampleImageFile(sourceImage);
            Console.WriteLine($"✓ Source file created ({new FileInfo(sourceImage).Length} bytes)\n");

            // Read image → byte array
            Console.WriteLine("Reading image into byte array...");
            byte[] imageBytes = File.ReadAllBytes(sourceImage);
            Console.WriteLine($"✓ Byte array size: {imageBytes.Length} bytes\n");

            // Store byte array in MemoryStream
            using (MemoryStream ms = new MemoryStream(imageBytes))
            {
                Console.WriteLine("Writing byte array back to image...");
                File.WriteAllBytes(destImage, ms.ToArray());
            }

            Console.WriteLine($"✓ Destination file created ({new FileInfo(destImage).Length} bytes)\n");

            // Verify files
            Console.WriteLine("Verifying files...");
            bool isSame = FilesAreIdentical(sourceImage, destImage);

            Console.WriteLine(isSame
                ? "✓ Files are IDENTICAL – Conversion successful!"
                : "✗ Files are DIFFERENT");

            // Display stats
            Console.WriteLine("\n=== ByteArray Statistics ===");
            Console.WriteLine($"File size      : {imageBytes.Length} bytes");
            Console.WriteLine($"First 20 bytes : {BitConverter.ToString(imageBytes, 0, Math.Min(20, imageBytes.Length))}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"✗ Error: {ex.Message}");
        }
        finally
        {
            if (File.Exists(sourceImage)) File.Delete(sourceImage);
            if (File.Exists(destImage)) File.Delete(destImage);
        }
    }

    static void CreateSampleImageFile(string path)
    {
        byte[] data = new byte[10_000]; // 10 KB mock image
        new Random().NextBytes(data);
        File.WriteAllBytes(path, data);
    }

    static bool FilesAreIdentical(string file1, string file2)
    {
        byte[] f1 = File.ReadAllBytes(file1);
        byte[] f2 = File.ReadAllBytes(file2);

        if (f1.Length != f2.Length)
            return false;

        for (int i = 0; i < f1.Length; i++)
        {
            if (f1[i] != f2[i])
                return false;
        }
        return true;
    }
}
