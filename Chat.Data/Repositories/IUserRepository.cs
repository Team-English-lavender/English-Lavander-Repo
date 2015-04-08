using Chat.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chat.Data.Repositories
{
    public interface IUserRepository : IRepository<User>
    {
        IQueryable<User> AllAuthors();

        User GetUserByUsername(string username);
    }
}
