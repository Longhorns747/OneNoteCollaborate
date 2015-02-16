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

        public async void syncTextbox(string currentText, string accessToken, string pageId)
        {
            using (var httpClient = new HttpClient { BaseAddress =  new Uri(APIData.BASE_URL)})
            {

                httpClient.DefaultRequestHeaders.TryAddWithoutValidation("authorization", "Bearer " + accessToken);
                using (var content = new StringContent(HTMLConsts.PATCH_WRAP + currentText + HTMLConsts.PATCH_CLOSE_WRAP, System.Text.Encoding.Default, "application/xhtml+xml"))
                {
                    HttpResponseMessage response = null;
                    var requestUri = new Uri(new Uri(APIData.BASE_URL), "beta/pages/" + pageId + "/content");
                    using (var request = new HttpRequestMessage { Method = new HttpMethod("PATCH"), RequestUri = requestUri, Content = content })
                    {
                        response = await httpClient.SendAsync(request);
                    }
                    string responseData = await response.Content.ReadAsStringAsync();
                    Clients.All.syncFromServer(currentText);        
                }
            }
        }
    }
}