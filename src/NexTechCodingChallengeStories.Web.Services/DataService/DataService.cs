using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace NexTechCodingChallengeStories.Web.Services.DataServices
{
    public class DataService : IDataService
    {
        public DataService()
        {
        }

        public async Task<T> GetData<T>(string baseurl, string jsonEndpointUrl)
        {
            T TObject = default(T);

            using (var client = new HttpClient())
            {

                client.BaseAddress = new Uri(baseurl);

                client.DefaultRequestHeaders.Clear();

                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage Res = await client.GetAsync(jsonEndpointUrl);

                if (!Res.IsSuccessStatusCode)
                {
                    throw new DataSeviceException("Internal server error. GetData");
                } 
                else
                {
                    var Response = Res.Content.ReadAsStringAsync().Result;

                    TObject = JsonConvert.DeserializeObject<T>(Response);
                }

                return TObject;
            }
        }

        public async Task<List<int>> GetAllData(string baseurl, string jsonEndpointUrl)
        {
            List<int> allData = new List<int>();

            using (var client = new HttpClient())
            {

                client.BaseAddress = new Uri(baseurl);

                client.DefaultRequestHeaders.Clear();

                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage Res = await client.GetAsync(jsonEndpointUrl);

                if (!Res.IsSuccessStatusCode)
                {
                    throw new DataSeviceException("Internal server error. GetAllData");
                }
                else
                {
                    var Response = Res.Content.ReadAsStringAsync().Result;

                    allData = JsonConvert.DeserializeObject<List<int>>(Response);
                }

                return allData;
            }
        }
    }
}
