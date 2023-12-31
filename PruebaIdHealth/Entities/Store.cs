﻿    using MongoDB.Bson;
    using MongoDB.Bson.Serialization.Attributes;
    using System.Text.Json.Serialization;

    namespace PruebaIdHealth.Entities;

    public class Store
    {

        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        public string? Name { get; set; }

        [BsonElement("categories")]
    [JsonPropertyName("categories")]
        public List<string>? CategoryIds { get; set; }

    }
