using HealthClinicApp.Models;
using HealthClinicApp.Services;

namespace HealthClinicApp.Menus
{
    public class AppointmentMenu
    {
        private readonly AppointmentService service = new AppointmentService();

        public void Show()
        {
            Console.WriteLine("\n--- Appointment Menu ---");
            Console.WriteLine("1. Book");
            Console.WriteLine("2. Check Availability");
            Console.WriteLine("3. Cancel");
            Console.WriteLine("4. Reschedule");
            Console.WriteLine("5. Daily Schedule");
            Console.WriteLine("0. Back");

            int choice = int.Parse(Console.ReadLine());

            switch (choice)
            {
                case 1:
                    Appointment a = new Appointment();
                    Console.Write("PatientId: "); a.PatientId = int.Parse(Console.ReadLine());
                    Console.Write("DoctorId: "); a.DoctorId = int.Parse(Console.ReadLine());
                    Console.Write("DateTime: "); a.AppointmentDate = DateTime.Parse(Console.ReadLine());
                    service.BookAppointment(a);
                    break;

                case 2:
                    service.CheckDoctorAvailability(
                        int.Parse(Console.ReadLine()),
                        DateTime.Parse(Console.ReadLine()));
                    break;

                case 3:
                    service.CancelAppointment(int.Parse(Console.ReadLine()));
                    break;

                case 4:
                    service.RescheduleAppointment(
                        int.Parse(Console.ReadLine()),
                        int.Parse(Console.ReadLine()),
                        DateTime.Parse(Console.ReadLine()));
                    break;

                case 5:
                    service.ViewDailySchedule(DateTime.Parse(Console.ReadLine()));
                    break;

                case 0: return;
            }
        }
    }
}
