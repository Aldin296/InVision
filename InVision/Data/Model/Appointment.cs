using System.Text.Json.Serialization;

namespace InVision.Data
{
    public class Appointment
    {
		[JsonIgnore]
        public string Id { get; set; }

		public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public string Text { get; set; }
        public string Color { get; set; }
        public bool Delete { get; set; } = false;

		public Appointment()
		{
			Id = Guid.NewGuid().ToString();
		}

	}
}
