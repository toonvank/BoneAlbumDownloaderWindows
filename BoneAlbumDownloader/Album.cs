using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoneAlbumDownloader
{
    public class Album
    {
        public string AlbumName { get; set; }
        public string Artist { get; set; }
        public string DownloadLink { get; set; }
        public string Image { get; set; }
        public string ReleaseDate { get; set; }
    }
}
