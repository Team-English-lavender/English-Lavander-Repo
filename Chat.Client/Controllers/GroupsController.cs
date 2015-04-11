using Chat.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Chat.Client.Controllers
{
    using Models;

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
                        Messages = g.Messages.Select(gm => new MessagesExportModel()
                        {
                          Id  = gm.Id,
                          UserId = gm.UserId,
                          GroupId = gm.GroupId,
                          MessageText = gm.MessageText,
                          Time = gm.Time
                        }),
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
                return ResponseMessage(Request.CreateErrorResponse(HttpStatusCode.NotFound, "Currently dont have created groups"));
            }

            return this.Ok(groups);
        }

        [HttpPost]
        [Route("PostGroup")]
        public IHttpActionResult PostGroup([FromUri]string name)
        {
            if (this.Data.Groups.All().Where(g => g.Name == name).Any())
	        {
		        return BadRequest("Already exists group with that name.");
	        }

            Group newGroup = new Group()
            {
                Name = name               
            };

            this.Data.Groups.Add(newGroup);
            this.Data.SaveChanges();

            return this.Created("Groups", newGroup);
        }
    }
}
