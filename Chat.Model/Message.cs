using Chat.Data;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chat.Model
{
    public class Message
    {
        //[BsonId]
        //public ObjectId Id { get; set; }
        [Key]
        public int Id { get; set; }

        public DateTime Time { get; set; }

        public string MessageText { get; set; }

        public string UserId { get; set; }

        public int GroupId { get; set; }

        public virtual User User { get; set; }

        public virtual Group Group { get; set; }
    }
}
