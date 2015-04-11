using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Chat.Client.Models
{
    public class MessagesExportModel
    {
        public int Id { get; set; }

        public DateTime Time { get; set; }

        public string MessageText { get; set; }

        public string UserId { get; set; }

        public int GroupId { get; set; }
    }
}