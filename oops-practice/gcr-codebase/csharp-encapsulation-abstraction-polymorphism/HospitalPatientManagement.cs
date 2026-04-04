using System;
using System.Collections.Generic;

namespace OopsPractice.EncapsulationAbstractionPolymorphism
{
    interface IMedicalRecord
    {
        void AddRecord(string entry);
        IEnumerable<string> ViewRecords();
    }

    abstract class Patient
    {
        private string _patientId;
        private string _name;
        private int _age;
        protected List<string> MedicalHistory = new List<string>();

        protected Patient(string id, string name, int age)
        {
            _patientId = id; _name = name; _age = age;
        }

        public string PatientId => _patientId;
        public string Name => _name;
        public int Age => _age;

        public abstract double CalculateBill();

        public void GetPatientDetails() => Console.WriteLine($"{Name} (Id:{PatientId}), Age {Age}");
    }

    class InPatient : Patient, IMedicalRecord
    {
        private int _days;
        public InPatient(string id, string name, int age, int days) : base(id, name, age) { _days = days; }
        public override double CalculateBill() => _days * 2000 + 500; // simplified
        public void AddRecord(string entry) => MedicalHistory.Add(entry);
        public IEnumerable<string> ViewRecords() => MedicalHistory;
    }

    class OutPatient : Patient, IMedicalRecord
    {
        public OutPatient(string id, string name, int age) : base(id, name, age) { }
        public override double CalculateBill() => 500; // base consultation
        public void AddRecord(string entry) => MedicalHistory.Add(entry);
        public IEnumerable<string> ViewRecords() => MedicalHistory;
    }

    class Program
    {
        static void Main()
        {
            Patient p1 = new InPatient("P001","Rita",30,3);
            Patient p2 = new OutPatient("P002","Sam",45);

            Console.WriteLine($"{p1.Name} bill: {p1.CalculateBill():C}");
            Console.WriteLine($"{p2.Name} bill: {p2.CalculateBill():C}");
        }
    }
}