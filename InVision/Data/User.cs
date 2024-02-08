using InVision.Pages;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace InVision.Data
{
	public class User
	{
		[BsonId]
		[BsonRepresentation(BsonType.ObjectId)]
		public string? Id { get; set; }
		public string Name { get; set; }
		public string Password { get; set; }
		public string Email { get; set; }
		public List<Note>? Notes { get; set; } 
		public List<KBoard>? KBoards { get; set; } 
		public byte[]? ProfilePicture { get; set; }
		public byte[] salt { get; set; }

		public User() 
		{

		}
        public User(string name, string password, string email)
        {
            Name = name;
            Password = password;
            Email = email;

        }

		
	}
}
