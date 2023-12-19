﻿using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace InVision.Data
{
	public class Note
	{
		[BsonId]
		[BsonRepresentation(BsonType.ObjectId)]
		public string? Id { get; set; }
		public string Title { get; set; }
		public string Content { get; set; }
		public bool IsDone { get; set; }
	}
}