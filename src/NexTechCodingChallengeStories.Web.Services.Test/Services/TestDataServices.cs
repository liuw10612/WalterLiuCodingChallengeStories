using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NexTechCodingChallengeStories.Web.Services.HttpService;
using NexTechCodingChallengeStories.Web.Services.Model;
using NexTechCodingChallengeStories.Web.Services.Test.Fixtures;

namespace NexTechCodingChallengeStories.Web.Services.Test.Services
{
    public class TestHttpService
    {
        [Fact]
        public async Task Get_On_Throw_Exception_GetData()
        {
            // Arrange
            var sut = new HttpService();

            // Act
            string baseUrl = "trash";
            string endPointUrl = "trash";

            // Assert
            Assert.ThrowsAsync<HttpSeviceException>(async () => await sut.GetData<Story>(baseUrl, endPointUrl));
            Assert.ThrowsAsync<HttpSeviceException>(async () => await sut.GetAllData(baseUrl, endPointUrl));
        }

        [Fact]
        public async Task Get_On_Success_GetAllData()
        {
            // Arrange
            var sut = new HttpService();

            // Act
            string baseUrl = "https://hacker-news.firebaseio.com";
            string endPointUrl = $"/v0/newstories.json?print=pretty";
            var result = await sut.GetAllData(baseUrl, endPointUrl);

            // Assert
            Assert.Equal(result.Count>100, true);
        }

        [Fact]
        public async Task Get_On_Success_GetData()
        {
            // Arrange
            var sut = new HttpService();

            // Act
            string baseUrl = "https://hacker-news.firebaseio.com";
            string endPointUrl = "/v0/item/36702537.json?print=pretty";
            var result = await sut.GetData<Story>(baseUrl, endPointUrl);

            // Assert
            Assert.Equal(result is null, false);

        }
    }
}
