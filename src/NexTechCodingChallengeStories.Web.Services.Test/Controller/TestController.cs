using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using NexTechCodingChallengeStories.Web.Controllers;
using NexTechCodingChallengeStories.Web.Services.CacheService;
using NexTechCodingChallengeStories.Web.Services.DataServices;
using NexTechCodingChallengeStories.Web.Services.Model;
using NexTechCodingChallengeStories.Web.Services.Interfaces;
using NexTechCodingChallengeStories.Web.Services.Repository;
using NexTechCodingChallengeStories.Web.Services.Test.Fixtures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NexTechCodingChallengeStories.Web.Services.Test.Controller
{
    public  class TestController
    {
        [Fact]
        public async Task Get_OnNoStoryFound_Return404()
        {
            // Arrange
            var mockLogger = new Mock<ILogger<StoriesController>>();
            var storyDataProvider = new StoryDataProvider();

            //mockStoryDataProvider
            //    .Setup(Service => Service.GetOnePageStoriesAsync(1, 10))
            //    .ReturnsAsync(new List<StoryTitle>());

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
