using System;
using System.Collections.Generic;
using BorrowIt.Common.Mongo.Attributes;
using BorrowIt.Common.Mongo.Models;
using MongoDB.Bson.Serialization.Attributes;

namespace BorrowIt.Auth.Infrastructure.Entities.Users
{
    [MongoEntity("Users")]
    public class UserEntity : MongoEntity
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public IEnumerable<Guid> Roles { get; set; }
        public string FirstName { get; set; }
        public string SecondName { get; set; }
        public DateTime BirthDate { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime? ModifyDate { get; set; }
        public string AddressCity { get; set; }
        public string AddressStreet { get; set; }
        public string AddressPostCode { get; set; }
    }
}