using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace NexTechCodingChallengeStories.Web.Services.HttpService
{
    public interface IHttpService
    {
        Task<T> HttpGetGeneric<T>(string url);

    }
}
