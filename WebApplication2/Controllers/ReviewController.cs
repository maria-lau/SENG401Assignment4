using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApplication2.Models;
using WebApplication2.Models.Database;

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
            ReviewDatabase db = ReviewDatabase.getInstance();
            JObject status = db.addReview(review);
            return status.ToString();
        }

        // GET with URL: api/Review/GetReview/{companyName: "Google"}
        [HttpGet]
        public string GetReview(string companyName)
        {
            ReviewDatabase db = ReviewDatabase.getInstance();
            string[] tokens = companyName.Split('"');
            JArray reviews = db.getReviews(tokens[1]);
            return reviews.ToString();
        }
    }
}
