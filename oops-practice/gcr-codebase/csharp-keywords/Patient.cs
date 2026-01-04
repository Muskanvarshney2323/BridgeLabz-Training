using System;

namespace csharp_keywords
{
    class Patient
    {
        public static string HospitalName = "General Hospital";
        private static int totalPatients = 0;

        public readonly string PatientID;
        public string Name;
        public int Age;
        public string Ailment;

        public Patient(string Name, int Age, string Ailment, string PatientID)
        {
            this.Name = Name;
            this.Age = Age;
            this.Ailment = Ailment;
            this.PatientID = PatientID; // readonly
            totalPatients++;
        }

        public static void GetTotalPatients()
        {
            Console.WriteLine($"Total Patients: {totalPatients}");
        }

        public void DisplayDetails(object obj)
        {
            if (obj is Patient p)
            {
                Console.WriteLine("--- Patient ---");
                Console.WriteLine($"Hospital: {HospitalName}");
                Console.WriteLine($"Name: {p.Name}");
                Console.WriteLine($"Age: {p.Age}");
                Console.WriteLine($"Ailment: {p.Ailment}");
                Console.WriteLine($"ID: {p.PatientID}");
            }
            else
            {
                Console.WriteLine("Object is not a Patient instance.");
            }
        }

        static void Main()
        {
            var p = new Patient("Maya", 34, "Fever", "PT-3001");
            p.DisplayDetails(p);
            Patient.GetTotalPatients();
        }
    }
}