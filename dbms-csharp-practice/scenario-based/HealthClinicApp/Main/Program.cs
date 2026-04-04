using HealthClinicApp.Menus;

namespace HealthClinicApp
{
    class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                Console.WriteLine("\n--- HEALTH CLINIC SYSTEM ---");
                Console.WriteLine("1. Patient Management");
                Console.WriteLine("2. Doctor Management");
                Console.WriteLine("3. Appointment Scheduling");
                Console.WriteLine("4. Visit Management");
                Console.WriteLine("5. Billing Menu");
                Console.WriteLine("6. Admin Service");
                Console.WriteLine("0. Exit");

                int choice = int.Parse(Console.ReadLine());

                switch (choice)
                {
                    case 1:
                        new PatientMenu().Show();
                        break;
                    case 2:
                        new DoctorMenu().Show();
                        break;
                    case 3:
                        new AppointmentMenu().Show();
                        break;
                    case 4:
                        new VisitMenu().Show();
                        break;
                    case 5:
                        new BillingMenu().Show();
                        break;
                    case 6:
                        new AdminMenu().Show();
                        break;

                    case 0:
                        return;
                    default:
                        Console.WriteLine("Invalid choice");
                        break;
                }
            }
        }
    }
}
