
class Program
{
    public static void Main(string[] args)
    {
        int[] nums = { 2, 7, 11, 15 };
        int target = 9;

        for (int i = 0; i < nums.Length; i++)
        {
            for (int j = i + 1; j < nums.Length; j++)
            {
                if (nums[i] + nums[j] == target)
                {
                    Console.WriteLine("Two Sum numbers are: " + nums[i] + " and " + nums[j]);
                    return;
                }
            }
        }

        Console.WriteLine("No pair found");
    }
}
