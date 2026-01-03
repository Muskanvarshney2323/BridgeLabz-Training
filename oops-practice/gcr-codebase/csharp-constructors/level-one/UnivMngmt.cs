using System;

class Student
{
    // Access modifiers
    public int rollNumber;        // Public
    protected string name;        // Protected
    private double CGPA;          // Private

    // Constructor
    public Student(int roll, string n, double cgpa)
    {
        rollNumber = roll;
        name = n;
        CGPA = cgpa;
    }

    // Public method to get CGPA
    public double GetCGPA()
    {
        return CGPA;
    }

    // Public method to set CGPA
    public void SetCGPA(double cgpa)
    {
        CGPA = cgpa;
    }
}

// Subclass
class PostgraduateStudent : Student
{
    string specialization;

    public PostgraduateStudent(int roll, string n, double cgpa, string spec)
        : base(roll, n, cgpa)
    {
        specialization = spec;
    }

    public void DisplayPGStudent()
    {
        Console.WriteLine("Roll Number: " + rollNumber); // public
        Console.WriteLine("Name: " + name);              // protected
        Console.WriteLine("CGPA: " + GetCGPA());         // private via method
        Console.WriteLine("Specialization: " + specialization);
    }
}

class Program
{
    static void Main()
    {
        PostgraduateStudent pg =
            new PostgraduateStudent(101, "Amit", 8.5, "Computer Science");

        pg.DisplayPGStudent();

        Console.WriteLine();

        // Modifying CGPA using public method
        pg.SetCGPA(9.0);
        Console.WriteLine("Updated CGPA: " + pg.GetCGPA());
    }
}
