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
    using Data;
    using Models;

    [HubName("chatHub")]
    public class ChatHub : Hub
    {

        //Sender will be replaced with the username of the logged in user
        public void SendMessage(string sender, string message, int groupId)
        {
            Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;

            DateTime time = DateTime.Now;
            string format = "HH:mm:ss";

            if (Context.User.Identity.Name != sender)
            {
                //the user in the client is different with the registered user
                //Probably logout the user server side and client side
            }
            Clients.All.broadCastMessage(time.ToString(format), sender, message, groupId);
        }

        public override Task OnConnected()
        {
            return base.OnConnected();
        }

        public override Task OnDisconnected(bool stopCalled)
        {
            string mssg = "Left Chat";

            this.SendMessage(Context.User.Identity.Name, mssg, 0);

            return base.OnDisconnected(stopCalled);
        }

        public void SendMessageToGroup(string sender, string message, string groupName)
        {
            Clients.Group(groupName).broadCastMessage(DateTime.Now, sender, message);
        }

        public Task JoinRoom(string roomName)
        {
            return Groups.Add(Context.ConnectionId, roomName);
        }

        public Task LeaveRoom(string roomName)
        {
            return Groups.Remove(Context.ConnectionId, roomName);
        }
    }
}