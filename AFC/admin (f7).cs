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
        public Form7()
        {
            InitializeComponent();
            panela.Visible = false;
            panelb.Visible = false;
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

        //private void pictureBox11_Click(object sender, EventArgs e)
        //{
        //    using (OpenFileDialog openFileDialog = new OpenFileDialog())
        //    {
        //        openFileDialog.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp;*.gif";
        //        openFileDialog.Title = "Select a Profile Picture";

        //        if (openFileDialog.ShowDialog() == DialogResult.OK)
        //        {
        //            profilepic1.Image = Image.FromFile(openFileDialog.FileName);
        //        }
        //    }
        //}

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
    }
}