using System;
using System.Collections.Generic;
using System.Reflection;

namespace CollectionReflection.AdvancedLevel
{
    /// <summary>
    /// Problem 11: Dependency Injection using Reflection
    /// Implement a simple DI container that scans classes with [Inject] attribute 
    /// and injects dependencies dynamically.
    /// </summary>
    /// 
    // Custom Inject Attribute
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Property | AttributeTargets.Field)]
    public class InjectAttribute : Attribute
    {
    }

    // Define service interfaces and implementations
    interface ILogger
    {
        void Log(string message);
    }

    class ConsoleLogger : ILogger
    {
        public void Log(string message)
        {
            Console.WriteLine($"[LOG] {message}");
        }
    }

    interface IDatabase
    {
        void Connect();
        void Disconnect();
    }

    class SqlDatabase : IDatabase
    {
        public void Connect()
        {
            Console.WriteLine("Connected to SQL Database");
        }

        public void Disconnect()
        {
            Console.WriteLine("Disconnected from SQL Database");
        }
    }

    // Class that requires dependency injection
    [Inject]
    class UserService
    {
        [Inject]
        public ILogger Logger { get; set; }

        [Inject]
        public IDatabase Database { get; set; }

        public void RegisterUser(string username)
        {
            Logger?.Log($"Registering user: {username}");
            Database?.Connect();
            Logger?.Log($"User '{username}' registered successfully");
            Database?.Disconnect();
        }
    }

    [Inject]
    class OrderService
    {
        [Inject]
        public ILogger Logger { get; set; }

        [Inject]
        public IDatabase Database { get; set; }

        public void CreateOrder(string orderId)
        {
            Logger?.Log($"Creating order: {orderId}");
            Database?.Connect();
            Logger?.Log($"Order '{orderId}' created successfully");
            Database?.Disconnect();
        }
    }

    // Simple DI Container
    class DIContainer
    {
        private Dictionary<Type, Type> _bindings = new Dictionary<Type, Type>();
        private Dictionary<Type, object> _singletons = new Dictionary<Type, object>();

        // Register a binding between interface and implementation
        public void Register<TInterface, TImplementation>() where TImplementation : TInterface
        {
            _bindings[typeof(TInterface)] = typeof(TImplementation);
        }

        // Register a singleton instance
        public void RegisterSingleton<TInterface>(object instance)
        {
            _singletons[typeof(TInterface)] = instance;
        }

        // Resolve and create instance with dependency injection
        public T Resolve<T>() where T : class
        {
            return (T)Resolve(typeof(T));
        }

        private object Resolve(Type type)
        {
            // Check if we have a singleton registered
            if (_singletons.ContainsKey(type))
                return _singletons[type];

            // Create instance of the type
            object instance = Activator.CreateInstance(type);

            // Inject dependencies into properties
            PropertyInfo[] properties = type.GetProperties(BindingFlags.Public | BindingFlags.Instance);
            foreach (PropertyInfo property in properties)
            {
                // Check if property has Inject attribute
                if (property.GetCustomAttribute<InjectAttribute>() != null)
                {
                    Type propertyType = property.PropertyType;
                    
                    if (_bindings.ContainsKey(propertyType))
                    {
                        // Resolve the dependency
                        object dependency = Activator.CreateInstance(_bindings[propertyType]);
                        property.SetValue(instance, dependency);
                    }
                    else if (_singletons.ContainsKey(propertyType))
                    {
                        property.SetValue(instance, _singletons[propertyType]);
                    }
                }
            }

            // Inject dependencies into fields
            FieldInfo[] fields = type.GetFields(BindingFlags.Public | BindingFlags.Instance);
            foreach (FieldInfo field in fields)
            {
                if (field.GetCustomAttribute<InjectAttribute>() != null)
                {
                    Type fieldType = field.FieldType;
                    
                    if (_bindings.ContainsKey(fieldType))
                    {
                        object dependency = Activator.CreateInstance(_bindings[fieldType]);
                        field.SetValue(instance, dependency);
                    }
                    else if (_singletons.ContainsKey(fieldType))
                    {
                        field.SetValue(instance, _singletons[fieldType]);
                    }
                }
            }

            return instance;
        }
    }

    class DependencyInjectionContainer
    {
        static void Main()
        {
            Console.WriteLine("=== Dependency Injection Using Reflection ===\n");

            // Create DI container
            DIContainer container = new DIContainer();

            // Register bindings
            container.Register<ILogger, ConsoleLogger>();
            container.Register<IDatabase, SqlDatabase>();

            // Example 1: Resolve UserService
            Console.WriteLine("Example 1: UserService with Dependency Injection\n");
            UserService userService = container.Resolve<UserService>();
            userService.RegisterUser("JohnDoe");

            // Example 2: Resolve OrderService
            Console.WriteLine("\n\nExample 2: OrderService with Dependency Injection\n");
            OrderService orderService = container.Resolve<OrderService>();
            orderService.CreateOrder("ORD-12345");

            // Example 3: Using singleton instances
            Console.WriteLine("\n\nExample 3: Using Singleton Instances\n");
            DIContainer singletonContainer = new DIContainer();
            singletonContainer.RegisterSingleton<ILogger>(new ConsoleLogger());
            singletonContainer.RegisterSingleton<IDatabase>(new SqlDatabase());
            
            UserService userService2 = singletonContainer.Resolve<UserService>();
            userService2.RegisterUser("AliceSmith");

            Console.WriteLine("\n\nExample 4: Scanning for [Inject] Attribute\n");
            ScanAndDisplayInjectableClasses();
        }

        static void ScanAndDisplayInjectableClasses()
        {
            // Get all types in current assembly that have [Inject] attribute
            Assembly assembly = Assembly.GetExecutingAssembly();
            Type[] types = assembly.GetTypes();

            Console.WriteLine("Classes with [Inject] attribute:");
            foreach (Type type in types)
            {
                if (type.GetCustomAttribute<InjectAttribute>() != null)
                {
                    Console.WriteLine($"\n  Class: {type.Name}");
                    
                    PropertyInfo[] properties = type.GetProperties(BindingFlags.Public | BindingFlags.Instance);
                    if (properties.Length > 0)
                    {
                        Console.WriteLine("    Properties with [Inject]:");
                        foreach (PropertyInfo prop in properties)
                        {
                            if (prop.GetCustomAttribute<InjectAttribute>() != null)
                            {
                                Console.WriteLine($"      - {prop.PropertyType.Name} {prop.Name}");
                            }
                        }
                    }
                }
            }
        }
    }
}
