namespace Chat.Client.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;
    using System.Net.Http;
    using System.Web.Http;
    using Microsoft.AspNet.Identity;

    using Data;
    using Model;
    using Models;

    [Authorize]
    [RoutePrefix("api/Messages")]
    public class MessagesController : BaseController
    {
        private const int LastMessagesCount = 3;

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
        public IHttpActionResult GetAll()
        {
            var messages = this.Data.Messages.GetAll().ToList();

            if (!messages.Any())
            {
                return ResponseMessage(Request.CreateErrorResponse(HttpStatusCode.NotFound, "No messages in data base."));
            }
            return this.Ok(messages);
        }

        // GET api/messages/GetByGroup
        [HttpGet]
        [Route("GetAllByGroup")]
        public IHttpActionResult GetAllByGroup([FromUri]int groupId)
        {
            var group = this.Data.Groups.All().Where(g => g.Id == groupId).FirstOrDefault();

            if (group == null)
            {
                return ResponseMessage(Request.CreateErrorResponse(HttpStatusCode.NotFound, "No such group."));
            }

            var messages = this.Data.Messages.GetAllByGroupId(groupId);
            if (!messages.Any())
            {
                return ResponseMessage(Request.CreateResponse(HttpStatusCode.PartialContent, "No messages in this group."));
            }

            return this.Ok(messages);
        }

        [HttpGet]
        [Route("GetLastByGroup")]
        public IHttpActionResult GetLastByGroup([FromUri]int groupId, int? count)
        {
            int messagesCount = count ?? LastMessagesCount;

            var messages = this.Data.Messages
                .GetLastByGroup(groupId, count, LastMessagesCount)
                .ToList();
            if (!messages.Any())
            {
                return ResponseMessage(Request
                    .CreateResponse(HttpStatusCode.PartialContent, "No messages in this group."));
            }

            return this.Ok(messages);
        }

        // POST api/messages
        [HttpPost]
        [Route("PostMessage")]
        public IHttpActionResult Post([FromBody]MessagesImportModel model)
        {
            var currentUserId = this.User.Identity.GetUserId();

            if (currentUserId == null)
            {
                return this.BadRequest("Not logged please logg in !");
            }

            Message newMessage = new Message()
            {
                MessageText = model.Text,
                Time = DateTime.Now,
                UserId = currentUserId,
                GroupId = model.GroupId
            };
            
            this.Data.Messages.Add(newMessage);
            this.Data.SaveChanges();

            return this.Created("Message", newMessage);
        }
    }
}
