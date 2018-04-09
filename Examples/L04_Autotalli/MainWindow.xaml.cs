using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using JAMK.IT;

namespace Autotalli
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //kuvat
        private const string polku = @"C:\Users\l2912\Source\Repos\TTOS0300-User-Interface-Programming\Examples\L04_Autotalli\";
        List<Auto> autot = new List<Auto>();
        public MainWindow()
        {
            InitializeComponent();
            LoadData();
        }

        private void LoadData()
        {
            try
            {
                //aloituskuva
                NaytaKuva("autotalli.png");
                //ladataan kaikki autot muistiin
                //autot = JAMK.IT.Autotalli1.HaeAutot(); tämä oli dummy-dataa
                autot = JAMK.IT.BLAutotalli.HaeAutotDB();
                //täytetään combobox autojen merkeillä
                //vaihtoehto 1 manuaalisesti
                List<String> merkit = new List<string>();
                merkit.Add("Audi");
                merkit.Add("Saab");
                merkit.Add("Volvo");
                cmbMerkit.ItemsSource = merkit;
                //vaihtoehto 2 linqlla datasta
                var result = autot.Select(m => m.Merkki).Distinct();
                cmbMerkit.ItemsSource = result;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void NaytaKuva(string url)
        {
            try
            {
                //lisätään vakiopolku kuvatiedostoon
                if (url != "")
                {
                    url = polku + url;
                    //kuvan näyttö
                    if (System.IO.File.Exists(url))
                    {
                        BitmapImage bi = new BitmapImage();
                        bi.BeginInit();
                        bi.UriSource = new Uri(url);
                        bi.EndInit();
                        imgAuto.Stretch = Stretch.Fill;
                        imgAuto.Source = bi;
                    }
                }
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnHaeAutot_Click(object sender, RoutedEventArgs e)
        {
            dgAutot.ItemsSource = JAMK.IT.BLAutotalli.HaeAutot();
        }

        private void dgAutot_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            /*
            //olemme itse populoineet DataGridin Auto-olioilla, joten kukin
            //DataGridin rivi eli alkio on itse asiassa Auto-olio
            object obj = dgAutot.SelectedItem;
            if (obj != null)
            {
                Auto valittu = (Auto)obj;
                NaytaKuva(valittu.URL);
            }
            */
            //TAI lyhyesti
            Auto valittu = dgAutot.SelectedItem as Auto;
            if (valittu != null)
            {
                NaytaKuva(valittu.URL);
            }
        }

        private void btnHaeAuditontent_Click(object sender, RoutedEventArgs e)
        {
            //näkyviin suodattamalla autot-listasta audit. Käytetään LINQ
            var result = autot.Where(m => m.Merkki.Contains("Audi"));
            dgAutot.ItemsSource = result;
        }

        private void cmbMerkit_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string merkki = cmbMerkit.SelectedItem.ToString();
            var result = autot.Where(m => m.Merkki.Contains(merkki)).ToList();
            dgAutot.ItemsSource = result;
            NaytaKuva("autotalli.png");
        }

        private void btnHaeAutotMysql_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                autot = JAMK.IT.DBAutotalli.GetAllAutosFromMySQL();
                dgAutot.ItemsSource = autot;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}