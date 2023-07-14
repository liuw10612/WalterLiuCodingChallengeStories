using System;
using System.Collections.Generic;
using System.Text;

namespace NexTechCodingChallengeStories.Web.Services.HttpService
{
    public class HttpSeviceException : Exception
    {
        public HttpSeviceException(string errorMessage) : base(errorMessage)
        {

        }
    }
}
