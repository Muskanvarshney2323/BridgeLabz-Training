using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace CollectionAnnotations.Advanced
{
    /// <summary>
    /// Advanced Level - Problem 6: Implement a Custom Serialization Attribute JsonField
    /// Define an attribute JsonField to mark fields for JSON serialization.
    /// Write a method to convert an object to a JSON string by reading the attributes.
    /// </summary>
    /// 
    // Custom JsonField Attribute
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field)]
    public class JsonFieldAttribute : Attribute
    {
        public string Name { get; set; }

        public JsonFieldAttribute(string name = null)
        {
            Name = name;
        }
    }

    class User
    {
        [JsonField("user_id")]
        public int UserId { get; set; }

        [JsonField("user_name")]
        public string Username { get; set; }

        [JsonField("email_address")]
        public string Email { get; set; }

        [JsonField("is_active")]
        public bool IsActive { get; set; }

        [JsonField("join_date")]
        public string JoinDate { get; set; }

        // Property without JsonField attribute (will not be serialized)
        public string InternalNote { get; set; }

        public User(int userId, string username, string email, bool isActive, string joinDate)
        {
            UserId = userId;
            Username = username;
            Email = email;
            IsActive = isActive;
            JoinDate = joinDate;
            InternalNote = "Internal tracking";
        }
    }

    class Product
    {
        [JsonField("product_id")]
        public int ProductId { get; set; }

        [JsonField("product_name")]
        public string Name { get; set; }

        [JsonField("price")]
        public decimal Price { get; set; }

        [JsonField("in_stock")]
        public bool InStock { get; set; }

        public string WarehouseLocation { get; set; }

        public Product(int id, string name, decimal price, bool inStock)
        {
            ProductId = id;
            Name = name;
            Price = price;
            InStock = inStock;
            WarehouseLocation = "Warehouse A";
        }
    }

    class Order
    {
        [JsonField("order_id")]
        public string OrderId { get; set; }

        [JsonField("customer_name")]
        public string CustomerName { get; set; }

        [JsonField("total_amount")]
        public decimal TotalAmount { get; set; }

        [JsonField("order_status")]
        public string Status { get; set; }

        public Order(string orderId, string customerName, decimal totalAmount, string status)
        {
            OrderId = orderId;
            CustomerName = customerName;
            TotalAmount = totalAmount;
            Status = status;
        }
    }

    // JSON Serializer using Reflection
    public static class JsonSerializer
    {
        public static string ToJson<T>(T obj) where T : class
        {
            if (obj == null)
                return "null";

            Type objType = typeof(T);
            StringBuilder json = new StringBuilder();
            json.Append("{");

            bool isFirst = true;

            // Serialize properties
            PropertyInfo[] properties = objType.GetProperties(BindingFlags.Public | BindingFlags.Instance);

            foreach (PropertyInfo property in properties)
            {
                var jsonFieldAttr = property.GetCustomAttribute<JsonFieldAttribute>();

                if (jsonFieldAttr != null)
                {
                    if (!isFirst)
                        json.Append(", ");

                    string fieldName = jsonFieldAttr.Name ?? property.Name;
                    object value = property.GetValue(obj);

                    json.Append($"\"{fieldName}\": {FormatValue(value)}");
                    isFirst = false;
                }
            }

            json.Append("}");
            return json.ToString();
        }

        public static string ToJsonWithIndent<T>(T obj, int indentLevel = 0) where T : class
        {
            if (obj == null)
                return "null";

            Type objType = typeof(T);
            StringBuilder json = new StringBuilder();
            string indent = new string(' ', indentLevel * 2);
            string nextIndent = new string(' ', (indentLevel + 1) * 2);

            json.AppendLine("{");

            bool isFirst = true;
            PropertyInfo[] properties = objType.GetProperties(BindingFlags.Public | BindingFlags.Instance);

            foreach (PropertyInfo property in properties)
            {
                var jsonFieldAttr = property.GetCustomAttribute<JsonFieldAttribute>();

                if (jsonFieldAttr != null)
                {
                    if (!isFirst)
                        json.AppendLine(",");

                    string fieldName = jsonFieldAttr.Name ?? property.Name;
                    object value = property.GetValue(obj);

                    json.Append($"{nextIndent}\"{fieldName}\": {FormatValue(value)}");
                    isFirst = false;
                }
            }

            json.AppendLine();
            json.Append($"{indent}}}");
            return json.ToString();
        }

        private static string FormatValue(object value)
        {
            if (value == null)
                return "null";

            Type valueType = value.GetType();

            if (valueType == typeof(string))
                return $"\"{value}\"";
            else if (valueType == typeof(bool))
                return value.ToString().ToLower();
            else if (valueType == typeof(decimal) || valueType == typeof(double) || valueType == typeof(float))
                return value.ToString();
            else if (valueType.IsPrimitive)
                return value.ToString();
            else
                return "\"" + value.ToString() + "\"";
        }
    }

    class JsonFieldAttributeDemo
    {
        static void Main()
        {
            Console.WriteLine("=== Custom JsonField Attribute - Advanced Level ===\n");

            // Example 1: Serialize User object
            Console.WriteLine("Example 1: Serialize User Object\n");
            SerializeUser();

            // Example 2: Serialize Product object
            Console.WriteLine("\n\nExample 2: Serialize Product Object\n");
            SerializeProduct();

            // Example 3: Serialize Order with formatted JSON
            Console.WriteLine("\n\nExample 3: Serialize Order (Formatted JSON)\n");
            SerializeOrderFormatted();

            // Example 4: Serialize multiple objects
            Console.WriteLine("\n\nExample 4: Serialize Array of Users\n");
            SerializeMultipleObjects();

            // Example 5: Display JsonField mapping
            Console.WriteLine("\n\nExample 5: JsonField Mapping\n");
            DisplayJsonFieldMapping();
        }

        static void SerializeUser()
        {
            User user = new User(1001, "john_doe", "john@example.com", true, "2024-01-15");
            Console.WriteLine("User Object:");
            Console.WriteLine(JsonSerializer.ToJson(user));
        }

        static void SerializeProduct()
        {
            Product product = new Product(501, "Laptop Computer", 999.99m, true);
            Console.WriteLine("Product Object:");
            Console.WriteLine(JsonSerializer.ToJson(product));
        }

        static void SerializeOrderFormatted()
        {
            Order order = new Order("ORD-12345", "Alice Smith", 2500.50m, "SHIPPED");
            Console.WriteLine("Order Object (Formatted):");
            Console.WriteLine(JsonSerializer.ToJsonWithIndent(order));
        }

        static void SerializeMultipleObjects()
        {
            User[] users = new User[]
            {
                new User(1001, "john_doe", "john@example.com", true, "2024-01-15"),
                new User(1002, "jane_smith", "jane@example.com", true, "2024-02-20"),
                new User(1003, "bob_wilson", "bob@example.com", false, "2024-03-10")
            };

            Console.WriteLine("[\n");
            for (int i = 0; i < users.Length; i++)
            {
                Console.Write(JsonSerializer.ToJsonWithIndent(users[i], 1));
                if (i < users.Length - 1)
                    Console.WriteLine(",");
                Console.WriteLine();
            }
            Console.WriteLine("]");
        }

        static void DisplayJsonFieldMapping()
        {
            Type[] types = { typeof(User), typeof(Product), typeof(Order) };

            foreach (Type type in types)
            {
                Console.WriteLine($"\n{type.Name} Field Mapping:");
                Console.WriteLine("{0,-25} {1,-25}", "Property Name", "JSON Field Name");
                Console.WriteLine(new string('-', 50));

                PropertyInfo[] properties = type.GetProperties(BindingFlags.Public | BindingFlags.Instance);

                foreach (PropertyInfo property in properties)
                {
                    var jsonFieldAttr = property.GetCustomAttribute<JsonFieldAttribute>();

                    if (jsonFieldAttr != null)
                    {
                        string fieldName = jsonFieldAttr.Name ?? property.Name;
                        Console.WriteLine("{0,-25} {1,-25}", property.Name, fieldName);
                    }
                    else
                    {
                        Console.WriteLine("{0,-25} {1,-25}", property.Name, "(not mapped)");
                    }
                }
            }
        }
    }
}
