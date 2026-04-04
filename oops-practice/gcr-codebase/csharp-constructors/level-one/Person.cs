using System;
class Person
{
    string name;
    int age;
    // parameterized constructor
    public Person(string n, int a)
    {
        name = n;
        age = a;
    }
    //copy constructor
    public Person(Person p)
    {
        name = p.name;
        age = p.age;
    }
    public void DisplayInfo()
    {
        Console.WriteLine("Name: " + name);
        Console.WriteLine("Age: " + age);
    }
    public static void Main()
    {
        // Creating object using parameterized constructor
        Person person1 = new Person("Alice", 30);
        person1.DisplayInfo();

        Console.WriteLine();

        // Creating object using copy constructor
        Person person2 = new Person(person1);
        person2.DisplayInfo();
    }
}