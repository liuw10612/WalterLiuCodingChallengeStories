using NexTechCodingChallengeStories.Web.Services.CacheService;
using NexTechCodingChallengeStories.Web.Services.DataServices;
using NexTechCodingChallengeStories.Web.Services.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace NexTechCodingChallengeStories.Web.Services.Interfaces
{
    public interface IStoryRepository 
    {
        Task<Story> GetByIdAsync(int id);

        Task<List<int>> GetNewStoriesAsync();
        Task<List<StoryTitle>> GetOnePageStoriesAsync(int page, int pageSize);
        Task<List<StoryTitle>> GetOnePageFullSearchStoriesAsync(string searchText);
        Task<int> GetStoriesCountAsync();
    }
}
