using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace OpenVote.Shared
{
    class RestService
    {
        HttpClient _client;

        public RestService()
        {
            _client = new HttpClient();
        }

        public async Task<bool> RegisterUser()
        {
            var uri = new Uri(string.Format(Constants.RegisterUserUrl, string.Empty));
            var response = await _client.GetAsync(uri);
            bool result = false;
            if (response.IsSuccessStatusCode)
            {
                string resultString = await response.Content.ReadAsStringAsync();
                result = (bool) JsonConvert.DeserializeObject(resultString);
            }
            return result;
        }

        public async Task<bool> CheckInUser()
        {
            var uri = new Uri(string.Format(Constants.CheckInUserUrl, string.Empty));
            var response = await _client.GetAsync(uri);
            bool result = false;
            if (response.IsSuccessStatusCode)
            {
                string resultString = await response.Content.ReadAsStringAsync();
                result = (bool)JsonConvert.DeserializeObject(resultString);
            }
            return result;
        }
    }
}
