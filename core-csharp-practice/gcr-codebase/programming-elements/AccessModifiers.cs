using System;

// Base class
class AccessDemo
{
    public int a = 10;                
    private int b = 20;                
    protected int c = 30;             
    internal int d = 40;             
    protected internal int e = 50;     

    public void ShowBase()
    {
        Console.WriteLine(a);   // allowed
        Console.WriteLine(b);   // allowed (same class)
        Console.WriteLine(c);   // allowed
        Console.WriteLine(d);   // allowed
        Console.WriteLine(e);   // allowed
    }
}

// Derived class
class ChildDemo : AccessDemo
{
    public void ShowChild()
    {
        Console.WriteLine(a);   
       // Console.WriteLine(b); 
        Console.WriteLine(c);   
        Console.WriteLine(d);   
        Console.WriteLine(e);  
    }
}

class Program
{
    static void Main()
    {
        AccessDemo obj = new AccessDemo();
        obj.ShowBase();

        ChildDemo child = new ChildDemo();
        child.ShowChild();

        Console.WriteLine(obj.a);   
        // Console.WriteLine(obj.b); 
        // Console.WriteLine(obj.c); 
        Console.WriteLine(obj.d);  
        Console.WriteLine(obj.e);   
    }
}
