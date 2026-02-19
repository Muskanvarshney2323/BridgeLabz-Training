using System.Text.RegularExpressions;

namespace TechVilla.Utils
{
    public static class StringUtils
    {
        public static string FormatName(string name)
        {
            if (string.IsNullOrWhiteSpace(name)) return string.Empty;
            name = name.Trim();
            return char.ToUpper(name[0]) + name.Substring(1).ToLower();
        }

        public static bool IsValidEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email)) return false;
            var pattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
            return Regex.IsMatch(email, pattern);
        }
    }
}
