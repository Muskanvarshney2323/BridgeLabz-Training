
class SecondHighest
{
    public static void Main(string[] args)
    {
        int[] arr = { 10, 45, 22, 89, 34 };

        int highest = int.MinValue;
        int secondHighest = int.MinValue;

        foreach (int num in arr)
        {
            if (num > highest)
            {
                secondHighest = highest;
                highest = num;
            }
            else if (num > secondHighest && num != highest)
            {
                secondHighest = num;
            }
        }

        Console.WriteLine("Second Highest Number is: " + secondHighest);
    }
}
