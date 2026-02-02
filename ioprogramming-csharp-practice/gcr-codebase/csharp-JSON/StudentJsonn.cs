using System;
using System.Collections.Generic;
using Newtonsoft.Json;

public class Student
{
    public string name { get; set; }
    public int age { get; set; }
    public List<string> subjects { get; set; }
}

class Basic1_StudentJson
{
    static void Main()
    {
        var student = new Student {
            name = "Asha",
            age = 20,
            subjects = new List<string>{ "Math", "Physics", "Chemistry" }
        };

        string json = JsonConvert.SerializeObject(student, Formatting.Indented);
        Console.WriteLine(json);
    }
}
