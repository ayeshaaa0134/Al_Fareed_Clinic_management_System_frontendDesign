using AFC;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using static Mysqlx.Expect.Open.Types.Condition.Types;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace database_project
{

    public partial class Form1 : Form
    {
        string Time = "";
        private string connectionstring = "server = LocalHost; user = root; password = aliqaiser1123; database = clinic_management_system";
        private int loggedInUserId;

        public Form1(int user_id)
        {
            InitializeComponent();
            loggedInUserId = user_id;

            settings.Hide();
            notify.Hide();
            recordsTab.Hide();
            presTab.Hide();
            profile.Hide();
            log.Hide();
            pres_edit.Hide();
           // d_board.Show();

            LoadDoctorProfile();

            search.Text = "Search";
            search.ForeColor = Color.Gray;

            // Hook the events
            search.Enter += searchBox_Enter;
            search.Leave += searchBox_Leave;
        }





        private void searchBox_Enter(object sender, EventArgs e)
        {
            if (search.Text == "Search")
            {
                search.Text = "";
                search.ForeColor = Color.Black;
            }
        }

        private void searchBox_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(search.Text))
            {
                search.Text = "Search";
                search.ForeColor = Color.Gray;
            }
        }


        private void LoadDoctorProfile()
        {
            using (MySqlConnection conn = new MySqlConnection(connectionstring))
            {
                try
                {
                    conn.Open();
                    string query = "SELECT * FROM users WHERE user_id = @id";
                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@id", loggedInUserId);
                    MySqlDataReader reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {
                        // Replace doctorInfoBox with the actual name of your multiline TextBox
                        string info = $"User ID: {reader["user_id"]}\r\n" +
                  $"Name: {reader["name"]}\r\n" +
                  $"Email: {reader["email"]}\r\n" +
                  $"Contact: {reader["contact"]}\r\n" +
                  $"Role: {reader["role"]}\r\n" +
                  $"Specialization: {reader["specialization"]}\r\n" +
                  $"Shift Time: {reader["shift_time"]}";

                        doc_info.Text = info;
                    }
                    else
                    {
                        doc_info.Text = "Doctor not found.";
                    }
                }
                catch (MySqlException ex)
                {
                    MessageBox.Show("Error loading profile: " + ex.Message);
                }
            }
        }


        string selectedTimeFrame = "Daily";  // Default


        void LoadSpecializationFolders()
        {
            string query = "SELECT DISTINCT specialization FROM doctor";
            using (MySqlConnection db = new MySqlConnection(connectionstring))
            {
                MySqlCommand cmd = new MySqlCommand(query, db);

                try
                {
                    db.Open();
                    MySqlDataReader reader = cmd.ExecuteReader();

               //     TreeNode patientRoot = treeView1.Nodes.Add("Patient Records");

                    while (reader.Read())
                    {
                        string specialization = reader.GetString("specialization");
                    //    patientRoot.Nodes.Add(specialization + " Patients");
                    }

                    reader.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error loading specializations: " + ex.Message);
                }
            }
        }


        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            string selectedText = e.Node.Text;

            if (selectedText.EndsWith("Patients"))
            {
                string specialization = selectedText.Replace(" Patients", "");
                LoadPatientsBySpecialization(specialization);
            }
        }

        void LoadPatientsBySpecialization(string specialization)
        {
            string query = @"
        SELECT 
            p.id AS PatientID,
            p.name AS PatientName,
            p.age,
            p.gender,
            m.diagnosis,
            a.date AS AppointmentDate,
            d.name AS DoctorName,
            d.specialization
        FROM patients p
        LEFT JOIN appointments a ON p.id = a.patient_id
        LEFT JOIN doctor d ON a.doctor_id = d.id
        LEFT JOIN medical_history m ON p.id = m.patient_id
        WHERE d.specialization = @specialization";

            using (MySqlConnection db = new MySqlConnection(connectionstring))
            {
                MySqlCommand cmd = new MySqlCommand(query, db);
                cmd.Parameters.AddWithValue("@specialization", specialization);

                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                //dataGridView1.DataSource = dt;
            }
        }



        private void panel4_Paint(object sender, PaintEventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            //noti.Show();
            //noti.BringToFront();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //settingsTab.Show();
            //settingsTab.BringToFront();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //presTab.Show();
            //presTab.BringToFront();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            //recordsTab.Show();
            //recordsTab.BringToFront();
        }

        private void textBox13_TextChanged(object sender, EventArgs e)
        {

        }

        private void button8_Click(object sender, EventArgs e)
        {
            //profileTab.Show();
            //profileTab.BringToFront();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            //noti.Hide();
            //recordsTab.Hide();
            //settingsTab.Hide();
            //profileTab.Hide();
            //presTab.Hide();
            //recordsEdit.Hide();
        }

        private void button24_Click(object sender, EventArgs e)
        {
           
        }

        private void pictureBox6_Click(object sender, EventArgs e)
        {
            //recordsEdit.Show();
            //recordsEdit.BringToFront();
        }

        private void chart1_Click(object sender, EventArgs e)
        {

        }

        private void settings_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button1_Click_1(object sender, EventArgs e)
        {
           //
           //d_board.Show();
            settings.Hide();
            notify.Hide();
            recordsTab.Hide();
            presTab.Hide();
            profile.Hide();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            settings.Show();
            settings.BringToFront();
        }

        private void button4_Click_1(object sender, EventArgs e)
        {
            notify.Show();
            notify.BringToFront();
        }

        private void button9_Click_1(object sender, EventArgs e)
        {
            profile.Show();
            profile.BringToFront();
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            recordsTab.Show();
            recordsTab.BringToFront();
        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            //presTab.Show();
            //presTab.BringToFront();
            pres_edit.Show();
            pres_edit.BringToFront();
        }

        private void panel20_Paint(object sender, PaintEventArgs e)
        {

        }

        private void propertyGrid1_Click(object sender, EventArgs e)
        {

        }

        private void label10_Click(object sender, EventArgs e)
        {

        }

        private void panel9_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel39_Paint(object sender, PaintEventArgs e)
        {

        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            
            
        }

        private void linkLabel1_LinkClicked_1(object sender, LinkLabelLinkClickedEventArgs e)
        {
            log.Show();
            log.BringToFront();
        }

        private void button10_Click(object sender, EventArgs e)
        {
            log.Hide();
        }

        private void button11_Click(object sender, EventArgs e)
        {
            //extraform form = new extraform();
            //form.Show();
            //form.BringToFront();
            //this.Close();
        }

        private void recordsTab_Paint(object sender, PaintEventArgs e)
        {

        }

        private async void label16_Click(object sender, EventArgs e)
        {

        }

        private async void label17_Click(object sender, EventArgs e)
        {
           
        }
       


        private async Task<int> GetTodaysAppointmentCountAsync()
        {
            int count = 0;
            string query = "";
            if (Time == "Daily")
            {
                query = "SELECT COUNT(AppointmentDate) FROM Appointments WHERE DATE(AppointmentDate) = CURDATE()";
            }
            else if (Time == "Weekly")
            {
                query = "SELECT COUNT(AppointmentDate) FROM Appointments WHERE YEARWEEK(AppointmentDate, 1) = YEARWEEK(CURDATE(), 1)";
            }
            else if (Time == "Monthly")
            {
                query = "SELECT COUNT(AppointmentDate) FROM Appointments WHERE MONTH(AppointmentDate) = MONTH(CURDATE()) AND YEAR(AppointmentDate) = YEAR(CURDATE())";
            }

            using (MySqlConnection conn = new MySqlConnection(connectionstring))
            {
                await conn.OpenAsync();

                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    object result = await cmd.ExecuteScalarAsync();
                    if (result != null)
                        count = Convert.ToInt32(result);
                }
            }

            return count;
        }


      

        private async Task<string> GetMostCommonDiagnosisAsync()
        {
            string mostCommonDiagnosis = "";
            string query = "SELECT diagnosis, COUNT(*) AS freq " +
                    "FROM medical_history " +
                    "GROUP BY diagnosis " +
                    "ORDER BY freq DESC " +
                    "LIMIT 1";
            using (MySqlConnection conn = new MySqlConnection(connectionstring))
            {
                await conn.OpenAsync();

                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    object result = await cmd.ExecuteScalarAsync();
                    if (result != null)
                    {
                        // If there is a result, fetch the most common diagnosis name
                        mostCommonDiagnosis = result.ToString();
                    }
                }
            }

            return mostCommonDiagnosis;
        }


        private async void button8_Click_1(object sender, EventArgs e)
        {
           Time = "Daily";
            await UpdateData();
           
        }

        private async void button7_Click(object sender, EventArgs e)
        {
            Time = "Weekly";
            await UpdateData();
            

        }

        private async void button6_Click(object sender, EventArgs e)
        {
            Time = "Monthly";
            await UpdateData();
            
        }



        private async Task UpdateData()
        {
            try
            {
                // Get the appointment count
                int count = await GetTodaysAppointmentCountAsync();
                p_count.Text = count.ToString();  // Update the appointment count label

                // Get the most common diagnosis
                string commonDiagnosis = await GetMostCommonDiagnosisAsync();
               c_cases.Text = commonDiagnosis;  // Update the most common diagnosis label (you'll need to create this label in your UI)
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }
        

        private void dashboard_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel14_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label18_Click(object sender, EventArgs e)
        {

        }

        private void label19_Click(object sender, EventArgs e)
        {

        }

        private void c_cases_Paint(object sender, PaintEventArgs e)
        {

        }

        private void doc_info_TextChanged(object sender, EventArgs e)
        {
           
        }

        private void pres_edit_Paint(object sender, PaintEventArgs e)
        {

        }

        private void pres_edit_form_Paint(object sender, PaintEventArgs e)
        {

        }

        private void save_Click(object sender, EventArgs e)
        {
           
            string prescriptionText = pres_edit_form.Text;

           

            using (MySqlConnection conn = new MySqlConnection(connectionstring))
            {
                try
                {
                    conn.Open();

                    string query = "INSERT INTO medical_history (prescription_text) VALUES (@prescription)";

                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@prescription", prescriptionText);

                        int result = cmd.ExecuteNonQuery();

                        if (result > 0)
                        {
                            MessageBox.Show("Prescription saved successfully.");
                            pres_edit_form.Clear();

                        }
                        else
                        {
                            MessageBox.Show("Failed to save prescription.");
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
            }
        }

        private void panel30_Paint(object sender, PaintEventArgs e)
        {

        }

        private void SearchMedicine(string keyword)
        {
            if (string.IsNullOrWhiteSpace(keyword))
            {
                results.Clear();
                return;
            }

            

            using (MySqlConnection conn = new MySqlConnection(connectionstring))
            {
                try
                {
                    conn.Open();

                    string query = "SELECT medicine_name, quantity_in_stock FROM pharmacy_inventory WHERE medicine_name LIKE @search";

                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@search", "%" + keyword + "%");

                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            results.Clear();

                            if (reader.HasRows)
                            {
                                while (reader.Read())
                                {
                                    string medName = reader["medicine_name"].ToString();
                                    int quantity = Convert.ToInt32(reader["quantity_in_stock"]);

                                    string status = quantity > 0 ? $"Available ({quantity} in stock)" : "Not Available";

                                    results.AppendText($"{medName} - {status}\r\n");
                                }
                            }
                            else
                            {
                                results.Text = "No matching medicine found.";
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
            }
        }

        

        private void search_TextChanged(object sender, EventArgs e)
        {
            SearchMedicine(search.Text.Trim());
        }

        private void searchbtn_Click(object sender, EventArgs e)
        {
            SearchMedicine(search.Text.Trim());
        }

        private void pres_edit_form_TextChanged(object sender, EventArgs e)
        {

        }

        private void panel9_Paint_1(object sender, PaintEventArgs e)
        {

        }

        
    }
}
