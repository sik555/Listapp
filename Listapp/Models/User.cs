using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Driver;

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

    public class UserValidator: AbstractValidator<User>
    {

        public UserValidator()
        {

            RuleFor(x => x.Username).Length(0, 50);
            RuleFor(x => x.Username).NotNull();
            RuleFor(x => x.email).EmailAddress();
            RuleFor(x => x.email).NotNull();
            RuleFor(x => x.Password).NotNull();
            RuleFor(x => x.Password).Length(6, 18);
        }



    }
}
