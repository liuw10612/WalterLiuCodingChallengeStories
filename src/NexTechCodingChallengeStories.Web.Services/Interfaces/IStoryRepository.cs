using NexTechCodingChallengeStories.Web.Services.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace NexTechCodingChallengeStories.Web.Services.Interfaces
{
    public interface IStoryRepository : IAsyncRepository<Story>
    {
    }
}
