using System;
using System.Collections.Generic;
using EventTracker.Api.Controllers;
using EventTracker.Core.Entities;
using EventTracker.Contracts;
using EventTracker.Application.Services;

namespace EventTrackerApp
{
    internal class Startup
    {
        static void Main(string[] args)
        {
            Console.WriteLine("=== Audit Tracker | Automatic Logging System ===\n");

            // Types that will be inspected for audit attributes
            IList<Type> auditTargets = new List<Type>
            {
                typeof(DocumentController),
                typeof(AccountController)
            };

            IAuditTrackingService auditService =
                new AuditScannerService(auditTargets.ToList());

            // Generate audit logs
            List<ActivityLog> auditLogs = auditService.CollectAuditLogs();

            // Display logs on console
            Console.WriteLine("Generated Audit Logs:\n");
            foreach (ActivityLog entry in auditLogs)
            {
                Console.WriteLine(entry);
            }

            // Persist logs as JSON
            auditService.SaveLogsAsJson(auditLogs, "AuditLogs.json");

            Console.WriteLine("\nPress any key to close...");
            Console.ReadKey();
        }
    }
}
