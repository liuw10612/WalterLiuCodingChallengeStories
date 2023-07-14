using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using NexTechCodingChallengeStories.Web.Services.Model;


namespace NexTechCodingChallengeStories.Web.Services.HttpService
{
    public class HttpService : IHttpService
    {
        public HttpService()
        {
        }

        public async Task<T> HttpGetGeneric<T>(string url)
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    var request = new HttpRequestMessage
                    {
                        Method = HttpMethod.Get,
                        RequestUri = new Uri(url),
                    };
                    var response = await client.SendAsync(request).ConfigureAwait(false);
                    response.EnsureSuccessStatusCode();

                    var responseJson = await response.Content.ReadAsStringAsync().ConfigureAwait(false);

                    return JsonConvert.DeserializeObject<T>(responseJson);
                }
            }
            catch (Exception e)
            {
                // log and send notification
                throw new HttpSeviceException("Internal server error : HttpGetOneStory()");
            }
        }
    }
}
