using CodingChallengeStories.Web.Services.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CodingChallengeStories.Web.Services.CacheService
{
    public interface ICachedData
    {
        public void SetCachedDataAllIds(List<int> cachedStoriesAllIds);
        List<StoryTitle>? GetCachedStoresOnePage(int _pageNumber, int _pageSize);
        void SetCachedDataOnePage(List<StoryTitle> cachedStories, int pageNumber, int pageSize);
        List<int> GetCachedStoresAllIds();
        void RemoveOneStory(int storyId);
        public bool NotBadUrlId(int storyId);
    }
}
