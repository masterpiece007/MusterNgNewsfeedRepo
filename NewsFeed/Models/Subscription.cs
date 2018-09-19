using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NewsFeed.Models
{
    public class Subscription
    {
        public string UserId { get; set; }
        public string[] SouceIds { get; set; }
        public List<SelectListItem> Items { get; set; }
    }
}