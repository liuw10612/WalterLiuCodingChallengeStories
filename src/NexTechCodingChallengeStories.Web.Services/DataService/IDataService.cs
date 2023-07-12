using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace NexTechCodingChallengeStories.Web.Services.DataService
{
    public interface IDataService
    {
        Task<T> GetData<T>(string baseurl, string jsonEndpoint);

        Task<List<int>> GetAllData(string baseurl, string jsonEndpoint);
    }
}
