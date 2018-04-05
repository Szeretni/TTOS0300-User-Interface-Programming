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

namespace Birds
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        List<Bird> linnut = new List<Bird>();
        public MainWindow()
        {
            InitializeComponent();
            linnut = BirdViewModel.Get3TestBirds();
            lstData.DataContext = linnut;
        }

        private void Image_MouseEnter(object sender, MouseEventArgs e)
        {
            var kuva = sender as Image;
            var lintu = kuva.DataContext as Bird;
            tbName.Text = lintu.Name;
            tbPrice.Text = lintu.Value.ToString();
            //kuvan näyttö
            string url = "pack://application:,,,/" + lintu.ImgFile;
            BitmapImage bi = new BitmapImage();
            bi.BeginInit();
            bi.UriSource = new Uri(url);
            bi.EndInit();
            imgBird.Source = bi;
        }
    }
}
