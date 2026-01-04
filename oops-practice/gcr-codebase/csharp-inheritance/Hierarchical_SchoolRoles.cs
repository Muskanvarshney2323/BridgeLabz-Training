using System;

namespace csharp_inheritance
{
    class Person
    {
        public string Name { get; set; }
        public int Age { get; set; }

        public Person(string name, int age)
        {
            Name = name;
            Age = age;
        }
    }

    class Teacher : Person
    {
        public string Subject { get; set; }

        public Teacher(string name, int age, string subject) : base(name, age)
        {
            Subject = subject;
        }

        public void DisplayRole()
        {
            Console.WriteLine($"Teacher: {Name}, Age: {Age}, Subject: {Subject}");
        }
    }

    class Student : Person
    {
        public string Grade { get; set; }

        public Student(string name, int age, string grade) : base(name, age)
        {
            Grade = grade;
        }

        public void DisplayRole()
        {
            Console.WriteLine($"Student: {Name}, Age: {Age}, Grade: {Grade}");
        }
    }

    class Staff : Person
    {
        public string Role { get; set; }

        public Staff(string name, int age, string role) : base(name, age)
        {
            Role = role;
        }

        public void DisplayRole()
        {
            Console.WriteLine($"Staff: {Name}, Age: {Age}, Role: {Role}");
        }
    }

    class Program
    {
        static void Main()
        {
            var people = new Person[]
            {
                new Teacher("Mr. A", 40, "Math"),
                new Student("Sam", 16, "10th"),
                new Staff("Janitor", 50, "Maintenance")
            };

            foreach (var p in people)
            {
                switch (p)
                {
                    case Teacher t:
                        t.DisplayRole();
                        break;
                    case Student s:
                        s.DisplayRole();
                        break;
                    case Staff st:
                        st.DisplayRole();
                        break;
                }
            }
        }
    }
}