using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;


namespace InVision_API.Models
{
	public class KBoard
	{

		[BsonId]
		[BsonRepresentation(BsonType.ObjectId)]
		public string Id { get; set; }
		public string Name { get; set; }
		public string Description { get; set; }
		public string CreatedBy { get; set; }
		//public List<TodoItem> Items { get; set; } = new List<TodoItem>();
	}
}
