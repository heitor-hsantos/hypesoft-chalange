using MongoDB.Bson.Serialization.Attributes;

namespace Hypesoft.Domain.Entities

    
{
    public class Product
    {
        [BsonId]
        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        public string Id { get; set; }
        
        [BsonElement("name")]
        public string Name { get; set; }
        
        [BsonElement("price")]
        public decimal Price { get; set; }
        
        [BsonElement("description")]
        public string Description { get; set; }
        
        [BsonElement("category")]
        public string Category { get; set; }
        
        [BsonElement("stockQuantity")]
        public int StockQuantity { get; set; }
        
        
        
    }
}