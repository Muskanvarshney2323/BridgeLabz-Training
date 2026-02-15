using System;
using EventTracker.Core.Metadata;

namespace EventTracker.Api.Controllers
{
    public class DocumentController
    {
        [AuditInfo("Document upload operation")]
        public void Upload(string documentName)
        {
            Console.WriteLine($"{documentName} has been uploaded.");
        }

        [AuditInfo("Document removal operation")]
        public void Remove(string documentName)
        {
            Console.WriteLine($"{documentName} has been removed.");
        }
    }
}
