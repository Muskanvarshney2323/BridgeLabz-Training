using HealthClinicApp.Models;
using HealthClinicApp.Services;

namespace HealthClinicApp.Menus
{
    public class VisitMenu
    {
        private readonly VisitService service = new VisitService();

        public void Show()
        {
            Console.WriteLine("\n--- Visit Management ---");
            Console.WriteLine("1. Record Visit");
            Console.WriteLine("2. View Medical History");
            Console.WriteLine("3. Add Prescriptions");
            Console.WriteLine("0. Back");

            int choice = int.Parse(Console.ReadLine());

            switch (choice)
            {
                case 1:
                    var v = new Visit();
                    Console.Write("AppointmentId: "); v.AppointmentId = int.Parse(Console.ReadLine());
                    Console.Write("PatientId: "); v.PatientId = int.Parse(Console.ReadLine());
                    Console.Write("DoctorId: "); v.DoctorId = int.Parse(Console.ReadLine());
                    v.VisitDate = DateTime.Now;
                    Console.Write("Diagnosis: "); v.Diagnosis = Console.ReadLine();
                    Console.Write("Notes: "); v.Notes = Console.ReadLine();

                    int visitId = service.RecordVisit(v);
                    Console.WriteLine($"Visit ID: {visitId}");
                    break;

                case 2:
                    Console.Write("PatientId: ");
                    service.ViewMedicalHistory(int.Parse(Console.ReadLine()));
                    break;

                case 3:
                    Console.Write("VisitId: ");
                    int vid = int.Parse(Console.ReadLine());

                    var list = new List<Prescription>();
                    Console.Write("How many medicines? ");
                    int count = int.Parse(Console.ReadLine());

                    for (int i = 0; i < count; i++)
                    {
                        var p = new Prescription();
                        Console.Write("Medicine: "); p.MedicineName = Console.ReadLine();
                        Console.Write("Dosage: "); p.Dosage = Console.ReadLine();
                        Console.Write("Duration: "); p.Duration = Console.ReadLine();
                        list.Add(p);
                    }
                    service.AddPrescriptions(vid, list);
                    break;

                case 0: return;
            }
        }
    }
}
