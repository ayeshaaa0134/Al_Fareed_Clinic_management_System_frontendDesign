using MySql.Data.MySqlClient;//using for connecting MySql to app
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
using Newtonsoft.Json;//using json  for writing files
using System.IO;
using System.Windows.Automation.Peers;//File static class used for read, write and delete functions

namespace AFC
{
    public partial class Form7: Form
    {
        private string info = "server = LocalHost; user = root; password = aliqaiser1123; database = clinic_management_system";
        bool isFaded = false;
        public Form7()
        {
            InitializeComponent();
            j.Visible = false;
            panelb.Visible = false;
            logout.Visible = false;
            patient_tab.Visible = false;
            total_info();
        }
        public static void corner_round(Control c, int r)
        {
            var path = new System.Drawing.Drawing2D.GraphicsPath();

            path.AddArc(0, 0                     , r, r, 180, 90);
            path.AddArc(c.Width - r, 0           , r, r, 270, 90);
            path.AddArc(c.Width - r, c.Height - r, r, r,   0, 90);
            path.AddArc(0, c.Height - r          , r, r,  90, 90);

            path.CloseAllFigures();
            c.Region = new Region(path);
        }

        private void total_info()
        {
            using (MySqlConnection conn = new MySqlConnection(info))
            {
                conn.Open();
                string[] query = { "Select count(*) from users where role = 'Doctor'", "Select count(*) from patients", "Select count(*) from appointments"};
                Label[] label = { t_doctor, t_patient, t_app };
                for(int i = 0; i < query.Length; i++)
                {
                    MySqlCommand cmd = new MySqlCommand(query[i], conn);
                    object result = cmd.ExecuteScalar();
                    if (result != null)
                    {
                        label[i].Text = Convert.ToString(result);
                    }
                }
            }      
        }

        private void Form7_Load(object sender, EventArgs E)
        {
            total_info(); //showing all information on dashboard

            //corner rounds
            Panel[] panel = { a, b, c, d, e, f, g, h, l };
            for(int i = 0; i < panel.Length; i++)
            {
                corner_round(panel[i], 20);
            }
            Panel[] big_panels = {i_ratio, i, j, logout, k };
            for (int j = 0; j < big_panels.Length; j++)
            {
                corner_round(big_panels[j], 40);
            }
            //imagePaths
            //if (File.Exists("doctors.json"))
            //{
            //    doctorList = JsonSerializer.Deserialize<List<DoctorInfo>>(File.ReadAllText("doctors.json"));
            //    foreach (var doc in doctorList)
            //    {
            //        AddDoctorPanel(doc.Name, doc.Specialization, doc.ImagePath);
            //    }
            //}
        }

        private void chart1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            j.Hide();
            panelb.Hide();
            patient_tab.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            panelb.Show();
            panelb.BringToFront();
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
            j.Show();
            j.BringToFront();
            if (!isFaded)
            {
                j.BackColor = Color.FromArgb(77, j.BackColor); // fade to 30%
                isFaded = true;
            }
            else
            {
                j.BackColor = Color.FromArgb(255, j.BackColor); // full opacity
                isFaded = false;
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            j.Hide();
        }

        private void pictureBox11_Click_3(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp;*.gif";
                openFileDialog.Title = "Select a Profile Picture";

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    pic.Image = Image.FromFile(openFileDialog.FileName);
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
            j.Show();
            j.BringToFront();
        }

        private void panel16_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button7_Click_1(object sender, EventArgs e)
        {
            
        }
        private void AddDoctorPanel(string doctorname, string specialization, string imagePath)
        {
            // Create a new panel for the doctor
            Panel doctorPanel = new Panel();
            doctorPanel.Size = new Size(155, 167); // You can adjust the size as needed
            doctorPanel.BackColor = Color.FromArgb(149, 210, 179);
            doctorPanel.Margin = new Padding(3);
            corner_round(doctorPanel, 20);

            // Create a picture box for the doctor's image

            PictureBox doctorImage = new PictureBox();
            doctorImage.Size = new Size(100, 100); // Adjust size as needed
            doctorImage.SizeMode = PictureBoxSizeMode.Zoom;
            doctorImage.ImageLocation = imagePath;  // Set image path
            doctorImage.Location = new Point(25, 10); // Adjust location

            // Create a label for the doctor's name
            Label doctorNameLabel = new Label();
            doctorNameLabel.Text = Convert.ToString(doctorname);
            doctorNameLabel.TextAlign = ContentAlignment.MiddleCenter;
            doctorNameLabel.Location = new Point(0, 110);  // Adjust location
            doctorNameLabel.Size = new Size(150, 30); // Adjust size as needed
            doctorNameLabel.Font = new Font("Ebrima", 12, FontStyle.Bold);
            doctorNameLabel.ForeColor = Color.White;

            Label doctorNameLabel1 = new Label();
            doctorNameLabel1.Text = Convert.ToString(specialization);
            doctorNameLabel1.TextAlign = ContentAlignment.MiddleCenter;
            doctorNameLabel1.Location = new Point(0, 130);  // Adjust location
            doctorNameLabel1.Size = new Size(150, 30); // Adjust size as needed
            doctorNameLabel1.Font = new Font("Ebrima", 10);
            doctorNameLabel1.ForeColor = Color.White;

            // Add the image and label to the doctor panel
            doctorPanel.Controls.Add(doctorImage);
            doctorPanel.Controls.Add(doctorNameLabel);
            doctorPanel.Controls.Add(doctorNameLabel1);

            // Add the new doctor panel to the FlowLayoutPanel
            flowLayoutPanel1.Controls.Add(doctorPanel);  // 'flowDoctorsPanel' is the name of your FlowLayoutPanel
        }

        private void flowLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel5_Paint(object sender, PaintEventArgs e)
        {

        }

        private void j_Paint(object sender, PaintEventArgs e)
        {

        }
        public class DoctorInfo
        {
            public string Name { get; set; }
            public string Specialization { get; set; }
            public string ImagePath { get; set; }
        }
        List<DoctorInfo> doctorList = new List<DoctorInfo>();
        private void button7_Click_2(object sender, EventArgs e)
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
                    j.Visible = false;
                }
                catch (MySqlException ex)
                {
                    MessageBox.Show(ex.ToString());
                }
                var newDoctor = new DoctorInfo
                {
                    Name = dname.Text,
                    Specialization = dspecialization.Text,
                    ImagePath = uploadedImagePath // Assume this comes from upload logic
                };
                //doctorList.Add(newDoctor);
                //File.WriteAllText("doctors.json", JsonSerializer.Serialize(doctorList));
                AddDoctorPanel(newDoctor.Name, newDoctor.Specialization, newDoctor.ImagePath);
            }
        }

        private void button6_Click_1(object sender, EventArgs e)
        {
            j.Hide();
        }
        string uploadedImagePath;
        private void pictureBox11_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp;*.gif";
                openFileDialog.Title = "Select a Profile Picture";

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    uploadedImagePath = openFileDialog.FileName;
                    pic.Image = Image.FromFile(openFileDialog.FileName);
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            patient_tab.Show();
            patient_tab.BringToFront();
        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            patient_tab.Show();
            patient_tab.BringToFront();
        }
    }
}