using System;
class CollinearPoints
{
    static void Main()
    {
        Console.Write("x1: "); double x1 = Convert.ToDouble(Console.ReadLine());
        Console.Write("y1: "); double y1 = Convert.ToDouble(Console.ReadLine());
        Console.Write("x2: "); double x2 = Convert.ToDouble(Console.ReadLine());
        Console.Write("y2: "); double y2 = Convert.ToDouble(Console.ReadLine());
        Console.Write("x3: "); double x3 = Convert.ToDouble(Console.ReadLine());
        Console.Write("y3: "); double y3 = Convert.ToDouble(Console.ReadLine());

        bool slopes = AreCollinearBySlope(x1,y1,x2,y2,x3,y3);
        bool area = AreCollinearByArea(x1,y1,x2,y2,x3,y3);
        Console.WriteLine("Collinear by slope? " + slopes);
        Console.WriteLine("Collinear by area? " + area);
    }

    public static bool AreCollinearBySlope(double x1,double y1,double x2,double y2,double x3,double y3)
    {
        double dx1 = x2 - x1;
        double dx2 = x3 - x2;
        if (Math.Abs(dx1) < 1e-12 && Math.Abs(dx2) < 1e-12) return true;
        if (Math.Abs(dx1) < 1e-12 || Math.Abs(dx2) < 1e-12) return false;
        double slope1 = (y2 - y1) / dx1;
        double slope2 = (y3 - y2) / dx2;
        return Math.Abs(slope1 - slope2) < 1e-9;
    }

    public static bool AreCollinearByArea(double x1,double y1,double x2,double y2,double x3,double y3)
    {
        double area = 0.5 * (x1 * (y2 - y3) + x2 * (y3 - y1) + x3 * (y1 - y2));
        return Math.Abs(area) < 1e-9;
    }
}