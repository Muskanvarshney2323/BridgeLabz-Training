using System;
class Circle
{
    double radius;
    public Circle(double r)
    {
        radius = r;
    }
    public Circle() : this(2.0) //constructor chaining 
    {

    }
    public void Area()
    {
        Console.WriteLine("Area of Circle with radius "  + radius + " is: " + (Math.PI * radius * radius));
    }
    public static void Main()
    {
        // Creating object using parameterized constructor
        Circle c1 = new Circle(5.0);
        c1.Area();
        // Creating object using default constructor
        Circle c2 = new Circle();
        c2.Area();
    }
}