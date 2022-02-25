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
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Net;
using System.Text;
using Microsoft.Win32;
using System.Media;

namespace BoneAlbumDownloader
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
		string savePathOutput, album, answer, username;
		bool ispressed = false;
		SoundPlayer player, player2;
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
        };

		/* EXAMPLE USAGE
	FileDownloader fileDownloader = new FileDownloader();
	// This callback is triggered for DownloadFileAsync only
	fileDownloader.DownloadProgressChanged += ( sender, e ) => Console.WriteLine( "Progress changed " + e.BytesReceived + " " + e.TotalBytesToReceive );
	// This callback is triggered for both DownloadFile and DownloadFileAsync
	fileDownloader.DownloadFileCompleted += ( sender, e ) => Console.WriteLine( "Download completed" );
	fileDownloader.DownloadFileAsync( "https://INSERT_DOWNLOAD_LINK_HERE", @"C:\downloadedFile.txt" );
*/
		public class FileDownloader : IDisposable
		{
			private const string GOOGLE_DRIVE_DOMAIN = "drive.google.com";
			private const string GOOGLE_DRIVE_DOMAIN2 = "https://drive.google.com";

			// In the worst case, it is necessary to send 3 download requests to the Drive address
			//   1. an NID cookie is returned instead of a download_warning cookie
			//   2. download_warning cookie returned
			//   3. the actual file is downloaded
			private const int GOOGLE_DRIVE_MAX_DOWNLOAD_ATTEMPT = 3;

			public delegate void DownloadProgressChangedEventHandler(object sender, DownloadProgress progress);

			// Custom download progress reporting (needed for Google Drive)
			public class DownloadProgress
			{
				public long BytesReceived, TotalBytesToReceive;
				public object UserState;

				public int ProgressPercentage
				{
					get
					{
						if (TotalBytesToReceive > 0L)
							return (int)(((double)BytesReceived / TotalBytesToReceive) * 100);

						return 0;
					}
				}
			}

			// Web client that preserves cookies (needed for Google Drive)
			private class CookieAwareWebClient : WebClient
			{
				private class CookieContainer
				{
					private readonly Dictionary<string, string> cookies = new Dictionary<string, string>();

					public string this[Uri address]
					{
						get
						{
							string cookie;
							if (cookies.TryGetValue(address.Host, out cookie))
								return cookie;

							return null;
						}
						set
						{
							cookies[address.Host] = value;
						}
					}
				}

				private readonly CookieContainer cookies = new CookieContainer();
				public DownloadProgress ContentRangeTarget;

				protected override WebRequest GetWebRequest(Uri address)
				{
					WebRequest request = base.GetWebRequest(address);
					if (request is HttpWebRequest)
					{
						string cookie = cookies[address];
						if (cookie != null)
							((HttpWebRequest)request).Headers.Set("cookie", cookie);

						if (ContentRangeTarget != null)
							((HttpWebRequest)request).AddRange(0);
					}

					return request;
				}

				protected override WebResponse GetWebResponse(WebRequest request, IAsyncResult result)
				{
					return ProcessResponse(base.GetWebResponse(request, result));
				}

				protected override WebResponse GetWebResponse(WebRequest request)
				{
					return ProcessResponse(base.GetWebResponse(request));
				}

				private WebResponse ProcessResponse(WebResponse response)
				{
					string[] cookies = response.Headers.GetValues("Set-Cookie");
					if (cookies != null && cookies.Length > 0)
					{
						int length = 0;
						for (int i = 0; i < cookies.Length; i++)
							length += cookies[i].Length;

						StringBuilder cookie = new StringBuilder(length);
						for (int i = 0; i < cookies.Length; i++)
							cookie.Append(cookies[i]);

						this.cookies[response.ResponseUri] = cookie.ToString();
					}

					if (ContentRangeTarget != null)
					{
						string[] rangeLengthHeader = response.Headers.GetValues("Content-Range");
						if (rangeLengthHeader != null && rangeLengthHeader.Length > 0)
						{
							int splitIndex = rangeLengthHeader[0].LastIndexOf('/');
							if (splitIndex >= 0 && splitIndex < rangeLengthHeader[0].Length - 1)
							{
								long length;
								if (long.TryParse(rangeLengthHeader[0].Substring(splitIndex + 1), out length))
									ContentRangeTarget.TotalBytesToReceive = length;
							}
						}
					}

					return response;
				}
			}

			private readonly CookieAwareWebClient webClient;
			private readonly DownloadProgress downloadProgress;

			private Uri downloadAddress;
			private string downloadPath;

			private bool asyncDownload;
			private object userToken;

			private bool downloadingDriveFile;
			private int driveDownloadAttempt;

			public event DownloadProgressChangedEventHandler DownloadProgressChanged;
			public event AsyncCompletedEventHandler DownloadFileCompleted;

			public FileDownloader()
			{
				webClient = new CookieAwareWebClient();
				webClient.DownloadProgressChanged += DownloadProgressChangedCallback;
				webClient.DownloadFileCompleted += DownloadFileCompletedCallback;

				downloadProgress = new DownloadProgress();
			}

			public void DownloadFile(string address, string fileName)
			{
				DownloadFile(address, fileName, false, null);
			}

			public void DownloadFileAsync(string address, string fileName, object userToken = null)
			{
				DownloadFile(address, fileName, true, userToken);
			}

			private void DownloadFile(string address, string fileName, bool asyncDownload, object userToken)
			{
				downloadingDriveFile = address.StartsWith(GOOGLE_DRIVE_DOMAIN) || address.StartsWith(GOOGLE_DRIVE_DOMAIN2);
				if (downloadingDriveFile)
				{
					address = GetGoogleDriveDownloadAddress(address);
					driveDownloadAttempt = 1;

					webClient.ContentRangeTarget = downloadProgress;
				}
				else
					webClient.ContentRangeTarget = null;

				downloadAddress = new Uri(address);
				downloadPath = fileName;

				downloadProgress.TotalBytesToReceive = -1L;
				downloadProgress.UserState = userToken;

				this.asyncDownload = asyncDownload;
				this.userToken = userToken;

				DownloadFileInternal();
			}

			private void DownloadFileInternal()
			{
				if (!asyncDownload)
				{
					webClient.DownloadFile(downloadAddress, downloadPath);

					// This callback isn't triggered for synchronous downloads, manually trigger it
					DownloadFileCompletedCallback(webClient, new AsyncCompletedEventArgs(null, false, null));
				}
				else if (userToken == null)
					webClient.DownloadFileAsync(downloadAddress, downloadPath);
				else
					webClient.DownloadFileAsync(downloadAddress, downloadPath, userToken);
			}

			private void DownloadProgressChangedCallback(object sender, DownloadProgressChangedEventArgs e)
			{
				if (DownloadProgressChanged != null)
				{
					downloadProgress.BytesReceived = e.BytesReceived;
					if (e.TotalBytesToReceive > 0L)
						downloadProgress.TotalBytesToReceive = e.TotalBytesToReceive;

					DownloadProgressChanged(this, downloadProgress);
				}
			}

			private void DownloadFileCompletedCallback(object sender, AsyncCompletedEventArgs e)
			{
				if (!downloadingDriveFile)
				{
					if (DownloadFileCompleted != null)
						DownloadFileCompleted(this, e);
				}
				else
				{
					if (driveDownloadAttempt < GOOGLE_DRIVE_MAX_DOWNLOAD_ATTEMPT && !ProcessDriveDownload())
					{
						// Try downloading the Drive file again
						driveDownloadAttempt++;
						DownloadFileInternal();
					}
					else if (DownloadFileCompleted != null)
						DownloadFileCompleted(this, e);
				}
			}

			// Downloading large files from Google Drive prompts a warning screen and requires manual confirmation
			// Consider that case and try to confirm the download automatically if warning prompt occurs
			// Returns true, if no more download requests are necessary
			private bool ProcessDriveDownload()
			{
				FileInfo downloadedFile = new FileInfo(downloadPath);
				if (downloadedFile == null)
					return true;

				// Confirmation page is around 50KB, shouldn't be larger than 60KB
				if (downloadedFile.Length > 60000L)
					return true;

				// Downloaded file might be the confirmation page, check it
				string content;
				using (var reader = downloadedFile.OpenText())
				{
					// Confirmation page starts with <!DOCTYPE html>, which can be preceeded by a newline
					char[] header = new char[20];
					int readCount = reader.ReadBlock(header, 0, 20);
					if (readCount < 20 || !(new string(header).Contains("<!DOCTYPE html>")))
						return true;

					content = reader.ReadToEnd();
				}

				int linkIndex = content.LastIndexOf("href=\"/uc?");
				if (linkIndex >= 0)
				{
					linkIndex += 6;
					int linkEnd = content.IndexOf('"', linkIndex);
					if (linkEnd >= 0)
					{
						downloadAddress = new Uri("https://drive.google.com" + content.Substring(linkIndex, linkEnd - linkIndex).Replace("&amp;", "&"));
						return false;
					}
				}

				return true;
			}

			// Handles the following formats (links can be preceeded by https://):
			// - drive.google.com/open?id=FILEID&resourcekey=RESOURCEKEY
			// - drive.google.com/file/d/FILEID/view?usp=sharing&resourcekey=RESOURCEKEY
			// - drive.google.com/uc?id=FILEID&export=download&resourcekey=RESOURCEKEY
			private string GetGoogleDriveDownloadAddress(string address)
			{
				int index = address.IndexOf("id=");
				int closingIndex;
				if (index > 0)
				{
					index += 3;
					closingIndex = address.IndexOf('&', index);
					if (closingIndex < 0)
						closingIndex = address.Length;
				}
				else
				{
					index = address.IndexOf("file/d/");
					if (index < 0) // address is not in any of the supported forms
						return string.Empty;

					index += 7;

					closingIndex = address.IndexOf('/', index);
					if (closingIndex < 0)
					{
						closingIndex = address.IndexOf('?', index);
						if (closingIndex < 0)
							closingIndex = address.Length;
					}
				}

				string fileID = address.Substring(index, closingIndex - index);

				index = address.IndexOf("resourcekey=");
				if (index > 0)
				{
					index += 12;
					closingIndex = address.IndexOf('&', index);
					if (closingIndex < 0)
						closingIndex = address.Length;

					string resourceKey = address.Substring(index, closingIndex - index);
					return string.Concat("https://drive.google.com/uc?id=", fileID, "&export=download&resourcekey=", resourceKey, "&confirm=t");
				}
				else
					return string.Concat("https://drive.google.com/uc?id=", fileID, "&export=download&confirm=t");
			}

			public void Dispose()
			{
				webClient.Dispose();
			}
		}

		public MainWindow()
        {
            InitializeComponent();
			this.Cursor = new Cursor(System.IO.Path.Combine(Environment.CurrentDirectory, @"Pics\", "cursor2.cur"));
			downloading.Visibility = Visibility.Hidden;
			prgProgress.Visibility = Visibility.Hidden;
			for (int i = 0; i < 48; i++)
            {
                cmbAlbum.Items.Add(albums[i, 0]);
            }
			player = new SoundPlayer(System.IO.Path.Combine(Environment.CurrentDirectory, @"Music\", "song1.wav"));
			player.LoadCompleted += delegate (object sender, AsyncCompletedEventArgs e) {
				player.Play();
			};
			player.LoadAsync();
		}


		private void IsDone(string download, string albumnaam)
        {
			// Configure save file dialog box
			var dialog = new Microsoft.Win32.SaveFileDialog();
			dialog.FileName = albumnaam; // Default file name
			dialog.DefaultExt = ".rar"; // Default file extension
			dialog.Filter = "Rar files (.zip)|*.rar"; // Filter files by extension

			// Show save file dialog box
			bool? result = dialog.ShowDialog();

			// Process save file dialog box results
			if (result == true)
			{
				// Save document
				string filename = dialog.FileName;
				FileDownloader fileDownloader = new FileDownloader();
				downloading.Visibility = Visibility.Visible;
				prgProgress.Visibility = Visibility.Visible;
				downloading.Content = "Download started";
				//fileDownloader.DownloadProgressChanged += (sender, e) => downloading.Content = "Progress changed " + e.BytesReceived + " " + e.TotalBytesToReceive;
				fileDownloader.DownloadProgressChanged += (sender, e) => prgProgress.Maximum = e.TotalBytesToReceive;
				fileDownloader.DownloadProgressChanged += (sender, e) => prgProgress.Value = e.BytesReceived;
				fileDownloader.DownloadFileCompleted += (sender, b) => downloading.Content = $"Download completed to \n{filename}";
				fileDownloader.DownloadFileAsync($"{download}", filename);
			}
		}

        private void clickClip_Click(object sender, RoutedEventArgs e)
        {
            try
            {
				Clipboard.SetText(album);
				MessageBox.Show("Latest link succesfully copied.");
			}
            catch (Exception)
            {
				MessageBox.Show("Please select an album first");
            }
		}

        private void resetLocation_Click(object sender, RoutedEventArgs e)
        {

		}

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
			MessageBox.Show("Made by @seshwoods. 2021. No copyright intended.");
        }

        private void copyDrive_Checked(object sender, RoutedEventArgs e)
        {
			Clipboard.SetText("https://drive.google.com/drive/folders/1OsKCRoX7YGaxkzhtHZ4gHI-LjXgzUw-1?usp=sharing");
			MessageBox.Show("Drive link copied.");
		}

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (ispressed == false)
            {
				player.Play();
			}
            else
            {
				player2.Play();
			}
		}

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
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
			}
			if (cursor == "b" || cursor == "seshcrownblack")
			{
				this.Cursor = new Cursor(System.IO.Path.Combine(Environment.CurrentDirectory, @"Pics\", "sesh2.cur"));
			}
			else if (cursor == "e" || cursor == "eddy")
			{
				this.Cursor = new Cursor(System.IO.Path.Combine(Environment.CurrentDirectory, @"Pics\", "ed.cur"));

			}
			else if (cursor == "s" || cursor == "standard")
			{
				this.Cursor = new Cursor(System.IO.Path.Combine(Environment.CurrentDirectory, @"Pics\", "cursor2.cur"));
			}
		}

		private void Button_Click_1(object sender, RoutedEventArgs e)
        {
			if (ispressed == false)
			{
				player.Stop();
			}
			else
			{
				player2.Stop();
			}
		}

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
			i++;
            if (i == 2)
            {
				MessageBox.Show("This is an album downloader. Not a music player.");
            }
            else
            {
				player.Stop();
				player2 = new SoundPlayer(System.IO.Path.Combine(Environment.CurrentDirectory, @"Music\", "song2.wav"));
				player2.LoadCompleted += delegate (object sender2, AsyncCompletedEventArgs f) {
					player2.Play();
				};
				player2.LoadAsync();
				ispressed = true;
			}
			
		}

        private void cmbAlbum_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
			txtLink.Visibility = Visibility.Hidden;
            int i = cmbAlbum.SelectedIndex;
            for (int j = 0; j < 48; j++)
            {
                if (j == i)
                {
                    string image = albums[j, 2];
                    string link = albums[j, 3];
					album = albums[j, 0];
					backgroundImage.Source = new BitmapImage(new Uri($@"{image}", UriKind.Relative));
					IsDone(link, album);
				}
            }
        }
    }
}
