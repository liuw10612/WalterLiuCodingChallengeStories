using NexTechCodingChallengeStories.Web.Services.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NexTechCodingChallengeStories.Web.Services.CacheService
{
    public interface ICachedData
    {
        Dictionary<int, Story> cachedData { get; set; }

        Dictionary<int, Story> cachedShowingData { get; set; }
    }
}
