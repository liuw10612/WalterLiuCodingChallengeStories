using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace CodingChallengeStories.Web.Services.Model
{
    public class CacheInfo
    {
        public CacheInfo(int cachePages, int cachePageSize, int cacheExpireHours)
        {
            CachePages = cachePages;
            CachePageSize = cachePageSize;
            CacheExpireHours = cacheExpireHours;
        }
        [JsonProperty("cachePages")]
        public int CachePages { get; set; }

        [JsonProperty("cachePageSize")]
        public int CachePageSize { get; set; }

        [JsonProperty("cacheExpireHours")]
        public int CacheExpireHours { get; set; }
    }
}
