using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Chat.Client.Models
{
    public class GroupsExportModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public virtual IEnumerable<UsersExportModel> Users { get; set; }

        public virtual IEnumerable<MessagesExportModel> Messages { get; set; }
    }
}