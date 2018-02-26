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
        // Body:
        //  {
        //	review: {
        //	    "companyName":"Google",
        //	    "username":"Google",
        //	    "review":"Google",
        //	    "stars":5,
        //	    "timestamp":"time"
        //	    }
        //  }
        [HttpPost]
        public string PostReview(ReviewInfo review)
        {
            return review.companyName;
        }

        // GET with URL: api/Review/GetReview/{companyName: "Google"}
        [HttpGet]
        public string GetReview(string companyName)
        {
            Newtonsoft.Json.Linq.JObject json = Newtonsoft.Json.Linq.JObject.Parse(companyName);
            string name = json["companyName"].ToString();
            return name;
        }
    }
}
 