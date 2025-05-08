using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using Recepioant;
using database_project;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;
namespace AFC
{
    public partial class Form3: Form
    {
        private string info = "server = LocalHost; user = root; password = aliqaiser1123; database = clinic_management_system";
        public Form3()
        {
            InitializeComponent();
            login.Visible = false;
            reg.Visible = false;
            success.Visible = false;
            rs.Visible = true; 
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
           // Form4 form4 = new Form4();
            //his.Hide();
            //form4.ShowDialog();
            login.Show();
            login.BringToFront();
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            //Form4 form4 = new Form4();
            //this.Hide();
            //form4.ShowDialog();
            login.Show();
            login.BringToFront();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string email = l_name.Text.Trim();
            string password = l_password.Text.Trim();
            using (MySqlConnection connect = new MySqlConnection(info))
            {
                try
                {
                    connect.Open();
                    string data = "SELECT * FROM users WHERE Email = @email AND Password = @password";
                    MySqlCommand cmd = new MySqlCommand(data, connect);
                    cmd.Parameters.AddWithValue("@email", email);
                    cmd.Parameters.AddWithValue("@password", password);
                    MySqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        MessageBox.Show("\t\tLogin Succesful\t\t");

                        string role = reader["Role"].ToString();
                        int doctorId = Convert.ToInt32(reader["user_id"]);
                        if (role.Contains("Admin"))
                        {
                            Form7 form7 = new Form7();
                            form7.ShowDialog();
                            this.Close();

                        }
                        if (role.Contains("Receptionist"))
                        {
                            FormAppointments form = new FormAppointments();
                            form.ShowDialog();
                            this.Close();


                        }
                        if (role.Contains("Doctor"))
                        {
                            Form1 form1 = new Form1(doctorId);
                            form1.ShowDialog();
                            this.Hide();
                        }
                    }
                    else
                        MessageBox.Show("\t\tWrong Credentials\t\t");

                }
                catch (MySqlException ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
            }
            
        }

        private void linkLabel3_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            login.Hide();
            reg.Hide();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            reg.Show();
            reg.BringToFront();
            int a;
        }
        
        private void panel16_Paint(object sender, PaintEventArgs e)
        {

        }

        private void linkLabel4_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            reg.Hide();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            success.Show();
            success.BringToFront();
        }

        private void linkLabel5_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            success.Hide();
            reg.Hide();
            login.BringToFront();
        }

        private void panel17_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label23_Click(object sender, EventArgs e)
        {
            Form7 form7 = new Form7();
            this.Hide();
            form7.ShowDialog();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            
        }

        private void button9_Click(object sender, EventArgs e)
        {
            
        }

        private void success_Paint(object sender, PaintEventArgs e)
        {

        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void reg_Paint(object sender, PaintEventArgs e)
        {

        }

        private void rs_Paint(object sender, PaintEventArgs e)
        {

        }

        private void linkLabel1_LinkClicked_1(object sender, LinkLabelLinkClickedEventArgs e)
        {
            login.Show();
            login.BringToFront();
        }

        private void linkLabel2_LinkClicked_1(object sender, LinkLabelLinkClickedEventArgs e)
        {
            login.Show();
            login.BringToFront();
        }

        private void button7_Click_1(object sender, EventArgs e)
        {
            string name = r_name.Text.Trim();
            string email = r_email.Text.Trim();
            string contact = r_contact.Text.Trim();
            string password = r_password.Text.Trim();
            string role = " ";
            if (r_doctor.Checked) role += "Doctor";
            if (r_pharmacist.Checked) role += "Pharmacist";
            if (r_receptionist.Checked) role += "Receptionist";

            using (MySqlConnection conn = new MySqlConnection(info))
                try
                {
                    conn.Open();

                    string check = "SELECT COUNT(*) FROM users WHERE Email = @email";
                    MySqlCommand checkcmd = new MySqlCommand(check, conn);
                    checkcmd.Parameters.AddWithValue("@email", email);
                    int exist = Convert.ToInt32(checkcmd.ExecuteScalar());
                    if (exist > 0)
                    {
                        MessageBox.Show("Email already exist");
                    }

                    string query = "INSERT INTO users (name, email, contact, password, role)" + "VALUES (@name, @email, @contact, @password, @role)";
                    
                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@name", name);
                    cmd.Parameters.AddWithValue("@email", email);
                    cmd.Parameters.AddWithValue("@contact", contact);
                    cmd.Parameters.AddWithValue("@password", password);
                    cmd.Parameters.AddWithValue("@role", role);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("REGISTRATION SUCCESSFUL");

                    login.BringToFront();
                }
                catch(MySqlException ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }

        }

        private void linkLabel4_LinkClicked_1(object sender, LinkLabelLinkClickedEventArgs e)
        {
            login.BringToFront();
        }

        private void label4_Click(object sender, EventArgs e)
        {
            Form7 form7 = new Form7();
            form7.ShowDialog();
            this.Close();
        }

        private void login_Paint(object sender, PaintEventArgs e)
        {

        }

        private void l_name_TextChanged(object sender, EventArgs e)
        {

        }
    }
}