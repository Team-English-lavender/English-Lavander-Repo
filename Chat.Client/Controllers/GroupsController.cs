﻿namespace Chat.Client.Controllers
{
    using System.Linq;
    using System.Net;
    using System.Net.Http;
    using System.Web.Http;

    using Data;
    using Microsoft.AspNet.Identity;
    using Models;
    using System.Threading.Tasks;
    using System.Web;
    using Chat.Client.Providers;

    [Authorize]
    [RoutePrefix("api/Groups")]
    public class GroupsController : BaseController
    {
        public GroupsController()
            : this(new ChatData(new ChatDbContext()))
        {
        }

        public GroupsController(IChatData data)
            : base(data)
        {

        }

        [HttpGet]
        [Route("GetAll")]
        public IHttpActionResult GetAll()
        {
            var groups = this.Data.Groups.All()
                .Select(g =>
                    new GroupsExportModel()
                    {
                        Id = g.Id,
                        Name = g.Name,
                        Users = g.Users.Select(u => new UsersExportModel()
                        {
                            Id = u.Id,
                            UserName = u.UserName
                        })
                    }
                )
                .ToList();

            if (!groups.Any())
            {
                return ResponseMessage(Request.CreateResponse(HttpStatusCode.PartialContent, "Currently dont have created groups"));
            }

            return this.Ok(groups);
        }

        [HttpGet]
        [Route("GetUserGroups")]
        public IHttpActionResult GetUserGroups()
        {
            var userId = this.User.Identity.GetUserId();
            var groups = this.Data.Groups.All()
                .Where(g => g.Users.Any(u => u.Id == userId))
                .Select(g =>
                    new GroupsExportModel()
                    {
                        Id = g.Id,
                        Name = g.Name,
                        Users = g.Users.Select(u => new UsersExportModel()
                        {
                            Id = u.Id,
                            UserName = u.UserName
                        })
                    }
                )
                .ToList();

            if (!groups.Any())
            {
                return ResponseMessage(Request.CreateResponse(HttpStatusCode.PartialContent,
                        "Currently dont have groups with that user."));
            }

            return this.Ok(groups);
        }

        [HttpPost]
        [Route("CreateGroup")]
        public IHttpActionResult CreateGroup([FromBody]GroupsExportModel model)
        {
            if (this.Data.Groups.All().Where(g => g.Name == model.Name).Any())
            {
                return ResponseMessage(Request.CreateErrorResponse(HttpStatusCode.NotFound,
                        "Already exists group with that name."));
            }

            Group newGroup = new Group()
            {
                Name = model.Name,
                IsPublic = model.IsPublic,
                // Would be great if we could attach users here
                // but there is a cast promlem
                //Users = model.Users
            };

            this.Data.Groups.Add(newGroup);
            this.Data.SaveChanges();

            return this.Created("Groups", newGroup);
        }

        [HttpPost]
        [Route("AddUserToGroup")]
        public IHttpActionResult AddUserToGroup([FromBody]GroupAddUserImportModel model)
        {
            var user = this.Data.Users.All().Where(u => u.Id == model.UserId).FirstOrDefault();
            var group = this.Data.Groups.All().Where(g => g.Id == model.GroupId).FirstOrDefault();

            if (user == null)
            {
                return ResponseMessage(Request.CreateErrorResponse(HttpStatusCode.NotFound,
                        "No User with specified Id found."));
            }
            if (group == null)
            {
                return ResponseMessage(Request.CreateErrorResponse(HttpStatusCode.NotFound,
                        "No Group with specified Id found."));
            }

            group.Users.Add(user);
            this.Data.SaveChanges();
            return this.Ok(new { GroupId = group.Id, UserId = user.Id });
        }
    }
}
