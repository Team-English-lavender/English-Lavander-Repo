namespace Chat.Data.ExportModels
{
    using System;

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