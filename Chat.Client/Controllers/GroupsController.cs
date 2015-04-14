namespace Chat.Client.Controllers
{
    using System.Linq;
    using System.Net;
    using System.Net.Http;
    using System.Web.Http;

    using Data;
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

        [HttpGet]
        [Route("GetByUserId")]
        public IHttpActionResult GetByUserId([FromUri]string userId)
        {
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
                return ResponseMessage(Request.CreateErrorResponse(HttpStatusCode.NotFound,
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
                IsPublic = model.IsPublic

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
            return this.Ok(new { GroupId = group.Id, UserId = user.Id});
        }
    }
}
