using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Kargo_Takip;
using MaterialSkin.Controls;

namespace kargo_takip
{
    public partial class Delivery : MaterialForm
    {
        database database = new database();

        private string selPostID;
        private string selPostReceiver;

        public string data1
        {
            get { return selPostID; }
            set { selPostID = value; }
        }

        public string data2
        {
            get { return selPostReceiver; }
            set { selPostReceiver = value; }
        }


        public Delivery()
        {
            InitializeComponent();
            Sizable = false;
        }

        private void materialButton1_Click(object sender, EventArgs e)
        {
            Main Main = new Main();

            database.crud($"UPDATE gonderiler " +
                          $"SET Durum='Teslim Edildi.' " +
                          $"WHERE TakipNo='{selPostID}'");

            database.crud($"INSERT INTO teslim (TakipNo, TeslimAlanAd, TeslimAlanTC) " +
                          $"VALUES ('{selPostID}', '{txtReceiver2Name.Text}', '{txtReceiver2ID.Text}') ");

            MessageBox.Show("Teslim edildi.");

            Main.Show();
            this.Hide();
        }

        private void materialButton2_Click(object sender, EventArgs e)
        {
            Main Main = new Main();
            Main.Show();
            this.Hide();
        }

        private void Delivery_Load(object sender, EventArgs e)
        {
            // Formu ekranda ortalama
            this.Location = new Point((Screen.PrimaryScreen.WorkingArea.Width - this.Width) / 2,
                (Screen.PrimaryScreen.WorkingArea.Height - this.Height) / 2);

            metroSetLabel4.Text = selPostID;
            metroSetLabel3.Text = selPostReceiver;
        }

    }
}
