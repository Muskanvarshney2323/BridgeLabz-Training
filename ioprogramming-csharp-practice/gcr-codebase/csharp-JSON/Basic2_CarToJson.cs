using System;
using Newtonsoft.Json;

public class Car
{
    public string Make { get; set; }
    public string Model { get; set; }
    public int Year { get; set; }
}

class Basic2_CarToJson
{
    static void Main()
    {
        var car = new Car { Make = "Toyota", Model = "Corolla", Year = 2020 };
        string json = JsonConvert.SerializeObject(car, Formatting.Indented);
        Console.WriteLine(json);
    }
}
