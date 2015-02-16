using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;
using System.Net.Http;
using OneNoteCollaborate.Configuration;

namespace OneNoteCollaborate.Hubs
{
    public class CollabHub : Hub
    {
        public void Hello()
        {
            Clients.All.hello();
        }

        public async void syncTextbox(string currentText, string accessToken)
        {
            using (var httpClient = new HttpClient { BaseAddress =  new Uri(APIData.BASE_URL)})
            {

                httpClient.DefaultRequestHeaders.TryAddWithoutValidation("authorization", "Bearer " + accessToken);
                using (var content = new StringContent(APIData.HTML_WRAP + currentText + "</body></html>", System.Text.Encoding.Default, "application/xhtml+xml"))
                {
                    using (var response = await httpClient.PostAsync("pages", content))
                    {
                        string responseData = await response.Content.ReadAsStringAsync();
                        Clients.All.syncFromServer(currentText);
                    }
                }
            }
        }
    }
}