using NexTechCodingChallengeStories.Web.Services.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NextechNewsWebApp.Angular.Service
{
    public  class CachedDataService  
    {
        private List<StoryTitle>? cachedStories = null;
        private DateTime cacheTime=DateTime.Now.AddDays(-2);
        private int pageNumber=-1;
        private int pageSize = -10;

        public CachedDataService()
        {

        }
        public void SetCachedData(int _pageNumber, int _pageSize, List<StoryTitle>? _cachedStories)
        {
            TimeSpan timeLapsed = DateTime.Now.Subtract(cacheTime); 
            if (pageNumber != _pageNumber ||
                pageSize != _pageSize ||
                timeLapsed.TotalHours > 1)
            {
                pageNumber = _pageNumber;
                pageSize = _pageSize;
                cacheTime = DateTime.Now;
                cachedStories = _cachedStories;
            }
        }

        public List<StoryTitle>? GetCachedStores(int _pageNumber, int _pageSize)
        {
            TimeSpan timeLapsed = DateTime.Now.Subtract(cacheTime); // could also write `now - otherTime`
            if (pageNumber == _pageNumber &&
                pageSize == _pageSize &&
                timeLapsed.TotalHours < 1 &&
                cachedStories != null)
            {
                return cachedStories;
            }
            else
                return null;
        }
        
    }
}
