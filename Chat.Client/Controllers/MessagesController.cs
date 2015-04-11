using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using Chat.Data;
using Chat.Model;
using Microsoft.AspNet.Identity;

namespace Chat.Client.Controllers
{
    using System.Net;
    using System.Net.Http;
    using Models;
    using WebGrease.Css.Extensions;

    [Authorize]
    [RoutePrefix("api/Messages")]
    public class MessagesController : BaseController
    {
        private const int LastMessagesCount = 20;

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
        [Route("GetAllByGroup")]
        public IHttpActionResult GetAllByGroup([FromUri]int groupId)
        {
            var group = this.Data.Groups.All().Where(g => g.Id == groupId).FirstOrDefault();

            if (group == null)
            {
                return ResponseMessage(Request.CreateErrorResponse(HttpStatusCode.NotFound, "No such group."));
            }

            var messages = this.Data.Messages.All()
                .Where(m => m.Group.Id == groupId)
                .Select(m =>
                    new MessagesExportModel()
                    {
                        Id = m.Id,
                        MessageText = m.MessageText,
                        Time = m.Time,
                        UserId = m.UserId,
                        UserName = m.User.UserName,
                        GroupId = m.GroupId,
                        GroupName = m.Group.Name
                    }
                )
                .ToList();

            if (!messages.Any())
            {
                return ResponseMessage(Request.CreateErrorResponse(HttpStatusCode.NotFound, "No messages in this group."));
            }

            return this.Ok(messages);
        }

        [HttpGet]
        [Route("GetLastByGroup")]
        public IHttpActionResult GetLastByGroup([FromUri]int groupId, int? count)
        {
            int messagesCount = count ?? LastMessagesCount;

            var messages = this.Data.Messages.All()
                .Where(m => m.Group.Id == groupId)
                .OrderByDescending(m => m.Id)
                .Take(messagesCount)
                .Select(m =>
                    new MessagesExportModel()
                    {
                        Id = m.Id, 
                        GroupId = m.GroupId,
                        GroupName = m.Group.Name,
                        MessageText = m.MessageText, 
                        Time = m.Time, 
                        UserId = m.UserId,
                        UserName = m.User.UserName
                    }
                )
                .ToList();

            if (!messages.Any())
            {
                return ResponseMessage(Request.CreateErrorResponse(HttpStatusCode.NotFound, "No messages in this group."));
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
