using System;

namespace TechVilla.Exceptions
{
    public class DuplicateCitizenException : Exception
    {
        public string CitizenName { get; }
        public DuplicateCitizenException(string name) : base($"Citizen '{name}' already exists") => CitizenName = name;
    }
}
