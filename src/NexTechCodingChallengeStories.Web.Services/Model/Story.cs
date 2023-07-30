using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace CodingChallengeStories.Web.Services.Model
{
    public class Story 
    {
        public int Id { get; set; }
        public bool Deleted { get; set; }
        public string Type { get; set; } = default!;
        public string By { get; set; } = default!;
        public decimal Time { get; set; }
        public string Text { get; set; } = default!;
        public bool Dead { get; set; }
        public int Parent { get; set; }
        public int Poll { get; set; }
        public int[] Kids { get; set; } = new int[0];
        public string Url { get; set; } = default!;
        public int Score { get; set; }
        public string Title { get; set; } = default!;
        public int[] Parts { get; set; } = new int[0];
        public int Descendants { get; set; }
    }
    public class StoryTitle  
    {
        public StoryTitle()
        {
        }
        public StoryTitle(Story story)
        {
            Id = story.Id;
            Time = UnixTimestampToDateTime((double)story.Time);
            Title = story.Title;
            Url = story.Url;
        }
        public StoryTitle(int id, decimal time, string title, string url)
        {
            Id = id;
            Time = UnixTimestampToDateTime((double)time);
            Title = title;
            Url = url;
        }
        [JsonProperty("id")]
        public int Id { get; set; }
        [JsonProperty("time")]
        public DateTime Time { get; set; }
        [JsonProperty("title")]
        public string Title { get; set; }
        [JsonProperty("url")]
        public string Url { get; set; }

        /// <summary>
        /// Convert UNIX time to local time
        /// </summary>
        /// <param name="unixTime"></param>
        /// <returns></returns>
        private DateTime UnixTimestampToDateTime(double unixTime)
        {
            DateTime unixStart = new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc);
            long unixTimeStampInTicks = (long)(unixTime * TimeSpan.TicksPerSecond);
            return new DateTime(unixStart.Ticks + unixTimeStampInTicks, System.DateTimeKind.Utc);
        }
    }
}
