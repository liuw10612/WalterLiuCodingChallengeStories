using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using CodingChallengeStories.Web.Controllers;
using CodingChallengeStories.Web.Services.CacheService;
using CodingChallengeStories.Web.Services.HttpServices;
using CodingChallengeStories.Web.Services.Model;
using CodingChallengeStories.Web.Services.DataProvider;
using CodingChallengeStories.Web.Services.Test.Fixtures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodingChallengeStories.Web.Services.Test.Controller
{
    public  class TestController
    {
        [Fact]
        public async Task Get_OnSuccess_GetStoriesCount_ReturnsStatusCode200()
        {
            // Arrange1 : setup StoryDataProvider with DI parameters
            var mockLogger1 = new Mock<ILogger<StoryDataProvider>>();
            var mockCachedDataService = new Mock<ICachedData>();
            var mockDataService = new Mock<IHttpService> { DefaultValue = DefaultValue.Mock, };
            var storyDataProvider = new StoryDataProvider(mockLogger1.Object, mockDataService.Object, mockCachedDataService.Object);

            // Arrange
            var mockLogger = new Mock<ILogger<StoriesController>>();
            var sut = new StoriesController(mockLogger.Object, storyDataProvider);

            // Act
            var result = (OkObjectResult)await sut.GetStoriesCount();
            //var result = await sut.OnePage(1, 10);    // it will throw, since mockCachedDataService is moc

            // Assert
            result.StatusCode.Should().Be(200);
        }

        [Fact]
        public async Task Get_On_NoStoryFound_Return404()
        {
            // Arrange
            var mockLogger = new Mock<ILogger<StoriesController>>();
            var storyDataProvider = new StoryDataProvider(); // default empty constructor, no DI parameters
            var sut = new StoriesController(mockLogger.Object, storyDataProvider);

            // Act
            var result = await sut.OnePage(1, 10);

            // Assert
            result.Should().BeOfType<NotFoundResult>();
            var objectResult = (NotFoundResult)result;
            objectResult.StatusCode.Should().Be(404);
        }

    }
}
