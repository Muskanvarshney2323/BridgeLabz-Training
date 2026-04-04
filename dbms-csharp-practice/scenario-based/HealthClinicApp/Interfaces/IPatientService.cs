using HealthClinicApp.Models;

namespace HealthClinicApp.Interfaces
{
    public interface IPatientService
    {
        void RegisterPatient(Patient patient);
        Patient GetPatientById(int id);   // 🔥 ADDED
        void UpdatePatient(Patient patient);
        List<Patient> SearchPatients(string keyword);
        void ViewVisitHistory(int patientId);
    }
}
