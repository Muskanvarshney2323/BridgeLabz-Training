using System;
using System.Text.RegularExpressions;
using System.Reflection;
using AddressBook.Models;
using AddressBook.Attributes;

namespace AddressBook.Utils
{
    /// <summary>
    /// Validator class using reflection to validate contacts based on attributes
    /// Demonstrates reflection capabilities to access and validate attributes at runtime
    /// </summary>
    public class ValidationHelper
    {
        /// <summary>
        /// Validates a contact object using reflection to check custom attributes
        /// </summary>
        public static bool ValidateContact(Contact contact)
        {
            if (contact == null)
            {
                Console.WriteLine("Contact cannot be null");
                return false;
            }

            Type contactType = contact.GetType();
            PropertyInfo[] properties = contactType.GetProperties();

            foreach (PropertyInfo property in properties)
            {
                // Check for Required attribute
                var requiredAttr = property.GetCustomAttribute<Required>();
                if (requiredAttr != null)
                {
                    var value = property.GetValue(contact);
                    if (value == null || string.IsNullOrWhiteSpace(value.ToString()))
                    {
                        Console.WriteLine($"Error: {requiredAttr.ErrorMessage} ({property.Name})");
                        return false;
                    }
                }

                // Check for ValidateName attribute
                var nameAttr = property.GetCustomAttribute<ValidateName>();
                if (nameAttr != null)
                {
                    var value = property.GetValue(contact)?.ToString();
                    if (!Regex.IsMatch(value ?? "", @"^[a-zA-Z\s]*$"))
                    {
                        Console.WriteLine($"Error: {nameAttr.ErrorMessage} ({property.Name})");
                        return false;
                    }
                }

                // Check for ValidateEmail attribute
                var emailAttr = property.GetCustomAttribute<ValidateEmail>();
                if (emailAttr != null)
                {
                    var value = property.GetValue(contact)?.ToString();
                    if (!IsValidEmail(value ?? ""))
                    {
                        Console.WriteLine($"Error: {emailAttr.ErrorMessage}");
                        return false;
                    }
                }

                // Check for ValidatePhoneNumber attribute
                var phoneAttr = property.GetCustomAttribute<ValidatePhoneNumber>();
                if (phoneAttr != null)
                {
                    var value = property.GetValue(contact)?.ToString();
                    if (!Regex.IsMatch(value ?? "", @"^\d{10}$"))
                    {
                        Console.WriteLine($"Error: {phoneAttr.ErrorMessage}");
                        return false;
                    }
                }

                // Check for ValidateZipCode attribute
                var zipAttr = property.GetCustomAttribute<ValidateZipCode>();
                if (zipAttr != null)
                {
                    var value = property.GetValue(contact)?.ToString();
                    if (!Regex.IsMatch(value ?? "", @"^\d{5}$"))
                    {
                        Console.WriteLine($"Error: {zipAttr.ErrorMessage}");
                        return false;
                    }
                }
            }

            return true;
        }

        private static bool IsValidEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Gets all properties marked with Required attribute using reflection
        /// </summary>
        public static PropertyInfo[] GetRequiredProperties(Type type)
        {
            PropertyInfo[] properties = type.GetProperties();
            System.Collections.Generic.List<PropertyInfo> requiredProps = 
                new System.Collections.Generic.List<PropertyInfo>();

            foreach (PropertyInfo prop in properties)
            {
                if (prop.GetCustomAttribute<Required>() != null)
                {
                    requiredProps.Add(prop);
                }
            }

            return requiredProps.ToArray();
        }
    }
}
