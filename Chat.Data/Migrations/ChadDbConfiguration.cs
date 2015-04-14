namespace Chat.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using Microsoft.AspNet.Identity;
    using Model;

    public sealed class ChadDbConfiguration : DbMigrationsConfiguration<ChatDbContext>
    {
        public ChadDbConfiguration()
        {
            this.AutomaticMigrationsEnabled = true;
            this.AutomaticMigrationDataLossAllowed = true;
        }

        protected override void Seed(ChatDbContext context)
        {
            if (!context.Users.Any())
            {
                User userOne = new User()
                {
                    UserName = "TheFirst",
                    Email = "first@asd.com",
                    PasswordHash = "AOxyBrLsqTkOirMbW0OhW6D3kr3kysZmyOzJoelgiBZh2zmV2C6QG3F62aihRHKLUg=="
                };
                context.Users.Add(userOne);
                Friend firstFriend = new Friend(){Id = userOne.Id, UserName = userOne.UserName};
                User userTwo = new User()
                {
                    UserName = "TheSecond",
                    Email = "second@asd.com",
                    PasswordHash = "AOxyBrLsqTkOirMbW0OhW6D3kr3kysZmyOzJoelgiBZh2zmV2C6QG3F62aihRHKLUg=="
                };
                context.Users.Add(userTwo);
                Friend secondFriend = new Friend(){Id = userTwo.Id, UserName = userTwo.UserName};
                User userThree = new User()
                {
                    UserName = "TheThird",
                    Email = "third@asd.com",
                    PasswordHash = "AOxyBrLsqTkOirMbW0OhW6D3kr3kysZmyOzJoelgiBZh2zmV2C6QG3F62aihRHKLUg=="
                };
                context.Users.Add(userThree);
                userThree.Friends.Add(firstFriend);
                userThree.Friends.Add(secondFriend);
                Friend thidrFriend = new Friend(){Id = userThree.Id, UserName = userThree.UserName};
                User userFour = new User()
                {
                    UserName = "TheFourth",
                    Email = "four@asd.com",
                    PasswordHash = "AOxyBrLsqTkOirMbW0OhW6D3kr3kysZmyOzJoelgiBZh2zmV2C6QG3F62aihRHKLUg=="
                };
                context.Users.Add(userFour);
                userFour.Friends.Add(firstFriend);
                userFour.Friends.Add(secondFriend);
                userFour.Friends.Add(thidrFriend);
                Group groupOne = new Group()
                {
                    Name = "The Group One",
                };
                context.Groups.Add(groupOne);
                groupOne.Users.Add(userOne);
                groupOne.Users.Add(userTwo);

                Group groupTwo = new Group()
                {
                    Name = "The Group Two",
                };
                context.Groups.Add(groupTwo);
                groupTwo.Users.Add(userThree);
                groupTwo.Users.Add(userFour);

                Message messageOneGrOne = new Message()
                {
                    Group = groupOne,
                    MessageText = "The first message text GroupOne.",
                    Time = DateTime.Now,
                    User = userOne
                };
                context.Messages.Add(messageOneGrOne);
                Message messageTwoGrOne = new Message()
                {
                    Group = groupOne,
                    MessageText = "The second message text GroupOne.",
                    Time = DateTime.Now,
                    User = userTwo
                };
                context.Messages.Add(messageTwoGrOne);
                Message messageThreeGrOne = new Message()
                {
                    Group = groupOne,
                    MessageText = "The third message text GroupOne.",
                    Time = DateTime.Now,
                    User = userThree
                };
                context.Messages.Add(messageThreeGrOne);
                Message messageFourGrOne = new Message()
                {
                    Group = groupOne,
                    MessageText = "The fourth message text GroupOne.",
                    Time = DateTime.Now,
                    User = userFour
                };
                context.Messages.Add(messageFourGrOne);
                // :::::::::::::::::::::::::::::::::::::::::::

                Message messageOneGrTwo = new Message()
                {
                    Group = groupTwo,
                    MessageText = "The first message text GroupTwo.",
                    Time = DateTime.Now,
                    User = userOne
                };
                context.Messages.Add(messageOneGrTwo);
                Message messageTwoGrTwo = new Message()
                {
                    Group = groupTwo,
                    MessageText = "The second message text GroupTwo.",
                    Time = DateTime.Now,
                    User = userTwo
                };
                context.Messages.Add(messageTwoGrTwo);
                Message messageThreeGrTwo = new Message()
                {
                    Group = groupTwo,
                    MessageText = "The third message text GroupTwo.",
                    Time = DateTime.Now,
                    User = userThree
                };
                context.Messages.Add(messageThreeGrTwo);
                Message messageFourGrTwo = new Message()
                {
                    Group = groupTwo,
                    MessageText = "The fourth message text GroupTwo.",
                    Time = DateTime.Now,
                    User = userFour
                };
                context.Messages.Add(messageFourGrTwo);

                context.SaveChanges();
            }
        }
    }
}
