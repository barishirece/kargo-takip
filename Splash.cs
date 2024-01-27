using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MaterialSkin.Controls;


namespace kargo_takip
{
    public partial class Splash : MaterialForm
    {
        public Splash()
        {
            InitializeComponent();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            materialProgressBar1.Value += 5;

            if (materialProgressBar1.Value == 100)
            {
                timer1.Enabled = false;
                Login Login = new Login();
                timer1.Stop();
                Login.Show();
                this.Hide();
            }
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void Splash_Load(object sender, EventArgs e)
        {
            // Formu ekranda ortalama
            this.Location = new Point((Screen.PrimaryScreen.WorkingArea.Width - this.Width) / 2,
                          (Screen.PrimaryScreen.WorkingArea.Height - this.Height) / 2);
        }
    }
}
