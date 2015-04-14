using Newtonsoft.Json;

namespace Core
{
    internal class Yields
    {
        [JsonProperty("symbol")]
        public string Symbol { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("list")]
        public Yield[] List { get; set; }
    }

    internal class Yield
    {
        [JsonProperty("time")]
        public object Time { get; set; }

        [JsonProperty("date")]
        public string Date { get; set; }

        [JsonProperty("value")]
        public double Value { get; set; }

        [JsonProperty("percent")]
        public double Percent { get; set; }
    }
}
