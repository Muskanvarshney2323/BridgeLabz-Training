class ContactPerson
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Address { get; set; }
    public string City { get; set; }
    public string State { get; set; }

    public string Zip { get; set; }
    public string PhoneNumber { get; set; }
    public string Email { get; set; }
     public ContactPerson()
     
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
    
    // ================= UC7 =================
    // Override equals method to check duplicate person
    // Duplicate is checked using FirstName + LastName
    public override bool Equals(object obj)
    {
        if (obj == null || !(obj is ContactPerson))
            return false;

        ContactPerson other = (ContactPerson)obj;
        return this.FirstName.Equals(other.FirstName, StringComparison.OrdinalIgnoreCase)
            && this.LastName.Equals(other.LastName, StringComparison.OrdinalIgnoreCase);
    }

    // UC7: Overriding GetHashCode (mandatory when Equals is overridden)
    public override int GetHashCode()
    {
        return (FirstName + LastName).ToLower().GetHashCode();
    }
    
}


