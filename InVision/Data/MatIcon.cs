using System.Text.Json.Serialization;

namespace InVision.Data
{
	public class MatIcon
	{
		[JsonPropertyName("name")]
		public string Name { get; set; }

		[JsonPropertyName("version")]
		public int Version { get; set; }

		[JsonPropertyName("popularity")]
		public int Popularity { get; set; }

		[JsonPropertyName("codepoint")]
		public int Codepoint { get; set; }

		[JsonPropertyName("unsupported_families")]
		public List<string> UnsupportedFamilies { get; set; }

		[JsonPropertyName("categories")]
		public List<string> Categories { get; set; }

		[JsonPropertyName("tags")]
		public List<string> Tags { get; set; }

		[JsonPropertyName("sizes_px")]
		public List<int> SizesPx { get; set; }
	}
}
