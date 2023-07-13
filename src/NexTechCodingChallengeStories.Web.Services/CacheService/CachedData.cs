using NexTechCodingChallengeStories.Web.Services.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NexTechCodingChallengeStories.Web.Services.CacheService
{
    public  class CachedDataService  
    {
        private List<StoryTitle>? cachedStoriesOnePage = null;
        private List<int> cachedStoryAllIds = new List<int> { };
        private DateTime cacheTime=DateTime.Now.AddDays(-2);
        private int pageNumber=-1;
        private int pageSize = -10;

        public CachedDataService()
        {

        }
        public void SetCachedDataOnePage(int _pageNumber, int _pageSize, List<StoryTitle>? _cachedStories)
        {
            TimeSpan timeLapsed = DateTime.Now.Subtract(cacheTime); 
            if (pageNumber != _pageNumber ||
                pageSize != _pageSize ||
                timeLapsed.TotalHours > 1)
            {
                pageNumber = _pageNumber;
                pageSize = _pageSize;
                cacheTime = DateTime.Now;
                cachedStoriesOnePage = _cachedStories;
            }
        }

        public List<StoryTitle>? GetCachedStoresOnePage(int _pageNumber, int _pageSize)
        {
            TimeSpan timeLapsed = DateTime.Now.Subtract(cacheTime); // could also write `now - otherTime`
            if (pageNumber == _pageNumber &&
                pageSize == _pageSize &&
                timeLapsed.TotalHours < 1 &&
                cachedStoriesOnePage != null)
            {
                return cachedStoriesOnePage;
            }
            else
                return null;
        }

        public void SetCachedDataAllIds(List<int> _cachedStoriesAllIds)
        {
            TimeSpan timeLapsed = DateTime.Now.Subtract(cacheTime);
            if (cachedStoryAllIds.Count==0 || timeLapsed.TotalHours > 1)
            {
                if (cachedStoryAllIds.Count>0)
                    cachedStoryAllIds.Clear();
                cachedStoryAllIds.AddRange(_cachedStoriesAllIds.OrderByDescending(x=>x)); // get all ids, sort it 1st time, pretty fast
            }
        }

        public List<int> GetCachedStoresAllIds()
        {
            return cachedStoryAllIds;
        }

    }
}
