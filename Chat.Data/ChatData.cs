using Chat.Data.Repositories;
using Chat.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chat.Data
{
    public class ChatData : IChatData
    {
        private IChatDbContext context;
        private IDictionary<Type, Object> repositories;

        public ChatData(IChatDbContext context)
        {
            this.context = context;
            this.repositories = new Dictionary<Type, object>();
        }

        public IUserRepository Users
        {
            get
            {
                return (IUserRepository)this.GetRepository<User>();
            }
        }

        public IRepository<Group> Groups
        {
            get
            {
                return this.GetRepository<Group>();
            }
        }

        public IRepository<Message> Messages
        {
            get
            {
                return this.GetRepository<Message>();
            }
        }

        public int SaveChanges()
        {
            return this.context.SaveChanges();
        }

        private IRepository<T> GetRepository<T>() where T : class
        {
            var type = typeof(T);
            if (!this.repositories.ContainsKey(type))
            {
                var repositoryType = typeof(GenericRepository<T>);
                if (type.IsAssignableFrom(typeof(User)))
                {
                    repositoryType = typeof(UserRepository);
                }
                this.repositories.Add(typeof(T), Activator.CreateInstance(repositoryType, this.context));
            }

            return (IRepository<T>)this.repositories[type];
        }
    }
}
