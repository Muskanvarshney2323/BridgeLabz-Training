using System.Collections.Generic;
using HealthCareClinicSystem.Models;

namespace HealthCareClinicSystem.Interfaces
{
    public interface IPatientService
    {
        int RegisterPatient(Patient patient);
        bool UpdatePatient(Patient patient);
        Patient GetPatientById(int patientId);
        Patient GetPatientByPhone(string phone);
        List<Patient> SearchPatients(string searchTerm);
        List<Visit> GetPatientVisitHistory(int patientId);
        List<Prescription> GetPrescriptionsByVisit(int visitId);
    }
}