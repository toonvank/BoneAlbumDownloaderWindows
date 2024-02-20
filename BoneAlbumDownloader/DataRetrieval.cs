using FireSharp.Interfaces;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace BoneAlbumDownloader
{
    public class DataRetrieval
    {
        IFirebaseConfig fcon = new FireSharp.Config.FirebaseConfig()
        {
            AuthSecret = "O1itSeLoiYkjfMjzk3iv9q4wK99k70VkJ0COQPMZ",
            BasePath = "https://bonesinfoapp-default-rtdb.europe-west1.firebasedatabase.app/"
        };
        public async Task<List<Album>> GetAlbums()
        {
            IFirebaseClient client = new FireSharp.FirebaseClient(fcon);
            var response = client.Get("Albums");
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var data = response.Body;
                if (!string.IsNullOrEmpty(data))
                {
                    if (data.StartsWith("{")) // Check if data is a single object
                    {
                        // Wrap the single object in an array to make it valid JSON array
                        data = "[" + data + "]";
                    }
                    return JsonConvert.DeserializeObject<List<Album>>(data);
                }
            }
            // Return an empty list if there's no data or an error occurred
            return new List<Album>();
        }
    }
}
