namespace Chat.Client.Controllers
{
    using System;
    using System.Linq;
    using System.Net;
    using System.Net.Http;
    using System.Web.Http;
    using Data;
    using Microsoft.AspNet.Identity;
    using Model;
    using Models;
    using WebGrease.Css.Extensions;

    [Authorize]
    [RoutePrefix("api/Users")]
    public class UsersController : BaseController
    {
        public UsersController()
            : this (new ChatData(new ChatDbContext()))
        {
        }

        public UsersController(IChatData data)
            : base (data)
        {
        }

        [HttpGet]
        [Route("GetAll")]
        public IHttpActionResult GetAll()
        {
            var users = this.Data.Users.All()
                .Select(u =>
                    new UsersExportModel()
                    {
                        Id = u.Id,
                        UserName = u.UserName
                    })
                .ToList();

            if (!users.Any())
            {
                return ResponseMessage(Request.CreateResponse(HttpStatusCode.NotFound, "Currently no users are subscribed."));
            }

            return this.Ok(users);
        }

        [HttpGet]
        [Route("GetByUserName")]
        public IHttpActionResult GetByUserName([FromUri]string userName)
        {
            var user = this.Data.Users.All().Where(u => u.UserName == userName).FirstOrDefault();

            if (user == null)
            {
                return ResponseMessage(Request.CreateResponse(HttpStatusCode.NotFound,
                        String.Format("No user with user name {0}", userName)));
            }

            return this.Ok(user);
        }

        [HttpGet]
        [Route("GetByUserId")]
        public IHttpActionResult GetByUserId([FromUri]string userId)
        {
            var user = this.Data.Users.All().Where(u => u.Id == userId).FirstOrDefault();

            if (user == null)
            {
                return ResponseMessage(Request.CreateResponse(HttpStatusCode.NotFound,
                        String.Format("No user with user name {0}", userId)));
            }

            return this.Ok(user);
        }

        [HttpGet]
        [Route("GetUsersByGroup")]
        public IHttpActionResult GetUsersByGroup([FromUri] int groupId)
        {
            var group = this.Data.Groups.All().Where(g => g.Id == groupId).FirstOrDefault();
            var users = group.Users.Select(u => 
                    new UsersExportModel()
                    {
                        Id = u.Id,
                        UserName = u.UserName
                    }
                );

            if (!users.Any())
            {
                return ResponseMessage(Request.CreateResponse(HttpStatusCode.NotFound,
                        String.Format("No users in group with groupId {0}", groupId)));
            }

            return this.Ok(users);
        }

        [HttpPost]
        [Route("AddFriend")]
        public IHttpActionResult AddFriend([FromBody] FriendsImportModel model)
        {
            var userId = this.User.Identity.GetUserId();
            var user = this.Data.Users.All()
                .Where(u => u.Id == userId)
                .FirstOrDefault();

            if (model.Id == this.User.Identity.GetUserId())
            {
                return ResponseMessage(Request.CreateResponse(HttpStatusCode.BadRequest,
                        String.Format("User {0} cant add self to friends.", model.UserName)));
            }

            if (user.Friends.Where(f => f.Id == model.Id).Any())
            {
                return ResponseMessage(Request.CreateResponse(HttpStatusCode.Conflict,
                       String.Format("Friend {0} is already added to friend list.", model.UserName)));
            }

            Friend newFriend = new Friend()
            {
                Id = model.Id,
                UserName = model.UserName
            };

            user.Friends.Add(newFriend);
            this.Data.SaveChanges();

            return this.Ok(newFriend);
        }

        [HttpGet]
        [Route("GetAllFriends")]
        public IHttpActionResult GetAllFriend()
        {
            var userId = this.User.Identity.GetUserId();
            var friends = this.Data.Users.All().Where(u => u.Id == userId)
                .Select(u => u.Friends).ToList();

            if (!friends.Any())
            {
                return ResponseMessage(Request.CreateResponse(HttpStatusCode.Conflict,
                    "Dont have any friends in the list."));
            }

            return this.Ok(friends);
        }
    }
}
