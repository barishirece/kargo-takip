using MaterialSkin.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kargo_Takip
{
    public class FillDistricts
    {
        string[] ankara = { "Akyurt", "Altındağ", "Ayaş", "Balâ", "Beypazarı", "Çamlıdere", "Çankaya", "Çubuk", "Elmadağ", "Etimesgut", "Evren", "Gölbaşı", "Güdül", "Haymana", "Kalecik", "Kahramankazan", "Keçiören", "Kızılcahamam", "Mamak", "Nallıhan", "Polatlı", "Pursaklar", "Sincan", "Şereflikoçhisar", "Yenimahalle" };
        string[] bartin = { "Amasra", "Bartın", "Kurucaşile", "Ulus" };
        string[] kastamonu = { "Abana", "Ağlı", "Araç", "Azdavay", "Bozkurt", "Cide", "Çatalzeytin", "Daday", "Devrekani", "Doğanyurt", "Hanönü", "İhsangazi", "İnebolu", "Kastamonu", "Küre", "Pınarbaşı", "Seydiler", "Şenpazar", "Taşköprü", "Tosya" };
        string[] istanbul = { "Adalar", "Arnavutköy", "Ataşehir", "Avcılar", "Bağcılar", "Bahçelievler", "Bakırköy", "Başakşehir", "Bayrampaşa", "Beşiktaş", "Beykoz", "Beylikdüzü", "Beyoğlu", "Büyükçekmece", "Çatalca", "Çekmeköy", "Esenler", "Esenyurt", "Eyüp", "Fatih", "Gaziosmanpaşa", "Güngören", "Kadıköy", "Kağıthane", "Kartal", "Küçükçekmece", "Maltepe", "Pendik", "Sancaktepe", "Sarıyer", "Silivri", "Sultanbeyli", "Sultangazi", "Şile", "Şişli", "Tuzla", "Ümraniye", "Üsküdar", "Zeytinburnu" };

        // girilen ilk comboboxin textine gore (il); diger comboboxa ilceler yerlestirilir
        public void fill(MaterialComboBox city, MaterialComboBox district)
        {
            district.Items.Clear();

            switch (city.Text)
            {
                case "İstanbul":
                    for (int i = 0; i < istanbul.Length; i++)
                        district.Items.Add(istanbul[i]);
                    break;
                case "Ankara":
                    for (int i = 0; i < ankara.Length; i++)
                        district.Items.Add(ankara[i]);
                    break;
                case "Bartın":
                    for (int i = 0; i < bartin.Length; i++)
                        district.Items.Add(bartin[i]);
                    break;
                case "Kastamonu":
                    for (int i = 0; i < kastamonu.Length; i++)
                        district.Items.Add(kastamonu[i]);
                    break;
            }
        }
    }
}
