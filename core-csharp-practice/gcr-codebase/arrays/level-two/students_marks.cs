using System;
class StudentsMarks
{
    static void Main()
    {
        Console.WriteLine("Enter the number of students: ");
        int numberOfStudents = Convert.ToInt32(Console.ReadLine());

        string[] studentNames = new string[numberOfStudents];
        int[,] marks = new int[numberOfStudents, 3]; // 3 subjects: Physics, Chemistry, Maths
        double[] percentages = new double[numberOfStudents];
        char[] grades = new char[numberOfStudents];

        for (int i = 0; i < numberOfStudents; i++)
        {
            Console.WriteLine($"Enter name of student {i + 1}: ");
            studentNames[i] = Console.ReadLine();

            Console.WriteLine($"Enter marks for {studentNames[i]} in Physics: ");
            marks[i, 0] = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine($"Enter marks for {studentNames[i]} in Chemistry: ");
            marks[i, 1] = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine($"Enter marks for {studentNames[i]} in Maths: ");
            marks[i, 2] = Convert.ToInt32(Console.ReadLine());
        }

        for (int i = 0; i < numberOfStudents; i++)
        {
            int totalMarks = marks[i, 0] + marks[i, 1] + marks[i, 2];
            percentages[i] = (totalMarks / 300.0) * 100;

            if (percentages[i] >= 90)
                grades[i] = 'A';
            else if (percentages[i] >= 80)
                grades[i] = 'B';
            else if (percentages[i] >= 70)
                grades[i] = 'C';
            else if (percentages[i] >= 60)
                grades[i] = 'D';
            else
                grades[i] = 'F';
        }

        Console.WriteLine("\nStudent Results:");
        for (int i = 0; i < numberOfStudents; i++)
        {
            Console.WriteLine($"Name: {studentNames[i]}, Percentage: {percentages[i]:F2}%, Grade: {grades[i]}");
        }
    }
}