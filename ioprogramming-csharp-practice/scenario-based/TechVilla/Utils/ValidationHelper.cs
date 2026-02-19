using System;

namespace TechVilla.Utils
{
    public static class ValidationHelper
    {
        public static bool IsAdult(int age) => age >= 18;
        public static bool HasMinimumResidency(int years, int minimum) => years >= minimum;
    }
}
