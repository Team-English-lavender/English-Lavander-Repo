using Chat.Model;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace Chat.Data
{
    public class Group
    {
        // MongoDb Id Property
        //[BsonId]
        //public ObjectId Id { get; set; }

        public Group()
        {
            this.Users = new HashSet<User>();
            this.Messages = new HashSet<Message>();
        }

        [Key]
        public int Id { get; set; }

        [StringLength(100, MinimumLength = 1)]
        public string Name { get; set; }

        public bool IsPublic { get; set; }

        public virtual ICollection<User> Users { get; set; }

        public virtual ICollection<Message> Messages { get; set; }
    }
}
