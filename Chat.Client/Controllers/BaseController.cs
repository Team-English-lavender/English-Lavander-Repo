using Chat.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Chat.Client.Controllers
{
    public class BaseController : ApiController
    {
        private IChatData data;

        public IChatData Data
        {
            get { return this.data; }
        }

        public BaseController()
            : this (new ChatData(new ChatDbContext()))
        {

        }

        public BaseController(IChatData data)
        {
            this.data = data;
        }
       
    }
}
