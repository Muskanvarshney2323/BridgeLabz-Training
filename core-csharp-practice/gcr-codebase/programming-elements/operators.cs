using System;

class OperatorsDemo
{
    static void Main()
    {
        // Arithmetic Operators
        Console.WriteLine("Arithmetic Operators");
        int a = 10, b = 5;
        Console.WriteLine("Addition: " + (a + b));
        Console.WriteLine("Subtraction: " + (a - b));
        Console.WriteLine("Multiplication: " + (a * b));
        Console.WriteLine("Division: " + (a / b));
        Console.WriteLine("Modulus: " + (a % b));

        // Relational Operators
        Console.WriteLine("\nRelational Operators");
        Console.WriteLine("a > b : " + (a > b));
        Console.WriteLine("a < b : " + (a < b));
        Console.WriteLine("a == b : " + (a == b));
        Console.WriteLine("a != b : " + (a != b));


        // Assignment Operators
        Console.WriteLine("\nAssignment Operators");
        int x = 10;
        x += 5;   // x = x + 5
        Console.WriteLine("x += 5 : " + x);
        x -= 3;   // x = x - 3
        Console.WriteLine("x -= 3 : " + x);

 
        // Unary Operators
        Console.WriteLine("\nUnary Operators");
        int y = 5;
        Console.WriteLine("Post Increment y++ : " + (y++));
        Console.WriteLine("Pre Increment ++y : " + (++y));

        // Ternary Operator
        Console.WriteLine("\nTernary Operator");
        int num = 20;
        string result = (num % 2 == 0) ? "Even" : "Odd";
        Console.WriteLine("Number is: " + result);

        // Bitwise Operators
        Console.WriteLine("\nBitwise Operators");
        int p = 5, q = 3;
        Console.WriteLine("p & q : " + (p & q));
        Console.WriteLine("p | q : " + (p | q));
        Console.WriteLine("p ^ q : " + (p ^ q));

        // isOperator
        Console.WriteLine("\nIs Operator");
        string name = "CSharp";
        Console.WriteLine(name is string);

        Console.WriteLine("\nProgram Ended Successfully");
    }
}
