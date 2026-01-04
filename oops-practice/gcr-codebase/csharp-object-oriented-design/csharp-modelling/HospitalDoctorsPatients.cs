using System;
using System.Collections.Generic;

namespace csharp_modelling
{
    // Association with communication: Doctor consults Patient
    class Patient
    {
        public string Name { get; set; }
        public List<Doctor> Doctors { get; private set; }

        public Patient(string name)
        {
            Name = name;
            Doctors = new List<Doctor>();
        }

        public void AddDoctor(Doctor d)
        {
            if (!Doctors.Contains(d)) Doctors.Add(d);
        }
    }

    class Doctor
    {
        public string Name { get; set; }
        public List<Patient> Patients { get; private set; }

        public Doctor(string name)
        {
            Name = name;
            Patients = new List<Patient>();
        }

        public void Consult(Patient p)
        {
            if (!Patients.Contains(p))
            {
                Patients.Add(p);
                p.AddDoctor(this);
            }

            Console.WriteLine($"Dr. {Name} is consulting {p.Name}.");
        }
    }

    class Hospital
    {
        public string Name { get; set; }
        public List<Doctor> Doctors { get; private set; }
        public List<Patient> Patients { get; private set; }

        public Hospital(string name)
        {
            Name = name;
            Doctors = new List<Doctor>();
            Patients = new List<Patient>();
        }

        public void AdmitDoctor(Doctor d) => Doctors.Add(d);
        public void AdmitPatient(Patient p) => Patients.Add(p);
    }

    class Program
    {
        static void Main()
        {
            var hosp = new Hospital("City Hospital");
            var doc1 = new Doctor("Adams");
            var doc2 = new Doctor("Baker");
            var pat1 = new Patient("John");
            var pat2 = new Patient("Sara");

            hosp.AdmitDoctor(doc1);
            hosp.AdmitDoctor(doc2);
            hosp.AdmitPatient(pat1);
            hosp.AdmitPatient(pat2);

            doc1.Consult(pat1);
            doc1.Consult(pat2);
            doc2.Consult(pat1);
        }
    }
}