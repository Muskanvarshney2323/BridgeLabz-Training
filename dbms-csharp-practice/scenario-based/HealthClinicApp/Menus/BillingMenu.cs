using HealthClinicApp.Services;

namespace HealthClinicApp.Menus
{
    public class BillingMenu
    {
        private BillingService service = new BillingService();

        public void Show()
        {
            Console.WriteLine("\n--- Billing Menu ---");
            Console.WriteLine("1. Generate Bill");
            Console.WriteLine("2. Record Payment");
            Console.WriteLine("3. Outstanding Bills");
            Console.WriteLine("4. Revenue Report");
            Console.WriteLine("0. Back");

            int ch = int.Parse(Console.ReadLine());

            switch (ch)
            {
                case 1:
                    service.GenerateBill(int.Parse(Console.ReadLine()));
                    break;
                case 2:
                    service.RecordPayment(
                        int.Parse(Console.ReadLine()),
                        decimal.Parse(Console.ReadLine()),
                        Console.ReadLine());
                    break;
                case 3:
                    service.ViewOutstandingBills();
                    break;
                case 4:
                    service.RevenueReport(
                        DateTime.Parse(Console.ReadLine()),
                        DateTime.Parse(Console.ReadLine()));
                    break;
            }
        }
    }
}
