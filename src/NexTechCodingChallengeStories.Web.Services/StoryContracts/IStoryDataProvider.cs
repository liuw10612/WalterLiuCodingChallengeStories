using NexTechCodingChallengeStories.Web.Services.CacheService;
using NexTechCodingChallengeStories.Web.Services.DataServices;
using NexTechCodingChallengeStories.Web.Services.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace NexTechCodingChallengeStories.Web.Services.StoryContracts
{ 
    public interface IStoryDataProvider
    {
        Task<Story> GetByIdAsync(int id);
        Task<List<int>> GetNewStoriesAsync();
        Task<List<StoryTitle>> GetOnePageStoriesAsync(int page, int pageSize);
        Task<List<StoryTitle>> GetOnePageFullSearchStoriesAsync(string searchText);
        Task<int> GetStoriesCountAsync();
    }
}
