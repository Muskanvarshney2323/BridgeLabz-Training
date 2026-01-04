using System;

namespace csharp_inheritance
{
    class Course
    {
        public string CourseName { get; set; }
        public int Duration { get; set; }

        public Course(string name, int duration)
        {
            CourseName = name;
            Duration = duration;
        }

        public virtual void Display()
        {
            Console.WriteLine($"Course: {CourseName}, Duration: {Duration} days");
        }
    }

    class OnlineCourse : Course
    {
        public string Platform { get; set; }
        public bool IsRecorded { get; set; }

        public OnlineCourse(string name, int duration, string platform, bool recorded) : base(name, duration)
        {
            Platform = platform;
            IsRecorded = recorded;
        }

        public override void Display()
        {
            base.Display();
            Console.WriteLine($"Platform: {Platform}, Recorded: {IsRecorded}");
        }
    }

    class PaidOnlineCourse : OnlineCourse
    {
        public double Fee { get; set; }
        public double Discount { get; set; }

        public PaidOnlineCourse(string name, int duration, string platform, bool recorded, double fee, double discount) : base(name, duration, platform, recorded)
        {
            Fee = fee;
            Discount = discount;
        }

        public override void Display()
        {
            base.Display();
            Console.WriteLine($"Fee: {Fee}, Discount: {Discount}%");
        }
    }

    class Program
    {
        static void Main()
        {
            var c = new PaidOnlineCourse("C# Basics", 30, "Udemy", true, 99.99, 10);
            c.Display();
        }
    }
}