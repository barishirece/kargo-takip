using Kargo_Takip;
using MaterialSkin.Controls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace kargo_takip
{
    public partial class Login : MaterialForm
    {
        public Login()
        {
            InitializeComponent();
            Sizable = false;
        }

        private void materialButton1_Click(object sender, EventArgs e)
        {

        }

        private void Login_Load(object sender, EventArgs e)
        {
            // Formu ekranda ortalama
            this.Location = new Point((Screen.PrimaryScreen.WorkingArea.Width - this.Width) / 2,
                          (Screen.PrimaryScreen.WorkingArea.Height - this.Height) / 2);

            // varsayilan kullanici adi ve sifre
            txtUserName.Text = "admin";
            txtPW.Text = "admin";
        }

        private void Login_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void materialButton1_Click_1(object sender, EventArgs e)
        {
            string kullaniciAdi = txtUserName.Text,
                   sifre = txtPW.Text;

            // eger kullanici adi-sifre cifti veritabaninda mevcutsa true doner
            bool flag = database.check($"SELECT * " +
                                       $"FROM kullanicilar " +
                                       $"WHERE kullaniciAdi='{kullaniciAdi}' AND sifre='{sifre}'");

            if (flag)
            {
                Main Main = new Main();
                Main.Show();
                this.Hide();
                Main.activeUser = kullaniciAdi;
            }
            else
                MessageBox.Show("Sanırım yanlış şifre girdiniz. 👉👈😓");
        }
    }
}
