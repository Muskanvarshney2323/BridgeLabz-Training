using System.Collections.Generic;
using HealthCareClinicSystem.Models;

namespace HealthCareClinicSystem.Interfaces
{
    public interface IVisitService
    {
        int RecordVisit(
            int appointmentId,
            string diagnosis,
            string notes,
            List<Prescription> prescriptions
        );
        List<Visit> GetPatientMedicalHistory(int patientId);
        Visit GetVisitById(int visitId);
        List<Prescription> GetPrescriptionsByVisit(int visitId);
        void DisplayVisitDetails(int visitId);
    }
}