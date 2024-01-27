using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Kargo_Takip
{
    public class TableHeaderTexts
    {
        public void gonderiler(DataGridView dg)
        {
            if (dg.Rows.Count > 0)
            {
                dg.Columns[0].HeaderText = "Takip No";
                dg.Columns[1].HeaderText = "Gönderici TCKN";
                dg.Columns[2].HeaderText = "Gönderici Adı";
                dg.Columns[3].HeaderText = "Gönderici Tel.";
                dg.Columns[4].HeaderText = "Gönderici E-Mail";
                dg.Columns[5].HeaderText = "Gönderici İl";
                dg.Columns[6].HeaderText = "Gönderici İlçe";
                dg.Columns[7].HeaderText = "Gönderici Adres";
                dg.Columns[8].HeaderText = "Alıcı TCKN";
                dg.Columns[9].HeaderText = "Alıcı Adı";
                dg.Columns[10].HeaderText = "Alıcı Tel.";
                dg.Columns[11].HeaderText = "Alıcı E-Mail";
                dg.Columns[12].HeaderText = "Alıcı İl";
                dg.Columns[13].HeaderText = "Alıcı İlçe";
                dg.Columns[14].HeaderText = "Alıcı Adres";
                dg.Columns[15].HeaderText = "Gönderi Servisi";
                dg.Columns[16].HeaderText = "Desi";
                dg.Columns[17].HeaderText = "Teslim Tercihi";
                dg.Columns[18].HeaderText = "SMS";
                dg.Columns[19].HeaderText = "Koli";
            }
        }

        public void teslim(DataGridView dg)
        {
            if (dg.Rows.Count > 0)
            {
                dg.Columns[0].HeaderText = "Sıra No";
                dg.Columns[0].Width = 40;
                dg.Columns[1].HeaderText = "Takip Numarası";
                dg.Columns[1].Width = 130;
                dg.Columns[2].HeaderText = "Teslim Alanın Adı Soyadı";
                dg.Columns[2].Width = 200;
                dg.Columns[3].HeaderText = "Teslim Alanın TCKN";
                dg.Columns[3].Width = 200;
            }
        }
    }
}
