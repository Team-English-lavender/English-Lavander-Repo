using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chat.Data.Repositories
{
    using ExportModels;
    using Model;

    public interface IMessagesRepository : IRepository<Message>
    {
        IQueryable<MessagesExportModel> GetAll();

        IQueryable<MessagesExportModel> GetAllByGroupId(int groupId);

        IQueryable<MessagesExportModel> GetLastByGroup(int groupId, int? count, int lastMessagesCount);
    }
}
