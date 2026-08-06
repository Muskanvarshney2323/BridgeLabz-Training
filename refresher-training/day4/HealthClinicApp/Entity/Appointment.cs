using System;

namespace HealthClinicApp.Entity
{
    public class Appointment
    {
        public int AppointmentID { get; set; }

        public int DoctorID { get; set; }

        public int PatientID { get; set; }

        public DateTime VisitDate { get; set; }

        public TimeSpan VisitTime { get; set; }

        public string AppointmentStatus { get; set; }
    }
}