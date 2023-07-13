using NexTechCodingChallengeStories.Web.Services.Entities;
using NexTechCodingChallengeStories.Web.Services.Interfaces;
using NexTechCodingChallengeStories.Web.Services.DataServices;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using NexTechCodingChallengeStories.Web.Services.CacheService;
using System.Linq.Expressions;
using Microsoft.Extensions.Logging;

namespace NexTechCodingChallengeStories.Web.Services.Repository
{
    public class StoryRepository : IStoryRepository
    {
        private const string _baseAPIUrl = "https://hacker-news.firebaseio.com";
        ILogger<StoryRepository> _logger;
        private IDataService _dataService;
        private ICachedData _cachedDataService;

        public StoryRepository()
        {

        }
        public StoryRepository(ILogger<StoryRepository> logger, IDataService dataService, ICachedData cachedDataService)
        {
            _logger= logger;
            _dataService = dataService;
            _cachedDataService = cachedDataService;
        }

        public async Task<Story> GetByIdAsync(int id)
        {
            
            string itemEndpoint = $"/v0/item/{id}.json?print=pretty";

            var item = await _dataService.GetData<Story>(_baseAPIUrl, itemEndpoint);

            return item;
        }

        public async Task<List<int>> GetNewStoriesAsync()
        {
            try
            { 
                string newStoriesEndpoint = $"/v0/newstories.json?print=pretty";

                var newStories = await _dataService.GetAllData(_baseAPIUrl, newStoriesEndpoint);

                return newStories;
            }
            catch (DataSeviceException e)
            {
                _logger.LogError(e, "Http Error while GetNewStoriesAsync()");
                return null;
            }
            catch (Exception e)
            {
                _logger.LogError(e, "No HTTP Error while GetNewStoriesAsync()");
                throw;
            }
        }
        public async Task<List<StoryTitle>> GetOnePageStoriesAsync(int page, int pageSize)
        {
            try
            {
                var cachedDataOnePage =  _cachedDataService.GetCachedStoresOnePage(page, pageSize);
                if (cachedDataOnePage != null)
                    return cachedDataOnePage; // cached data exist, use it.
                else // no cached one page or expired
                {
                    List<int> cachedDataAllIds = _cachedDataService.GetCachedStoresAllIds();
                    if (cachedDataAllIds.Count == 0) // 1st time or expired
                    {
                        cachedDataAllIds = await GetNewStoriesAsync();
                        _cachedDataService.SetCachedDataAllIds(cachedDataAllIds);
                    }
                    // take one page IDs
                    var selectedPage = cachedDataAllIds.Skip((page - 1) * pageSize).Take(pageSize);

                    // loop one page ids and filter out bad url ones
                    // BUG : it may have less than one page size items
                    List<StoryTitle> stories = new List<StoryTitle>();  //return list :  title + url + id + time
                    foreach (int storyId in selectedPage)
                    {
                        var story = await GetByIdAsync(storyId);

                        if (story != null )
                        {
                            if (Uri.IsWellFormedUriString(story.url, UriKind.RelativeOrAbsolute))
                            {
                                // ignore bad URL ones)
                                stories.Add(new StoryTitle
                                {
                                    id = story.id,
                                    time = story.time,
                                    title = story.title,
                                    url = story.url,
                                });
                            }
                            else // remove this bad one from the list, so it will not be checked
                            {
                                _cachedDataService.RemoveOneStory(storyId);
                            }
                        }
                    }
                    if (page == CachedDataService.CachePageNumber && pageSize == CachedDataService.CachePageSize) // save 1st page as cache
                        _cachedDataService.SetCachedDataOnePage(stories);

                    return stories;
                }
            }
            catch (DataSeviceException e)
            {
                _logger.LogError(e, "Http Error while GetOnePageStoriesAsync()");
                return null;
            }
            catch (Exception e)
            {
                _logger.LogError(e, "No HTTP Error while GetOnePageStoriesAsync()");
                throw;
            }
        }
        public async Task<int> GetStoriesCountAsync()
        {
            try
            {
                string newStoriesEndpoint = $"/v0/newstories.json?print=pretty";

                var newStories = await _dataService.GetAllData(_baseAPIUrl, newStoriesEndpoint);
                if (newStories !=null && newStories.Count > 0)
                {
                    _cachedDataService.SetCachedDataAllIds(newStories); // save for cache
                    return newStories.Count;
                }
                return 0;
            }
            catch (DataSeviceException e)
            {
                _logger.LogError(e, "Http Error while GetStoriesCountAsync()");
                return 0;
            }
            catch (Exception e)
            {
                _logger.LogError(e, "No HTTP Error while GetStoriesCountAsync()");
                throw;
            }

        }
    }
}
