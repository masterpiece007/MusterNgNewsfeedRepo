using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NewsAPI.Models;

namespace NewsApi.Integration.Helper
{
    public class TopNewsHelper
    {
        private NewsApiManager NewsApiManager { get; set; }
        public TopNewsHelper(NewsApiManager newsApiManager)
        {
            NewsApiManager = newsApiManager;
        }
        public ArticlesResult TopNews(TopHeadlinesRequest request)
        {
            return NewsApiManager.NewsApiClient.GetTopHeadlines(request);
        }
    }
}
