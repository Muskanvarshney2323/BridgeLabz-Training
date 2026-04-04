using HealthClinicApp.Models;
using HealthClinicApp.Services;
using HealthClinicApp.Utilities;

namespace HealthClinicApp.Menus
{
    public class PatientMenu
    {
        private readonly PatientService service;

        public PatientMenu()
        {
            service = new PatientService();
        }

        public void Show()
        {
            Console.WriteLine("\n--- Patient Management ---");
            Console.WriteLine("1. Register Patient");
            Console.WriteLine("2. Update Patient");
            Console.WriteLine("3. Search Patient");
            Console.WriteLine("4. View Visit History"); 
            Console.WriteLine("0. Back");

           int choice = int.Parse(Console.ReadLine() ?? "0");


            switch (choice)
            {
                case 1:
                    Register();
                    break;
                case 2:
                    Update();
                    break;
                case 3:
                    Search();
                    break;
                case 4:
                    Console.Write("Enter Patient ID: ");
                    service.ViewVisitHistory(int.Parse(Console.ReadLine()));
                    break;
                case 0:
                    return;
                 default: Console.WriteLine("Invalid choice!");
                    break;
            }
        }

        private void Register()
        {
             Console.Write("Name: ");
            string name = Console.ReadLine() ?? "";

            Console.Write("DOB (yyyy-mm-dd): ");
            DateTime dob = DateTime.Parse(Console.ReadLine() ?? "");

            // 🔹 PHONE VALIDATION LOOP
            string phone;
            while (true)
            {
                Console.Write("Phone (10 digits): ");
                phone = Console.ReadLine() ?? "";
                if (InputValidator.IsValidPhone(phone)) break;
                Console.WriteLine(" Invalid Phone! Must be exactly 10 digits.");
            }

            // 🔹 EMAIL VALIDATION LOOP
            string email;
            while (true)
            {
                Console.Write("Email: ");
                email = Console.ReadLine() ?? "";
                if (InputValidator.IsValidEmail(email)) break;
                Console.WriteLine(" Invalid Email format!");
            }

            Console.Write("Address: ");
            string address = Console.ReadLine() ?? "";

            // 🔹 BLOOD GROUP VALIDATION LOOP
            string bloodGroup;
            while (true)
            {
                Console.Write("Blood Group (A+, O-, etc in CAPITAL): ");
                bloodGroup = Console.ReadLine() ?? "";
                if (InputValidator.IsValidBloodGroup(bloodGroup)) break;
                Console.WriteLine(" Invalid Blood Group! Use CAPITAL like A+, O-, AB+");
            }

            Patient patient = new Patient
            {
                FullName = name,
                DOB = dob,
                Phone = phone,
                Email = email,
                Address = address,
                BloodGroup = bloodGroup
            };

            service.RegisterPatient(patient);
        }

        private void Update()
        {
            Console.Write("Enter Patient ID: ");
            int id = int.Parse(Console.ReadLine());

            var patient = service.GetPatientById(id);
            Console.WriteLine($"Old Name: {patient.FullName}");

            Console.Write("New Name: "); patient.FullName = Console.ReadLine();
            Console.Write("New Address: "); patient.Address = Console.ReadLine();
            Console.Write("New Blood Group: "); patient.BloodGroup = Console.ReadLine();

            service.UpdatePatient(patient);
        }

        private void Search()
        {
            Console.Write("Enter name/phone/id: ");
            var results = service.SearchPatients(Console.ReadLine());

            foreach (var p in results)
                Console.WriteLine($"{p.PatientID} | {p.FullName} | {p.Phone}");
        }
    }
}
