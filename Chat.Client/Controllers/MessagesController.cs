using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using Chat.Data.Repositories;
using Chat.Data;
using Chat.Model;
using System.Data.Entity;
using Microsoft.AspNet.Identity;

namespace Chat.Client.Controllers
{
    [Authorize]
    public class MessagesController : BaseController
    {

        public MessagesController()
            : this (new ChatData(new ChatDbContext()))
        {            

        }

        public MessagesController(IChatData data)
            : base (data)
        {
        }

        // GET api/messages/GetAll
        [HttpGet]
        [Route("GetAll")]
        public IEnumerable<string> GetAll()
        {
            return this.Data.Messages.All().Select(m => m.MessageText).ToList();
        }

        // GET api/messages/GetByGroup
        [HttpGet]
        [Route("GetByGroup")]
        public IEnumerable<Message> GetByGroup([FromBody]int groupId)
        {
            return this.Data.Messages.All().Where(m => m.Group.Id == groupId).ToList();
        }

        // POST api/messages
        [HttpPost]
        [Route("PostMessage")]
        public IHttpActionResult Post([FromUri]string text, int groupId)
        {
            var currentUserId = this.User.Identity.GetUserId();
            if (currentUserId == null)
            {
                return this.BadRequest("Not logged please logg in !");
            }

            Message newMessage = new Message()
            {
                MessageText = text,
                Time = DateTime.Now,
                UserId = currentUserId,
                GroupId = groupId
            };
            
            this.Data.Messages.Add(newMessage);
            this.Data.SaveChanges();
            return Ok();
        }
    }
}
