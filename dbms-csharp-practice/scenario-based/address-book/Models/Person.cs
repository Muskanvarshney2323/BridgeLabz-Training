namespace AddressBookSystem.Models
{
    public class Person
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zip { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }

        public Person()
        {
            FirstName = "";
            LastName = "";
            Address = "";
            City = "";
            State = "";
            Zip = "";
            PhoneNumber = "";
            Email = "";
        }

        public override string ToString()
        {
            return $"Name: {FirstName} {LastName}, Address: {Address}, City: {City}, State: {State}, " +
                   $"Zip: {Zip}, Phone: {PhoneNumber}, Email: {Email}";
        }

        public override bool Equals(object? obj)
        {
            if (obj is Person person)
            {
                return FirstName.Equals(person.FirstName, StringComparison.OrdinalIgnoreCase) &&
                       LastName.Equals(person.LastName, StringComparison.OrdinalIgnoreCase);
            }
            return false;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(FirstName.ToLower(), LastName.ToLower());
        }
    }
}
