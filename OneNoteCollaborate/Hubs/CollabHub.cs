using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;
using System.Net.Http;

namespace OneNoteCollaborate.Hubs
{
    public class CollabHub : Hub
    {
        public void Hello()
        {
            Clients.All.hello();
        }

        public void syncTextbox(string currentText, string accessToken)
        {
            using (var content = new StringContent(currentText))
            {
                Clients.All.syncFromServer(currentText);
            }
        }
    }
}