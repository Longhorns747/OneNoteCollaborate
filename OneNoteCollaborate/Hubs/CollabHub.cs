using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;

namespace OneNoteCollaborate.Hubs
{
    public class CollabHub : Hub
    {
        public void Hello()
        {
            Clients.All.hello();
        }

        public void syncTextbox(string currentText)
        {
            Clients.All.syncFromServer(currentText);
        }
    }
}