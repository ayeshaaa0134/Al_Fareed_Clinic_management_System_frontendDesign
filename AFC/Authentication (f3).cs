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

namespace AFC
{
    public partial class Form3: Form
    {
        public Form3()
        {
            InitializeComponent();
            login.Visible = false;
            reg.Visible = false;
            success.Visible = false;
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            //Form4 form4 = new Form4();
            //this.Hide();
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
    }
}