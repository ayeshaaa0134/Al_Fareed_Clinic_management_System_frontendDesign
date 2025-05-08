using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using MySql.Data.MySqlClient; // Make sure this namespace is used
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AFC;

namespace Recepioant
{
    public partial class FormAppointments : Form
    {
        private string connectionString = "server=localhost;user=root;password=aliqaiser1123;database=clinic_management_system";

        public FormAppointments()
        {
            InitializeComponent();
            history.Hide();
            booking.Hide();
            LoadAllAppointments(); // Load all appointments on form load
            logout.Hide();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            history.Hide();
            booking.Hide();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            history.Show();
            history.BringToFront();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            booking.Show();
            booking.BringToFront();
        }

        private void Patientname_TextChanged(object sender, EventArgs e)
        {
            // You can add validation or other logic here if needed
        }

        private void Contactnum_TextChanged(object sender, EventArgs e)
        {
            // You can add validation or other logic here if needed
        }

        private void CNIC_TextChanged(object sender, EventArgs e)
        {
            // You can add validation or other logic here if needed
        }

        private void Female_CheckedChanged(object sender, EventArgs e)
        {
            // You can add logic here if needed
        }

        private void Male_CheckedChanged(object sender, EventArgs e)
        {
            // You can add logic here if needed
        }

