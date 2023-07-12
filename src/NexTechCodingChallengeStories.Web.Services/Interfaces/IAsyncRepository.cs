using NexTechCodingChallengeStories.Web.Services.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace NexTechCodingChallengeStories.Web.Services.Interfaces
{
    public interface IAsyncRepository<T> where T : Entity
    {
        Task<T> GetByIdAsync(int id);

        Task<List<int>> GetNewStoriesAsync();
    }
}
