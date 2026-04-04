using System.Collections.Generic;
using HealthCareClinicSystem.Models;

namespace HealthCareClinicSystem.Interfaces
{
    public interface IDoctorService
    {
        int AddDoctor(Doctor doctor);
        bool UpdateDoctor(Doctor doctor);
        bool UpdateDoctorSpecialty(int doctorId, int specialtyId);
        bool DeactivateDoctor(int doctorId);
        Doctor GetDoctorById(int doctorId);
        List<Doctor> GetDoctorsBySpecialty(string specialtyName);
        List<Doctor> GetAllActiveDoctors();
        List<Specialty> GetAllSpecialties();
        bool CanDeactivateDoctor(int doctorId);
    }
}