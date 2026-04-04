using System;

namespace CollectionAnnotations.Exercises
{
    /// <summary>
    /// Exercise 1: Use Method Overriding Correctly
    /// Create a parent class Animal with a method MakeSound(). 
    /// Override it in the Dog class using override.
    /// </summary>
    class MethodOverridingCorrectly
    {
        static void Main()
        {
            Console.WriteLine("=== Method Overriding in C# ===\n");

            // Create instances
            Animal animal = new Animal();
            Dog dog = new Dog();
            Cat cat = new Cat();

            // Display original implementation
            Console.WriteLine("Animal Sound:");
            animal.MakeSound();

            // Display overridden implementation in Dog
            Console.WriteLine("\nDog Sound:");
            dog.MakeSound();

            // Display overridden implementation in Cat
            Console.WriteLine("\nCat Sound:");
            cat.MakeSound();

            // Polymorphism example
            Console.WriteLine("\n\n--- Polymorphism Example ---");
            Animal[] animals = new Animal[] { new Dog(), new Cat(), new Animal() };

            foreach (Animal a in animals)
            {
                Console.WriteLine($"{a.GetType().Name}: ");
                a.MakeSound();
                Console.WriteLine();
            }

            // Virtual method behavior
            Console.WriteLine("--- Using Virtual Methods ---");
            DisplayAnimalSound(new Dog());
            DisplayAnimalSound(new Cat());
            DisplayAnimalSound(new Animal());
        }

        static void DisplayAnimalSound(Animal animal)
        {
            Console.WriteLine($"Animal Type: {animal.GetType().Name}");
            animal.MakeSound();
            Console.WriteLine();
        }
    }

    // Parent class
    class Animal
    {
        public virtual void MakeSound()
        {
            Console.WriteLine("Animal makes a generic sound.");
        }

        public virtual void Move()
        {
            Console.WriteLine("Animal is moving.");
        }
    }

    // Child class - Dog
    class Dog : Animal
    {
        // Override the parent method
        public override void MakeSound()
        {
            Console.WriteLine("Woof! Woof!");
        }

        // Override another method
        public override void Move()
        {
            Console.WriteLine("Dog is running on four legs.");
        }
    }

    // Child class - Cat
    class Cat : Animal
    {
        // Override the parent method
        public override void MakeSound()
        {
            Console.WriteLine("Meow! Meow!");
        }

        // Override another method
        public override void Move()
        {
            Console.WriteLine("Cat is moving gracefully on four legs.");
        }
    }

    // Child class - Bird
    class Bird : Animal
    {
        // Override the parent method
        public override void MakeSound()
        {
            Console.WriteLine("Tweet! Tweet!");
        }

        // Override another method
        public override void Move()
        {
            Console.WriteLine("Bird is flying in the sky.");
        }
    }
}
