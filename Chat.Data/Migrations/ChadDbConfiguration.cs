namespace Chat.Data.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    public sealed class ChadDbConfiguration : DbMigrationsConfiguration<Chat.Data.ChatDbContext>
    {
        public ChadDbConfiguration()
        {
            this.AutomaticMigrationsEnabled = true;
            this.AutomaticMigrationDataLossAllowed = true;
        }

        protected override void Seed(Chat.Data.ChatDbContext context)
        {
        }
    }
}