        private void Add_Click(object sender, EventArgs e)
        {
            // Patient Information
            string patientName = Patientname.Text;
            string patientContact = Contactnum.Text;
            string patientCnic = CNIC.Text;
            string gender = Male.Checked ? "Male" : (Female.Checked ? "Female" : "");

            if (string.IsNullOrEmpty(patientName) || string.IsNullOrEmpty(patientContact) || string.IsNullOrEmpty(patientCnic) || string.IsNullOrEmpty(gender))
            {
                MessageBox.Show("Please fill in all patient details.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                MySqlTransaction transaction = connection.BeginTransaction();

                try
                {
                    // Insert Patient Information and get PatientID
                    int patientId = InsertPatient(connection, transaction, patientName, patientContact, patientCnic, gender);

                    // Appointment Information
                    string doctorName = DoctorName.Text; // Assuming the TextBox name
                    string appointmentDate = DateAndTime.Text; // Get date as string from TextBox
                    string appointmentType = AppointmentType.Text; // Assuming the TextBox name
                    int appointmentFee = -1; // To store the appointment fee
                    if (!int.TryParse(AppointmentFeeTextBox.Text, out appointmentFee))
                    {
                        MessageBox.Show("Invalid Appointment Fee. Please enter an integer.", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        transaction.Rollback();
                        return;
                    }

                    if (patientId > 0 && !string.IsNullOrEmpty(doctorName) && !string.IsNullOrEmpty(appointmentDate) && !string.IsNullOrEmpty(appointmentType) && appointmentFee != -1)
                    {
                        // Get Doctor's user_id
                        int doctorId = GetDoctorId(connection, transaction, doctorName);

                        if (doctorId > 0)
                        {
                            // Insert Appointment Information
                            InsertAppointment(connection, transaction, patientId, doctorId, appointmentDate, appointmentFee, appointmentType);
                            transaction.Commit();
                            MessageBox.Show("Patient and Appointment booked successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            ClearFields();
                            LoadAllAppointments(); // Reload all appointments after adding a new one
                        }
                        else
                        {
                            transaction.Rollback();
                            MessageBox.Show($"Doctor '{doctorName}' not found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    else
                    {
                        transaction.Rollback();
                        MessageBox.Show("Please fill in all appointment details, including the date and fee.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
                catch (MySqlException ex)
                {
                    transaction.Rollback();
                    MessageBox.Show("Database Error: " + ex.Message, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    MessageBox.Show("An error occurred: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private int InsertPatient(MySqlConnection connection, MySqlTransaction transaction, string name, string phone, string cnic, string gender)
        {
            string insertPatientQuery = "INSERT INTO Patients (Patient_Name, Phone_No, Patient_Cnic, Gender) " +
                                      "VALUES (@PatientName, @PhoneNo, @PatientCnic, @Gender); " +
                                      "SELECT LAST_INSERT_ID();";
            using (MySqlCommand command = new MySqlCommand(insertPatientQuery, connection, transaction))
            {
                command.Parameters.AddWithValue("@PatientName", name);
                command.Parameters.AddWithValue("@PhoneNo", phone);
                command.Parameters.AddWithValue("@PatientCnic", cnic);
                command.Parameters.AddWithValue("@Gender", gender);
                object result = command.ExecuteScalar();
                if (result != null && result != DBNull.Value)
                {
                    return Convert.ToInt32(result);
                }
                else
                {
                    throw new Exception("Failed to retrieve PatientID.");
                }
            }
        }

        private int GetDoctorId(MySqlConnection connection, MySqlTransaction transaction, string doctorName)
        {
            string getDoctorIdQuery = "SELECT user_id FROM users WHERE name = @DoctorName";
            using (MySqlCommand command = new MySqlCommand(getDoctorIdQuery, connection, transaction))
            {
                command.Parameters.AddWithValue("@DoctorName", doctorName);
                object result = command.ExecuteScalar();
                if (result != null && result != DBNull.Value)
                {
                    return Convert.ToInt32(result);
                }
                else
                {
                    return -1; // Doctor not found
                }
            }
        }

        private void InsertAppointment(MySqlConnection connection, MySqlTransaction transaction, int patientId, int doctorId, string appointmentDate, int appointmentFee, string appointmentType)
        {
            string insertAppointmentQuery = "INSERT INTO Appointments (PatientID, DoctorID, AppointmentDate, AppointmentFee, AppointmentType) " +
                                            "VALUES (@PatientID, @DoctorID, @AppointmentDate, @AppointmentFee, @AppointmentType)";
            using (MySqlCommand command = new MySqlCommand(insertAppointmentQuery, connection, transaction))
            {
                command.Parameters.AddWithValue("@PatientID", patientId);
                command.Parameters.AddWithValue("@DoctorID", doctorId);
                command.Parameters.AddWithValue("@AppointmentDate", appointmentDate);
                command.Parameters.AddWithValue("@AppointmentFee", appointmentFee);
                command.Parameters.AddWithValue("@AppointmentType", appointmentType);
                command.ExecuteNonQuery();
            }
        }

        private void ClearFields()
        {
            Patientname.Clear();
            Contactnum.Clear();
            CNIC.Clear();
            Male.Checked = false;
            Female.Checked = false;
            DoctorName.Clear();
            DateAndTime.Clear(); // Clear the DateAndTime TextBox
            AppointmentFeeTextBox.Clear(); // Clear the AppointmentFee TextBox
            AppointmentType.Clear();
        }

        private void DoctorName_TextChanged(object sender, EventArgs e)
        {
            // You can add validation or other logic here if needed
        }

        private void DateAndTime_TextChanged(object sender, EventArgs e)
        {
            // You can add validation or other logic here if needed
        }

        private void AppointmentType_TextChanged(object sender, EventArgs e)
        {
            // You can add validation or other logic here if needed
        }

        private void DashboardButton_Click(object sender, EventArgs e)
        {
            history.Hide();
            booking.Hide();
        }

        private void AppointmentButton_Click(object sender, EventArgs e)
        {
            booking.Show();
            booking.BringToFront();
        }

        private void PatientHistoryButton_Click(object sender, EventArgs e)
        {
            history.Show();
            history.BringToFront();
            LoadAllAppointments(); // Load all appointments when the history section is shown
        }

        // --- New code for Patient History in DataGridView ---

        // 1. Class to represent appointment data for DataGridView
        public class AppointmentView
        {
            public int AppointmentID { get; set; }
            public string PatientName { get; set; }
            public string DoctorName { get; set; }
            public DateTime AppointmentDate { get; set; }
            public int AppointmentFee { get; set; }
            public string AppointmentType { get; set; }
            // Add other properties you want to display
        }

        // 2. Function to fetch all appointments for DataGridView
        private List<AppointmentView> GetAllAppointments()
        {
            List<AppointmentView> appointments = new List<AppointmentView>();
            string query = "SELECT a.AppointmentID, p.Patient_Name, u.name AS DoctorName, a.AppointmentDate, a.AppointmentFee, a.AppointmentType " +
                           "FROM Appointments a " +
                           "INNER JOIN Patients p ON a.PatientID = p.PatientID " +
                           "INNER JOIN users u ON a.DoctorID = u.user_id";

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        using (MySqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                appointments.Add(new AppointmentView
                                {
                                    AppointmentID = reader.GetInt32("AppointmentID"),
                                    PatientName = reader.GetString("Patient_Name"),
                                    DoctorName = reader.GetString("DoctorName"),
                                    AppointmentDate = reader.GetDateTime("AppointmentDate"),
                                    AppointmentFee = reader.GetInt32("AppointmentFee"),
                                    AppointmentType = reader.GetString("AppointmentType")
                                });
                            }
                        }
                    }
                }
                catch (MySqlException ex)
                {
                    MessageBox.Show("Error retrieving all appointments: " + ex.Message, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            return appointments;
        }

        // 3. Function to fetch appointments by patient name for DataGridView
        private List<AppointmentView> GetAppointmentsByPatientName(string patientName)
        {
            List<AppointmentView> appointments = new List<AppointmentView>();
            string query = "SELECT a.AppointmentID, p.Patient_Name, u.name AS DoctorName, a.AppointmentDate, a.AppointmentFee, a.AppointmentType " +
                           "FROM Appointments a " +
                           "INNER JOIN Patients p ON a.PatientID = p.PatientID " +
                           "INNER JOIN users u ON a.DoctorID = u.user_id " +
                           "WHERE p.Patient_Name LIKE @PatientName";

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@PatientName", "%" + patientName + "%");
                        using (MySqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                appointments.Add(new AppointmentView
                                {
                                    AppointmentID = reader.GetInt32("AppointmentID"),
                                    PatientName = reader.GetString("Patient_Name"),
                                    DoctorName = reader.GetString("DoctorName"),
                                    AppointmentDate = reader.GetDateTime("AppointmentDate"),
                                    AppointmentFee = reader.GetInt32("AppointmentFee"),
                                    AppointmentType = reader.GetString("AppointmentType")
                                });
                            }
                        }
                    }
                }
                catch (MySqlException ex)
                {
                    MessageBox.Show("Error retrieving appointments by patient name: " + ex.Message, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            return appointments;
        }

        // 4. Load all appointments initially or when "Show all data" is entered
        private void LoadAllAppointments()
        {
            List<AppointmentView> allAppointments = GetAllAppointments();
            PatientHistoryDataGridView.DataSource = null;
            PatientHistoryDataGridView.DataSource = allAppointments;
        }

        // 5. Event handler for the Search button
        private void SearchButton_Click(object sender, EventArgs e)
        {
            string searchText = SearchTextBox.Text.Trim(); // Assuming your search TextBox is named SearchTextBox

            if (searchText.ToLower() == "show all data" || string.IsNullOrEmpty(searchText))
            {
                LoadAllAppointments();
            }
            else if (!string.IsNullOrEmpty(searchText))
            {
                List<AppointmentView> patientAppointments = GetAppointmentsByPatientName(searchText);
                PatientHistoryDataGridView.DataSource = null;
                PatientHistoryDataGridView.DataSource = patientAppointments;

                if (patientAppointments.Count == 0)
                {
                    MessageBox.Show($"No appointments found for patient '{searchText}'.", "Search Result", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void DashboardButton_Click_1(object sender, EventArgs e)
        {
            history.Hide();
            booking.Hide();
        }

        private void AppointmentButton_Click_1(object sender, EventArgs e)
        {
            booking.Show();
            booking.BringToFront();
        }

        private void PatientHistoryButton_Click_1(object sender, EventArgs e)
        {
            history.Show();
            history.BringToFront();
            LoadAllAppointments(); // Load all appointments when the history section is shown
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form3 form3 = new Form3();
            form3.Show();
            this.Close();
        }

        private void panel11_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel10_Paint(object sender, PaintEventArgs e)
        {

        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            logout.Show();
            logout.BringToFront();
        }

        private void button8_Click_1(object sender, EventArgs e)
        {
            Form3 variable = new Form3();
            variable.Show();
            this.Close();


        }

        private void button9_Click(object sender, EventArgs e)
        {
            logout.Hide();
        }



        // ... (rest of your existing methods) ...
    }
}