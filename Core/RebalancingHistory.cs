using Newtonsoft.Json;

namespace Core
{
    public class RebalancingHistory
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("rebalancing_id")]
        public int RebalancingId { get; set; }

        [JsonProperty("stock_id")]
        public int StockId { get; set; }

        [JsonProperty("stock_name")]
        public string StockName { get; set; }

        [JsonProperty("stock_symbol")]
        public string StockSymbol { get; set; }

        [JsonProperty("volume")]
        public double Volume { get; set; }

        [JsonProperty("price")]
        public double Price { get; set; }

        [JsonProperty("net_value")]
        public double NetValue { get; set; }

        [JsonProperty("weight")]
        public double Weight { get; set; }

        [JsonProperty("target_weight")]
        public double TargetWeight { get; set; }

        [JsonProperty("prev_weight")]
        public double? PrevWeight { get; set; }

        [JsonProperty("prev_target_weight")]
        public double? PrevTargetWeight { get; set; }

        [JsonProperty("prev_weight_adjusted")]
        public double? PrevWeightAdjusted { get; set; }

        [JsonProperty("prev_volume")]
        public double? PrevVolume { get; set; }

        [JsonProperty("prev_price")]
        public double? PrevPrice { get; set; }

        [JsonProperty("prev_net_value")]
        public double? PrevNetValue { get; set; }

        [JsonProperty("proactive")]
        public bool Proactive { get; set; }

        [JsonProperty("created_at")]
        public object CreatedAt { get; set; }

        [JsonProperty("updated_at")]
        public object UpdatedAt { get; set; }
    }
}
