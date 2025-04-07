using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Recepioant
{
    public partial class FormAppointments: Form
    {
        public FormAppointments()
        {
            InitializeComponent();
            history.Hide();
            booking.Hide();
        }
      
        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
           
        }

        private void panel5_Paint(object sender, PaintEventArgs e)
        {

        }

        private void Form1_Load_1(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            
        }

        private void button4_Click(object sender, EventArgs e)
        {
            
        }

        private void monthCalendar1_DateChanged(object sender, DateRangeEventArgs e)
        {

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
    }
}