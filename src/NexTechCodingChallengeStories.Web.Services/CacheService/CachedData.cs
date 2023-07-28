using CodingChallengeStories.Web.Services.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CodingChallengeStories.Web.Services.CacheService
{
    /// <summary>
    /// This class only cache the 1st page with page size = 10 at the moment,
    /// it can be updated to cache multiple pages later with config support;
    /// Since all story ids is not much, they are cached when 1st time called
    /// </summary>
    public class CachedDataService : ICachedData
    {
        private List<StoryTitle> _cachedStoriesOnePage = new List<StoryTitle> { };
        private List<int> _cachedStoryAllIds = new List<int> { };
        private List<int> _badUrlIds = new List<int> { };

        private DateTime cacheTime = DateTime.Now.AddDays(-2); // initialize expire time from 2 days ago
        public const int CachePageNumber = 1;
        public const int CachePageSize = 10;

        public CachedDataService() // UNIT TEST usage
        {
        }
        /// <summary>
        /// Save cache for 1 page  
        /// </summary>
        /// <param name="cachedStories"></param>
        public void SetCachedDataOnePage(List<StoryTitle> cachedStories)
        {
            TimeSpan timeLapsed = DateTime.Now.Subtract(cacheTime);
            if (_cachedStoriesOnePage == null 
                || _cachedStoriesOnePage.Count==0
                || timeLapsed.TotalHours > 1)
            {
                cacheTime = DateTime.Now;
                _cachedStoriesOnePage?.Clear();
                _cachedStoriesOnePage?.AddRange(cachedStories);
            }
        }
        /// <summary>
        /// Get the cached page
        /// </summary>
        /// <param name="pageNumber"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public List<StoryTitle>? GetCachedStoresOnePage(int pageNumber, int pageSize)
        {
            TimeSpan timeLapsed = DateTime.Now.Subtract(cacheTime); 
            if (CachePageNumber == pageNumber 
                && CachePageSize == pageSize 
                && timeLapsed.TotalHours < 1 
                && _cachedStoriesOnePage != null)
                return _cachedStoriesOnePage;
            else
                return null;
        }
        /// <summary>
        /// Save all the stories ID as cache
        /// </summary>
        /// <param name="cachedStoriesAllIds"></param>
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
        /// <summary>
        /// Get cached all stories ids
        /// </summary>
        /// <returns></returns>
        public List<int> GetCachedStoresAllIds()
        {
            return _cachedStoryAllIds;
        }
        /// <summary>
        /// Remove one story which has bad url by save it in the _badUrlIds list
        /// </summary>
        /// <param name="storyId"></param>
        public void RemoveOneStory(int storyId)
        {
            int? storyToRemove = _badUrlIds.SingleOrDefault(r => r == storyId);
            if (storyToRemove == null || storyToRemove == 0)
                _badUrlIds.Add(storyId);
        }
        /// <summary>
        /// check whether a story id  is in the bad one list
        /// </summary>
        /// <param name="storyId"></param>
        /// <returns></returns>
        public bool NotBadUrlId(int storyId)
        {
            int? storyToFind = _badUrlIds.SingleOrDefault(r => r == storyId);
            if (storyToFind == null || storyToFind ==0 )
                return true;
            return false;
        }
    }
}
