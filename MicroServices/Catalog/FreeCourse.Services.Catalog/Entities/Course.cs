using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace FreeCourse.Services.Catalog.Entities
{
    public class Course : BaseEntity
    {
        [BsonRepresentation(BsonType.Decimal128)]
        public decimal Price { get; set; }

        public string Picture { get; set; }
        public string Description { get; set; }

        [BsonRepresentation(BsonType.DateTime)]
        public DateTime CreatedTime { get; set; }


        [BsonRepresentation(BsonType.ObjectId)]
        public string CategoryId { get; set; }

        [BsonIgnore]
        public Category Category { get; set; }

        public string UserId { get; set; }

        public Feature Feature { get; set; }
    }
}
