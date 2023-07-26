using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CodingChallengeStories.Web.Services.HttpServices
{
    public interface IHttpService
    {
        Task<T> HttpGetGeneric<T>(string url);

    }
}
