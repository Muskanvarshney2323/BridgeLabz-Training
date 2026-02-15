using System.Collections.Generic;
using EventTracker.Core.Entities;

namespace EventTracker.Contracts
{
    public interface IAuditTrackingService
    {
        List<ActivityLog> CollectAuditLogs();
        void SaveLogsAsJson(List<ActivityLog> entries, string destinationPath);
    }
}
