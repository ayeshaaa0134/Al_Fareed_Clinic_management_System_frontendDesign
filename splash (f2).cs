using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AFC
{
    public partial class Form2: Form
    {
        public Form2()
        {
            InitializeComponent();
            //FadeIn();
            //FadeOut();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form3 form3 = new Form3();
            this.Hide();
            form3.ShowDialog();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            //FadeIn();
        }

        //private async void FadeIn()
        //{
        //    this.Opacity = 0; // Start fully transparent
        //    while (this.Opacity < 1.0)
        //    {
        //        await Task.Delay(50);
        //        this.Opacity += 0.02; // Gradually increase opacity
        //    }
        //    this.Opacity = 1; // Fully visible
        //}

        //private async void FadeOut()
        //{
        //    while (this.Opacity > 0.0)
        //    {
        //        await Task.Delay(50);
        //        this.Opacity -= 0.02; // Gradually decrease opacity
        //    }
        //    //this.Close(); // Close the form when fully transparent
        //    Form3 form3 = new Form3();
        //    this.Hide();
        //    form3.ShowDialog();
        //}
        //private void Form2_FormClosing(object sender, FormClosingEventArgs e)
        //{
        //    FadeOut();
        //}

    }
}