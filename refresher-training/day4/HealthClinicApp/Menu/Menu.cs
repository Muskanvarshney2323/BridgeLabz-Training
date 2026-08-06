using System;
using HealthClinicApp.Entity;
using HealthClinicApp.Services;

namespace HealthClinicApp
{
    public class Menu
    {
        HealthService service = new HealthService();

        public void ShowMenu()
        {
            while (true)
            {
                Console.WriteLine("\n===== Health Clinic Management System =====");
                Console.WriteLine("1. Add Doctor");
                Console.WriteLine("2. View Doctors");
                Console.WriteLine("3. Add Patient");
                Console.WriteLine("4. View Patients");
                Console.WriteLine("5. Add Appointment");
                Console.WriteLine("6. View Appointments");
                Console.WriteLine("7. Exit");

                Console.Write("Enter your choice: ");
                int choice = Convert.ToInt32(Console.ReadLine());

                switch (choice)
                {
                    case 1:
                        AddDoctor();
                        break;

                    case 2:
                        ViewDoctors();
                        break;

                    case 3:
                        AddPatient();
                        break;

                    case 4:
                        ViewPatients();
                        break;

                    case 5:
                        AddAppointment();
                        break;

                    case 6:
                        ViewAppointments();
                        break;

                    case 7:
                        Console.WriteLine("Thank You!");
                        return;

                    default:
                        Console.WriteLine("Invalid Choice");
                        break;
                }
            }
        }

        // ---------------- Doctor ----------------

        void AddDoctor()
        {
            Doctor doctor = new Doctor();

            Console.Write("Enter Doctor Name: ");
            doctor.DoctorName = Console.ReadLine();

            Console.Write("Enter Expertise: ");
            doctor.Expertise = Console.ReadLine();

            Console.Write("Enter Contact No: ");
            doctor.ContactNo = Console.ReadLine();

            service.AddDoctor(doctor);

            Console.WriteLine("Doctor Added Successfully.");
        }

        void ViewDoctors()
        {
            var doctors = service.GetAllDoctors();

            foreach (var doctor in doctors)
            {
                Console.WriteLine($"{doctor.DoctorID}  {doctor.DoctorName}  {doctor.Expertise}  {doctor.ContactNo}");
            }
        }

        // ---------------- Patient ----------------

        void AddPatient()
        {
            Patient patient = new Patient();

            Console.Write("Enter Patient Name: ");
            patient.PatientName = Console.ReadLine();

            Console.Write("Enter Age: ");
            patient.Age = Convert.ToInt32(Console.ReadLine());

            Console.Write("Enter Gender (M/F): ");
            patient.Gender = Convert.ToChar(Console.ReadLine());

            Console.Write("Enter Mobile No: ");
            patient.MobileNo = Console.ReadLine();

            service.AddPatient(patient);

            Console.WriteLine("Patient Added Successfully.");
        }

        void ViewPatients()
        {
            var patients = service.GetAllPatients();

            foreach (var patient in patients)
            {
                Console.WriteLine($"{patient.PatientID}  {patient.PatientName}  {patient.Age}  {patient.Gender}  {patient.MobileNo}");
            }
        }

        // ---------------- Appointment ----------------

        void AddAppointment()
        {
            Appointment appointment = new Appointment();

            Console.Write("Enter Doctor ID: ");
            appointment.DoctorID = Convert.ToInt32(Console.ReadLine());

            Console.Write("Enter Patient ID: ");
            appointment.PatientID = Convert.ToInt32(Console.ReadLine());

            Console.Write("Enter Visit Date (yyyy-mm-dd): ");
            appointment.VisitDate = Convert.ToDateTime(Console.ReadLine());

            Console.Write("Enter Visit Time (HH:mm:ss): ");
            appointment.VisitTime = TimeSpan.Parse(Console.ReadLine());

            Console.Write("Enter Status: ");
            appointment.AppointmentStatus = Console.ReadLine();

            service.AddAppointment(appointment);

            Console.WriteLine("Appointment Added Successfully.");
        }

        void ViewAppointments()
        {
            var appointments = service.GetAllAppointments();

            foreach (var appointment in appointments)
            {
                Console.WriteLine($"{appointment.AppointmentID}  {appointment.DoctorID}  {appointment.PatientID}  {appointment.VisitDate:d}  {appointment.VisitTime}  {appointment.AppointmentStatus}");
            }
        }
    }
}