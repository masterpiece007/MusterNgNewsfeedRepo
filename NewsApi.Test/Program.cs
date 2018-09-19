using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NewsApi.Integration;
using NewsApi.Integration.Helper;
using NewsApi.Integration.Model;
using NewsAPI.Constants;
using NewsAPI.Models;

namespace NewsApi.Test
{
    class Program
    {
        static void Main(string[] args)
        {
            string api = ConfigurationManager.AppSettings["api"];
            string url = ConfigurationManager.AppSettings["url"];

            NewsApiDriver driver = new NewsApiDriver(api, url);
            NewsApiManager newsApiManager = new NewsApiManager(driver);
            NewsSourceHelper sourceHelper = new NewsSourceHelper(newsApiManager);
            TopNewsHelper topNewsHelper = new TopNewsHelper(newsApiManager);
            AllNewsHelper allNewsHelper = new AllNewsHelper(newsApiManager);

//            var data = sourceHelper.SourcesAvailable(new SourceRequest{Category = Categories.Technology}).Result;
//            var sourcelang = sourceHelper.SourcesByCategory(Categories.Technology).Result;

            /*var data = topNewsHelper.TopNews(new TopHeadlinesRequest
            {
                Language = Languages.EN
            });
            Console.WriteLine(data);*/

            var dataNews = allNewsHelper.GetAllNews(null);
            Console.WriteLine(dataNews);
            Console.ReadLine();
        }
    }
}
