using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApplication2.Models;

namespace WebApplication2.Controllers
{
    public class ReviewController : System.Web.Mvc.Controller
    {
        // POST with api/Review/PostReview
        [HttpPost]
        public string PostReview(ReviewInfo review)
        {
            //System.Diagnostics.Debug.WriteLine(test);
            return review.companyName;
        }

        // GET with api/Review/GetReview/SampleCompanyName
        [HttpGet]
        public string GetReview(string companyName)
        {
            Newtonsoft.Json.Linq.JObject json = Newtonsoft.Json.Linq.JObject.Parse(companyName);
            string name = json["companyName"].ToString();
            return name;
        }
    }
}
 