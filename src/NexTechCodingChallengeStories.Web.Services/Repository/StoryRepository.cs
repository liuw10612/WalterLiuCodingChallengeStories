using NexTechCodingChallengeStories.Web.Services.Entities;
using NexTechCodingChallengeStories.Web.Services.Interfaces;
using NexTechCodingChallengeStories.Web.Services.DataService;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace NexTechCodingChallengeStories.Web.Services.Repository
{
    public class StoryRepository : IStoryRepository
    {
        private static string _baseAPIUrl = "https://hacker-news.firebaseio.com";
        private IDataService _dataService;

        public StoryRepository(IDataService dataService)
        {
            _dataService = dataService;
        }

        public async Task<Story> GetByIdAsync(int id)
        {
            
            string itemEndpoint = $"/v0/item/{id}.json?print=pretty";

            var item = await _dataService.GetData<Story>(_baseAPIUrl, itemEndpoint);

            return item;
        }

        public async Task<List<int>> GetNewStoriesAsync()
        {
            string newStoriesEndpoint = $"/v0/newstories.json?print=pretty";

            var newStories = await _dataService.GetAllData(_baseAPIUrl, newStoriesEndpoint);

            return newStories;
        }
        public async Task<List<StoryTitle>> GetOnePageStoriesAsync(int page, int pageSize, List<int> cachedDataAllIds)
        {
            try
            {
                // take one page IDs
                var selectedPage = cachedDataAllIds.Skip((page - 1) * pageSize).Take(pageSize);

                // loop one page ids and filter out bad url ones
                // it may have less than one page size items
                List<StoryTitle> stories = new List<StoryTitle>();  //return list :  title + url + id + time
                foreach (int storyId in selectedPage)
                {
                    var story = await GetByIdAsync(storyId);

                    if (story != null && Uri.IsWellFormedUriString(story.url, UriKind.RelativeOrAbsolute)) // ignore bad URL ones
                    {
                        stories.Add(new StoryTitle
                        {
                            id = story.id,
                            time = story.time,
                            title = story.title,
                            url = story.url,
                        });
                    }
                }
                return stories;
            }
            catch (Exception e)
            {
                throw;       
            }  
                    
        }
    }
}
