using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using NewsApi.Integration;
using NewsApi.Integration.Helper;
using NewsAPI.Constants;
using NewsAPI.Models;
using NewsFeed.EF;
using NewsFeed.Models;
using Source = NewsFeed.EF.Source;

namespace NewsFeed.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        static string api = ConfigurationManager.AppSettings["api"];
        static string url = ConfigurationManager.AppSettings["url"];

        static NewsApiDriver driver = new NewsApiDriver(api, url);
        static NewsApiManager newsApiManager = new NewsApiManager(driver);
        static NewsSourceHelper sourceHelper = new NewsSourceHelper(newsApiManager);
        static TopNewsHelper topNewsHelper = new TopNewsHelper(newsApiManager);

        private NewsFeedEntities _newsFeedEntities;

        public HomeController()
        {
            _newsFeedEntities = new NewsFeedEntities();
        }
        public ActionResult Index()
        {
            return RedirectToAction("AllNewsFeed");
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult Subscription()
        {
            var sources = _newsFeedEntities.SourceCategories.Select(c => new
            {
                id = c.EnumId,
                c.Name
            }).ToList();

            var listItems = new List<SelectListItem>();
            var userId = User.Identity.GetUserId();
            var userNewsCategories = _newsFeedEntities.UserNewsCategories.Where(c => c.UserId == userId).ToList();
            if (userNewsCategories.Any())
            {
                foreach (var userNewsCategory in userNewsCategories)
                {
                    listItems.Add(new SelectListItem
                    {
                        Text = userNewsCategory.NewsCategory,
                        Value = userNewsCategory.NewsCategory,
                        Selected = true
                    });
                }
            }

            foreach (var source in sources)
            {
                var isInList = listItems.Any(c => c.Text == source.Name);
                if (!isInList)
                {
                    listItems.Add(new SelectListItem
                    {
                        Text = source.Name,
                        Value = source.Name,
                        Selected = false
                    });
                }
            }

            ViewBag.ListItems = listItems;
            ViewBag.SelectedCategories = userNewsCategories;
            return View(new Subscription
            {
                Items = listItems
            });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Subscription(Subscription subscription)
        {
            if (ModelState.IsValid)
            {

                var userID = User.Identity.GetUserId();

                var userNewsCategories = _newsFeedEntities.UserNewsCategories.Where(c => c.UserId == userID).ToList();
                _newsFeedEntities.UserNewsCategories.RemoveRange(userNewsCategories);
                _newsFeedEntities.SaveChanges();

                if (subscription.SouceIds.Any())
                {
                    foreach (var subscriptionSouceId in subscription.SouceIds)
                    {
                        _newsFeedEntities.UserNewsCategories.AddOrUpdate(new UserNewsCategory
                        {
                            UserId = userID,
                            NewsCategory = subscriptionSouceId,
                            DateCreated = DateTime.Now
                        });


                        _newsFeedEntities.SaveChanges();
                    }
                }

                return RedirectToAction("Index");
            }

            return View(subscription);
        }

        [Authorize]
        public ActionResult MyNewsFeed()
        {
            var userId = User.Identity.GetUserId();
            var userNewsCategories = _newsFeedEntities.UserNewsCategories.Where(c => c.UserId == userId).ToList();
            var sourceForUserCategories = new List<string>();
            var newsPerCategory = new List<News>();

            if (userNewsCategories.Any())
            {
                var userCategoriesString = userNewsCategories.Select(c => c.NewsCategory).ToList();
                sourceForUserCategories = _newsFeedEntities.Sources.Where(x =>
                    userCategoriesString.Contains(x.Category)).Select(c=> c.Name).ToList();

                newsPerCategory = _newsFeedEntities.News.Where(c => sourceForUserCategories.Contains(c.Source))
                    .ToList();
            }

            return View(newsPerCategory);
        }

        [HttpGet]
        public ActionResult MyNewsFeedData()
        {
            try
            {
                var userId = User.Identity.GetUserId();
                var userNewsCategories = _newsFeedEntities.UserNewsCategories.Where(c => c.UserId == userId).ToList();
                var sourceForUserCategories = new List<string>();
                var newsPerCategory = new List<News>();

                if (userNewsCategories.Any())
                {
                    var userCategoriesString = userNewsCategories.Select(c => c.NewsCategory).ToList();
                    sourceForUserCategories = _newsFeedEntities.Sources.Where(x =>
                        userCategoriesString.Contains(x.Category)).Select(c => c.Name).ToList();

                    newsPerCategory = _newsFeedEntities.News.Where(c => sourceForUserCategories.Contains(c.Source))
                        .ToList();

                    return new JsonResult
                    {
                        Data = new
                        {
                            Status = HttpStatusCode.OK,
                            Data = newsPerCategory
                        },
                        JsonRequestBehavior = JsonRequestBehavior.AllowGet
                    };
                }

                return new JsonResult
                {
                    Data = new
                    {
                        Status = HttpStatusCode.BadRequest,
                        Data = "Error can not fetch news at this period."
                    },
                    JsonRequestBehavior = JsonRequestBehavior.AllowGet
                };
            }
            catch (Exception e)
            {
                return new JsonResult
                {
                    Data = new
                    {
                        Status = HttpStatusCode.BadRequest,
                        Data = e.Message
                    },
                    JsonRequestBehavior = JsonRequestBehavior.AllowGet
                };
            }
        }

        public ActionResult AllNewsFeed()
        {
            return View();
        }

        public ActionResult AllNewsFeedData()
        {
            try
            {
                var news = _newsFeedEntities.News.ToList();

                if (news.Any())
                {
                     
                    return new JsonResult
                    {
                        Data = new
                        {
                            Status = HttpStatusCode.OK,
                            Data = news
                        },
                        JsonRequestBehavior = JsonRequestBehavior.AllowGet
                    };
                }

                return new JsonResult
                {
                    Data = new
                    {
                        Status = HttpStatusCode.BadRequest,
                        Data = "Error can not fetch news at this period."
                    },
                    JsonRequestBehavior = JsonRequestBehavior.AllowGet
                };
            }
            catch (Exception e)
            {
                return new JsonResult
                {
                    Data = new
                    {
                        Status = HttpStatusCode.BadRequest,
                        Data = e.Message
                    },
                    JsonRequestBehavior = JsonRequestBehavior.AllowGet
                };
            }
        }
    }
}