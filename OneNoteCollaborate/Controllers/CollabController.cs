using Microsoft.Live;
using Newtonsoft.Json;
using OneNoteCollaborate.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using OneNoteCollaborate.Configuration;

namespace OneNoteCollaborate.Controllers
{
    public class CollabController : Controller
    {
        // GET: Collab
        public async Task<ActionResult> Index()
        {
            try
            {
                LiveAuthClient liveAuthClient = new LiveAuthClient(APIData.CLIENT_ID, APIData.CLIENT_SECRET, APIData.REDIRECT_URL);
                LiveLoginResult result = await liveAuthClient.ExchangeAuthCodeAsync(this.HttpContext);
                if (result.Status == LiveConnectSessionStatus.Connected)
                {
                    string accessToken = liveAuthClient.Session.AccessToken;

                    using (var client = new HttpClient())
                    {
                        client.BaseAddress = new Uri(APIData.BASE_URL);
                        client.DefaultRequestHeaders.Accept.Clear();
                        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                        client.DefaultRequestHeaders.Add("Authorization", "Bearer " + accessToken);
                        HttpResponseMessage response = await client.GetAsync(@"notebooks?select=id,name");
                        if (response.IsSuccessStatusCode)
                        {
                            string data = await response.Content.ReadAsStringAsync();
                            
                            JsonTextReader jsonReader = new JsonTextReader(new StringReader(data));
                            //deserialize to your class
                            List<Notebook> notebooks = new List<Notebook>();

                            while (jsonReader.Read())
                            {
                                if (jsonReader.Value != null && jsonReader.Value.Equals("id"))
                                {
                                    string id = jsonReader.ReadAsString();
                                    string name = "";
                                    jsonReader.Read();

                                    if(jsonReader.Value != null && jsonReader.Value.Equals("name"))
                                    {
                                        name = jsonReader.ReadAsString();
                                    }

                                    notebooks.Add(new Notebook(id, name));
                                }
                            }

                            string res = "";
                            foreach (Notebook note in notebooks) {
                                res = res + note.name + " ";
                            }
                            ViewBag.Notebooks = res;
                            ViewBag.Token = accessToken;
                        }
                    }
                }
            }
            catch (LiveAuthException authEx)
            {
                ViewBag.Notebooks = authEx.ToString();
            }
            catch (LiveConnectException connectEx)
            {
                ViewBag.Notebooks = connectEx.ToString();
            }

            return View();
        }

        // GET: Collab/Login
        public void Login()
        {
            LiveAuthClient liveAuthClient = new LiveAuthClient(APIData.CLIENT_ID, APIData.CLIENT_SECRET, null);
            string[] scopes = new string[] { "wl.signin", "wl.basic", "office.onenote", "office.onenote_create" };
            string loginUrl = liveAuthClient.GetLoginUrl(scopes, APIData.REDIRECT_URL);

            Response.BufferOutput = true;
            Response.Redirect(loginUrl);
        }
    }
}