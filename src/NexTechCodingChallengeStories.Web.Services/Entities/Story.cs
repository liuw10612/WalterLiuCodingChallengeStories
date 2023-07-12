using System;
using System.Collections.Generic;
using System.Text;

namespace NexTechCodingChallengeStories.Web.Services.Entities
{
    public class Story : Entity
    {
        public int id { get; set; }

        public bool deleted { get; set; }

        public string type { get; set; }

        public string by { get; set; }

        public decimal time { get; set; }

        public string text { get; set; }

        public bool dead { get; set; }

        public int parent { get; set; }

        public int poll { get; set; }

        public int[] kids { get; set; }

        public string url { get; set; }

        public int score { get; set; }

        public string title { get; set; }

        public int[] parts { get; set; }

        public int descendants { get; set; }

        public Story() { }

        public Story(int _id, string _title, string _url)
        {
            id = _id;
            title = _title;
            url = _url;
        }
    }
    public class StoryTitle  
    {
        public int id { get; set; }
        public decimal time { get; set; }
        public string title { get; set; }
        public string url { get; set; }

    }
}
