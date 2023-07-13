using System;
using System.Collections.Generic;
using System.Text;

namespace NexTechCodingChallengeStories.Web.Services.DataServices
{
    public class DataSeviceException : Exception
    {
        public DataSeviceException(string errorMessage) : base(errorMessage)
        {

        }
    }
}
