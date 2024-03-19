using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System.Text.Json.Serialization;

namespace InVision_API.Models
{
    public class Note
    {
        
        public string? Id { get; set; }
        public string Title { get; set; }
        public string? Content { get; set; }
        public string? Icon { get; set; }

        public Note()
        {
            Id = Guid.NewGuid().ToString();
        }
    }
}
