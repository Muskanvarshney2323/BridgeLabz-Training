using System;

class Student
{
    public int Roll;
    public string Name;
    public int Age;
    public string Grade;
    public Student Next;

    public Student(int roll, string name, int age, string grade)
    {
        Roll = roll;
        Name = name;
        Age = age;
        Grade = grade;
        Next = null;
    }
}

class StudentLinkedList
{
    private Student head;

    // Add at Beginning
    public void AddAtBeginning(int roll, string name, int age, string grade)
    {
        Student newNode = new Student(roll, name, age, grade);
        newNode.Next = head;
        head = newNode;
    }

    // Add at End
    public void AddAtEnd(int roll, string name, int age, string grade)
    {
        Student newNode = new Student(roll, name, age, grade);

        if (head == null)
        {
            head = newNode;
            return;
        }

        Student temp = head;
        while (temp.Next != null)
            temp = temp.Next;

        temp.Next = newNode;
    }

    // Add at Specific Position
    public void AddAtPosition(int pos, int roll, string name, int age, string grade)
    {
        if (pos == 1)
        {
            AddAtBeginning(roll, name, age, grade);
            return;
        }

        Student newNode = new Student(roll, name, age, grade);
        Student temp = head;

        for (int i = 1; i < pos - 1 && temp != null; i++)
            temp = temp.Next;

        if (temp == null)
        {
            Console.WriteLine("Invalid Position");
            return;
        }

        newNode.Next = temp.Next;
        temp.Next = newNode;
    }

    // Delete by Roll Number
    public void DeleteByRoll(int roll)
    {
        if (head == null)
        {
            Console.WriteLine("List is empty");
            return;
        }

        if (head.Roll == roll)
        {
            head = head.Next;
            Console.WriteLine("Record Deleted");
            return;
        }

        Student temp = head;
        while (temp.Next != null && temp.Next.Roll != roll)
            temp = temp.Next;

        if (temp.Next == null)
        {
            Console.WriteLine("Student not found");
            return;
        }

        temp.Next = temp.Next.Next;
        Console.WriteLine("Record Deleted");
    }

    // Search by Roll Number
    public void Search(int roll)
    {
        Student temp = head;

        while (temp != null)
        {
            if (temp.Roll == roll)
            {
                Console.WriteLine($"Found: {temp.Roll}, {temp.Name}, {temp.Age}, {temp.Grade}");
                return;
            }
            temp = temp.Next;
        }
        Console.WriteLine("Student not found");
    }

    // Update Grade
    public void UpdateGrade(int roll, string newGrade)
    {
        Student temp = head;

        while (temp != null)
        {
            if (temp.Roll == roll)
            {
                temp.Grade = newGrade;
                Console.WriteLine("Grade Updated");
                return;
            }
            temp = temp.Next;
        }
        Console.WriteLine("Student not found");
    }

    // Display All Records
    public void Display()
    {
        if (head == null)
        {
            Console.WriteLine("List is empty");
            return;
        }

        Student temp = head;
        while (temp != null)
        {
            Console.WriteLine($"Roll: {temp.Roll}, Name: {temp.Name}, Age: {temp.Age}, Grade: {temp.Grade}");
            temp = temp.Next;
        }
    }
}

class Program
{
    static void Main()
    {
        StudentLinkedList list = new StudentLinkedList();

        list.AddAtBeginning(1, "Aman", 20, "A");
        list.AddAtEnd(2, "Neha", 21, "B");
        list.AddAtPosition(2, 3, "Ravi", 22, "A+");

        list.Display();

        list.Search(2);
        list.UpdateGrade(2, "A");
        list.DeleteByRoll(1);

        Console.WriteLine("\nAfter Operations:");
        list.Display();
    }
}
