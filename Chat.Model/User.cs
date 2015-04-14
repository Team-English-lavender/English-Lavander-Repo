using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Security.Claims;
using Microsoft.AspNet.Identity;
using Chat.Data;

namespace Chat.Model
{
    public class User : IdentityUser
    {
        public User()
        {
            this.Groups = new HashSet<Group>();
            this.Messages = new HashSet<Message>();
            this.Friends = new HashSet<Friend>();
        }

        public virtual ICollection<Group> Groups { get; set; }

        public virtual ICollection<Message> Messages { get; set; }

        public virtual ICollection<Friend> Friends { get; set; } 

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<User> manager, string authenticationType)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, authenticationType);
            // Add custom user claims here
            return userIdentity;
        }
    }
}
