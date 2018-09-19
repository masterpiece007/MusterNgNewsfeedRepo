using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NewsApi.Integration.Model;
using NewsApi.Integration.Response;
using NewsApi.Integration.Utilities;
using NewsAPI.Constants;
using NewsAPI.Models;

namespace NewsApi.Integration.Helper
{
    public class NewsSourceHelper
    {
        private NewsApiManager NewsApiManager { get; set; }
        private HttpRequestHelper _httpRequestHelper;
        public NewsSourceHelper(NewsApiManager newsApiManager)
        {
            NewsApiManager = newsApiManager;
            _httpRequestHelper = new HttpRequestHelper(NewsApiManager.GetDriver());
        }

        public Task<SourceResult> AllSoures()
        {
            return _httpRequestHelper.MakeSourceRequest(Constants.SOURCE_URL, string.Empty);
        }

        public Task<SourceResult> SourcesAvailable(SourceRequest request)
        {
            string query = SetQueryString(request);
            return _httpRequestHelper.MakeSourceRequest(Constants.SOURCE_URL, query);
        }

        public Task<SourceResult> SourcesByLanguage(Languages language)
        {
            string lang = $"language={language.ToString()}";
            return _httpRequestHelper.MakeSourceRequest(Constants.SOURCE_URL, lang);

        }

        public Task<SourceResult> SourcesByCountry(Countries country)
        {
            
            string cou = $"country={country.ToString()}";
            return _httpRequestHelper.MakeSourceRequest(Constants.SOURCE_URL, cou);

        }

        public Task<SourceResult> SourcesByCategory(Categories category)
        {
            string cat = $"category={category.ToString()}";
            return _httpRequestHelper.MakeSourceRequest(Constants.SOURCE_URL, cat);

        }

        public string SetQueryString(SourceRequest request)
        {
            List<string> queryParams = new List<string>();
            if (request.Sources.Count > 0)
                queryParams.Add("sources=" + string.Join(",", (IEnumerable<string>)request.Sources));
            Categories? category = request.Category;
            if (category.HasValue)
            {
                List<string> stringList = queryParams;
                string str1 = "category=";
                category = request.Category;
                string lowerInvariant = category.Value.ToString().ToLowerInvariant();
                string str2 = str1 + lowerInvariant;
                stringList.Add(str2);
            }
            Languages? language = request.Language;
            if (language.HasValue)
            {
                List<string> stringList = queryParams;
                string str1 = "language=";
                language = request.Language;
                string lowerInvariant = language.Value.ToString().ToLowerInvariant();
                string str2 = str1 + lowerInvariant;
                stringList.Add(str2);
            }
            Countries? country = request.Country;
            if (country.HasValue)
            {
                List<string> stringList = queryParams;
                string str1 = "country=";
                country = request.Country;
                string lowerInvariant = country.Value.ToString().ToLowerInvariant();
                string str2 = str1 + lowerInvariant;
                stringList.Add(str2);
            }
            if (request.Page > 1)
                queryParams.Add("page=" + (object)request.Page);
            if (request.PageSize > 0)
                queryParams.Add("pageSize=" + (object)request.PageSize);
            string querystring = string.Join("&", queryParams.ToArray());

            return querystring;
        }
    }
}
