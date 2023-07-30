using CodingChallengeStories.Web.Services.Model;
using CodingChallengeStories.Web.Services.HttpServices;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using CodingChallengeStories.Web.Services.CacheService;
using System.Linq.Expressions;
using Microsoft.Extensions.Logging;

namespace CodingChallengeStories.Web.Services.DataProvider
{ 
    public class StoryDataProvider : IStoryDataProvider
    {
        private const string _baseUrl = "https://hacker-news.firebaseio.com";
        ILogger<StoryDataProvider> _logger;
        private IHttpService _httpService;
        private ICachedData _cachedDataService;
        private CacheInfo _cacheInfo;

        public StoryDataProvider() { }  // UNIT TEST usage

        public StoryDataProvider(ILogger<StoryDataProvider> logger, IHttpService httpService, ICachedData cachedDataService, CacheInfo cacheInfo)
        {
            _logger = logger;
            _httpService = httpService;
            _cachedDataService = cachedDataService;
            _cacheInfo = cacheInfo;
        }

        public async Task<Story> GetOneStoryByIdAsync(int id)
        {
            try
            {
                string url = _baseUrl + $"/v0/item/{id}.json?print=pretty";
                var item = await _httpService.HttpGetGeneric<Story>(url);
                return item;
            }   
            catch (Exception e)
            {
                _logger.LogError(e, $"Error while GetOneStoryByIdAsync id = {id}");
                return null;
             }
        }

        public async Task<List<int>> GetAllStoriesIdsAsync()
        {
            try
            { 
                string url = _baseUrl + $"/v0/newstories.json?print=pretty";
                var newStories = await _httpService.HttpGetGeneric<List<int>>(url);
                return newStories;
            }
            catch (HttpSeviceException e)
            {
                _logger.LogError(e, "DataSeviceException : Http Error while GetAllStoriesIdsAsync()");
                throw;
            }
            catch (Exception e)
            {
                _logger.LogError(e, "No HTTP Error while GetAllStoriesIdsAsync()");
                throw;
            }
        }
        public async Task<List<StoryTitle>> GetOnePageStoriesAsync(int page, int pageSize)
        {
            try
            {
                var cachedDataOnePage =  _cachedDataService.GetCachedStoresOnePage(page, pageSize);
                if (cachedDataOnePage !=null && cachedDataOnePage.Count > 0)
                    return cachedDataOnePage; // cached data exist, use it.
                else // no cached one page or expired
                {
                    List<int> cachedDataAllIds = _cachedDataService.GetCachedStoresAllIds();
                    if (cachedDataAllIds.Count == 0) // 1st time or expired
                    {
                        cachedDataAllIds = await GetAllStoriesIdsAsync();
                        _cachedDataService.SetCachedDataAllIds(cachedDataAllIds);
                    }
                    // take one page IDs
                    var selectedPage = cachedDataAllIds.Skip((page - 1) * pageSize).Take(pageSize);

                    // loop one page ids and filter out bad url ones
                    // Issue : it may have less than one page size items because of bad urls ingored;
                    List<StoryTitle> stories = new List<StoryTitle>();  //return list :  title + url + id + time
                    foreach (int storyId in selectedPage)
                    {
                        if (!_cachedDataService.NotBadUrlId(storyId))   // make sure it is not one of the bad ones
                            continue;

                        var story = await GetOneStoryByIdAsync(storyId);
                        if (story != null )
                        {
                            if (Uri.IsWellFormedUriString(story.Url, UriKind.RelativeOrAbsolute)) // ignore bad URL ones
                            {
                                stories.Add(new StoryTitle(story));
                            }
                            else // remove this bad one from the list, so it will not be checked
                            {
                                _cachedDataService.RemoveOneStory(storyId);
                            }
                        }
                    }
                    // try to save it for cache if correct page number and page size
                        _cachedDataService.SetCachedDataOnePage(stories, page, pageSize);

                    return stories;
                }
            }
            catch (HttpSeviceException e)
            {
                _logger.LogError(e, "DataSeviceException : Http Error while GetOnePageStoriesAsync()");
                throw;
            }
            catch (Exception e)
            {
                _logger.LogError(e, "No HTTP Error while GetOnePageStoriesAsync()");
                throw;
            }
        }
        public async Task<List<StoryTitle>> GetStoriesFullSearchAsync(string searchText)
        {
            try
            {
                List<int> cachedDataAllIds = _cachedDataService.GetCachedStoresAllIds();
                if (cachedDataAllIds.Count == 0) // 1st time or expired
                {
                    cachedDataAllIds = await GetAllStoriesIdsAsync();
                    _cachedDataService.SetCachedDataAllIds(cachedDataAllIds);
                }

                // loop all ids and filter by searchText and ignore bad url ones
                List<StoryTitle> stories = new List<StoryTitle>();  //return list :  title + url + id + time
                foreach (int storyId in cachedDataAllIds)
                {
                    if (!_cachedDataService.NotBadUrlId(storyId))
                        continue;

                    var story = await GetOneStoryByIdAsync(storyId);
                    if (story != null)
                    {
                        if (Uri.IsWellFormedUriString(story.Url, UriKind.RelativeOrAbsolute)) {  // ignore bad URL ones)
                            if (story.Title.ToLower().Contains(searchText.ToLower()))
                                stories.Add(new StoryTitle(story));
                        }
                        else // remove this bad URL one from the Id list, so it will not be checked next time
                        {
                            _cachedDataService.RemoveOneStory(storyId);
                        }
                    }
                }
                return stories;
            }
            catch (HttpSeviceException e)
            {
                _logger.LogError(e, "Http Error while GetStoriesFullSearchAsync()");
                return null;
            }
            catch (Exception e)
            {
                _logger.LogError(e, "No HTTP Error while GetStoriesFullSearchAsync()");
                throw;
            }
        }
        public async Task<int> GetStoriesCountAsync()
        {
            try
            {
                string url = _baseUrl +  $"/v0/newstories.json?print=pretty";

                var newStoryIds = await _httpService.HttpGetGeneric<List<int>>(url);
                if (newStoryIds != null && newStoryIds.Count > 0)
                {
                    _cachedDataService.SetCachedDataAllIds(newStoryIds); // save for cache
                    return newStoryIds.Count;
                }
                return 0;
            }
            catch (HttpSeviceException e)
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
        public CacheInfo GetCacheInfo()
        {
            return  _cacheInfo;
        }
    }
}
