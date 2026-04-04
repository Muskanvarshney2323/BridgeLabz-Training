using HealthClinicApp.Models;
using HealthClinicApp.Exceptions;

namespace HealthClinicApp.Utilities
{
    public static class PatientValidator
    {
        public static void Validate(Patient patient)
        {
            if (string.IsNullOrWhiteSpace(patient.FullName))
                throw new ClinicException("Patient name cannot be empty.");

            if (patient.DOB >= DateTime.Now)
                throw new ClinicException("Invalid Date of Birth.");

            if (string.IsNullOrWhiteSpace(patient.Phone))
                throw new ClinicException("Phone number required.");
        }
    }
}
