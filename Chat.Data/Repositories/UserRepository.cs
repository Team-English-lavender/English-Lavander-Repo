using Chat.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chat.Data.Repositories
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        public UserRepository(IChatDbContext context)
            : base (context)
        {
        }

        public IQueryable<User> AllAuthors()
        {
            return this.All().Where(x => x.Messages.Any());
        }

        public User GetUserByUsername(string username)
        {
            return this.All().FirstOrDefault(x => x.UserName == username);
        }

        public void CreateUser(string userName, string password)
        {
            this.DbSet.Add(new User() { UserName = userName });
        }
    }
}
