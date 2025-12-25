using System;
using System.Collections.Generic;
class OTPGenerator
{
    public static int Generate6DigitOTP()
    {
        Random rnd = new Random();
        return rnd.Next(100000, 1000000);
    }

    public static bool AreUnique(int[] arr)
    {
        var set = new HashSet<int>();
        foreach (var v in arr) if (!set.Add(v)) return false;
        return true;
    }

    static void Main()
    {
        int[] otps = new int[10];
        for (int i = 0; i < 10; i++) otps[i] = Generate6DigitOTP();
        Console.WriteLine("Generated OTPs: " + string.Join(", ", otps));
        Console.WriteLine("All unique: " + AreUnique(otps));
    }
}