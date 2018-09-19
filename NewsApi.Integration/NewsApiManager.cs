using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NewsAPI;

namespace NewsApi.Integration
{
    public class NewsApiManager
    {
        private NewsApiDriver Driver { get; set; }
        public NewsApiClient NewsApiClient { get; set; }
        public NewsApiManager(NewsApiDriver driver)
        {
            Driver = driver;
            NewsApiClient =  new NewsApiClient(Driver.ApiKey);
        }

        public NewsApiDriver GetDriver()
        {
            return Driver;
        }
    }
}
