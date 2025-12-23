using System;
class BMI
{
    static void Main()
    {
        Console.WriteLine("Enter the number of persons:");
        int numberOfPersons = Convert.ToInt32(Console.ReadLine());

        double[] heights = new double[numberOfPersons];
        double[] weights = new double[numberOfPersons];
        double[] bmis = new double[numberOfPersons];
        string[] statuses = new string[numberOfPersons];

        for (int i = 0; i < numberOfPersons; i++)
        {
            Console.Write("Enter height (in meters) of person {0}: ", i + 1);
            heights[i] = Convert.ToDouble(Console.ReadLine());

            Console.Write("Enter weight (in kg) of person {0}: ", i + 1);
            weights[i] = Convert.ToDouble(Console.ReadLine());

            bmis[i] = weights[i] / (heights[i] * heights[i]);

            if (bmis[i] < 18.5)
                statuses[i] = "Underweight";
            else if (bmis[i] >= 18.5 && bmis[i] < 24.9)
                statuses[i] = "Normal weight";
            else if (bmis[i] >= 25 && bmis[i] < 39.9)
                statuses[i] = "Overweight";
            else
                statuses[i] = "Obesity";
        }

        Console.WriteLine("\nHeight (m)\tWeight (kg)\tBMI\t\tStatus");
        for (int i = 0; i < numberOfPersons; i++)
        {
            Console.WriteLine("{0}\t\t{1}\t\t{2:F2}\t{3}", heights[i], weights[i], bmis[i], statuses[i]);
        }
    }
}