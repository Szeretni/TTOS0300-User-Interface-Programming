using Microsoft.Win32;
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

namespace MyAudioVideoPlayer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //muuttuja edustakoon yksinkertaista tilakonetta
        enum MusicPlays
        {
            Stop,
            Play,
            Pause
        }
        MusicPlays musicplays;
        public MainWindow()
        {
            InitializeComponent();
            IniMyStuff();
        }
        #region METHODS
        private void IniMyStuff()
        {
            //tänne kootaan kaikki ohjelman käynnityksen yhteydessä tarvittavat alustukset
            musicplays = MusicPlays.Stop;
            SetButtons();
        }
        private void SetButtons()
        {
            //asetetaan näytön buttosten näkyvyyttä/käytettävyyttä
            switch (musicplays)
            {
                case MusicPlays.Stop:
                    btnPlay.IsEnabled = true;
                    btnStop.IsEnabled = false;
                    btnPause.IsEnabled = false;
                    break;
                case MusicPlays.Play:
                    btnPlay.IsEnabled = false;
                    btnStop.IsEnabled = true;
                    btnPause.IsEnabled = true;
                    break;
                case MusicPlays.Pause:
                    btnPlay.IsEnabled = true;
                    btnStop.IsEnabled = false;
                    btnPause.IsEnabled = false;
                    break;
                default:
                    break;
            }
        }
        #endregion
        #region EVENTHANDLERS
        private void btnPlay_Click(object sender, RoutedEventArgs e)
        {
            //tämä metodi = tapahtuman käsittelijä suoritetaan joka kerta kun btnPlay buttosta klikataan
            try
            {
                //tutkitaan onko annettu tiedosto olemassa
                if (txtFilename.Text.Length > 0 &&
                    System.IO.File.Exists(txtFilename.Text))
                {
                    //nyt rokit soimaan
                    //LoadedBehavior täytyy olla Manual jotta voimme koodissa
                    //kontrolloida media Play, Pause ja Stop -metodeilla
                    if (musicplays == MusicPlays.Stop)
                    {
                        //ladataan tiedosto vain tarvittaessa
                        medElement.Source = new Uri(txtFilename.Text);
                    }
                    medElement.Play();
                    musicplays = MusicPlays.Play;
                    SetButtons();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnStop_Click(object sender, RoutedEventArgs e)
        {
            medElement.Stop();
            musicplays = MusicPlays.Stop;
            SetButtons();
        }

        private void btnPause_Click(object sender, RoutedEventArgs e)
        {
            medElement.Pause();
            musicplays = MusicPlays.Pause;
            SetButtons();
        }
        #endregion

        private void btnBrowse_Click(object sender, RoutedEventArgs e)
        {
            //näytetään käyttäjälle vakio Open-dialogi
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.InitialDirectory = @"d:\";
            ofd.Multiselect = false;
            ofd.Filter = "Rock files (*.mp3)|*.mp3|Media files (*.wmv)|*.wmv|All files (*.*)|*.*";
            Nullable<bool> result = ofd.ShowDialog();
            if (result == true)
            {
                txtFilename.Text = ofd.FileName;
            }
        }
    }
}
