using System;

namespace TechVilla.Exceptions
{
    public class InvalidAgeException : Exception
    {
        public int Age { get; }
        public InvalidAgeException(int age) : base($"Age {age} is invalid") => Age = age;
    }
}
