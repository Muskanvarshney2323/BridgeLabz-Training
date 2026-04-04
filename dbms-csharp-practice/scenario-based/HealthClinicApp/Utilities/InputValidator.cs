using System.Text.RegularExpressions;

namespace HealthClinicApp.Utilities
{
    public static class InputValidator
    {
        // Phone: exactly 10 digits
        public static bool IsValidPhone(string phone)
        {
            return Regex.IsMatch(phone, @"^\d{10}$");
        }

        // Email format validation
        public static bool IsValidEmail(string email)
        {
            return Regex.IsMatch(email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$");
        }

        // Blood Group validation (ONLY CAPITAL allowed)
        public static bool IsValidBloodGroup(string bloodGroup)
        {
            string[] validGroups = { "A+", "A-", "B+", "B-", "O+", "O-", "AB+", "AB-" };
            return validGroups.Contains(bloodGroup);
        }
    }
}
