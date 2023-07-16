using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using NexTechCodingChallengeStories.Web.Services.CacheService;
using NexTechCodingChallengeStories.Web.Services.HttpServices;
using NexTechCodingChallengeStories.Web.Services.Model;
using NexTechCodingChallengeStories.Web.Services.StoryContracts;
using NexTechCodingChallengeStories.Web.Services.Test.Fixtures;

namespace NexTechCodingChallengeStories.Web.Services.Test.StoryContracts
{
    public class TestStoryContracts
    {
        [Fact]
        public async Task Get_OnSuccess_InvokeGetStoriesCountAsyncOnce( )
        {
            // Arrange
            const string url = "https://hacker-news.firebaseio.com/v0/newstories.json?print=pretty";

            var mockLogger = new Mock<ILogger<StoryDataProvider>>();
            var httpService = new HttpService();
            var mockCachedDataService = new Mock<ICachedData>();

            var mockDataService = new Mock<IHttpService> { DefaultValue = DefaultValue.Mock, };

            var sut = new StoryDataProvider(mockLogger.Object, mockDataService.Object, mockCachedDataService.Object);

            // Act
            var result = await sut.GetStoriesCountAsync();

            // Assert
            mockDataService.Verify(
                service => service.HttpGetGeneric<It.IsAnyType>(url),
                Times.Once()
            );
        }

        [Fact]
        public async Task GetAllStoriesIdsAsync_WhenCalled_NotNullReturned()
        {
            // Arrange
            var mockLogger = new Mock<ILogger<StoryDataProvider>>();
            var mockCachedDataService = new Mock<ICachedData>();
            var mockDataService = new Mock<IHttpService> { DefaultValue = DefaultValue.Mock, };

            var sut = new StoryDataProvider(mockLogger.Object, mockDataService.Object, mockCachedDataService.Object);
            // Act
            var result =await sut.GetAllStoriesIdsAsync();

            // assert
            Assert.NotNull(result);
        }
    }
}
