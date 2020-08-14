using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Listapp.Models
{
    public class User
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        [BsonElement("username")]
        public string Username { get; set; }
        [BsonElement("Email")]
        public string email { get; set; }
        [BsonElement("password")]
        public string Password { get; set; }
    }
}
