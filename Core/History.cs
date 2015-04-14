using Newtonsoft.Json;

namespace Core
{
    public class History
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("status")]
        public string Status { get; set; }

        [JsonProperty("cube_id")]
        public int CubeId { get; set; }

        [JsonProperty("prev_bebalancing_id")]
        public int PrevBebalancingId { get; set; }

        [JsonProperty("category")]
        public string Category { get; set; }

        [JsonProperty("exe_strategy")]
        public string ExeStrategy { get; set; }

        [JsonProperty("created_at")]
        public object CreatedAt { get; set; }

        [JsonProperty("updated_at")]
        public long UpdatedAt { get; set; }

        [JsonProperty("cash")]
        public double Cash { get; set; }

        [JsonProperty("error_code")]
        public object ErrorCode { get; set; }

        [JsonProperty("error_message")]
        public string ErrorMessage { get; set; }

        [JsonProperty("error_status")]
        public object ErrorStatus { get; set; }

        [JsonProperty("holdings")]
        public object Holdings { get; set; }

        [JsonProperty("rebalancing_histories")]
        public RebalancingHistory[] RebalancingHistories { get; set; }
    }
}
