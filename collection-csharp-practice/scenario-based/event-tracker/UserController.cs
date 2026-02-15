using System;
using EventTracker.Core.Metadata;

namespace EventTracker.Api.Controllers
{
    public class AccountController
    {
        [AuditInfo("Account sign-in operation")]
        public void SignIn(string userId)
        {
            Console.WriteLine($"{userId} has signed in.");
        }

        [AuditInfo("Account sign-out operation")]
        public void SignOut(string userId)
        {
            Console.WriteLine($"{userId} has signed out.");
        }

        public void UtilityTask()
        {
            // This method is intentionally excluded from auditing
        }
    }
}
