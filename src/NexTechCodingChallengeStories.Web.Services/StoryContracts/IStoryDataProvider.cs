using NexTechCodingChallengeStories.Web.Services.CacheService;
using NexTechCodingChallengeStories.Web.Services.HttpService;
using NexTechCodingChallengeStories.Web.Services.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace NexTechCodingChallengeStories.Web.Services.StoryContracts
{ 
    public interface IStoryDataProvider
    {
        Task<Story> GetOneStoryByIdAsync(int id);
        Task<List<int>> GetAllStoriesIdsAsync();
        Task<List<StoryTitle>> GetOnePageStoriesAsync(int page, int pageSize);
        Task<List<StoryTitle>> GetStoriesFullSearchAsync(string searchText);
        Task<int> GetStoriesCountAsync();
    }
}
