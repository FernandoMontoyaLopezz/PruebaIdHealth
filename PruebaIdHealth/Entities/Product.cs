    using MongoDB.Bson;
    using MongoDB.Bson.Serialization.Attributes;

    namespace PruebaIdHealth.Entities;

    public class Product
    {

        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }
        public string? Sku { get; set; }
        public string? Name { get; set; }
        public double Price { get; set; }
        public int Amount { get; set; }
        public string? Description { get; set; }
}
