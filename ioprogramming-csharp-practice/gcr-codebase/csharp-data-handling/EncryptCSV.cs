using System;
using System.IO;
using System.Text;

class CsvSecurityTool
{
    static void Main(string[] args)
    {
        string fileName = "secure.csv";

        string encryptedValue = EncodeData("50000");

        SaveEncryptedData(fileName, encryptedValue);

        string storedValue = LoadEncryptedData(fileName);

        string originalValue = DecodeData(storedValue);

        Console.WriteLine($"Decrypted Salary = {originalValue}");
    }

    static string EncodeData(string input)
    {
        byte[] bytes = Encoding.UTF8.GetBytes(input);
        return Convert.ToBase64String(bytes);
    }

    static string DecodeData(string input)
    {
        byte[] bytes = Convert.FromBase64String(input);
        return Encoding.UTF8.GetString(bytes);
    }

    static void SaveEncryptedData(string path, string data)
    {
        File.WriteAllText(path, data);
    }

    static string LoadEncryptedData(string path)
    {
        return File.ReadAllText(path);
    }
}
