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
using Microsoft.Win32;
using System.ComponentModel;
using System.IO;
using System.Net;
using System.Media;
using System.Diagnostics;
using SharpCompress.Archives.Rar;
using SharpCompress.Common;
using SharpCompress.Archives;
using System.Threading;
using System.Windows.Threading;

namespace BoneAlbumDownloader
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        string album = "empty", filename, year, cursorKeeper, link="empty";
        int songCounter = 0;
        bool isFinished = false;
        int songArrayCounter = 15;
        //SoundPlayer player, player2, player3;
        MusicPlayer player = new MusicPlayer();
        MediaPlayer balls = new MediaPlayer();
        Links openLink = new Links();
        Setting sc = new Setting();
        int i = 0;
		private string[,] albums = { 
        { "BONES", "2012", "/Cover/1.jpg", "https://drive.google.com/uc?export=download&id=11zHwGe5fZZV8rkyRTX023L6FUHKc0k5g&confirm=t"}, 
        { "TYPICALRAPSHIT", "2012", "/Cover/2.jpg", "https://drive.google.com/uc?id=11LIoQ11Mk7jSR-X41Mx2-pXrwrNYixnh&export=download"}, 
        { "1MILLIONBLUNTS", "2012", "/Cover/3.jpg", "https://drive.google.com/uc?id=11zy9GDteCVgQarq2ZG6EfUIUjrV00-Jv&export=download&confirm=t"},
        { "SCUMBAG", "2013", "/Cover/4.jpg", "https://drive.google.com/uc?id=10no2l2fDCxeu86BOr8eapEiXTl2amVLf&export=download&confirm=t"} ,
        { "PAIDPROGRAMMING", "2013", "/Cover/5.jpg", "https://drive.google.com/uc?id=10Jh876-hhPz3sMSH3rrr62EbG-XrKizc&export=download&confirm=t"} ,
        { "LIVINGLEGEND", "2013", "/Cover/6.jpg", "https://drive.google.com/uc?id=1-yT0RIWOp6zJRYQLi50fhvsk7JtoetAG&export=download&confirm=t"} ,
        { "LAME", "2013", "/Cover/7.jpg", "https://drive.google.com/uc?id=12cSqtYaedKsMUl5rq3KwvTqpLoDWUtTw&export=download&confirm=t"} ,
        { "TEENAGER", "2013", "/Cover/8.jpg", "https://drive.google.com/uc?id=11GIGDb_bEvFpnZRTLhMEdy0OJ_yqFyWG&export=download&confirm=t"} ,
        { "SATURN", "2013", "/Cover/9.jpg", "https://drive.google.com/uc?id=10fQzsgdmU5C0YbcdmZz9vSW-9PXSiBC6&export=download&confirm=t"} ,
        { "CREEP", "2013", "/Cover/10.jpg", "https://drive.google.com/uc?id=1-DdgiUVfGEnG59kNmkkis0h7gBxuQsmE&export=download&confirm=t"} ,
        { "CRACKER", "2013", "/Cover/11.jpg", "https://drive.google.com/uc?id=1-CNIbcNxAd4o6sORm2Pgq7RJ13W3mJ8Q&export=download&confirm=t"} ,
        { "DEADBOY", "2014", "/Cover/12.jpg", "https://drive.google.com/uc?id=1-JLf98NWr2Jfxsakq_UCD3-DBZ7NgUz-&export=download&confirm=t"} ,
        { "TEENWITCH", "2014", "/Cover/13.jpg", "https://drive.google.com/uc?id=11HMwwKGuU3Zy-lrtME8fX4mk7w6ImIRW&export=download&confirm=t"} ,
        { "SKINNY", "2014", "/Cover/14.jpg", "https://drive.google.com/uc?id=10z8O0AdnRimbpxOwgkWguqOY24Du1mqu&export=download&confirm=t"} ,
        { "ROTTEN", "2014", "/Cover/15.jpg", "https://drive.google.com/uc?id=10cdFaTpFynEjlMPFIrhz5q8Vqx446f5r&export=download&confirm=t"} ,
        { "GARBAGE", "2014", "/Cover/16.jpg", "https://drive.google.com/uc?id=1-ciHqb4oij0HRoTOdosCCt5Y3MpXk8Aq&export=download&confirm=t"} ,
        { "HERMITOFEASTGRANDRIVER", "2015", "/Cover/17.jpg", "https://drive.google.com/uc?id=1-l6666xVyNvaV30YgttmgRg_xO5cBQI3&export=download&confirm=t"} ,
        { "YOUSHOULDHAVESEENYOURFACE", "2015", "/Cover/47.jpg", "https://drive.google.com/uc?id=11z9USy117ihV1huweh4g-jfQedDQ2Rqt&export=download&confirm=t"} ,
        { "BANSHEE", "2015", "/Cover/18.jpg", "https://drive.google.com/uc?id=1txmYPuYxNsSTQgKGO9J0gMi7rcw577O7&export=download&confirm=t"} ,
        { "KICKINGTHEBUCKET", "2015", "/Cover/50.jpg", "https://drive.google.com/uc?id=124aam0nUQHNn_eNPIxs0E1YI7WuZlu0E&export=download&confirm=t"} ,
        { "POWDER", "2015", "/Cover/19.jpg", "https://drive.google.com/uc?id=10c2rkj6jJxIy_wtDIKeLBNqH-Oxf-yJW&export=download&confirm=t"} ,
        { "FRAYED", "2015", "/Cover/20.jpg", "https://drive.google.com/uc?id=1-UjDfFHcCUlI4k0LzfBBz9HtuMpkz5sh&export=download&confirm=t"} ,
        { "GOODFORNOTHING", "2016", "/Cover/21.jpg", "https://drive.google.com/uc?id=1-dSfY0rJSdj2U4a-geJXWxoNcYv_HmZx&export=download&confirm=t"} ,
        { "PAIDPROGRAMMING2", "2016", "/Cover/22.jpg", "https://drive.google.com/uc?id=10KzHHys_gzJrcX7JxicsLBF4RuRwOUkY&export=download&confirm=t"} ,
        { "SOFWAREUPDATE1.0", "2016", "/Cover/23.jpg", "https://drive.google.com/uc?id=1102AZyrSiixDd2ftrauUpNp4ZPvlGmeF&export=download&confirm=t"} ,
        { "USELESS", "2016", "/Cover/24.jpg", "https://drive.google.com/uc?id=11dx1NTLTH-DTig0VDJGqgl93Od97aUtQ&export=download&confirm=t"} ,
        { "CARCASS", "2017", "/Cover/25.jpg", "https://drive.google.com/uc?id=1-A3HUUrbRCPmgKjaMwGlBnrjnW52eAbO&export=download&confirm=t"} ,
        { "FAILURE", "2017", "/Cover/26.jpg", "https://drive.google.com/uc?id=1-UYJ3XiLNN4xFjKE7ED9xoq7nXaQrQeb&export=download&confirm=t"} ,
        { "DISGRACE", "2017", "/Cover/27.jpg", "https://drive.google.com/uc?id=1-SlFfFEKIfUXWUKirpCjP15zylPDQ6i0&export=download&confirm=t"} ,
        { "UNRENDERED", "2017", "/Cover/28.jpg", "https://drive.google.com/uc?id=11di90rq18bI8uRc_9IhZjZOVf2eBM21C&export=download&confirm=t"} ,
        { "NOREDEEMINGQUALITIES", "2017", "/Cover/29.jpg", "https://drive.google.com/uc?id=10DBwnstTRV3Kwoz7OlJgs6qWXlNB6Pjy&export=download&confirm=t"} ,
        { "LIVINGSUCKS", "2018", "/Cover/30.jpg", "https://drive.google.com/uc?id=105V_VZf0y-KhUkjAaC6XBWnZ3wi9p_c8&export=download&confirm=t"} ,
        { "THEMANINTHERADIATOR", "2018", "/Cover/31.jpg", "https://drive.google.com/uc?id=11JiPQgsRXVfTwQEszJAJ4ZWTWr8rs5tV&export=download&confirm=t"} ,
        { "PERMANENTFROWN", "2018", "/Cover/32.jpg", "https://drive.google.com/uc?id=10RnLg1xfaRBP9LjB5hJx09VKbVdIgvQO&export=download&confirm=t"} ,
        { "AUGMENTED", "2018", "/Cover/33.jpg", "https://drive.google.com/uc?id=12smlbW8uHGko-32OC8SHvHhNZpMCd1iw&export=download&confirm=t"} ,
        { "UNDERTHEWILLOWTREE", "2019", "/Cover/34.jpg", "https://drive.google.com/uc?id=11XJW78UqhxuqHYaFUR1NUrs7HhLPSHG_&export=download&confirm=t"} ,
        { "SPARROWSCREEK", "2019", "/Cover/35.jpg", "https://drive.google.com/uc?id=12lQoA4qZXSy6CxNCVNoaq9HIOeN62288&export=download&confirm=t"} ,
        { "IFEELLIKEDIRT", "2019", "/Cover/36.jpg", "https://drive.google.com/uc?id=1-meyzQloPBix4zdQxVWexai4cBFRxitz&export=download&confirm=t"} ,
        { "OFFLINE", "2020", "/Cover/37.jpg", "https://drive.google.com/uc?id=10Fn8EiNaruikO0VFxolgQpsa79FRXm0t&export=download&confirm=t"} ,
        { "BRACE", "2020", "/Cover/39.jpg", "https://drive.google.com/uc?id=12QL9R4yreUt1d78AoOCOAzRQYgJVtw6Z&export=download&confirm=t"} ,
        { "REMAINS", "2020", "/Cover/40.jpg", "https://drive.google.com/uc?id=10c9URRnYOgorgk_rLndV9va_y7ZXcyHG&export=download&confirm=t"} ,
        { "FROMBEYONDTHEGRAVE", "2020", "/Cover/41.jpg", "https://drive.google.com/uc?id=1-YAMYz2eJ_d8mttudNO4EpMUFuXHyv2A&export=download&confirm=t"} ,
        { "DAMAGEDGOODS", "2020", "/Cover/42.jpg", "https://drive.google.com/uc?id=1-EdojsDkYfomktHkwqt_KyCsrvb9NO9g&export=download&confirm=t"} ,
        { "PUSHINGUPDAISIES", "2021", "/Cover/43.jpg", "https://drive.google.com/uc?id=10c7gdO4hEE2NQHMwFU3EYy1HUrVmrvpM&export=download&confirm=t"} ,
        { "FORBIDDENIMAGE", "2021", "/Cover/44.jpg", "https://drive.google.com/uc?id=12tpykjukI0wmJs1RQmS_cczD00ScJZO9&export=download&confirm=t"} ,
        { "INLOVINGMEMORY", "2021", "/Cover/45.jpg", "https://drive.google.com/uc?id=1-ncB6j_07q3W_OzTAjqImnSF3H6aOaEl&export=download&confirm=t"} ,
        { "BURDEN", "2021", "/Cover/46.jpg", "https://drive.google.com/uc?id=12C_efxglApaRJ7SzHVg2q33_vsPazfc3&export=download&confirm=t"} ,
        { "SCRAPS", "2021", "/Cover/scraps.jpeg", "https://drive.google.com/uc?id=10g2e6qRDSTklScbV-wExA_E0_gkGZvBK&export=download&confirm=t"} ,
	{ "WITHERED", "2022", "/Cover/withered.jpeg", "https://drive.google.com/uc?id=187jMPLC9STdpByYjDXHKdf7CXXUoLcOY&export=download&confirm=t"} ,
        };
        private string[,] songs =
        {
            {"SystemPreferences","song1.wav"},
            {"Sodium", "song2.wav"},
            {"STEVEWIKOSTHROWCHAIR", "song3.wav"},
            {"HeavyFog", "song4.wav"},
            {"Lysol", "song5.wav"},
            {"PileOfFlesh", "song6.wav"},
            {"HimalayanSalt", "song7.wav"},
            {"LowerThanLow", "song8.wav"},
            {"MyNephewHasAWhitePickupTruck", "song9.wav"},
            {"Baja", "song10.wav"},
            {"Cumulonimbus", "song11.wav"},
            {"TheHealingFields", "song12.wav"},
            {"MisterTenBelow", "song13.wav"},
            {"3M", "song14.wav"},
            {"EmptySongSlot", "song15.mp3"},
            {"EmptySongSlot", "song16.mp3"},
            {"EmptySongSlot", "song17.mp3"},
            {"EmptySongSlot", "song18.mp3"},
            {"EmptySongSlot", "song19.mp3"},
            {"EmptySongSlot", "song20.mp3"},
        };

        public MainWindow()
        {
            InitializeComponent();
            prgProgress.Visibility = Visibility.Hidden;
            lblSongName.Visibility = Visibility.Hidden;
            btnExplorer.Visibility = Visibility.Hidden;
            stckExplorerOptions.Visibility = Visibility.Hidden;
            lstAlbums.Visibility = Visibility.Visible;
            for (int i = 0; i < 48; i++)
            {
                lstAlbums.Items.Add(albums[i, 0]);
            }
            
            //player = new SoundPlayer(System.IO.Path.Combine(Environment.CurrentDirectory, @"Music\", "song1.wav"));
            //player.LoadCompleted += delegate (object sender, AsyncCompletedEventArgs e) {
            //	player.Play();
            //};
            //player.LoadAsync();
        }
        private void PlaySeshSound()
        {
            if (sc.AudioEffect != true)
            {
                Sesh sesh = new Sesh();
                sesh.Play();
            }
        }
        private void StartPos()
        {
            backgroundImage.Source = new BitmapImage(new Uri($@"/Cover/bg.jpg", UriKind.Relative));
            prgProgress.Visibility = Visibility.Hidden;
            btnExplorer.Visibility = Visibility.Hidden;
            stckExplorerOptions.Visibility = Visibility.Hidden;
            lstAlbums.Visibility = Visibility.Visible;
            downloading.Content = "select an album";
            downloading.Margin = new Thickness(0, 218, 0, 0);
        }
        private void DownloadDone()
        {
            stckExplorerOptions.Visibility = Visibility.Visible;
            cmbAlbum.Visibility = Visibility.Hidden;
            prgProgress.Visibility = Visibility.Hidden;
            btnExplorer.Visibility = Visibility.Visible;
            downloading.Content = $"Download completed to \n{filename}";
            PlaySeshSound();
        }
        private void Playing()
        {
            Thread.Sleep(300);
            slMedia.Maximum = balls.NaturalDuration.TimeSpan.TotalSeconds;
            DispatcherTimer wekker = new DispatcherTimer();
            wekker.Tick += new EventHandler(DispatcherTimer_Tick);
            wekker.Interval = new TimeSpan(0, 0, 1);
            wekker.Start();

        }
        private void DispatcherTimer_Tick(object sender, EventArgs e)
        {
            slMedia.Value = balls.Position.TotalSeconds;
        }
        private void IsDone(string download, string albumnaam)
        {
			// Configure save file dialog box
			var dialog = new Microsoft.Win32.SaveFileDialog();
            dialog.InitialDirectory = Properties.Settings.Default.Filename;
            dialog.FileName = albumnaam; // Default file name
			dialog.DefaultExt = ".rar"; // Default file extension
			dialog.Filter = "Rar files (.zip)|*.rar"; // Filter files by extension

			// Show save file dialog box
			bool? result = dialog.ShowDialog();
            // Process save file dialog box results
            if (result == true)
			{
                downloading.Margin = new Thickness(0, 127, 0, 0);
                cmbAlbum.Visibility = Visibility.Hidden;
                lstAlbums.Visibility = Visibility.Hidden;
                // Save document
                filename = dialog.FileName;
                Properties.Settings.Default.Filename = filename;
                Properties.Settings.Default.Save();
                Download.FileDownloader fileDownloader = new Download.FileDownloader();
				downloading.Visibility = Visibility.Visible;
				prgProgress.Visibility = Visibility.Visible;
                if (album != "empty")
                {
                    downloading.Content = $"Downloading {album} ({year})";
                }
                else
                {
                    downloading.Content = $"Downloading Bones Full Discography (10GB)";
                }
				//fileDownloader.DownloadProgressChanged += (sender, e) => downloading.Content = "Progress changed " + e.BytesReceived + " " + e.TotalBytesToReceive;
				fileDownloader.DownloadProgressChanged += (sender, e) => prgProgress.Maximum = e.TotalBytesToReceive;
				fileDownloader.DownloadProgressChanged += (sender, e) => prgProgress.Value = e.BytesReceived;
				fileDownloader.DownloadFileCompleted += (sender, b) => downloading.Content = $"Download completed to \n{filename}";
				fileDownloader.DownloadFileCompleted += (sender, b) => DownloadDone();
                sc.LoadSettings();
                if (sc.AutoOpenAfterDownload == true)
                {
                    fileDownloader.DownloadFileCompleted += (sender, b) => Explorer();
                }
                fileDownloader.DownloadFileAsync($"{download}", filename);
			}
            else
            {
                StartPos();
            }
		}

        private void clickClip_Click(object sender, RoutedEventArgs e)
        {
            if (link != "empty")
            {
                Clipboard.SetText(link);
                MessageBox.Show("Latest link succesfully copied.");
            }
            else
            {
                MessageBox.Show("No link to copy");
            }
        }

        private void copyDrive_Checked(object sender, RoutedEventArgs e)
        {
            openLink.openLink("https://drive.google.com/drive/folders/1OsKCRoX7YGaxkzhtHZ4gHI-LjXgzUw-1?usp=sharing");
        }
        private void BallPlay(string path)
        {
            balls.Open(new Uri(System.IO.Path.Combine(Environment.CurrentDirectory, @"Music\", path)));
            balls.Play();
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (Convert.ToString(play.Content) == "▶︎")
            {
                //player.Start();
                
                string songPath;
                sc.LoadSettings();
                if (sc.ShuffleMusic == true)
                {
                    Random rnd = new Random();
                    int _rnd = rnd.Next(songs.Length);
                    songPath = songs[_rnd/2, 1];
                    songCounter = _rnd/2;
                }
                else
                {
                    songCounter++;
                    songPath = songs[songCounter, 1];
                }
                CurrentSong();
                BallPlay(songPath);
                lblSongName.Visibility = Visibility.Visible;
                play.Content = "⬛";
                Playing();
                balls.MediaEnded += (finished, b) => BallNext();
                balls.MediaEnded += (finished, b) => CurrentSong();
                balls.MediaEnded += (finished, b) => isFinished = true;
                
            }
            else
            {
                play.Content = "▶︎";
                balls.Stop();
                //balls.Pause();
                lblSongName.Visibility = Visibility.Hidden;
            }
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            if (this.WindowState == WindowState.Normal)
            {
                Properties.Settings.Default.Window = WindowState.Normal;
                Properties.Settings.Default.Save();
            }
            else if (this.WindowState == WindowState.Maximized)
            {
                Properties.Settings.Default.Window = WindowState.Maximized;
                Properties.Settings.Default.Save();
            }
            Properties.Settings.Default.Filename = filename;
            Properties.Settings.Default.Volume = slVolume.Value;
            Properties.Settings.Default.Save();
            PlaySeshSound();
            Thread.Sleep(600);
            this.Close();
		}

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
			this.WindowState = WindowState.Minimized;
        }

        private void mnuchange_Click(object sender, RoutedEventArgs e)
        {
            
			string cursor = Microsoft.VisualBasic.Interaction.InputBox($"Answers: seshcrown[w]hite, seshcrown[b]lack, [e]ddy, [s]tandard\nPlease enter the highlighted letter.", "File directory input");
            if (cursor == "w" || cursor == "seshcrownwhite")
            {
				this.Cursor = new Cursor(System.IO.Path.Combine(Environment.CurrentDirectory, @"Pics\", "sesh1.cur"));
                cursorKeeper = "sesh1.cur";

            }
			if (cursor == "b" || cursor == "seshcrownblack")
			{
				this.Cursor = new Cursor(System.IO.Path.Combine(Environment.CurrentDirectory, @"Pics\", "sesh2.cur"));
                cursorKeeper = "sesh2.cur";
            }
			else if (cursor == "e" || cursor == "eddy")
			{
				this.Cursor = new Cursor(System.IO.Path.Combine(Environment.CurrentDirectory, @"Pics\", "ed.cur"));
                cursorKeeper = "ed.cur";

            }
			else if (cursor == "s" || cursor == "standard")
			{
				this.Cursor = new Cursor(System.IO.Path.Combine(Environment.CurrentDirectory, @"Pics\", "cursor2.cur"));
                cursorKeeper = "cursor2.cur";
            }
            Properties.Settings.Default.Cursor = cursorKeeper;
            Properties.Settings.Default.Save();
        }
        private void CurrentSong()
        {
            lblSongName.Content = songs[songCounter, 0];
        }
		private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            lblSongName.Visibility = Visibility.Hidden;
            //if (ispressed == false)
            //{
            //	player.Stop();
            //}
            //else
            //{
            //	player2.Stop();
            //}
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            BallNext();
            CurrentSong();
            lblSongName.Visibility = Visibility.Visible;
            //i++;
            //         if (i == 2)
            //         {
            //	MessageBox.Show("This is an album downloader. Not a music player.");
            //         }
            //         else
            //         {
            //	player.Stop();
            //	player2 = new SoundPlayer(System.IO.Path.Combine(Environment.CurrentDirectory, @"Music\", "song2.wav"));
            //	player2.LoadCompleted += delegate (object sender2, AsyncCompletedEventArgs f) {
            //		player2.Play();
            //	};
            //	player2.LoadAsync();
            //	ispressed = true;
            //}

        }
        private void BallPrevious()
        {
            play.Content = "⬛";
            if (songCounter == 0)
            {

            }
            else
            {
                songCounter--;
                string songPath = songs[songCounter, 1];
                balls.Stop();
                balls.Open(new Uri(System.IO.Path.Combine(Environment.CurrentDirectory, @"Music\", songPath)));
                balls.Play();
            }
        }
        private void BallNext()
        {
            play.Content = "⬛";
            if (songCounter == (songs.Length/2)-1)
            {

            }
            else
            {
                string songPath;
                if (sc.ShuffleMusic == true)
                {
                    Random rnd = new Random();
                    int _rnd = rnd.Next(songs.Length);
                    songPath = songs[_rnd/2, 1];
                    songCounter = _rnd / 2;
                }
                else
                {
                    songCounter++;
                    songPath = songs[songCounter, 1];
                }
                balls.Stop();
                balls.Open(new Uri(System.IO.Path.Combine(Environment.CurrentDirectory, @"Music\", songPath)));
                balls.Play();
            }
        }
        private void Button_Click_5(object sender, RoutedEventArgs e)
        {
            //player.Previous();
            if (songCounter != 0)
            {
                BallPrevious();
                CurrentSong();
                lblSongName.Visibility = Visibility.Visible;
            }
            //try
            //{
            //    player2.Stop();
            //    player3 = new SoundPlayer(System.IO.Path.Combine(Environment.CurrentDirectory, @"Music\", "song1.wav"));
            //    player3.LoadCompleted += delegate (object sender2, AsyncCompletedEventArgs f) {
            //        player3.Play();
            //    };
            //    player3.LoadAsync();
            //    ispressed = true;
            //}
            //catch (Exception)
            //{
            //    MessageBox.Show("You can't go back");
            //    throw;
            //}

        }

        private void lstAlbums_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int i = lstAlbums.SelectedIndex;
            for (int j = 0; j < 48; j++)
            {
                if (j == i)
                {
                    string image = albums[j, 2];
                    string link = albums[j, 3];
                    album = albums[j, 0];
                    year = albums[j, 1];
                    backgroundImage.Source = new BitmapImage(new Uri($@"{image}", UriKind.Relative));
                    IsDone(link, album);
                }
            }
            stckExplorerOptions.Visibility = Visibility.Hidden;
            btnExplorer.Visibility = Visibility.Hidden;
        }
        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                this.DragMove();
        }

        private void cmbAlbum_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int i = cmbAlbum.SelectedIndex;
            for (int j = 0; j < 48; j++)
            {
                if (j == i)
                {
                    string image = albums[j, 2];
                    link = albums[j, 3];
                    album = albums[j, 0];
                    year = albums[j, 1];
                    backgroundImage.Source = new BitmapImage(new Uri($@"{image}", UriKind.Relative));
                    IsDone(link, album);
                }
            }
            stckExplorerOptions.Visibility = Visibility.Hidden;
            btnExplorer.Visibility = Visibility.Hidden;
        }

        private void Explorer()
        {
            string path = filename.Replace($"{album}.rar", "");
            string extractPath = string.Empty;
            if (cbAutoExtract.IsChecked == true)
            {
                Properties.Settings.Default.Extract = true;
                Properties.Settings.Default.Save();
                // Read RAR file
                RarArchive rarArchive = RarArchive.Open(filename);
                // Extract all data
                int i = 0;
                foreach (var entry in rarArchive.Entries.Where(entry => !entry.IsDirectory))
                {
                    entry.WriteToDirectory(path, new ExtractionOptions()
                    {
                        ExtractFullPath = true,
                        Overwrite = true
                    });
                    while (i < 1)
                    {
                        extractPath = Convert.ToString(entry.Key);
                        int index = extractPath.IndexOf("\\");
                        if (index >= 0)
                            extractPath = extractPath.Substring(0, index);
                        i++;
                    }
                }
                rarArchive.Dispose();
            }
            else if (cbAutoExtract.IsChecked == false)
            {
                Properties.Settings.Default.Extract = false;
                Properties.Settings.Default.Save();
            }
            if (cbAutoDelete.IsChecked == true)
            {
                Properties.Settings.Default.Delete = true;
                Properties.Settings.Default.Save();
                File.Delete(filename);
            }
            else if (cbAutoDelete.IsChecked == false)
            {
                Properties.Settings.Default.Delete = false;
                Properties.Settings.Default.Save();
            }
            string extractedPath = System.IO.Path.Combine(path, extractPath);
            Process.Start(extractedPath);
            StartPos();
            if (cbAutoClose.IsChecked == true)
            {
                this.Close();
            }
        }
        private void btnExplorer_Click(object sender, RoutedEventArgs e)
        {
            Explorer();
        }

        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ClickCount == 2)
            {
                WindowState = WindowState == WindowState.Normal ? WindowState.Maximized : WindowState.Normal;
            }
                
            if (this.WindowState == WindowState.Maximized)
            {
                max.Content = "🗗";
            }
            else if (this.WindowState == WindowState.Normal)
            {
                max.Content = "🗖";
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //balls.Open(new Uri($@"C:\Users\Toon Van Kimmenade\Downloads\BoneAlbumDownloader\BoneAlbumDownloader\Music\song1.wav"));
            balls.Open(new Uri(System.IO.Path.Combine(Environment.CurrentDirectory, @"Music\", "song2.wav")));
            this.WindowState = Properties.Settings.Default.Window;
            cursorKeeper = Properties.Settings.Default.Cursor;
            this.Cursor = new Cursor(System.IO.Path.Combine(Environment.CurrentDirectory, @"Pics\", cursorKeeper));
            cbAutoExtract.IsChecked = Properties.Settings.Default.Extract;
            cbAutoDelete.IsChecked = Properties.Settings.Default.Delete;
            sc.LoadSettings();
            PlaySeshSound();
            if (sc.MusicOnStartup == false)
            {
                BallNext();
            }
        }

        private void slMedia_PreviewMouseMove(object sender, MouseEventArgs e)
        {
            
        }

        private void slMedia_PreviewMouseUp(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Released)
            {
                balls.ScrubbingEnabled = true;
                TimeSpan sliderPos = new TimeSpan(0, 0, 0, (int)slMedia.Value);
                balls.Position += sliderPos;
            }
        }

        private void btnSettings_Click(object sender, RoutedEventArgs e)
        {
            var window = new Settings();
            window.Show();
        }

        private void addSongs_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog
            {
                Filter = "mp3 (*.mp3*)|*.mp3*",
                FileName = "punten.txt",
                Multiselect = true,
                InitialDirectory = Properties.Settings.Default.Filename
            };
            if (ofd.ShowDialog() == true)
            {
                string sourcePath = ofd.FileName;
                string destinPath = Convert.ToString(new Uri(System.IO.Path.Combine(Environment.CurrentDirectory, @"Music\")));
                string filename = ofd.SafeFileName;
                string file = filename.ToString();
                string songname = $"song{songArrayCounter}.mp3";
                string destFile = System.IO.Path.Combine(destinPath, songname);
                destFile = destFile.Replace("/", "\\"); 
                destFile = destFile.Remove(0,8); 
                File.Copy(sourcePath, destFile, true);
                songArrayCounter++;
             }
        }
        private void Button_Click_6(object sender, RoutedEventArgs e)
        {
            if (this.WindowState == WindowState.Maximized)
            {
                WindowState = WindowState.Normal;
                max.Content = "🗖";
            }
            else if (this.WindowState == WindowState.Normal)
            {
                this.WindowState = WindowState.Maximized;
                max.Content = "🗗";
            }
        }

        private void btnDownloadAll_Click(object sender, RoutedEventArgs e)
        {
            IsDone("https://drive.google.com/file/d/17-NbQKQUvfYlHZqsz74BEECl0rpgKRvI/view?usp=sharing", "BonesFullDiscog");
        }

        private void slVolume_Loaded(object sender, RoutedEventArgs e)
        {
            if (Properties.Settings.Default.Volume != 0)
            {
                balls.Volume = Properties.Settings.Default.Volume;
            }
            else
            {
                balls.Volume = Properties.Settings.Default.Volume +0.4;
            }
            
            slVolume.Value = balls.Volume;
        }

        private void slVolume_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            balls.Volume = slVolume.Value;
        }
    }
}
