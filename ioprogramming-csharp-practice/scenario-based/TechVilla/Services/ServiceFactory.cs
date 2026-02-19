using System;

namespace TechVilla.Services
{
    public static class ServiceFactory
    {
        public static Service Create(string type, string name)
        {
            return type.ToLower() switch
            {
                "healthcare" => new HealthcareService(name),
                "education" => new EducationService(name),
                "emergency" => new EmergencyService(name),
                _ => throw new ArgumentException($"Unknown service type: {type}")
            };
        }
    }
}
