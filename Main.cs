using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Kargo_Takip;
using MaterialSkin.Controls;
namespace kargo_takip
{
    public partial class Main : MaterialForm
    {
        readonly MaterialSkin.MaterialSkinManager materialSkinManager;

        database db = new database(); // db sinifi
        Login Login = new Login(); // login formu
        Delivery Delivery = new Delivery(); // teslimat formu
        FillDistricts FillDistricts = new FillDistricts(); // il-ilceler
        TableHeaderTexts TableHeaderTexts = new TableHeaderTexts(); // tablo basliklari

        Random rnd = new Random();

        int TakipKodu;

        //toplu islem
        OleDbConnection Veritabani_Baglanti = new OleDbConnection("Provider=Microsoft.ACE.Oledb.12.0;Data Source=kargo-takip.accdb");
        OleDbCommand cmd;
        OleDbDataReader Veri_Adaptor;
        //OleDbDataAdapter Veri_Adaptor2;
        //DataSet Veri_Seti;

        public string activeUser { get; set; }

        public Main()
        {
            InitializeComponent();
            materialSkinManager = MaterialSkin.MaterialSkinManager.Instance;
            // materialSkinManager.EnforceBackcolorOnAllComponents = true;
            materialSkinManager.AddFormToManage(this);
            materialSkinManager.Theme = MaterialSkin.MaterialSkinManager.Themes.LIGHT;
            materialSkinManager.ColorScheme = new MaterialSkin.ColorScheme(MaterialSkin.Primary.Indigo500, MaterialSkin.Primary.Indigo700, MaterialSkin.Primary.Indigo100, MaterialSkin.Accent.Red400, MaterialSkin.TextShade.WHITE);

            Sizable = false;
        }

        private void materialButton2_Click(object sender, EventArgs e)
        {
            // rastgele takip no olusturma
            TakipKodu = rnd.Next(100000000, 999999999);

            // rastgele olusturulan takip no daha once kullanilmis ise tekrar olustur
            while (database.check($"SELECT * " +
                                 $"FROM gonderiler " +
                                 $"WHERE TakipNo='{TakipKodu}'") == true)
            {
                TakipKodu = rnd.Next(100000000, 999999999);
            }

            string teslimTur = "",
                   smsTercih = "",
                   koli = "",
                   kampanyaKod = "";

            if (switchDeliveryType.Checked) teslimTur = "Adrese Teslim"; else teslimTur = "Şubeye Teslim";
            if (materialCheckbox1.Checked) smsTercih = "0"; else smsTercih = "1";
            if (materialCheckbox2.Checked) koli = "0"; else koli = "1";
            if (materialCheckbox3.Checked) kampanyaKod = materialTextBox1.Text; else kampanyaKod = "-";

            // veritabanina yeni gonderi ekleme
            db.crud($"INSERT INTO gonderiler (TakipNo, GondericiTC, GondericiAd, GondericiTel, GondericiMail, GondericiIl, GondericiIlce, GondericiAdres, AliciTC, AliciAd, AliciTel, AliciMail, AliciIl, AliciIlce, AliciAdres, GonderiServisi, GonderiDesi, TeslimTur, SMStercih, Koli, KampanyaKodu, Durum) " +
                    $"VALUES ('{TakipKodu.ToString()}', '{txtSenderID.Text}', '{txtSenderName.Text}', '{txtSenderPhone.Text}', '{txtSenderEmail.Text}', '{cbSenderCity.Text}', '{cbSenderDistrict.Text}', '{txtSenderAddress.Text}', '{txtReceiverID.Text}' , '{txtReceiverName.Text}', '{txtReceiverPhone.Text}', '{txtReceiverEmail.Text}', '{cbReceiverCity.Text}', '{cbReceiverDistrict.Text}', '{txtReceiverAddress.Text}', '{cbShippingService.Text}', '{txtDesi.Text}', '{teslimTur}', '{smsTercih}', '{koli}', '{kampanyaKod}', 'Gönderi Bartın şubesi tarafından teslim alındı.')");
            MessageBox.Show("Gönderi eklendi.");
        }

        private void switchDeliveryType_CheckedChanged(object sender, EventArgs e)
        {
            if (switchDeliveryType.Checked)
                switchDeliveryType.Text = "Adrese Teslim";
            else 
                switchDeliveryType.Text = "Şubeye Teslim";
        }

        private void cbReceiverCity_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillDistricts.fill(cbReceiverCity, cbReceiverDistrict);
        }

