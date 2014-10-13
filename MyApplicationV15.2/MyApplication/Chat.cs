using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;

namespace MyApplication
{
    public class Chat : Hub
    {
        public void SendMessage(string nick, string message)
        {
            Clients.All.PublishMessage(nick, message);
        } 
    }
}