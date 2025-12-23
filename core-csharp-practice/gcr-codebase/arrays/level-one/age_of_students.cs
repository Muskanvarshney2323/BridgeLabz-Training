using System;
class AgeOfStudents
{
    static void Main()
    {
        int[] ages = new int[10];
        Console.WriteLine("Enter the age of 10 students: ");
        for(int i = 0; i < 10; i++)
        {
            ages[i] = Convert.ToInt32(Console.ReadLine());

        }
        for(int i = 0; i < 10; i++)
        {
            if (ages[i] >= 18)
            {
                Console.WriteLine("Student {0} can vote {1}", i + 1, ages[i]);
            }
            else
            {
                Console.WriteLine("Student {0} can not vote {1}", i + 1, ages[i]);
            }
        }
        

    }
}   