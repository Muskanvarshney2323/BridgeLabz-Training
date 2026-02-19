using System.Collections.Generic;
using HealthCareClinicSystem.Models;

namespace HealthCareClinicSystem.Interfaces
{
    public interface IAdminService
    {
        int AddSpecialty(string specialtyName, string description);
        bool UpdateSpecialty(int specialtyId, string specialtyName, string description);
        bool DeleteSpecialty(int specialtyId);
        List<Specialty> GetAllSpecialties();
        void DisplayAllSpecialties();
        List<AuditLog> GetAuditLogs(string tableName = null, string actionType = null);
        void DisplayAuditLogs(string tableName = null, string actionType = null);
        void PerformDatabaseBackup(string backupPath);
    }
}