using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace OfficeApiWithMongoDB.Models
{
    public class Employee
    {
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; } = ObjectId.GenerateNewId().ToString();
        public string Name { get; set; } = string.Empty;
        public DateTime Birthdate { get; set; }
        public string CardNumber { get; set; } = string.Empty;
        public decimal Salary { get; set; } = 0;
        public bool IsPermanent { get; set; }
        public byte[] Photo { get; set; } = default!;

    }
}
