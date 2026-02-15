using System;

namespace EventTracker.Core.Entities
{
    public class ActivityLog
    {
        public string LoggedAt { get; set; }
        public string SourceClass { get; set; }
        public string SourceMethod { get; set; }
        public string ActionInfo { get; set; }
        public string ExtraData { get; set; }

        public override string ToString()
        {
            return $"[{LoggedAt}] {SourceClass}.{SourceMethod} - {ActionInfo} | Data: {ExtraData}";
        }
    }
}
