using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Kargo_Takip
{
    public class database
    {
        public static DataTable db2datagrid(string query)
        {
            OleDbConnection connection = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=kargo-takip.accdb;Persist Security Info=False;");

            try
            {
                OleDbDataAdapter adapter = new OleDbDataAdapter(query, connection);
                DataTable table = new DataTable();
                adapter.Fill(table);
                return table;
            }
            catch (Exception ex)
            {
                Console.WriteLine(DateTime.Now + ": Göklerden gelen bir karar var: " + ex.Message);
                Console.WriteLine("Hatalı Sorgu: " + query);
                return null;
            }
            finally
            {
                if (connection != null && connection.State == ConnectionState.Open) connection.Close();
            }
        }

        public void crud(string query)
        {
            OleDbConnection baglanti = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=kargo-takip.accdb");
            OleDbCommand veri_komutu;
            veri_komutu = new OleDbCommand();

            try
            {
                baglanti.Open();
                veri_komutu.Connection = baglanti;

                veri_komutu.CommandText = query;
                //MessageBox.Show(veri_komutu.CommandText);
                veri_komutu.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Console.WriteLine(DateTime.Now + ": Göklerden gelen bir karar var: " + ex.Message);
                Console.WriteLine("Hatalı Sorgu: " + veri_komutu.CommandText);
            }
            finally
            {
                if (baglanti != null && baglanti.State == ConnectionState.Open) baglanti.Close();
            }
        }

        public static bool check(string query)
        {
            OleDbConnection conn = new OleDbConnection("Provider=Microsoft.ACE.OleDb.12.0; Data source=kargo-takip.accdb");
            OleDbCommand cmd = new OleDbCommand();
            OleDbDataReader Veri_Oku;
            bool slm = false;
            try
            {
                conn.Open();

                cmd.Connection = conn;
                cmd.CommandText = query;

                Veri_Oku = cmd.ExecuteReader();

                if (Veri_Oku.Read()) slm = true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(DateTime.Now + ": Göklerden gelen bir karar var: " + ex.Message);
                Console.WriteLine("Hatalı Sorgu: " + query);
            }
            finally
            {
                if (conn != null && conn.State == ConnectionState.Open) conn.Close();
            }
            return slm;
        }
    }
}
