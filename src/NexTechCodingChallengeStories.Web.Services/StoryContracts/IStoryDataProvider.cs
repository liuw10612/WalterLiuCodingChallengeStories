using NexTechCodingChallengeStories.Web.Services.Model;

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
