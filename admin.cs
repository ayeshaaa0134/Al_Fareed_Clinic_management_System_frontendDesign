using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace AFC
{
    public partial class Form7: Form
    {
        private string info = "server = LocalHost; user = root; password = aliqaiser1123; database = clinic_management_system";
        public Form7()
        {
            InitializeComponent();
            panela.Visible = false;
            panelb.Visible = false;
            logout.Visible = false;
        }

        private void Form7_Load(object sender, EventArgs e)
        {

        }

        private void chart1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            panela.Hide();
            panelb.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            panelb.Show();
        }

        private void button5_Click(object sender, EventArgs e)
        {
           
        }

        private void pictureBox10_Click(object sender, EventArgs e)
        {
         
        }

        private void button7_Click(object sender, EventArgs e)
        {
           
        }

        private void button5_Click_1(object sender, EventArgs e)
        {
          
        }
        private void button5_Click_2(object sender, EventArgs e)
        {
            
        }

        private void panel7_Paint(object sender, PaintEventArgs e)
        {

        }

        private void pictureBox10_Click_1(object sender, EventArgs e)
        {
            panela.Show();
            panela.BringToFront();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            panela.Hide();
        }

        private void pictureBox11_Click_3(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp;*.gif";
                openFileDialog.Title = "Select a Profile Picture";

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    profilepic1.Image = Image.FromFile(openFileDialog.FileName);
                }
            }

        }

        private void profilepic1_Click(object sender, EventArgs e)
        {

        }

        private void panela_Paint(object sender, PaintEventArgs e)
        {

        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            logout.Show();
            logout.BringToFront();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            
        }

        private void button9_Click(object sender, EventArgs e)
        {
            logout.Hide();
        }

        private void button8_Click_1(object sender, EventArgs e)
        {
            Form3 form3 = new Form3();
            form3.ShowDialog();
            this.Hide();
        }

        private void button9_Click_1(object sender, EventArgs e)
        {
            logout.Hide();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            panela.Show();
            panela.BringToFront();
        }

        private void panel16_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button7_Click_1(object sender, EventArgs e)
        {
            string name = dname.Text.Trim();
            string specialization = dspecialization.Text.Trim();
            string address = daddress.Text.Trim();
            string contact = dcontact.Text.Trim();
            string gender = "";

            if (dmale.Checked) gender += "Male";
            if (dfemale.Checked) gender += "Female";

            using (MySqlConnection conn = new MySqlConnection(info))
            {
                try
                {
                    conn.Open();
                    string query = "INSERT into doctors (name, specialization, address, contact, gender)" + "Values (@name, @specialization, @address, @contact, @gender)";
                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@name", name);
                    cmd.Parameters.AddWithValue("@specialization", specialization);
                    cmd.Parameters.AddWithValue("@address", address);
                    cmd.Parameters.AddWithValue("@contact", contact);
                    cmd.Parameters.AddWithValue("@gender", gender);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("\t\tSuccessfully Added\t\t");
                    panela.Visible = false;
                }
                catch(MySqlException ex)
                {
                    MessageBox.Show(ex.ToString());
                }
                AddDoctorPanel(name);
            }
        }
        private void AddDoctorPanel(string doctorname)//, string imagePath)
        {
            // Create a new panel for the doctor
            Panel doctorPanel = new Panel();
            doctorPanel.Size = new Size(155, 167); // You can adjust the size as needed
            doctorPanel.BackColor = Color.FromArgb(149, 210, 179);
            doctorPanel.Margin = new Padding(3);

            // Create a picture box for the doctor's image
            //PictureBox doctorImage = new PictureBox();
            //doctorImage.Size = new Size(100, 100); // Adjust size as needed
            //doctorImage.SizeMode = PictureBoxSizeMode.Zoom;
            //doctorImage.ImageLocation = imagePath;  // Set image path
            //doctorImage.Location = new Point(25, 10); // Adjust location

            // Create a label for the doctor's name
            Label doctorNameLabel = new Label();
            doctorNameLabel.Text = doctorname;
            doctorNameLabel.TextAlign = ContentAlignment.MiddleCenter;
            doctorNameLabel.Location = new Point(0, 120);  // Adjust location
            doctorNameLabel.Size = new Size(150, 30); // Adjust size as needed
            doctorNameLabel.Font = new Font("Bauhaus 93", 15, FontStyle.Bold);
            doctorNameLabel.ForeColor = Color.White;

            // Add the image and label to the doctor panel
            //doctorPanel.Controls.Add(doctorImage);
            doctorPanel.Controls.Add(doctorNameLabel);

            // Add the new doctor panel to the FlowLayoutPanel
            flowLayoutPanel1.Controls.Add(doctorPanel);  // 'flowDoctorsPanel' is the name of your FlowLayoutPanel
        }

        private void flowLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}