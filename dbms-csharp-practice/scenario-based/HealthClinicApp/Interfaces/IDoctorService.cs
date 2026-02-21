using HealthClinicApp.Models;

namespace HealthClinicApp.Interfaces
{
    public interface IDoctorService
    {
        void AddDoctor(Doctor doctor);
        void UpdateDoctorSpecialty(int doctorId, string newSpecialty);
        List<Doctor> GetDoctorsBySpecialty(string specialtyName);
        bool DeactivateDoctor(int doctorId);
    }
}
