using NexTechCodingChallengeStories.Web.Services.Entities;
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
                new StoryTitle {
                    id =1,
                    time = (decimal)12345678,
                    title = "title1",
                    url = "https://url1@msn.com/story1"
                },
                 new StoryTitle {
                    id =2,
                    time = (decimal)12345678,
                    title = "title2",
                    url = "https://url1@msn.com/story2"
                },
                  new StoryTitle {
                    id =3,
                    time = (decimal)12345678,
                    title = "title3",
                    url = "https://url1@msn.com/story3"
                },
                   new StoryTitle {
                    id =4,
                    time = (decimal)12345678,
                    title = "title4",
                    url = "https://url1@msn.com/story4"
                },
                   new StoryTitle {
                    id =5,
                    time = (decimal)12345678,
                    title = "title5",
                    url = "https://url1@msn.com/story5"
                },
            };

        public static List<int> GetTestIds() => new() { 9129911, 9129199, 9127761, 9128141, 9128264, 9127792, 9129248, 9127092, 9128367, 9038733 };
    }
}
