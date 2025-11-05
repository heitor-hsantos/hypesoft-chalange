using MongoDB.Bson.Serialization.Attributes;

namespace Hypesoft.Domain.Entities

    
{
    public class Product
    {
        [BsonId]
        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        public required string Id { get; set; }
        
        [BsonElement("name")]
        public required string Name { get; set; }
        
        [BsonElement("price")]
        public required decimal Price { get; set; }
        
        [BsonElement("description")]
        public string Description { get; set; }
        
        [BsonElement("category")]
        public required string Category { get; set; }
        
        [BsonElement("stockQuantity")]
       public required int StockQuantity { get; set; }
        
        
    }
}