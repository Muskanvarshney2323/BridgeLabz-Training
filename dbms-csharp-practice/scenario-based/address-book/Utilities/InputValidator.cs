using System.Text.RegularExpressions;
using AddressBookSystem.Interfaces;

namespace AddressBookSystem.Utilities
{
    public class InputValidator : IInputValidator
    {
        public bool IsValidEmail(string email)
        {
            try
            {
                string pattern = @"^[^\s@]+@[^\s@]+\.[^\s@]+$";
                return Regex.IsMatch(email, pattern);
            }
            catch
            {
                return false;
            }
        }

        public bool IsValidPhoneNumber(string phone)
        {
            string cleanedPhone = new string(phone.Where(char.IsDigit).ToArray());
            return cleanedPhone.Length >= 10;
        }

        public bool IsValidZip(string zip)
        {
            string cleanedZip = new string(zip.Where(char.IsDigit).ToArray());
            return cleanedZip.Length >= 5;
        }

        public string GetValidInput(string prompt, Func<string, bool> validator)
        {
            while (true)
            {
                Console.Write(prompt);
                string? input = Console.ReadLine();
                
                if (!string.IsNullOrWhiteSpace(input) && validator(input))
                {
                    return input;
                }

                Console.WriteLine("Invalid input. Please try again.");
            }
        }
    }
}
