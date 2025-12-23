using System;
class PosNegZero()
{
    static void Main()
    {
        int[] n = new int[5];
        Console.WriteLine("Enter 5 numbers: ");
        for(int i = 0; i < 5; i++)
        {
            n[i] = Convert.ToInt32(Console.ReadLine());
        }
        for(int i = 0; i <5 ; i++)
        {
            if (n[i] > 0)
            {
                Console.WriteLine("Number {0} is Positive", n[i]);
                if (n[i] % 2 == 0)
                {
                    Console.WriteLine("Number {0} is even", n[i]);
                }
                else
                {
                    Console.WriteLine("Number {0} is odd", n[i]);
                }
            }else if(n[i]< 0)
            {
                Console.WriteLine("Number {0} is Negative",n[i]);
            }
            else
            {
                Console.WriteLine("Number {0} is zero", n[i]);
            }
        }
        if (n[0] > n[n.Length - 1])
        {
            Console.WriteLine("1st no is greater than the last no");
        }
        else if (n[0] < n[n.Length - 1])
        {
            Console.WriteLine("Last no is greater than the 1st no");
        }
        else
        {
            Console.WriteLine("Both are equal");
        }
        
    }
}