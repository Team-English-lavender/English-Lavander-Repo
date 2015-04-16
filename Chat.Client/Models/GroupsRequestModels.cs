using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Chat.Client.Models
{
    using System.ComponentModel.DataAnnotations;

    public class GroupsExportModel
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 3)]
        [DataType(DataType.Text)]
        [Display(Name = "Name")]
        public string Name { get; set; }

        public bool IsPublic { get; set; }

        public virtual IEnumerable<UsersExportModel> Users { get; set; }
    }

    public class GroupAddUserImportModel
    {
        public string UserId { get; set; }

        public int GroupId { get; set; }
    }

    public class GropuTransModel
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }
}