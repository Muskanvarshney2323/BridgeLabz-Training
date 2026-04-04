using System;
class YoungestAndTallest
{
    static void Main()
    {
        string[] names = { "Amar", "Akbar", "Anthony" };
        int[] ages = new int[3];
        double[] heights = new double[3];

        for (int i = 0; i < 3; i++)
        {
            Console.Write($"Enter age of {names[i]}: "); ages[i] = Convert.ToInt32(Console.ReadLine());
            Console.Write($"Enter height (cm) of {names[i]}: "); heights[i] = Convert.ToDouble(Console.ReadLine());
        }

        int yi = IndexOfMin(ages); int hi = IndexOfMax(heights);
        Console.WriteLine($"Youngest: {names[yi]} (age {ages[yi]})");
        Console.WriteLine($"Tallest: {names[hi]} (height {heights[hi]} cm)");
    }

    static int IndexOfMin(int[] arr){int idx=0; for(int i=1;i<arr.Length;i++) if(arr[i]<arr[idx]) idx=i; return idx;}
    static int IndexOfMax(double[] arr){int idx=0; for(int i=1;i<arr.Length;i++) if(arr[i]>arr[idx]) idx=i; return idx;}
}