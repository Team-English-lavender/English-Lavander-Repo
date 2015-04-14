using Chat.Data.Repositories;
using Chat.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Chat.Data
{
    public interface IChatData
    {
        IUserRepository Users { get; }

        IRepository<Group> Groups { get; }

        IMessagesRepository Messages { get; }

        IRepository<Friend> Friends { get; } 

        int SaveChanges();
    }
}
