using System;
class Bird
{
    public string Name { get; set; }
    public string Species { get; set; }

    public Bird(string name, string species)
    {
        Name = name;
        Species = species;
    }

    public virtual void DisplayInfo()
    {
        Console.WriteLine($"Name: {Name}, Species: {Species}");
    }
}


interface IFlyable
{
    void Fly();
}

interface ISwimmable
{
    void Swim();
}


// Eagle - Can Fly
class Eagle : Bird, IFlyable
{
    public Eagle(string name) : base(name, "Eagle") { }

    public void Fly()
    {
        Console.WriteLine($"{Name} is flying high in the sky.");
    }
}

// Sparrow - Can Fly
class Sparrow : Bird, IFlyable
{
    public Sparrow(string name) : base(name, "Sparrow") { }

    public void Fly()
    {
        Console.WriteLine($"{Name} is flying short distances.");
    }
}

// Duck - Can Swim
class Duck : Bird, ISwimmable
{
    public Duck(string name) : base(name, "Duck") { }

    public void Swim()
    {
        Console.WriteLine($"{Name} is swimming in the pond.");
    }
}

// Penguin - Can Swim
class Penguin : Bird, ISwimmable
{
    public Penguin(string name) : base(name, "Penguin") { }

    public void Swim()
    {
        Console.WriteLine($"{Name} is swimming underwater.");
    }
}

// Seagull - Can Fly and Swim
class Seagull : Bird, IFlyable, ISwimmable
{
    public Seagull(string name) : base(name, "Seagull") { }

    public void Fly()
    {
        Console.WriteLine($"{Name} is flying near the sea.");
    }

    public void Swim()
    {
        Console.WriteLine($"{Name} is swimming on the sea surface.");
    }
}

class Program
{
    static void Main()
    {
        
        Bird[] birds = new Bird[]
        {
            new Eagle("Rocky"),
            new Sparrow("Chirpy"),
            new Duck("Daffy"),
            new Penguin("Pingu"),
            new Seagull("Sunny")
        };

        foreach (Bird bird in birds)
        {
            bird.DisplayInfo();

            if (bird is IFlyable flyable)
            {
                flyable.Fly();
            }

            if (bird is ISwimmable swimmable)
            {
                swimmable.Swim();
            }

            Console.WriteLine();
        }
    }
}
