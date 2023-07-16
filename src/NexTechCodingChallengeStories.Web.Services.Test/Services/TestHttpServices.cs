using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NexTechCodingChallengeStories.Web.Services.HttpServices;
using NexTechCodingChallengeStories.Web.Services.Model;
using NexTechCodingChallengeStories.Web.Services.Test.Fixtures;

namespace NexTechCodingChallengeStories.Web.Services.Test.Services
{
    public class TestHttpService
    {
        [Fact]
        public async Task Get_On_Throw_Exception_HttpGetGeneric()
        {
            // Arrange
            var sut = new HttpService();

            // Act
            string trashUrl = "trash.com";

            // Assert
            await Assert.ThrowsAsync<HttpSeviceException>(async () => await sut.HttpGetGeneric<Story>(trashUrl));
            await Assert.ThrowsAsync<HttpSeviceException>(async () => await sut.HttpGetGeneric<List<int>>(trashUrl));
        }

        [Fact]
        public async Task Get_On_Success_HttpGetGeneric()
        {
            // Arrange
            var sut = new HttpService();

            // Act1 : Get all the story ids
            string url1 = "https://hacker-news.firebaseio.com/v0/newstories.json?print=pretty";
            var result1 = await sut.HttpGetGeneric<List<int>>(url1);
            // Act2 : retrieve one story 
            string oneGoodStoryId = "36702537";
            string url2 = $"https://hacker-news.firebaseio.com/v0/item/{oneGoodStoryId}.json?print=pretty";
            var result2 = await sut.HttpGetGeneric<StoryTitle>(url2);

            // Assert1 : stories count > 100
            bool expected = true;
            bool actual1 = result1.Count > 100;
            Assert.Equal(expected, actual1);
            // Asser2 : good tory is not null
            bool expected2 =false;
            bool actual2 = result2 is null;
            Assert.Equal(expected2, actual2);
        }
    }
}
