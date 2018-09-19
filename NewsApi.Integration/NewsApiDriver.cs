using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewsApi.Integration
{
    public class NewsApiDriver
    {
        public string ApiKey { get; set; }
        public string Url { get; set; }
        public NewsApiDriver(string apiKey, string url = "https://newsapi.org/v2/")
        {
            ApiKey = apiKey;
            Url = url;
        }
    }
}
