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
using System.Windows.Shapes;

namespace BoneAlbumDownloader
{
    /// <summary>
    /// Interaction logic for Settings.xaml
    /// </summary>
    public partial class Settings : Window
    {
        Setting set = new Setting();
        public Settings()
        {
            InitializeComponent();
        }
        Links link = new Links();
        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                this.DragMove();
        }
        

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            link.openLink("https://gist.github.com/yasirkula/d0ec0c07b138748e5feaecbd93b6223c");
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnDrive_Click(object sender, RoutedEventArgs e)
        {
            link.openLink("https://drive.google.com/drive/folders/1OsKCRoX7YGaxkzhtHZ4gHI-LjXgzUw-1?usp=sharing");
        }

        private void btnLinks_Click(object sender, RoutedEventArgs e)
        {
            link.openLink("https://beacons.ai/seshwoods");
        }

        private void btnCoffee_Click(object sender, RoutedEventArgs e)
        {
            link.openLink("https://www.buymeacoffee.com/seshwoods");
        }

        private void genSave_Click(object sender, RoutedEventArgs e)
        {
            //audioEffect
            if (chkAudio.IsChecked == true)
            {
                set.AudioEffect = true;
            }
            else if(chkAudio.IsChecked == false)
            {
                set.AudioEffect = false;
            }
            Properties.Settings.Default.AudioEffect = set.AudioEffect;
            Properties.Settings.Default.Save();
            MessageBox.Show("Saved succesfully");
        }

        private void dlSave_Click(object sender, RoutedEventArgs e)
        {
            //AutoOpenAfterDownload
            if (chkAutoOpen.IsChecked == true)
            {
                set.AutoOpenAfterDownload = true;
            }
            else if (chkAutoOpen.IsChecked == false)
            {
                set.AutoOpenAfterDownload = false;
            }
            Properties.Settings.Default.AutoOpenAfterDownload = set.AutoOpenAfterDownload;
            Properties.Settings.Default.Save();
            MessageBox.Show("Saved succesfully");
        }

        private void musicSave_Click(object sender, RoutedEventArgs e)
        {
            //MusicOnStartup
            if (chkOnStartup.IsChecked == true)
            {
                set.MusicOnStartup = true;
            }
            else if (chkOnStartup.IsChecked == false)
            {
                set.MusicOnStartup = true;
            }
            Properties.Settings.Default.MusicOnStartup = set.MusicOnStartup;
            Properties.Settings.Default.Save();
            //ShuffleMusic
            if (chkShuffle.IsChecked == true)
            {
                set.ShuffleMusic = true;
            }
            else if (chkShuffle.IsChecked == false)
            {
                set.ShuffleMusic = false;
            }
            Properties.Settings.Default.ShuffleMusic = set.ShuffleMusic;
            Properties.Settings.Default.Save();
            MessageBox.Show("Saved succesfully");
        }
        private void gen()
        {
            //audioEffect
            if (set.AudioEffect == true)
            {
                chkAudio.IsChecked = false;
            }
            else if (set.AudioEffect == false)
            {
                chkAudio.IsChecked = true;
            }
        }
        private void dl()
        {
            //AutoOpenAfterDownload
            if (set.AutoOpenAfterDownload == true)
            {
                chkAutoOpen.IsChecked = true;
            }
            else if (set.AutoOpenAfterDownload == false)
            {
                chkAutoOpen.IsChecked = false;
            }
        }
        private void ms()
        {
            //MusicOnStartup
            if (set.MusicOnStartup == true)
            {
                chkOnStartup.IsChecked = true;
            }
            else if (set.MusicOnStartup == false)
            {
                chkOnStartup.IsChecked = false;
            }
            //ShuffleMusic
            if (set.ShuffleMusic == false)
            {
                chkShuffle.IsChecked = false;
            }
            else if (set.ShuffleMusic == true)
            {
                chkShuffle.IsChecked = false;
            }
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            gen();
            dl();
            ms();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Not available yet");
        }
    }
}
