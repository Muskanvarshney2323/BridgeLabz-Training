using System;
class Course
{
    //instance variables
    string courseName;
    int duration;
    double fee;
    //class variable
    static string instituteName = "Global Tech Institute";

    public Course(string n, int d, double f)
    {
        this.courseName = n;
        this.duration = d;
        this.fee = f;
    }
    public void DisplayCourseDetails()
    {
        Console.WriteLine("Institute Name: " + instituteName);
        Console.WriteLine("Course Name: " + courseName);
        Console.WriteLine("Course Duration (in months): " + duration);
        Console.WriteLine("Course Fee: " + fee);
    }
    public void updateInstituteName(string newName)
    {
        instituteName = newName;
    }
    public static void Main()
    {
        Course course1 = new Course("C# Programming", 6, 1500.00);
        course1.DisplayCourseDetails();

        Console.WriteLine();

        // Updating institute name
        course1.updateInstituteName("Tech Innovators Academy");

        Course course2 = new Course("Java Programming", 5, 1300.00);
        course2.DisplayCourseDetails();
    }

}