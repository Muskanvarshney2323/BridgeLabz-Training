using System.Collections.Generic;
using HealthCareClinicSystem.Models;

namespace HealthCareClinicSystem.Interfaces
{
    public interface IAdminRepository
    {
        int AddSpecialty(Specialty specialty);
        bool UpdateSpecialty(Specialty specialty);
        bool DeleteSpecialty(int specialtyId);
        List<Specialty> GetAllSpecialties();
        bool CanDeleteSpecialty(int specialtyId);
        List<AuditLog> GetAuditLogs(string tableName = null, string actionType = null);
        void PerformDatabaseBackup(string backupPath);
    }
}