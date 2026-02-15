using System;

namespace EventTracker.Core.Metadata
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
    public sealed class AuditInfoAttribute : Attribute
    {
        public string Details { get; private set; }

        public AuditInfoAttribute(string detailsText = "")
        {
            Details = detailsText;
        }
    }
}
