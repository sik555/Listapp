using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using FluentValidation;

namespace Listapp.Models
{
    [BsonIgnoreExtraElements]
    public class List
    {
        [BsonId]
        public string Id { get; set; }

        [BsonElement("Title")]
        public string Title { get; set; }

        [BsonElement("Description")]
        public string Description { get; set; }

        [BsonElement("Image")]
        public string Image { get; set; }

        [BsonElement("Items")]
        public List<Item> Items { get; set; }

        [BsonElement("Owner")]
        public User Owner { get; set; }
    }

    public class ListValidator : AbstractValidator<List>
    {

        public ListValidator()
        {

            RuleFor(x => x.Title).Length(0, 50);
            RuleFor(x => x.Title).NotNull();
            RuleFor(x => x.Description).NotNull();
            RuleFor(x => x.Owner).NotNull();
        }



    }
}
