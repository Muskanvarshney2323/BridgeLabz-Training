namespace AddressBookSystem.Attributes
{
    /// <summary>
    /// Custom attribute to mark properties that require validation
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class RequiredAttribute : Attribute
    {
        public string? ErrorMessage { get; set; }

        public RequiredAttribute()
        {
            ErrorMessage = "This field is required";
        }

        public RequiredAttribute(string errorMessage)
        {
            ErrorMessage = errorMessage;
        }
    }

    /// <summary>
    /// Custom attribute for email validation
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class EmailAttribute : Attribute
    {
        public string? ErrorMessage { get; set; }

        public EmailAttribute()
        {
            ErrorMessage = "Invalid email format";
        }

        public EmailAttribute(string errorMessage)
        {
            ErrorMessage = errorMessage;
        }
    }

    /// <summary>
    /// Custom attribute for phone number validation
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class PhoneAttribute : Attribute
    {
        public string? ErrorMessage { get; set; }

        public PhoneAttribute()
        {
            ErrorMessage = "Invalid phone number format";
        }

        public PhoneAttribute(string errorMessage)
        {
            ErrorMessage = errorMessage;
        }
    }

    /// <summary>
    /// Custom attribute for zip code validation
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class ZipCodeAttribute : Attribute
    {
        public string? ErrorMessage { get; set; }

        public ZipCodeAttribute()
        {
            ErrorMessage = "Invalid zip code format";
        }

        public ZipCodeAttribute(string errorMessage)
        {
            ErrorMessage = errorMessage;
        }
    }

    /// <summary>
    /// Attribute validator using reflection
    /// </summary>
    public class AttributeValidator
    {
        public static bool ValidateObject(object obj)
        {
            var properties = obj.GetType().GetProperties();

            foreach (var property in properties)
            {
                var requiredAttr = (RequiredAttribute?)Attribute.GetCustomAttribute(property, typeof(RequiredAttribute));
                if (requiredAttr != null)
                {
                    var value = property.GetValue(obj);
                    if (value == null || string.IsNullOrWhiteSpace(value.ToString()))
                    {
                        Console.WriteLine($"Validation Error: {requiredAttr.ErrorMessage}");
                        return false;
                    }
                }
            }

            return true;
        }

        public static List<string> GetValidationErrors(object obj)
        {
            List<string> errors = new List<string>();
            var properties = obj.GetType().GetProperties();

            foreach (var property in properties)
            {
                var requiredAttr = (RequiredAttribute?)Attribute.GetCustomAttribute(property, typeof(RequiredAttribute));
                if (requiredAttr != null)
                {
                    var value = property.GetValue(obj);
                    if (value == null || string.IsNullOrWhiteSpace(value.ToString()))
                    {
                        errors.Add($"{property.Name}: {requiredAttr.ErrorMessage}");
                    }
                }
            }

            return errors;
        }
    }
}
