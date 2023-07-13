using Moq;
using NexTechCodingChallengeStories.Web.Services.CacheService;
using NexTechCodingChallengeStories.Web.Services.Test.Fixtures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace NexTechCodingChallengeStories.Web.Services.Test.Services
{
    public class TestCacheService
    {
        [Fact]
        public void Get_On_Success_GetCachedStoresOnePage() {
            // Arrange
            var sut = new CachedDataService();

            // Act
            var testData = StoriesFixture.GetTestStories();
            sut.SetCachedDataOnePage(testData);
            var result = sut.GetCachedStoresOnePage(1, 10);

            // Assert
            Assert.Equal(result.Count, testData.Count);
        }

        [Fact]
        public void Get_On_Null_GetCachedStoresOnePage()
        {
            // Arrange
            var sut = new CachedDataService();

            // Act
            var testData = StoriesFixture.GetTestStories();
            sut.SetCachedDataOnePage(testData);
            var result1 = sut.GetCachedStoresOnePage(2, 10);        // wrong page number
            var result2 = sut.GetCachedStoresOnePage(1, 20);        // wrong page size

            // Assert
            Assert.Equal(result1, null);
            Assert.Equal(result2, null);
        }

        [Fact]
        public void Get_On_Success_GetCachedStoresAllIds()
        {
            // Arrange
            var sut = new CachedDataService();

            // Act
            var testDataAllIds = StoriesFixture.GetTestIds();
            sut.SetCachedDataAllIds(testDataAllIds);
            var result = sut.GetCachedStoresAllIds();

            // Assert
            Assert.Equal(result.Count, testDataAllIds.Count);
        }
        [Fact]
        public void Get_On_Zero_GetCachedStoresAllIds()
        {
            // Arrange
            var sut = new CachedDataService();

            // Act
            var result = sut.GetCachedStoresAllIds();

            // Assert
            Assert.Equal(result.Count, 0);
        }
    }
}
