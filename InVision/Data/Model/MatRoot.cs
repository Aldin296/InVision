using System.Text.Json.Serialization;

namespace InVision.Data.Model
{
    public class MatRoot
    {
        [JsonPropertyName("host")]
        public string Host { get; set; }

        [JsonPropertyName("asset_url_pattern")]
        public string AssetUrlPattern { get; set; }

        [JsonPropertyName("families")]
        public List<string> Families { get; set; }

        [JsonPropertyName("icons")]
        public List<MatIcon> Icons { get; set; }
    }
}
