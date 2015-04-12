using System;
namespace Chat.Data.Repositories
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;
    using Model;

    class MessagesRepository : GenericRepository<Message>
    {
        public MessagesRepository(IChatDbContext context)
            : base (context)
        {
        }

        //public IEnumerable<Message> GetAll()
        //{
        //    var messages = this.All()
        //        .Select(m => 
        //            new MessagesExportModel()
        //            {
        //                Id = m.Id,
        //                UserId = m.UserId,
        //                UserName = m.User.UserName,
        //                GroupId = m.GroupId,
        //                GroupName = m.Group.Name,
        //                Time = m.Time,
        //                MessageText = m.MessageText
        //            }
        //        )

        //}

        //// GET api/messages/GetByGroup
        //[HttpGet]
        //[Route("GetAllByGroup")]
        //public IHttpActionResult GetAllByGroup([FromUri]int groupId)
        //{
        //    var group = this.Data.Groups.All().Where(g => g.Id == groupId).FirstOrDefault();

        //    if (group == null)
        //    {
        //        return ResponseMessage(Request.CreateErrorResponse(HttpStatusCode.NotFound, "No such group."));
        //    }

        //    var messages = this.Data.Messages.All()
        //        .Where(m => m.Group.Id == groupId)
        //        .Select(m =>
        //            new MessagesExportModel()
        //            {
        //                Id = m.Id,
        //                MessageText = m.MessageText,
        //                Time = m.Time,
        //                UserId = m.UserId,
        //                UserName = m.User.UserName,
        //                GroupId = m.GroupId,
        //                GroupName = m.Group.Name
        //            }
        //        )
        //        .ToList();

        //    if (!messages.Any())
        //    {
        //        return ResponseMessage(Request.CreateErrorResponse(HttpStatusCode.NotFound, "No messages in this group."));
        //    }

        //    return this.Ok(messages);
        //}

        //[HttpGet]
        //[Route("GetLastByGroup")]
        //public IHttpActionResult GetLastByGroup([FromUri]int groupId, int? count)
        //{
        //    int messagesCount = count ?? LastMessagesCount;

        //    var messages = this.Data.Messages.All()
        //        .Where(m => m.Group.Id == groupId)
        //        .OrderByDescending(m => m.Id)
        //        .Take(messagesCount)
        //        .Select(m =>
        //            new MessagesExportModel()
        //            {
        //                Id = m.Id, 
        //                GroupId = m.GroupId,
        //                GroupName = m.Group.Name,
        //                MessageText = m.MessageText, 
        //                Time = m.Time, 
        //                UserId = m.UserId,
        //                UserName = m.User.UserName
        //            }
        //        )
        //        .ToList();

        //    if (!messages.Any())
        //    {
        //        return ResponseMessage(Request.CreateErrorResponse(HttpStatusCode.NotFound, "No messages in this group."));
        //    }

        //    return this.Ok(messages);
        //}

        //// POST api/messages
        //[HttpPost]
        //[Route("PostMessage")]
        //public IHttpActionResult Post([FromBody]MessagesImportModel model)
        //{
        //    var currentUserId = this.User.Identity.GetUserId();

        //    if (currentUserId == null)
        //    {
        //        return this.BadRequest("Not logged please logg in !");
        //    }

        //    Message newMessage = new Message()
        //    {
        //        MessageText = model.Text,
        //        Time = DateTime.Now,
        //        UserId = currentUserId,
        //        GroupId = model.GroupId
        //    };
            
        //    this.Data.Messages.Add(newMessage);
        //    this.Data.SaveChanges();
        //    return this.Created("Message", newMessage);
        //}
    }
}
