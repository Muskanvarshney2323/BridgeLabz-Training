using System;
using System.Collections.Generic;
using HealthCareClinicSystem.Models;

namespace HealthCareClinicSystem.Interfaces
{
    public interface IAppointmentService
    {
        int BookAppointment(int patientId, int doctorId, DateTime date, TimeSpan time);
        bool CheckDoctorAvailability(int doctorId, DateTime date, TimeSpan time);
        void DisplayDoctorAvailability(int doctorId, DateTime date);
        bool CancelAppointment(int appointmentId, string remarks);
        bool RescheduleAppointment(
            int appointmentId,
            DateTime newDate,
            TimeSpan newTime,
            int? newDoctorId
        );
        List<Appointment> GetDailyAppointmentSchedule(DateTime date);
        Appointment GetAppointmentById(int appointmentId);
    }
}