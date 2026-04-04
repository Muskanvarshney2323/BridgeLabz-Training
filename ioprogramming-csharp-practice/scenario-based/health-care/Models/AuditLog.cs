using System;

namespace HealthCareClinicSystem.Models
{
    public class AuditLog
    {
        public int AuditId { get; set; }
        public string TableName { get; set; }
        public string ActionType { get; set; }
        public int RecordId { get; set; }
        public DateTime ActionDate { get; set; }
        public string PerformedBy { get; set; }
    }
}