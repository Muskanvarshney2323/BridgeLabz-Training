using System;
class Program
{
    static void Main()
    {
        int total_pens = 14;
        int total_students = 3;

        int pens_per_student = total_pens / total_students;
        int remaining_pens = total_pens % total_students;

        Console.WriteLine("The Pen Per Student is " + pens_per_student + " and the remaining pen not distributed is " + remaining_pens);
    }
}