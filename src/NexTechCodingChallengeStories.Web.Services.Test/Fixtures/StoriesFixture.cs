using NexTechCodingChallengeStories.Web.Services.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace NexTechCodingChallengeStories.Web.Services.Test.Fixtures
{
    public static class StoriesFixture
    {
        public static List<StoryTitle> GetTestStories() => new()  {
            new StoryTitle(1, (decimal)12345678,  "title1", "https://url1@msn.com/story1"),
            new StoryTitle(2, (decimal)12345678,  "title2", "https://url1@msn.com/story2"),
            new StoryTitle(3, (decimal)12345678,  "title3", "https://url1@msn.com/story3"),
            new StoryTitle(4, (decimal)12345678,  "title4", "https://url1@msn.com/story4"),
            new StoryTitle(5, (decimal)12345678,  "title5", "https://url1@msn.com/story5"),
            new StoryTitle(6, (decimal)12345678,  "title1", "https://url1@msn.com/story6"),
            new StoryTitle(7, (decimal)12345678,  "title1", "https://url1@msn.com/story7"),
            new StoryTitle(8, (decimal)12345678,  "title1", "https://url1@msn.com/story8"),
            new StoryTitle(9, (decimal)12345678,  "title1", "https://url1@msn.com/story9"),
        };
        public static List<int> GetTestIds() => new() { 9129911, 9129199, 9127761, 9128141, 9128264, 9127792, 9129248, 9127092, 9128367, 9038733 };
    }
}
