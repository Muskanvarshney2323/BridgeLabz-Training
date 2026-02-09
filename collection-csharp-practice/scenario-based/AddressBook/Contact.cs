using System;

/// <summary>
/// UC 1-3, 7: Contact class represents a single person in the Address Book
/// Contains: First Name, Last Name, Address, City, State, Zip, Phone Number, Email
/// Implements IComparable and overrides equals for duplicate check and sorting
/// </summary>
public class Contact : IComparable<Contact>
{
    // Properties for contact details
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Address { get; set; }
    public string City { get; set; }
    public string State { get; set; }
    public string Zip { get; set; }
    public string PhoneNumber { get; set; }
    public string Email { get; set; }

    /// <summary>
    /// Constructor to initialize contact with all details
    /// </summary>
    public Contact(string firstName, string lastName, string address, string city, 
                   string state, string zip, string phoneNumber, string email)
    {
        FirstName = firstName;
        LastName = lastName;
        Address = address;
        City = city;
        State = state;
        Zip = zip;
        PhoneNumber = phoneNumber;
        Email = email;
    }

    /// <summary>
    /// UC 7: Override Equals method to check for duplicate entries
    /// Duplicate check is done on Person Name (First Name + Last Name)
    /// </summary>
    public override bool Equals(object obj)
    {
        if (obj == null || !(obj is Contact))
            return false;

        Contact other = (Contact)obj;
        return this.FirstName.Equals(other.FirstName, StringComparison.OrdinalIgnoreCase) &&
               this.LastName.Equals(other.LastName, StringComparison.OrdinalIgnoreCase);
    }

    /// <summary>
    /// Override GetHashCode if overriding Equals
    /// </summary>
    public override int GetHashCode()
    {
        return (FirstName + LastName).GetHashCode();
    }

    /// <summary>
    /// UC 11: Override ToString to print person entry in Console
    /// </summary>
    public override string ToString()
    {
        return $"First Name: {FirstName}\n" +
               $"Last Name: {LastName}\n" +
               $"Address: {Address}\n" +
               $"City: {City}\n" +
               $"State: {State}\n" +
               $"Zip: {Zip}\n" +
               $"Phone Number: {PhoneNumber}\n" +
               $"Email: {Email}";
    }

    /// <summary>
    /// UC 11, UC 12: CompareTo method for sorting
    /// Primary sort by name, used for alphabetical sorting by person's name
    /// </summary>
    public int CompareTo(Contact other)
    {
        if (other == null) return 1;

        // Compare by FirstName, then by LastName
        int firstNameComparison = this.FirstName.CompareTo(other.FirstName);
        if (firstNameComparison != 0)
            return firstNameComparison;

        return this.LastName.CompareTo(other.LastName);
    }

    /// <summary>
    /// UC 12: Method to compare contacts by city
    /// </summary>
    public int CompareByCity(Contact other)
    {
        if (other == null) return 1;
        return this.City.CompareTo(other.City);
    }

    /// <summary>
    /// UC 12: Method to compare contacts by state
    /// </summary>
    public int CompareByState(Contact other)
    {
        if (other == null) return 1;
        return this.State.CompareTo(other.State);
    }

    /// <summary>
    /// UC 12: Method to compare contacts by zip
    /// </summary>
    public int CompareByZip(Contact other)
    {
        if (other == null) return 1;
        return this.Zip.CompareTo(other.Zip);
    }
}
