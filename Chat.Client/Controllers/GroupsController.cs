using Chat.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Chat.Client.Controllers
{
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

            return Ok();
        }
    }
}
