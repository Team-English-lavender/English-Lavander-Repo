using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Chat.Model;

namespace Chat.Data
{
    public class ChatDbContext : IdentityDbContext<User>, IChatDbContext 
    {
        public ChatDbContext()
            : base("ChatDb", throwIfV1Schema: false)
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<ChatDbContext, Chat.Data.Migrations.ChadDbConfiguration>());
        }

        public IDbSet<Message> Messages { get; set; }

        public IDbSet<Group> Groups { get; set; }

        public new IDbSet<T> Set<T>() where T : class
        {
            return base.Set<T>();
        }

        public static ChatDbContext Create()
        {
            return new ChatDbContext();
        }

        //protected override void OnModelCreating(DbModelBuilder modelBuilder)
        //{
        //    Database.SetInitializer(new MigrateDatabaseToLatestVersion<ChatDbContext, Chat.Data.Migrations.ChadDbConfiguration>());
        //}
    }
}
