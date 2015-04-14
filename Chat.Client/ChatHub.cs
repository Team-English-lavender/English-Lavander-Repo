using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using System.Threading.Tasks;
using System.Threading;
using System.Globalization;

namespace Chat.Client
{
    [HubName("chatHub")]
    public class ChatHub : Hub
    {

        //Sender will be replaced with the username of the logged in user
        public void SendMessage(string sender, string message)
        {
            Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;

            DateTime time = DateTime.Now;
            string format = "HH:mm:ss";
            Clients.All.broadCastMessage(time.ToString(format), sender, message);
        }

        public override Task OnConnected()
        {
            return base.OnConnected();
        }

        public override Task OnDisconnected(bool stopCalled)
        {
            string mssg = "Left Chat";

            this.SendMessage("Somebody ", mssg);

            return base.OnDisconnected(stopCalled);
        }
    }
}