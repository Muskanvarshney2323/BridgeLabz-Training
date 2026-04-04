using System;

namespace AddressBook.Attributes
{
    /// <summary>
    /// Attribute to mark a class as a validatable entity
    /// Used via reflection for runtime validation
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    public class ValidatableEntity : Attribute
    {
        public ValidatableEntity() { }
    }

    /// <summary>
    /// Marks a property as required
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class Required : Attribute
    {
        public string ErrorMessage { get; set; } = "This field is required";
    }

    /// <summary>
    /// Validates that a name field contains only letters and spaces
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class ValidateName : Attribute
    {
        public string ErrorMessage { get; set; } = "Name must contain only letters and spaces";
    }

    /// <summary>
    /// Validates email format
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class ValidateEmail : Attribute
    {
        public string ErrorMessage { get; set; } = "Invalid email format";
    }

    /// <summary>
    /// Validates phone number format (10 digits)
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class ValidatePhoneNumber : Attribute
    {
        public string ErrorMessage { get; set; } = "Phone number must be 10 digits";
    }

    /// <summary>
    /// Validates zip code format (5 digits)
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class ValidateZipCode : Attribute
    {
        public string ErrorMessage { get; set; } = "Zip code must be 5 digits";
    }
}
