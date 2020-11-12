using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace VideoDisplayApp.UILibrary.API
{
    public class APIHelper : IAPIHelper
    {
        private readonly HttpClient httpClient;
        public APIHelper()
        {
            httpClient = new HttpClient();
        }

        /// <summary>
        /// Returns the result of the specified Uri Get request. 
        /// </summary>
        /// <param name="requestUri"></param>
        /// <param name="additionalHeaders"></param>
        /// <returns></returns>
        public async Task<string> GetAsync(string requestUri, Dictionary<string, string> additionalHeaders = null)
        {
            string result = "Error";
            using (HttpClientHandler httpClientHandler = new HttpClientHandler())
            {
                using (HttpClient httpClient = new HttpClient(httpClientHandler))
                {
                    AddHeaders(httpClient, additionalHeaders);
                    result = await httpClient.GetStringAsync(requestUri);
                }
            }
            return result;
        }

        private void AddHeaders(HttpClient httpClient, Dictionary<string, string> additionalHeaders)
        {
            httpClient.DefaultRequestHeaders.Add("Accept", "application/json");

            //No additional headers to be added
            if (additionalHeaders == null)
                return;

            foreach (KeyValuePair<string, string> current in additionalHeaders)
            {
                httpClient.DefaultRequestHeaders.Add(current.Key, current.Value);
            }
        }
    }
}
