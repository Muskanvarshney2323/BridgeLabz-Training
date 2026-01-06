using System;
class CustomerCallLog
{
    public string CustomerName { get; set; }
    public string PhoneNumber { get; set; }
    public DateTime CallTime { get; set; }
    public string IssueDescription { get; set; }

    public CustomerCallLog(string customerName, string phoneNumber, DateTime callTime, string issueDescription)
    {
        CustomerName = customerName;
        PhoneNumber = phoneNumber;
        CallTime = callTime;
        IssueDescription = issueDescription;
    }

    public void DisplayCallDetails()
    {
        Console.WriteLine("Customer Call Log:");
        Console.WriteLine($"Name: {CustomerName}");
        Console.WriteLine($"Phone: {PhoneNumber}");
        Console.WriteLine($"Call Time: {CallTime}");
        Console.WriteLine($"Issue: {IssueDescription}");
    }
}
class Program
{
    static void Main()
    {
        CustomerCallLog callLog = new CustomerCallLog(
            "Alice Johnson",
            "555-1234",
            DateTime.Now,
            "Unable to access account"
        );
 
        callLog.DisplayCallDetails();
    }
}   