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
    public class CategoryController : Controller
    {
        private RestClient client = new RestClient("https://localhost:44371/api/");

        public ActionResult GetCategories()
        {
            RestRequest request = new RestRequest("Post/GetCategories", Method.GET);

            request.OnBeforeDeserialization = resp => { resp.ContentType = "application/json"; };
            IRestResponse<List<Category>> response = client.Execute<List<Category>>(request);

            var entity = JsonConvert.DeserializeObject<List<Category>>(response.Content);
            return View(entity);
        }
    }
}