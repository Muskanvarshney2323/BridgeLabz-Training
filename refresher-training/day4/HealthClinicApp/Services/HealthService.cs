using Microsoft.Data.SqlClient;
using HealthClinicApp.Data;
using HealthClinicApp.Entity;

namespace HealthClinicApp.Services
{
    public class HealthService
    {
        private readonly string connectionString = DbConnection.ConnectionString;

        // =========================
        // Add Doctor
        // =========================
        public void AddDoctor(Doctor doctor)
        {
            using SqlConnection con = new SqlConnection(connectionString);

            string query = @"INSERT INTO Doctor
                            (DoctorName, Expertise, ContactNo)
                            VALUES
                            (@DoctorName, @Expertise, @ContactNo)";

            SqlCommand cmd = new SqlCommand(query, con);

            cmd.Parameters.AddWithValue("@DoctorName", doctor.DoctorName);
            cmd.Parameters.AddWithValue("@Expertise", doctor.Expertise);
            cmd.Parameters.AddWithValue("@ContactNo", doctor.ContactNo);

            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
        }

        // =========================
        // View Doctors
        // =========================
        public List<Doctor> GetAllDoctors()
        {
            List<Doctor> doctors = new List<Doctor>();

            using SqlConnection con = new SqlConnection(connectionString);

            string query = "SELECT * FROM Doctor";

            SqlCommand cmd = new SqlCommand(query, con);

            con.Open();

            SqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                Doctor doctor = new Doctor();

                doctor.DoctorID = Convert.ToInt32(reader["DoctorID"]);
                doctor.DoctorName = reader["DoctorName"].ToString();
                doctor.Expertise = reader["Expertise"].ToString();
                doctor.ContactNo = reader["ContactNo"].ToString();

                doctors.Add(doctor);
            }

            reader.Close();

            return doctors;
        }

        // =========================
        // Add Patient
        // =========================
        public void AddPatient(Patient patient)
        {
            using SqlConnection con = new SqlConnection(connectionString);

            string query = @"INSERT INTO Patient
                            (PatientName,Age,Gender,MobileNo)
                            VALUES
                            (@PatientName,@Age,@Gender,@MobileNo)";

            SqlCommand cmd = new SqlCommand(query, con);

            cmd.Parameters.AddWithValue("@PatientName", patient.PatientName);
            cmd.Parameters.AddWithValue("@Age", patient.Age);
            cmd.Parameters.AddWithValue("@Gender", patient.Gender);
            cmd.Parameters.AddWithValue("@MobileNo", patient.MobileNo);

            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
        }

        // =========================
        // View Patients
        // =========================
        public List<Patient> GetAllPatients()
        {
            List<Patient> patients = new List<Patient>();

            using SqlConnection con = new SqlConnection(connectionString);

            string query = "SELECT * FROM Patient";

            SqlCommand cmd = new SqlCommand(query, con);

            con.Open();

            SqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                Patient patient = new Patient();

                patient.PatientID = Convert.ToInt32(reader["PatientID"]);
                patient.PatientName = reader["PatientName"].ToString();
                patient.Age = Convert.ToInt32(reader["Age"]);
                patient.Gender = Convert.ToChar(reader["Gender"]);
                patient.MobileNo = reader["MobileNo"].ToString();

                patients.Add(patient);
            }

            reader.Close();

            return patients;
        }

        // =========================
        // Add Appointment
        // =========================
        public void AddAppointment(Appointment appointment)
        {
            using SqlConnection con = new SqlConnection(connectionString);

            string query = @"INSERT INTO Appointment
                            (DoctorID,PatientID,VisitDate,VisitTime,AppointmentStatus)
                            VALUES
                            (@DoctorID,@PatientID,@VisitDate,@VisitTime,@AppointmentStatus)";

            SqlCommand cmd = new SqlCommand(query, con);

            cmd.Parameters.AddWithValue("@DoctorID", appointment.DoctorID);
            cmd.Parameters.AddWithValue("@PatientID", appointment.PatientID);
            cmd.Parameters.AddWithValue("@VisitDate", appointment.VisitDate);
            cmd.Parameters.AddWithValue("@VisitTime", appointment.VisitTime);
            cmd.Parameters.AddWithValue("@AppointmentStatus", appointment.AppointmentStatus);

            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
        }

        // =========================
        // View Appointments
        // =========================
        public List<Appointment> GetAllAppointments()
        {
            List<Appointment> appointments = new List<Appointment>();

            using SqlConnection con = new SqlConnection(connectionString);

            string query = "SELECT * FROM Appointment";

            SqlCommand cmd = new SqlCommand(query, con);

            con.Open();

            SqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                Appointment appointment = new Appointment();

                appointment.AppointmentID = Convert.ToInt32(reader["AppointmentID"]);
                appointment.DoctorID = Convert.ToInt32(reader["DoctorID"]);
                appointment.PatientID = Convert.ToInt32(reader["PatientID"]);
                appointment.VisitDate = Convert.ToDateTime(reader["VisitDate"]);
                appointment.VisitTime = (TimeSpan)reader["VisitTime"];
                appointment.AppointmentStatus = reader["AppointmentStatus"].ToString();

                appointments.Add(appointment);
            }

            reader.Close();

            return appointments;
        }
    }
}