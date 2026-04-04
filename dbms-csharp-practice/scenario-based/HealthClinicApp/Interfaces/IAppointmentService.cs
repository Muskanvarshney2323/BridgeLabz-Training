using HealthClinicApp.Models;

namespace HealthClinicApp.Interfaces
{
    public interface IAppointmentService
    {
        void BookAppointment(Appointment appt);
        void CheckDoctorAvailability(int doctorId, DateTime date);
        void CancelAppointment(int appointmentId);
        void RescheduleAppointment(int appointmentId, int doctorId, DateTime newDate);
        void ViewDailySchedule(DateTime date);
    }
}
