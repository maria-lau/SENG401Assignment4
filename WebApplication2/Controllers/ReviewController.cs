using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Mvc;
using WebApplication2.Models;

namespace WebApplication2.Controllers
{
    public class ReviewController : ApiController
    {
        // GET: api/Review
        public IEnumerable<ReviewInfo> Get()
        {
            //should be using POST instead of GET to save reviews.
            var reviewInfoList = new List<ReviewInfo>();
            for(int i = 0; i < 10; i++)
            {
                var reviewInfo = new ReviewInfo
                {
                    companyName = $"companyName {i}",
                    username = $"username {i}",
                    review = $"review {i}",
                    stars = i,
                    timestamp = (Int32)DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1)).TotalSeconds
                };
                reviewInfoList.Add(reviewInfo);
            }
            return reviewInfoList;
        }

        // GET: api/Review/5
        //public ReviewInfo Get(int id)
        //{
        //    return new ReviewInfo
        //    {
        //        companyName = $"companyName {id}",
        //        username = $"username {id}",
        //        review = $"review {id}",
        //        stars = id,
        //        timestamp = (Int32)DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1)).TotalSeconds
        //    };
        //}

        //GET: api/Review/String
        public void Get(String id)
        {

            using (WebClient wc = new WebClient())
            {
              try
              {
                  var json = wc.DownloadString(id);
              }
              catch (Exception) { }
            }

            
        }


        // POST: api/Review
        public void Post([FromBody]string value)
        {
            var reviewInfoList = new List<ReviewInfo>();
            for (int i = 0; i < 10; i++)
            {
                var reviewInfo = new ReviewInfo
                {
                    companyName = $"companyName {i}",
                    username = $"username {i}",
                    review = $"review {i}",
                    stars = i,
                    timestamp = (Int32)DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1)).TotalSeconds
                };
                reviewInfoList.Add(reviewInfo);
            }
        }

        // PUT: api/Review/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Review/5
        public void Delete(int id)
        {
        }
    }
}
 