using System.Collections.Generic;
using HealthCareClinicSystem.Models;

namespace HealthCareClinicSystem.Interfaces
{
    public interface IPatientRepository
    {
        int RegisterPatient(Patient patient);
        bool UpdatePatient(Patient patient);
        Patient GetPatientById(int patientId);
        Patient GetPatientByPhone(string phone);
        Patient GetPatientByEmail(string email);
        List<Patient> SearchPatients(string searchTerm);
        List<Visit> GetPatientVisitHistory(int patientId);
        bool PatientExists(string phone, string email);
    }
}