        private void cbSenderCity_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillDistricts.fill(cbSenderCity, cbSenderDistrict);
        }

        private void materialButton3_Click(object sender, EventArgs e)
        {
            Delivery Delivery = new Delivery();

            // takip no ve alici bilgisini teslimat formuna gonderme
            Delivery.data1 = dataGridView1.CurrentRow.Cells[0].Value.ToString(); // takip no
            Delivery.data2 = dataGridView1.CurrentRow.Cells[9].Value.ToString(); // alici adi
            Delivery.Show();
            this.Hide();
        }

        private void materialButton1_Click(object sender, EventArgs e)
        {
            db.crud($"DELETE FROM gonderiler " +
                              $"WHERE TakipNo='{dataGridView1.CurrentRow.Cells[0].Value.ToString()}'");
            dgRefresh();
            MessageBox.Show("Gönderi silindi.");
        }

        private void materialButton4_Click(object sender, EventArgs e)
        {
            // listboxa gonderi ekleme
            if (materialComboBox1.SelectedIndex == -1)
            {
                MessageBox.Show("Seçim yapmadınız.");
            }
            else
            {
                materialListBox1.AddItem(materialComboBox1.Text);
                materialComboBox1.Text = "";
                materialComboBox1.Items.RemoveAt(materialComboBox1.SelectedIndex);
            }
        }

        private void materialListBox1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            materialComboBox1.Items.Add(materialListBox1.Items[materialListBox1.SelectedIndex].Text);
            materialListBox1.Items.RemoveAt(materialListBox1.SelectedIndex);
        }

        private void materialButton5_Click(object sender, EventArgs e)
        {
            if (materialListBox1.Items.Count == 0)
            {
                MessageBox.Show("Gönderi Seçimi Yapılmadı.");
            }
            else
            {
                string durum = "Zonguldak Aktarma Merkezine Yönlendirildi.";

                if (materialComboBox6.Text == "Şubeye Geldi.") 
                    durum = "Gönderi Bartın şubesine ulaştı.";
                else if (materialComboBox6.Text == "Aktarma Merkezine Yönlendirildi.")
                    durum = materialComboBox7.Text + " Aktarma Merkezine Yönlendirildi.";
                else if (materialComboBox6.Text == "Dağıtıma Çıkarıldı.")
                    durum = "Dağıtıma çıkarıldı.";

                for (int i = 0; i < materialListBox1.Items.Count; i++)
                {
                    db.crud($"UPDATE gonderiler " +
                            $"SET Durum='{durum}' " +
                            $"WHERE TakipNo='{materialListBox1.Items[i].Text}'");
                }

                MessageBox.Show("Bilgiler güncellendi.");
                materialListBox1.Items.Clear();
            }
        }

        private void dataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            // dg2'de secilen gonderinin takip no'suna gore musteri bilgilerini gosterme
            dataGridView3.DataSource = database.db2datagrid($"SELECT * " +
                                                            $"FROM teslim " +
                                                            $"WHERE TakipNo='{dataGridView2.CurrentRow.Cells[0].Value.ToString()}'");
            TableHeaderTexts.teslim(dataGridView3);
        }

        private void materialButton7_Click(object sender, EventArgs e)
        {
            string kullaniciAdi = txtAddUserName.Text,
                   sifre = txtAddPW.Text;

            // yeni kullanici ekleme
            db.crud($"INSERT INTO kullanicilar (kullaniciAdi, sifre) " +
                    $"VALUES ('{kullaniciAdi}', '{sifre}')");

            txtAddUserName.Clear();
            txtAddPW.Clear();

            // kullanici eklendikten sonra tabloyu yenileme
            dgRefresh();

            MessageBox.Show("Yeni kullanıcı eklendi.");
        }

        private void materialButton8_Click(object sender, EventArgs e)
        {
            // kullanici bilgilerini guncelleme
            db.crud($"UPDATE kullanicilar " +
                                $"SET kullaniciAdi='{txtEditUserName.Text}', sifre='{txtEditPW.Text}' " +
                                $"WHERE Kimlik={Convert.ToInt32(txtUserID.Text)}");

            dgRefresh();

            MessageBox.Show("Bilgiler güncellendi.");
        }

        private void dataGridView4_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            metroSetTabControl3.SelectedTab = metroSetSetTabPage5;

            txtUserID.Text = dataGridView4.CurrentRow.Cells[0].Value.ToString();
            txtEditUserName.Text = dataGridView4.CurrentRow.Cells[1].Value.ToString();
            txtEditPW.Text = dataGridView4.CurrentRow.Cells[2].Value.ToString();
        }

        private void Main_Load(object sender, EventArgs e)
        {

            this.MaximizeBox = false;

            // Il ve Ilceler
            FillDistricts.fill(cbSenderCity, cbSenderDistrict);
            FillDistricts.fill(cbReceiverCity, cbReceiverDistrict);

            this.Location = new Point((Screen.PrimaryScreen.WorkingArea.Width - this.Width) / 2,
                          (Screen.PrimaryScreen.WorkingArea.Height - this.Height) / 2);

            // kargo arama sekmesinde tum gonderileri listeleme
            dgRefresh();
            TableHeaderTexts.gonderiler(dataGridView1);

            dataGridView4.Columns[1].Width = 200;
            dataGridView4.Columns[2].Visible = false;

            dataGridView2.DataSource = database.db2datagrid($"SELECT * " +
                                                                $"FROM gonderiler " +
                                                                $"WHERE Durum='Teslim Edildi.'");
            TableHeaderTexts.gonderiler(dataGridView2);

            //toplu islem
            cmd = new OleDbCommand();
            Veritabani_Baglanti.Open();
            cmd.Connection = Veritabani_Baglanti;
            cmd.CommandText = $"SELECT * " +
                              $"FROM gonderiler " +
                              $"WHERE Durum <> 'Teslim Edildi.'";
            Veri_Adaptor = cmd.ExecuteReader();


            while (Veri_Adaptor.Read())
            {
                materialComboBox1.Items.Add(Veri_Adaptor["TakipNo"].ToString());
            }
            Veritabani_Baglanti.Close();
        }

        private void materialTabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Sekme secimine gore form basligi
            switch (materialTabControl1.SelectedIndex)
            {
                case 0:
                    this.Text = "Gönderi Ekle";
                    break;
                case 1:
                    this.Text = "Teslim Et";
                    break;
                case 2:
                    this.Text = "Toplu İşlem";
                    break;
                case 3:
                    this.Text = "Teslim Edilenler";
                    break;
                case 4:
                    this.Text = "Kullanıcılar";
                    break;
                default:
                    this.Text = "Bilgi";
                    break;
            }

            dgRefresh(); // tab degistiginde refresh

            materialTextBox212.Clear();

            //toplu islem
            cmd = new OleDbCommand();
            Veritabani_Baglanti.Open();
            cmd.Connection = Veritabani_Baglanti;
            cmd.CommandText = $"SELECT * " +
                              $"FROM gonderiler " +
                              $"WHERE Durum <> 'Teslim Edildi.'";
            Veri_Adaptor = cmd.ExecuteReader();

            materialComboBox1.Items.Clear();
            while (Veri_Adaptor.Read())
            {
                materialComboBox1.Items.Add(Veri_Adaptor["TakipNo"].ToString());
            }
            Veritabani_Baglanti.Close();
        }

        private void materialTextBox212_Click(object sender, EventArgs e)
        {

        }

        private void materialTextBox212_TextChanged(object sender, EventArgs e)
        {
            dataGridView2.DataSource = database.db2datagrid($"SELECT * " +
                                                $"FROM gonderiler " +
                                                $"WHERE Durum='Teslim Edildi.'" +
                                                $"AND TakipNo LIKE '{materialTextBox212.Text}%'");
        }

        private void materialComboBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void materialTextBox210_TextChanged(object sender, EventArgs e)
        {
            dataGridView1.DataSource = database.db2datagrid($"SELECT * " +
                                                            $"FROM gonderiler " +
                                                            $"WHERE TakipNo LIKE '{materialTextBox210.Text}%' " +
                                                            $"AND Durum <> 'Teslim Edildi.'");
            TableHeaderTexts.gonderiler(dataGridView1);
        }

        private void materialButton6_Click(object sender, EventArgs e)
        {
            db.crud($"DELETE FROM kullanicilar " +
                    $"WHERE Kimlik={Convert.ToInt32(txtUserID.Text)}");

            dgRefresh();
            MessageBox.Show("Kullanıcı silindi.");
        }

        void dgRefresh()
        {
            dataGridView1.DataSource = database.db2datagrid($"SELECT * " +
                                                            $"FROM gonderiler " +
                                                            $"WHERE Durum <> 'Teslim Edildi.'");

            dataGridView2.DataSource = database.db2datagrid($"SELECT * " +
                                                            $"FROM gonderiler " +
                                                            $"WHERE Durum='Teslim Edildi.'");

            dataGridView4.DataSource = database.db2datagrid($"SELECT * " +
                                                            $"FROM Kullanicilar");
        }

    }
}
