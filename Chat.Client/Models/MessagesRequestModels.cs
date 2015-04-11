using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Chat.Client.Models
{
    using System.ComponentModel.DataAnnotations;

    public class MessagesImportModel
    {
        [Required]
        [StringLength(Int32.MaxValue, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 1)]
        [DataType(DataType.Text)]
        [Display(Name = "Text")]
        public string Text { get; set; }

        [Required]
        public int GroupId { get; set; }
    }

    public class MessagesExportModel
    {
        public int Id { get; set; }

        public DateTime Time { get; set; }

        public string MessageText { get; set; }

        public string UserId { get; set; }

        public string UserName { get; set; }

        public int GroupId { get; set; }

        public string GroupName { get; set; }
    }
}