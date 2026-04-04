using System;
class BMI2
{
    static void Main()
    {
        Console.WriteLine("Enter the number of persons:");
        int numberOfPersons = Convert.ToInt32(Console.ReadLine());

        double[][] personData = new double[numberOfPersons][];
        string[] weightStatus = new string[numberOfPersons];

        for (int i = 0; i < numberOfPersons; i++)
        {
            personData[i] = new double[3]; // 0: Height, 1: Weight, 2: BMI

            Console.Write("Enter height (in meters) of person {0}: ", i + 1);
            personData[i][0] = Convert.ToDouble(Console.ReadLine());

            Console.Write("Enter weight (in kg) of person {0}: ", i + 1);
            personData[i][1] = Convert.ToDouble(Console.ReadLine());

            // Calculate BMI
            personData[i][2] = personData[i][1] / (personData[i][0] * personData[i][0]);

            // Determine weight status
            if (personData[i][2] < 18.5)
                weightStatus[i] = "Underweight";
            else if (personData[i][2] >= 18.5 && personData[i][2] < 24.9)
                weightStatus[i] = "Normal weight";
            else if (personData[i][2] >= 25 && personData[i][2] < 39.9)
                weightStatus[i] = "Overweight";
            else
                weightStatus[i] = "Obesity";
        }

        Console.WriteLine("\nHeight (m)\tWeight (kg)\tBMI\t\tStatus");
        for (int i = 0; i < numberOfPersons; i++)
        {
            Console.WriteLine("{0}\t\t{1}\t\t{2:F2}\t{3}", personData[i][0], personData[i][1], personData[i][2], weightStatus[i]);
        }
    }
}