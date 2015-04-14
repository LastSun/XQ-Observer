using Newtonsoft.Json;

namespace Core
{
    public class Histories
    {
        [JsonProperty("list")]
        public History[] List { get; set; }

        [JsonProperty("maxPage")]
        public int MaxPage { get; set; }

        [JsonProperty("count")]
        public int Count { get; set; }

        [JsonProperty("page")]
        public int Page { get; set; }

        [JsonProperty("totalCount")]
        public int TotalCount { get; set; }
    }
}
