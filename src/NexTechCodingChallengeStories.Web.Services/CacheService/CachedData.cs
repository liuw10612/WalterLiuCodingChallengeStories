using NexTechCodingChallengeStories.Web.Services.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NexTechCodingChallengeStories.Web.Services.CacheService
{
    public class CachedDataService : ICachedData
    {
        private List<StoryTitle>? cachedStoriesOnePage = null;
        private List<int> cachedStoryAllIds = new List<int> { };
        private List<int> badUrlIds = new List<int> { };

        private DateTime cacheTime = DateTime.Now.AddDays(-2);
        public static int CachePageNumber = 1;
        public static int CachePageSize = 10;

        public CachedDataService()
        {

        }
        public void SetCachedDataOnePage(List<StoryTitle>? _cachedStories)
        {
            TimeSpan timeLapsed = DateTime.Now.Subtract(cacheTime);
            if (cachedStoriesOnePage == null ||
                timeLapsed.TotalHours > 1)
            {
                cacheTime = DateTime.Now;
                cachedStoriesOnePage = _cachedStories;
            }
        }

        public List<StoryTitle>? GetCachedStoresOnePage(int _pageNumber, int _pageSize)
        {
            TimeSpan timeLapsed = DateTime.Now.Subtract(cacheTime); // could also write `now - otherTime`
            if (CachePageNumber == _pageNumber &&
                CachePageSize == _pageSize &&
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
            if (cachedStoryAllIds.Count == 0 || timeLapsed.TotalHours > 1)
            {
                cachedStoryAllIds.Clear();
                badUrlIds.Clear();
                cachedStoryAllIds.AddRange(_cachedStoriesAllIds.OrderByDescending(x => x)); // get all ids, sort it 1st time, pretty fast
            }
        }

        public List<int> GetCachedStoresAllIds()
        {
            return cachedStoryAllIds;
        }
        public void RemoveOneStory(int storyId)
        {
            int? storyToRemove = badUrlIds.SingleOrDefault(r => r == storyId);
            if (storyToRemove == null || storyToRemove == 0)
                badUrlIds.Add(storyId);
        }
        public bool NotBadUrlId(int storyId)
        {
            int? storyToFind = badUrlIds.SingleOrDefault(r => r == storyId);
            if (storyToFind == null || storyToFind ==0 )
                return true;
            return false;
        }

    }
}
