using System;
using System.Collections.Generic;
using HealthCareClinicSystem.Models;

namespace HealthCareClinicSystem.Interfaces
{
    public interface IAppointmentRepository
    {
        int BookAppointment(Appointment appointment);
        bool CheckDoctorAvailability(int doctorId, DateTime date, TimeSpan time);
        Dictionary<TimeSpan, int> GetDoctorSlotAvailability(
            int doctorId,
            DateTime date,
            int maxSlotsPerDay
        );
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