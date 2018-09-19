using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using NewsApi.Integration.Utilities;
using NewsAPI.Constants;
using NewsAPI.Models;

namespace NewsApi.Integration.Helper
{
    public class AllNewsHelper
    {
        private NewsApiManager NewsApiManager { get; set; }
        public AllNewsHelper(NewsApiManager newsApiManager)
        {
            NewsApiManager = newsApiManager;
        }

        public ArticlesResult GetAllNews(List<string> sources)
        {
            return NewsApiManager.NewsApiClient.GetEverything(new EverythingRequest
            {
                Sources = sources,
                From = DateTime.Now.AddHours(-10),
                To = DateTime.Now,
                SortBy = SortBys.PublishedAt,
            });
        }
    }
}
