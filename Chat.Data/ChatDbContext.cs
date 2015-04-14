namespace Chat.Data
{
    using System.Data.Entity;
    using Microsoft.AspNet.Identity.EntityFramework;

    using Model;

    public class ChatDbContext : IdentityDbContext<User>, IChatDbContext 
    {
        public ChatDbContext()
            : base("ChatDb", throwIfV1Schema: false)
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<ChatDbContext, Chat.Data.Migrations.ChadDbConfiguration>());
        }

        public IDbSet<Message> Messages { get; set; }

        public IDbSet<Group> Groups { get; set; }

        public IDbSet<Friend> Friends { get; set; } 

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
