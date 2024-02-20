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
        DataRetrieval dataRetrieval = new DataRetrieval();
        Links openLink = new Links();
        Setting sc = new Setting();
        int i = 0;
        private List<Album> albums;
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
            albums = dataRetrieval.GetAlbums().Result;
            prgProgress.Visibility = Visibility.Hidden;
            lblSongName.Visibility = Visibility.Hidden;
            btnExplorer.Visibility = Visibility.Hidden;
            stckExplorerOptions.Visibility = Visibility.Hidden;
            lstAlbums.Visibility = Visibility.Visible;
            albums.RemoveAll(album => album == null);
            foreach (var album in albums)
            {
                lstAlbums.Items.Add(album.AlbumName);
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
            foreach(Album albumObj in albums)
            {
                string link = albumObj.DownloadLink;
                album = albumObj.AlbumName;
                year = albumObj.ReleaseDate;
                backgroundImage.Source = new BitmapImage(new Uri($@"{albumObj.Image}", UriKind.Relative));
                IsDone(link, album);
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
            foreach (Album albumObj in albums)
            {
                string link = albumObj.DownloadLink;
                album = albumObj.AlbumName;
                year = albumObj.ReleaseDate;
                backgroundImage.Source = new BitmapImage(new Uri($@"{albumObj.Image}", UriKind.Relative));
                IsDone(link, album);
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
