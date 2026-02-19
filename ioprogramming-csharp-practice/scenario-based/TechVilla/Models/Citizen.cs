namespace TechVilla.Models
{
    public class Citizen
    {
        public string Name { get; set; }
        public int Age { get; set; }
        public decimal Income { get; set; }
        public int ResidencyYears { get; set; }

        public override string ToString()
        {
            return $"{Name}, Age: {Age}, Income: {Income}, Residency: {ResidencyYears}";
        }
    }
}
