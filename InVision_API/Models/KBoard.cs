using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System;
using MongoDB.Bson.Serialization.IdGenerators;
using Newtonsoft.Json;

namespace InVision_API.Models
{
	public class KBoard
	{
		private static int lastAssignedId = 0;

		/*public KBoard()
		{
			// Increment the last assigned ID and assign it to the current instance
			++lastAssignedId;
			Id = Convert.ToString(lastAssignedId);
		*/
		[JsonIgnore]
		public string Id { get; set; }
		//public string Id { get; set; }
		public string Name { get; set; }
		public string Description { get; set; }
		public string CreatedBy { get; set; }
		[JsonIgnore]
		public List<TodoItem> Items { get; set; } = new List<TodoItem>();

		public KBoard()
		{
			Id = Guid.NewGuid().ToString();
		}
	}
}
