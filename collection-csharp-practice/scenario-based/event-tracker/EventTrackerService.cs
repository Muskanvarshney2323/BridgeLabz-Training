using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using EventTracker.Core.Metadata;
using EventTracker.Core.Entities;
using EventTracker.Interfaces;
using Newtonsoft.Json;

namespace EventTracker.Application.Services
{
    public class AuditScannerService : IEventTrackerService
    {
        private readonly IList<Type> _targetTypes;

        public AuditScannerService(IList<Type> targetTypes)
        {
            _targetTypes = targetTypes;
        }

        public List<ActivityLog> ScanAndLogEvents()
        {
            List<ActivityLog> results = new List<ActivityLog>();

            foreach (Type currentType in _targetTypes)
            {
                MethodInfo[] declaredMethods = currentType.GetMethods(
                    BindingFlags.Instance | BindingFlags.Public | BindingFlags.DeclaredOnly
                );

                foreach (MethodInfo methodInfo in declaredMethods)
                {
                    AuditInfoAttribute attribute =
                        methodInfo.GetCustomAttribute<AuditInfoAttribute>();

                    if (attribute == null)
                        continue;

                    ActivityLog logEntry = new ActivityLog
                    {
                        LoggedAt = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
                        SourceClass = currentType.Name,
                        SourceMethod = methodInfo.Name,
                        ActionInfo = attribute.Details,
                        ExtraData = "N/A"
                    };

                    results.Add(logEntry);
                }
            }

            return results;
        }

        public void ExportLogsToJson(List<ActivityLog> logEntries, string outputPath)
        {
            string jsonOutput = JsonConvert.SerializeObject(
                logEntries,
                Formatting.Indented
            );

            File.WriteAllText(outputPath, jsonOutput);
            Console.WriteLine($"\nAudit logs successfully written to: {outputPath}");
        }
    }
}
