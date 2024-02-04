using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
namespace InVision_API.Models
{

    public class User
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public List<Note> Notes { get; set; } 
        public List<KBoard> KBoards { get; set; } 

		public byte[] salt { get; set; } //Für die berechnung des hash passwortes, bzw. den Login

        public User(List<Note>? notes, List<KBoard>? kBoards)
        {
            // Initialize lists as empty arrays in the constructor
            Notes = new List<Note>();
            KBoards = new List<KBoard>();
        }
    }
}
