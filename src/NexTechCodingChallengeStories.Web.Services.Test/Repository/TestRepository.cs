using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using NexTechCodingChallengeStories.Web.Services.CacheService;
using NexTechCodingChallengeStories.Web.Services.DataServices;
using NexTechCodingChallengeStories.Web.Services.Repository;
using NexTechCodingChallengeStories.Web.Services.Test.Fixtures;

namespace NexTechCodingChallengeStories.Web.Services.Test.Repository
{
    public class TestRepository
    {
        [Fact]
        public async Task Get_OnSuccess_InvokeGetStoriesCountAsyncOnce()
        {
            // Arrange
            const string baseAPIUrl = "https://hacker-news.firebaseio.com";
            const string newStoriesEndpoint = $"/v0/newstories.json?print=pretty";

            var mockLogger = new Mock<ILogger<StoryRepository>>();
            var mockDataService = new Mock<IDataService>();
            var mockCachedDataService = new Mock<ICachedData>();

            mockDataService
                .Setup(Service => Service.GetAllData(baseAPIUrl, newStoriesEndpoint))
                .ReturnsAsync(StoriesFixture.GetTestIds());

            var sut = new StoryRepository(mockLogger.Object, mockDataService.Object, mockCachedDataService.Object);

            // Act
            var result = await sut.GetStoriesCountAsync();

            // Assert
            mockDataService.Verify(
                service => service.GetAllData(baseAPIUrl, newStoriesEndpoint),
                Times.Once()
            );
        }

        [Fact]
        public async Task Get_OnSuccess_GetNewStoriesAsync()
        {
            // Arrange
            const string baseAPIUrl = "https://hacker-news.firebaseio.com";
            const string newStoriesEndpoint = $"/v0/newstories.json?print=pretty";

            var mockLogger = new Mock<ILogger<StoryRepository>>();
            var mockDataService = new Mock<IDataService>();
            var mockCachedDataService = new Mock<ICachedData>();

            mockDataService
                .Setup(Service => Service.GetAllData(baseAPIUrl, newStoriesEndpoint))
                .ReturnsAsync(StoriesFixture.GetTestIds());

            var sut = new StoryRepository(mockLogger.Object, mockDataService.Object, mockCachedDataService.Object);
            // Act
            var result = await sut.GetNewStoriesAsync();

            // Assert
            Assert.Equal(result.Count, StoriesFixture.GetTestIds().Count);
        }
    }
}
