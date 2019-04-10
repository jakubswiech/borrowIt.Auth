using System;
using BorrowIt.Common.Mongo.Attributes;
using BorrowIt.Common.Mongo.Models;
using MongoDB.Bson.Serialization.Attributes;

namespace BorrowIt.Auth.Infrastructure.Entities.Users
{
    [MongoEntity("Roles")]
    public class RoleEntity : IMongoEntity
    {
        [BsonId]
        public Guid Id { get; }
        public string Name { get; set; }
    }
}