class Program
{
    static void Main()
    {
        Console.Write("Enter the first integer (a): ");
        int a = Convert.ToInt32(Console.ReadLine());

        Console.Write("Enter the second integer (b): ");
        int b = Convert.ToInt32(Console.ReadLine());

        Console.Write("Enter the third integer (c): ");
        int c = Convert.ToInt32(Console.ReadLine());

        int result1 = a + b * c;       // Multiplication has higher precedence than addition
        int result2 = a * b + c;       // Multiplication has higher precedence than addition
        int result3 = c + a / b;       // Division has higher precedence than addition
        int result4 = a % b + c;       // Modulus has higher precedence than addition

        Console.WriteLine("The results of Int Operations are:");
        Console.WriteLine("a + b * c = " + result1);
        Console.WriteLine("a * b + c = " + result2);
        Console.WriteLine("c + a / b = " + result3);
        Console.WriteLine("a % b + c = " + result4);
    }
}