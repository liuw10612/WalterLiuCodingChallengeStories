using System;
using System.Collections.Generic;
using System.Text;

namespace CodingChallengeStories.Web.Services.HttpServices
{
    public class HttpSeviceException : Exception
    {
        public HttpSeviceException(string errorMessage) : base(errorMessage)
        {

        }
    }
}
