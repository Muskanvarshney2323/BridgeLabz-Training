using System;
using System.Reflection;

namespace CollectionAnnotations.Intermediate
{
    /// <summary>
    /// Intermediate Level - Problem 4: Create a MaxLength Attribute for Field Validation
    /// Define a field-level attribute MaxLength(int value) that restricts maximum length 
    /// of a string field.
    /// </summary>
    /// 
    // Custom MaxLength Attribute
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field)]
    public class MaxLengthAttribute : Attribute
    {
        public int MaxLength { get; set; }

        public MaxLengthAttribute(int maxLength)
        {
            MaxLength = maxLength;
        }
    }

    class User
    {
        [MaxLength(20)]
        public string Username { get; set; }

        [MaxLength(50)]
        public string Email { get; set; }

        [MaxLength(100)]
        public string FullName { get; set; }

        [MaxLength(500)]
        public string Bio { get; set; }

        public int Age { get; set; } // No MaxLength

        public User(string username, string email, string fullName, string bio, int age)
        {
            // Validate and set values
            ValidateAndSet(nameof(Username), username);
            ValidateAndSet(nameof(Email), email);
            ValidateAndSet(nameof(FullName), fullName);
            ValidateAndSet(nameof(Bio), bio);
            Age = age;
        }

        private void ValidateAndSet(string propertyName, string value)
        {
            Type userType = this.GetType();
            PropertyInfo property = userType.GetProperty(propertyName);

            if (property != null)
            {
                var maxLengthAttr = property.GetCustomAttribute<MaxLengthAttribute>();

                if (maxLengthAttr != null && value != null && value.Length > maxLengthAttr.MaxLength)
                {
                    throw new ArgumentException(
                        $"Property '{propertyName}' exceeds maximum length of {maxLengthAttr.MaxLength}. " +
                        $"Provided length: {value.Length}");
                }
            }

            property?.SetValue(this, value);
        }

        public override string ToString()
        {
            return $"User: {Username}, Email: {Email}, Name: {FullName}, Age: {Age}";
        }
    }

    class MaxLengthAttributeDemo
    {
        static void Main()
        {
            Console.WriteLine("=== MaxLength Attribute - Intermediate Level ===\n");

            // Example 1: Create valid user
            Console.WriteLine("Example 1: Creating Valid User\n");
            CreateValidUser();

            // Example 2: Attempt to create invalid user (too long username)
            Console.WriteLine("\n\nExample 2: Creating Invalid User (Username Too Long)\n");
            CreateInvalidUser("this_is_a_very_long_username_that_exceeds_limit", 
                              "user@example.com", 
                              "John Doe", 
                              "A great developer");

            // Example 3: Attempt to create invalid user (too long email)
            Console.WriteLine("\n\nExample 3: Creating Invalid User (Email Too Long)\n");
            CreateInvalidUser("john123", 
                              "this.is.a.very.long.email.address.that.exceeds.the.maximum.length@example.com", 
                              "John Doe", 
                              "A developer");

            // Example 4: Display MaxLength constraints for User class
            Console.WriteLine("\n\nExample 4: User Field Constraints\n");
            DisplayFieldConstraints();

            // Example 5: Interactive user creation
            Console.WriteLine("\n\nExample 5: Interactive User Creation\n");
            InteractiveUserCreation();
        }

        static void CreateValidUser()
        {
            try
            {
                User user = new User("john123", "john@example.com", "John Doe", "I love coding!", 25);
                Console.WriteLine("✓ User created successfully!");
                Console.WriteLine(user);
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine($"✗ Error: {ex.Message}");
            }
        }

        static void CreateInvalidUser(string username, string email, string fullName, string bio)
        {
            try
            {
                User user = new User(username, email, fullName, bio, 30);
                Console.WriteLine("✓ User created successfully!");
                Console.WriteLine(user);
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine($"✗ Validation Error: {ex.Message}");
            }
        }

        static void DisplayFieldConstraints()
        {
            Type userType = typeof(User);
            PropertyInfo[] properties = userType.GetProperties();

            Console.WriteLine("Field Constraints:\n");
            Console.WriteLine("{0,-20} {1,-20} {2,-15}", "Field Name", "Max Length", "Current Type");
            Console.WriteLine(new string('-', 55));

            foreach (PropertyInfo property in properties)
            {
                var maxLengthAttr = property.GetCustomAttribute<MaxLengthAttribute>();

                if (maxLengthAttr != null)
                {
                    Console.WriteLine("{0,-20} {1,-20} {2,-15}", 
                        property.Name,
                        maxLengthAttr.MaxLength,
                        property.PropertyType.Name);
                }
                else
                {
                    Console.WriteLine("{0,-20} {1,-20} {2,-15}", 
                        property.Name,
                        "No limit",
                        property.PropertyType.Name);
                }
            }
        }

        static void InteractiveUserCreation()
        {
            Console.Write("Enter username (max 20 chars): ");
            string username = Console.ReadLine();

            Console.Write("Enter email (max 50 chars): ");
            string email = Console.ReadLine();

            Console.Write("Enter full name (max 100 chars): ");
            string fullName = Console.ReadLine();

            Console.Write("Enter bio (max 500 chars): ");
            string bio = Console.ReadLine();

            Console.Write("Enter age: ");
            int age = int.Parse(Console.ReadLine());

            try
            {
                User user = new User(username, email, fullName, bio, age);
                Console.WriteLine("\n✓ User created successfully!");
                Console.WriteLine(user);
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine($"\n✗ Validation Error: {ex.Message}");
            }
        }
    }

    // Additional example: Product with MaxLength validation
    class Product
    {
        [MaxLength(30)]
        public string ProductName { get; set; }

        [MaxLength(500)]
        public string Description { get; set; }

        [MaxLength(10)]
        public string SKU { get; set; }

        public decimal Price { get; set; }

        public Product(string name, string description, string sku, decimal price)
        {
            ValidateProperty(nameof(ProductName), name);
            ValidateProperty(nameof(Description), description);
            ValidateProperty(nameof(SKU), sku);

            ProductName = name;
            Description = description;
            SKU = sku;
            Price = price;
        }

        private void ValidateProperty(string propertyName, string value)
        {
            Type productType = this.GetType();
            PropertyInfo property = productType.GetProperty(propertyName);

            if (property != null)
            {
                var maxLengthAttr = property.GetCustomAttribute<MaxLengthAttribute>();

                if (maxLengthAttr != null && value != null && value.Length > maxLengthAttr.MaxLength)
                {
                    throw new ArgumentException(
                        $"'{propertyName}' exceeds maximum length of {maxLengthAttr.MaxLength}. " +
                        $"Current length: {value.Length}");
                }
            }
        }

        public override string ToString()
        {
            return $"Product: {ProductName} (SKU: {SKU}) - ${Price}";
        }
    }
}
