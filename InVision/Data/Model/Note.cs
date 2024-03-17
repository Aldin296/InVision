using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace InVision.Data.Model
{
    public class Note
    {
        public string? Id { get; set; }
        public string Title { get; set; }
        public string? Content { get; set; }
        public string Icon { get; set; }

        public Note()
        {
            Id = Guid.NewGuid().ToString();
        }
    }
}