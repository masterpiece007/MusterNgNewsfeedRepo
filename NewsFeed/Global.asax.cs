using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq; 
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using NewsApi.Integration;
using NewsApi.Integration.Helper;
using NewsApi.Integration.Model;
using NewsAPI.Constants;
using NewsFeed.EF;
using NewsFeed.Utilities;

namespace NewsFeed
{
    public class MvcApplication : System.Web.HttpApplication
    {
        static string api = ConfigurationManager.AppSettings["api"];
        static string url = ConfigurationManager.AppSettings["url"];

        static NewsApiDriver driver = new NewsApiDriver(api, url);
        static NewsApiManager newsApiManager = new NewsApiManager(driver);
        static NewsSourceHelper sourceHelper = new NewsSourceHelper(newsApiManager);
        static TopNewsHelper topNewsHelper = new TopNewsHelper(newsApiManager);
        static AllNewsHelper _allNewsHelper = new AllNewsHelper(newsApiManager);

        private NewsFeedEntities _newsFeedEntities;
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            _newsFeedEntities = new NewsFeedEntities();
            SeedSourceCategories(_newsFeedEntities);
            SeedCountry(_newsFeedEntities);
            SeedLanguage(_newsFeedEntities);
            SeedNewsSource(_newsFeedEntities);
            FetchDailyNews(_newsFeedEntities);
        }

        private void FetchDailyNews(NewsFeedEntities newsFeedEntities)
        {
            try
            {
                var sources = newsFeedEntities.Sources.Select(c => c.Name).ToList();
                foreach (var source in sources)
                {
                    var allNews = _allNewsHelper.GetAllNews(new List<string>
                    {
                        source
                    });
                    if (allNews.Status == Statuses.Ok)
                    {
                        foreach (var allNew in allNews.Articles)
                        {
                            var exist = newsFeedEntities.News.FirstOrDefault(c => c.Title == allNew.Title);
                            if (exist == null)
                            {
                                newsFeedEntities.News.Add(new News
                                {
                                    Title = allNew.Title,
                                    Author = allNew.Author,
                                    Description = allNew.Description,
                                    PublishedAt = allNew.PublishedAt.Value,
                                    Source = allNew.Source.Name,
                                    Url = allNew.Url,
                                    UrlToImage = allNew.UrlToImage,
                                    DateCreated = DateTime.Now
                                });
                                newsFeedEntities.SaveChanges();
                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
        private void SeedNewsSource(NewsFeedEntities newsFeedEntities)
        {
            try
            {
                 
                    var data = sourceHelper.AllSoures().Result;
                if (data.SourceResponses != null)
                {
                    foreach (var response in data.SourceResponses)
                    {
                        var dbNewsSource =
                            newsFeedEntities.Sources.FirstOrDefault(c => response.Id.ToString() == c.Name);
                        if (dbNewsSource == null)
                        {
                            newsFeedEntities.Sources.Add(new Source
                            {
                                Name = response.Id.ToString(),
                                DateCreated = DateTime.Now,
                                Category = response.Category,
                                Country = response.Country,
                                Description = response.Description,
                                ImagePath = response.ImagePath,
                                Language = response.Language,
                                Url = response.Url.AbsoluteUri,
                                NameId = response.Name
                            });

                            newsFeedEntities.SaveChanges();
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
            
        }
        public void SeedSourceCategories(NewsFeedEntities entities)
        {
            var categories = Enum.GetValues(typeof(Categories)).Cast<Categories>()
                .ToDictionary(t => t.ToDescription(), t => (int)t);

            foreach (var category in categories)
            {
                var dbCategory = entities.SourceCategories.FirstOrDefault(c=> c.Name == category.Key && c.EnumId == category.Value);
                if (dbCategory == null)
                {
                    entities.SourceCategories.Add(new SourceCategory
                    {
                        DateCreated = DateTime.Now,
                        Name = category.Key,
                        EnumId = category.Value
                    });
                }
            }

            entities.SaveChanges();
        }
        public void SeedLanguage(NewsFeedEntities entities)
        {
            var languages = Enum.GetValues(typeof(Languages)).Cast<Languages>()
                .ToDictionary(t => t.ToDescription(), t => (int)t);

            foreach (var language in languages)
            {
                var dbCategory = entities.NewsLanguages.FirstOrDefault(c => c.Name == language.Key);
                if (dbCategory == null)
                {
                    entities.NewsLanguages.Add(new NewsLanguage
                    {
                        DateCreated = DateTime.Now,
                        Name = language.Key,
                        EnumId = language.Value
                    });
                }
            }

            entities.SaveChanges();
        }
        public void SeedCountry(NewsFeedEntities entities)
        {
            var countries = Enum.GetValues(typeof(Countries)).Cast<Countries>()
                .ToDictionary(t => t.ToDescription(), t => (int)t);

            foreach (var country in countries)
            {
                var dbCountry = entities.NewsCountries.FirstOrDefault(c => c.Name == country.Key);
                if (dbCountry == null)
                {
                    entities.NewsCountries.Add(new NewsCountry
                    {
                        DateCreated = DateTime.Now,
                        Name = country.Key,
                        EnumId = country.Value
                    });
                }
            }

            entities.SaveChanges();
        }
    }
}
