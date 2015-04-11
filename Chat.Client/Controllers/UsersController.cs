namespace Chat.Client.Controllers
{
    using System;
    using System.Linq;
    using System.Net;
    using System.Net.Http;
    using System.Web.Http;
    using Data;
    using Models;

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


    }
}
