using System;

namespace TechVilla.Utils
{
    public static class InputHelper
    {
        public static string ReadLine(string prompt)
        {
            Console.Write(prompt);
            return Console.ReadLine() ?? string.Empty;
        }
    }
}
