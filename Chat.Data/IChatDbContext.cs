using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chat.Data
{
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Runtime.Remoting;

    using Chat.Model;
    using Chat.Data.Repositories;

    public interface IChatDbContext
    {
        IDbSet<User> Users { get; set; }

        IDbSet<Message> Messages { get; set; }

        IDbSet<Group> Groups { get; set; }

        IDbSet<Friend> Friends { get; set; } 

        int SaveChanges();

        DbEntityEntry<TEntity> Entry<TEntity>(TEntity entity) where TEntity : class;

        IDbSet<T> Set<T>() where T : class;
    }
}
