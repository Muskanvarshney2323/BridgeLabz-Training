using System;
class Circle
{
    double radius;
    public Circle(double r)
    {
        radius = r;
    }

    public void Area()
    {
        Console.WriteLine("Area of Circle with radius "  + radius + " is: " + (Math.PI * radius * radius));
    }
    public static void Main()
    {
        Circle c1 = new Circle(5.0);
        c1.Area();
    }
}