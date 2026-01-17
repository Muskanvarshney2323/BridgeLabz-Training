public class User
{
    public string Name { get; set; }
    public int Steps { get; set; }

    public User(string name, int steps)
    {
        Name = name;
        Steps = steps;
    }
}
