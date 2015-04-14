namespace Chat.Data.Repositories
{
    using Chat.Model;
    using System.Linq;
    using ExportModels;

    class MessagesRepository : GenericRepository<Message>, IMessagesRepository
    {
        public MessagesRepository(IChatDbContext context)
            : base (context)
        {
        }

        public IQueryable<MessagesExportModel> GetAll()
        {
            var messages = this.All()
                .Select(m =>
                    new MessagesExportModel()
                    {
                        Id = m.Id,
                        UserId = m.UserId,
                        UserName = m.User.UserName,
                        GroupId = m.GroupId,
                        GroupName = m.Group.Name,
                        Time = m.Time,
                        MessageText = m.MessageText
                    }
                );

            return messages;
        }

        public IQueryable<MessagesExportModel> GetAllByGroupId(int groupId)
        {
            var messages = this.All()
                .Where(m => m.Group.Id == groupId)
                .Select(m =>
                    new MessagesExportModel()
                    {
                        Id = m.Id,
                        MessageText = m.MessageText,
                        Time = m.Time,
                        UserId = m.UserId,
                        UserName = m.User.UserName,
                        GroupId = m.GroupId,
                        GroupName = m.Group.Name
                    }
                );

            return messages;
        }

        public IQueryable<MessagesExportModel> GetLastByGroup(int groupId, int? count, int lastMessagesCount)
        {
            int messagesCount = count ?? lastMessagesCount;

            var messages = this.All()
                .Where(m => m.Group.Id == groupId)
                .OrderByDescending(m => m.Id)
                .Take(messagesCount)
                .Select(m =>
                    new MessagesExportModel()
                    {
                        Id = m.Id,
                        GroupId = m.GroupId,
                        GroupName = m.Group.Name,
                        MessageText = m.MessageText,
                        Time = m.Time,
                        UserId = m.UserId,
                        UserName = m.User.UserName
                    }
                );

            return messages;
        }
    }
}
