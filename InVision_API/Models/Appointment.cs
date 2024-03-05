using System.Text.Json.Serialization;

namespace InVision_API.Models
{
	public class Appointment
	{
		[JsonIgnore]
		public string Id { get; set; }

		public DateTime Start { get; set; }
		public DateTime End { get; set; }
		public string Text { get; set; }
		public string Color { get; set; }


		public Appointment()
		{
			Id = Guid.NewGuid().ToString();
		}
	}
	
}
