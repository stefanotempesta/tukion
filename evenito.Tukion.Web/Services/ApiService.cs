using evenito.Tukion.Server.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace evenito.Tukion.Web.Services
{
    public class ApiService : IDisposable
    {
        private HttpClient client;

        public ApiService(string baseUri)
        {
            client = new HttpClient();
            client.BaseAddress = new Uri(baseUri);
            
            // API calls should be authorized
            //client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", apiKey);
        }

        public async Task<IEnumerable<VideoModel>> GetVideos()
        {
            var message = await client.GetAsync("api/videos");
            if (!message.IsSuccessStatusCode)
            {
                throw new InvalidOperationException(message.StatusCode.ToString());
            }

            string response = await message.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<IEnumerable<VideoModel>>(response);
        }

        public void Dispose()
        {
            client?.Dispose();
        }
    }
}
