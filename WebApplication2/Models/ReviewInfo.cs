using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.Serialization;

namespace WebApplication2.Models
{
    [DataContract]
    public class ReviewInfo
    {
        [DataMember(Name = "companyName")]
        public string companyName { get; set; }

        [DataMember(Name = "username")]
        public string username { get; set; }

        [DataMember(Name = "review")]
        public string review { get; set; }

        [DataMember(Name = "stars")]
        public double stars { get; set; }

        [DataMember(Name = "timestamp")]
        public Int32 timestamp { get; set; }
    }
}