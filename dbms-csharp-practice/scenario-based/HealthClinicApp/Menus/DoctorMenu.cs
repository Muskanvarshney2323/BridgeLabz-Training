using HealthClinicApp.Models;
using HealthClinicApp.Services;

namespace HealthClinicApp.Menus
{
    public class DoctorMenu
    {
        private readonly DoctorService service = new DoctorService();

        public void Show()
        {
            while (true)
            {
                Console.WriteLine("\n--- Doctor Management ---");
                Console.WriteLine("1. Add Doctor");
                Console.WriteLine("2. Update Doctor Specialty");
                Console.WriteLine("3. View Doctors by Specialty");
                Console.WriteLine("4. Deactivate Doctor");
                Console.WriteLine("0. Back");
                Console.Write("Enter choice: ");

                string? choice = Console.ReadLine();
                switch (choice)
                {
                    case "1": AddDoctor(); break;
                    case "2": UpdateSpecialty(); break;
                    case "3": ViewBySpecialty(); break;
                    case "4": DeactivateDoctor(); break;
                    case "0": return;
                    default: Console.WriteLine("Invalid choice!"); break;
                }
            }
        }

        private void AddDoctor()
        {
            var doctor = new Doctor();

            Console.Write("Full Name: ");
            doctor.FullName = Console.ReadLine()!;

            // Phone validation
            while (true)
            {
                Console.Write("Phone (10 digits): ");
                string? phone = Console.ReadLine();
                if (!string.IsNullOrEmpty(phone) && phone.Length == 10 && long.TryParse(phone, out _))
                {
                    doctor.Phone = phone;
                    break;
                }
                Console.WriteLine("❌ Invalid Phone! Must be exactly 10 digits.");
            }

            // Email validation
            while (true)
            {
                Console.Write("Email: ");
                string? email = Console.ReadLine();
                if (!string.IsNullOrEmpty(email) && email.Contains("@") && email.Contains("."))
                {
                    doctor.Email = email;
                    break;
                }
                Console.WriteLine("❌ Invalid Email format!");
            }

            Console.Write("Specialty: ");
            doctor.Specialty = Console.ReadLine()!;

            Console.Write("Consultation Fee: ");
            doctor.ConsultationFee = decimal.Parse(Console.ReadLine()!);

            service.AddDoctor(doctor);
        }

        private void UpdateSpecialty()
        {
            Console.Write("Enter Doctor ID to update: ");
            int id = int.Parse(Console.ReadLine()!);

            Console.Write("Enter new Specialty: ");
            string specialty = Console.ReadLine()!;

            service.UpdateDoctorSpecialty(id, specialty);
        }

        private void ViewBySpecialty()
        {
            Console.Write("Enter Specialty: ");
            string specialty = Console.ReadLine()!;
            var doctors = service.GetDoctorsBySpecialty(specialty);

            if (doctors.Count == 0)
            {
                Console.WriteLine("No doctors found for this specialty.");
                return;
            }

            foreach (var d in doctors)
            {
                Console.WriteLine($"{d.Id} | {d.FullName} | {d.Phone} | {d.Email} | Fee: {d.ConsultationFee}");
            }
        }

        private void DeactivateDoctor()
        {
            Console.Write("Enter Doctor ID to deactivate: ");
            int id = int.Parse(Console.ReadLine()!);

            if (service.DeactivateDoctor(id))
            {
                Console.WriteLine("Doctor deactivated successfully.");
            }
        }
    }
}
