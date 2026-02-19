using System.Collections.Generic;
using HealthCareClinicSystem.Models;

namespace HealthCareClinicSystem.Interfaces
{
    public interface IVisitRepository
    {
        int RecordVisit(Visit visit, List<Prescription> prescriptions);
        List<Visit> GetPatientMedicalHistory(int patientId);
        Visit GetVisitById(int visitId);
        List<Prescription> GetPrescriptionsByVisit(int visitId);
    }
}