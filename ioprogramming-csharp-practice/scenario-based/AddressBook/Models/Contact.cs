using System;
using AddressBook.Attributes;

namespace AddressBook.Models
{
    /// <summary>
    /// Contact model representing a person in the Address Book
    /// Uses attributes for field validation and metadata
    /// </summary>
    [Serializable]
    [ValidatableEntity]
    public class Contact
    {
        [Required]
        [ValidateName]
        public string FirstName { get; set; }

        [Required]
        [ValidateName]
        public string LastName { get; set; }

        [Required]
        [ValidateEmail]
        public string Email { get; set; }

        [Required]
        [ValidatePhoneNumber]
        public string PhoneNumber { get; set; }

        [Required]
        public string Address { get; set; }

        [Required]
        public string City { get; set; }

        [Required]
        public string State { get; set; }

        [Required]
        [ValidateZipCode]
        public string Zip { get; set; }

        public Contact()
        {
        }

        public Contact(string firstName, string lastName, string email, 
            string phoneNumber, string address, string city, string state, string zip)
        {
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            PhoneNumber = phoneNumber;
            Address = address;
            City = city;
            State = state;
            Zip = zip;
        }

        public override string ToString()
        {
            return $"Name: {FirstName} {LastName}, Email: {Email}, Phone: {PhoneNumber}, " +
                   $"Address: {Address}, City: {City}, State: {State}, Zip: {Zip}";
        }

        public override bool Equals(object obj)
        {
            if (obj is Contact contact)
            {
                return FirstName.Equals(contact.FirstName, StringComparison.OrdinalIgnoreCase) &&
                       LastName.Equals(contact.LastName, StringComparison.OrdinalIgnoreCase);
            }
            return false;
        }

        public override int GetHashCode()
        {
            return (FirstName + LastName).ToLower().GetHashCode();
        }
    }
}
