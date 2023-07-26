using CodingChallengeStories.Web.Services.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CodingChallengeStories.Web.Services.CacheService
{
    /// <summary>
    /// This class only cache the 1st page with page size = 10 at the moment,
    /// it can be updated to cache multiple pages later;
    /// Since all story ids is not much, they are cached when 1st time called
    /// </summary>
    public class CachedDataService : ICachedData
    {
        private List<StoryTitle> _cachedStoriesOnePage = new List<StoryTitle> { };
        private List<int> _cachedStoryAllIds = new List<int> { };
        private List<int> _badUrlIds = new List<int> { };

        private DateTime cacheTime = DateTime.Now.AddDays(-2);
        public const int CachePageNumber = 1;
        public const int CachePageSize = 10;

        public CachedDataService()
        {

        }
        public void SetCachedDataOnePage(List<StoryTitle>? cachedStories)
        {
            TimeSpan timeLapsed = DateTime.Now.Subtract(cacheTime);
            if (_cachedStoriesOnePage == null 
                || _cachedStoriesOnePage.Count==0
                || timeLapsed.TotalHours > 1)
            {
                cacheTime = DateTime.Now;
                _cachedStoriesOnePage.Clear();
                _cachedStoriesOnePage.AddRange(cachedStories);
            }
        }

        public List<StoryTitle>? GetCachedStoresOnePage(int pageNumber, int pageSize)
        {
            TimeSpan timeLapsed = DateTime.Now.Subtract(cacheTime); // could also write `now - otherTime`
            if (CachePageNumber == pageNumber 
                && CachePageSize == pageSize 
                && timeLapsed.TotalHours < 1 
                && _cachedStoriesOnePage != null)
                return _cachedStoriesOnePage;
            else
                return null;
        }

        public void SetCachedDataAllIds(List<int> cachedStoriesAllIds)
        {
            TimeSpan timeLapsed = DateTime.Now.Subtract(cacheTime);
            if (_cachedStoryAllIds.Count == 0 || timeLapsed.TotalHours > 1)
            {
                _cachedStoryAllIds.Clear();
                _badUrlIds.Clear();
                _cachedStoryAllIds.AddRange(cachedStoriesAllIds.OrderByDescending(x => x)); // get all ids, sort it 1st time, pretty fast
            }
        }

        public List<int> GetCachedStoresAllIds()
        {
            return _cachedStoryAllIds;
        }
        public void RemoveOneStory(int storyId)
        {
            int? storyToRemove = _badUrlIds.SingleOrDefault(r => r == storyId);
            if (storyToRemove == null || storyToRemove == 0)
                _badUrlIds.Add(storyId);
        }
        public bool NotBadUrlId(int storyId)
        {
            int? storyToFind = _badUrlIds.SingleOrDefault(r => r == storyId);
            if (storyToFind == null || storyToFind ==0 )
                return true;
            return false;
        }
    }
}
