using System.Text.Json.Serialization;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Biblio.DAL.Models.Book
{
    public class Book
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        [JsonIgnore]
        public string Id { get; set; } = "";
        public string Author { get; set; }
        public string Genre { get; set; }

        [BsonElement("Name")]
        public string Name { get; set; }
        public int NumberOfPages { get; set; }
        public decimal Price { get; set; }
    }
}