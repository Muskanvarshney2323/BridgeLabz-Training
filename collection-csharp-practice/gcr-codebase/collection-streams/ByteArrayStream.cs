using System;
using System.IO;

/// <summary>
/// Problem 5: ByteArray Stream - Convert Image to ByteArray
/// Converts an image file into a byte array and writes it back to another image file.
/// Verifies that the new file is identical to the original.
/// </summary>
class ImageByteArrayProgram
{
    static void Main(string[] args)
    {
        Console.WriteLine("╔════════════════════════════════════════════════════╗");
        Console.WriteLine("║   ByteArray Stream - Convert Image to ByteArray   ║");
        Console.WriteLine("╚════════════════════════════════════════════════════╝\n");

        string sourceImage = "source_image.bin"; // Using binary file as mock image
        string byteArrayFile = "image_bytearray.bin";
        string destImage = "dest_image.bin";

        try
        {
            // Create sample image file (binary data)
            Console.WriteLine("Creating sample image file...");
            CreateSampleImageFile(sourceImage);
            FileInfo sourceInfo = new FileInfo(sourceImage);
            Console.WriteLine($"✓ Created: {sourceImage} ({sourceInfo.Length} bytes)\n");

            // Read image file into byte array
            Console.WriteLine("=== Converting Image to ByteArray ===");
            byte[] imageByteArray = ReadImageToByteArray(sourceImage);
            Console.WriteLine($"✓ Image converted to ByteArray: {imageByteArray.Length} bytes");

            // Store byte array in MemoryStream
            Console.WriteLine("\n=== Storing ByteArray in MemoryStream ===");
            using (MemoryStream memoryStream = new MemoryStream(imageByteArray))
            {
                Console.WriteLine($"✓ MemoryStream created with {memoryStream.Length} bytes");
                Console.WriteLine($"Stream position: {memoryStream.Position}");

                // Write back to destination image
                Console.WriteLine($"\n=== Writing ByteArray back to Image File ===");
                memoryStream.Position = 0; // Reset stream position
                WriteByteArrayToImage(destImage, memoryStream.ToArray());
                Console.WriteLine($"✓ ByteArray written to: {destImage}");
            }

            // Verify both files are identical
            Console.WriteLine("\n=== Verifying Files ===");
            if (FilesAreIdentical(sourceImage, destImage))
            {
                Console.WriteLine("✓ Source and destination images are IDENTICAL!");
                Console.WriteLine("✓ ByteArray conversion successful!");
            }
            else
            {
                Console.WriteLine("✗ Files are NOT identical!");
            }

            // Display byte array statistics
            Console.WriteLine("\n=== ByteArray Statistics ===");
            Console.WriteLine($"Original file size: {sourceInfo.Length} bytes");
            Console.WriteLine($"ByteArray length: {imageByteArray.Length} bytes");
            Console.WriteLine($"First 20 bytes (Hex): {BitConverter.ToString(imageByteArray, 0, Math.Min(20, imageByteArray.Length))}");
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
            if (File.Exists(sourceImage)) File.Delete(sourceImage);
            if (File.Exists(byteArrayFile)) File.Delete(byteArrayFile);
            if (File.Exists(destImage)) File.Delete(destImage);
        }
    }

    static void CreateSampleImageFile(string filePath)
    {
        using (FileStream fs = new FileStream(filePath, FileMode.Create))
        {
            byte[] imageData = new byte[10000]; // 10 KB mock image
            Random random = new Random();
            random.NextBytes(imageData);
            fs.Write(imageData, 0, imageData.Length);
        }
    }

    static byte[] ReadImageToByteArray(string imagePath)
    {
        using (FileStream fs = new FileStream(imagePath, FileMode.Open, FileAccess.Read))
        {
            byte[] imageByteArray = new byte[fs.Length];
            fs.Read(imageByteArray, 0, imageByteArray.Length);
            return imageByteArray;
        }
    }

    static void WriteByteArrayToImage(string imagePath, byte[] imageByteArray)
    {
        using (FileStream fs = new FileStream(imagePath, FileMode.Create, FileAccess.Write))
        {
            fs.Write(imageByteArray, 0, imageByteArray.Length);
        }
    }

    static bool FilesAreIdentical(string file1, string file2)
    {
        FileInfo info1 = new FileInfo(file1);
        FileInfo info2 = new FileInfo(file2);

        if (info1.Length != info2.Length)
        {
            return false;
        }

        using (FileStream fs1 = new FileStream(file1, FileMode.Open, FileAccess.Read))
        using (FileStream fs2 = new FileStream(file2, FileMode.Open, FileAccess.Read))
        {
            byte[] buffer1 = new byte[4096];
            byte[] buffer2 = new byte[4096];
            int bytesRead1, bytesRead2;

            while (true)
            {
                bytesRead1 = fs1.Read(buffer1, 0, buffer1.Length);
                bytesRead2 = fs2.Read(buffer2, 0, buffer2.Length);

                if (bytesRead1 != bytesRead2)
                    return false;

                if (bytesRead1 == 0)
                    break;

                for (int i = 0; i < bytesRead1; i++)
                {
                    if (buffer1[i] != buffer2[i])
                        return false;
                }
            }
        }

        return true;
    }
}
