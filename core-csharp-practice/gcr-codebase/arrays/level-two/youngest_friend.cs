using System;
class YoungestFriend
{
    static void Main()
    {
        string[] friends = { "Amar", "Akbar", "Anthony" };
        int[] ages = new int[3];
        double[] heights = new double[3];

        for (int i = 0; i < friends.Length; i++)
        {
            Console.Write($"Enter age of {friends[i]}: ");
            ages[i] = Convert.ToInt32(Console.ReadLine());

            Console.Write($"Enter height of {friends[i]} (in cm): ");
            heights[i] = Convert.ToDouble(Console.ReadLine());
        }

        int youngestIndex = 0;
        double tallestHeight = heights[0];
        int tallestIndex = 0;

        for (int i = 1; i < friends.Length; i++)
        {
            if (ages[i] < ages[youngestIndex])
            {
                youngestIndex = i;
            }

            if (heights[i] > tallestHeight)
            {
                tallestHeight = heights[i];
                tallestIndex = i;
            }
        }

        Console.WriteLine($"The youngest friend is {friends[youngestIndex]} with age {ages[youngestIndex]}.");
        Console.WriteLine($"The tallest friend is {friends[tallestIndex]} with height {heights[tallestIndex]} cm.");
    }
}