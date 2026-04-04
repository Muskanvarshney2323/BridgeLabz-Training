using System;
class BMIProgram
{
    static void Main()
    {
        const int people = 10; double[,] data = new double[people,3];
        for (int i = 0; i < people; i++)
        {
            Console.Write($"Person #{i+1} weight (kg): "); data[i,0] = Convert.ToDouble(Console.ReadLine());
            Console.Write($"Person #{i+1} height (cm): "); data[i,1] = Convert.ToDouble(Console.ReadLine());
            data[i,2] = CalculateBMI(data[i,0], data[i,1]);
        }

        Console.WriteLine("Idx\tWeight\tHeight\tBMI\tStatus");
        for (int i = 0; i < people; i++) Console.WriteLine($"{i+1}\t{data[i,0]:F1}\t{data[i,1]:F1}\t{data[i,2]:F2}\t{GetBMIStatus(data[i,2])}");
    }

    static double CalculateBMI(double weightKg, double heightCm)
    {
        double m = heightCm / 100.0; if (m <= 0) return 0; return weightKg / (m * m);
    }

    static string GetBMIStatus(double bmi)
    {
        if (bmi <= 0) return "Invalid"; if (bmi < 18.5) return "Underweight"; if (bmi < 25) return "Normal"; if (bmi < 30) return "Overweight"; return "Obese";
    }
}