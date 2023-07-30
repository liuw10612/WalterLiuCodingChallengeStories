using Moq;
using CodingChallengeStories.Web.Services.CacheService;
using CodingChallengeStories.Web.Services.Test.Fixtures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace CodingChallengeStories.Web.Services.Test.Services
{
    public class TestCacheService
    {
        [Fact]
        public void Get_On_Success_GetCachedStoresOnePage() {
            // Arrange
            var sut = new CachedDataService(3, 10, 2);  // cache for top 3 pages for page size=10, cache expire after 2 hours

            // Act
            var testData = StoriesFixture.GetTestStories();
            sut.SetCachedDataOnePage(testData, 1, 10);
            var expectedResult = sut.GetCachedStoresOnePage(1, 10);

            // Assert
            Assert.Equal(testData.Count, expectedResult!.Count);
        }

        [Fact]
        public void Get_On_Null_GetCachedStoresOnePage()
        {
            // Arrange
            var sut = new CachedDataService();

            // Act
            var testData = StoriesFixture.GetTestStories();
            sut.SetCachedDataOnePage(testData, 1, 10);
            var result1 = sut.GetCachedStoresOnePage(2, 10);        // wrong page number
            var result2 = sut.GetCachedStoresOnePage(1, 20);        // wrong page size

            // Assert
            Assert.Null(result1);
            Assert.Null(result2);
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
            Assert.Equal(testDataAllIds.Count, result.Count);
        }
        [Fact]
        public void Get_On_Zero_GetCachedStoresAllIds()
        {
            // Arrange
            var sut = new CachedDataService();

            // Act
            var result = sut.GetCachedStoresAllIds();

            // Assert
            Assert.Empty(result);
        }
    }
}
