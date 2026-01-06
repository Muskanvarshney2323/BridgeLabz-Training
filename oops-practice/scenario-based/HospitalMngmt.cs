using System;

// Abstraction using Interface
interface IPayable
{
    double CalculateBill();
}

// Base Class
class Patient
{
    public int PatientId { get; set; }
    public string Name { get; set; }
    public int Age { get; set; }

    public virtual void DisplayInfo()
    {
        Console.WriteLine("Patient ID: " + PatientId);
        Console.WriteLine("Name: " + Name);
        Console.WriteLine("Age: " + Age);
    }
}

// Inheritance + Polymorphism
class InPatient : Patient, IPayable
{
    public int NumberOfDays { get; set; }
    public double DailyCharge { get; set; }

    public double CalculateBill()
    {
        return NumberOfDays * DailyCharge;
    }

    public override void DisplayInfo()
    {
        base.DisplayInfo();
        Console.WriteLine("Patient Type: In-Patient");
        Console.WriteLine("Days Admitted: " + NumberOfDays);
        Console.WriteLine("Total Bill: " + CalculateBill());
    }
}

// Inheritance + Polymorphism
class OutPatient : Patient, IPayable
{
    public double ConsultationFee { get; set; }

    public double CalculateBill()
    {
        return ConsultationFee;
    }

    public override void DisplayInfo()
    {
        base.DisplayInfo();
        Console.WriteLine("Patient Type: Out-Patient");
        Console.WriteLine("Consultation Fee: " + CalculateBill());
    }
}

// Doctor Class
class Doctor
{
    public int DoctorId
     { 
        get;
        set; 
    }
    public string DoctorName
    {
        get;
        set; 
    }
    public string Specialization 
    {
        get; 
        set; 
    }

    public void DisplayDoctor()
    {
        Console.WriteLine("Doctor ID: " + DoctorId);
        Console.WriteLine("Doctor Name: " + DoctorName);
        Console.WriteLine("Specialization: " + Specialization);
    }
}

// Bill Class
class Bill
{
    public void PrintBill(IPayable payable)
    {
        Console.WriteLine("Bill Amount: " + payable.CalculateBill());
    }
}

// Main Program
class Program
{
    static void Main()
    {
        Doctor doctor = new Doctor
        {
            DoctorId = 1,
            DoctorName = "Dr. Sharma",
            Specialization = "Cardiology"
        };

        InPatient inPatient = new InPatient
        {
            PatientId = 101,
            Name = "Rahul",
            Age = 30,
            NumberOfDays = 5,
            DailyCharge = 2000
        };

        OutPatient outPatient = new OutPatient
        {
            PatientId = 102,
            Name = "Anita",
            Age = 25,
            ConsultationFee = 500
        };

        Bill bill = new Bill();

        Console.WriteLine(".... Doctor Details ....");
        doctor.DisplayDoctor();

        Console.WriteLine("\n\n.... In Patient Details ....");
        inPatient.DisplayInfo();
        bill.PrintBill(inPatient);

        Console.WriteLine("\n\n.... Out Patient Details ....");
        outPatient.DisplayInfo();
        bill.PrintBill(outPatient);
    }
}
