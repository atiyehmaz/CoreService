using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ClientApp.Models;
using RestSharp;
using Newtonsoft.Json;

namespace ClientApp.Controllers
{
    public class PostController : Controller
    {
        private RestClient client = new RestClient("https://localhost:44371/api/");

        public ActionResult GetPosts()
        {
            RestRequest request = new RestRequest("Post/GetPosts", Method.GET);

            request.OnBeforeDeserialization = resp => { resp.ContentType = "application/json"; };
            IRestResponse<List<Post>> response = client.Execute<List<Post>>(request);

            var entity = JsonConvert.DeserializeObject<List<Post>>(response.Content);
            return View(entity);
        }

        [HttpGet]
        public ActionResult AddPost()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddPost(Post Post)
        {
            var request = new RestRequest("Post/AddPost", Method.POST) { RequestFormat = RestSharp.DataFormat.Json };

            request.AddHeader("Content-Type", "application/json");
            request.RequestFormat = DataFormat.Json;
            request.AddParameter("application/json; charset=utf-8", JsonConvert.SerializeObject(Post), "application/json", ParameterType.RequestBody);

            var response = client.Execute(request);
            return RedirectToAction("GetPosts");
        }

    }
}