using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web.Mvc;
using MonitorsArena.Widgets.Models;
using Telerik.Sitefinity.Mvc;

namespace MonitorsArena.Widgets.Controllers
{
    /// <summary>
    /// News Ticker widget controller - displays scrolling or static news items
    /// This widget is packaged in a separate class library for reusability
    /// </summary>
    [ControllerToolboxItem(
        Name = "NewsTicker",
        Title = "News Ticker",
        SectionName = "Custom Widgets",
        CssClass = "sfNewsIcn")]
    public class NewsTickerController : Controller
    {
        #region Properties

        [Category("Content")]
        [DisplayName("Widget Title")]
        [Description("The title displayed above the news ticker")]
        public string Title { get; set; }

        [Category("Settings")]
        [DisplayName("Number of Items")]
        [Description("Number of news items to display")]
        public int ItemCount { get; set; }

        [Category("Settings")]
        [DisplayName("Auto Scroll")]
        [Description("Enable automatic scrolling of news items")]
        public bool AutoScroll { get; set; }

        [Category("Settings")]
        [DisplayName("Refresh Interval (seconds)")]
        [Description("Time in seconds between scrolling to next item (only if auto scroll is enabled)")]
        public int RefreshInterval { get; set; }

        [Category("Settings")]
        [DisplayName("Filter by Category")]
        [Description("Filter news items by category (leave empty to show all)")]
        public string CategoryFilter { get; set; }

        [Category("Advanced")]
        [DisplayName("CSS Class")]
        [Description("CSS class to apply to the widget container")]
        public string CssClass { get; set; }

        #endregion

        #region Constructor

        public NewsTickerController()
        {
            this.ItemCount = 5;
            this.Title = "Latest News";
            this.AutoScroll = true;
            this.RefreshInterval = 5;
            this.CategoryFilter = string.Empty;
        }

        #endregion

        #region Actions

        /// <summary>
        /// Default action - renders the widget
        /// </summary>
        public ActionResult Index()
        {
            var model = new NewsTickerModel
            {
                Title = this.Title,
                CssClass = this.CssClass,
                AutoScroll = this.AutoScroll,
                RefreshInterval = this.RefreshInterval,
                NewsItems = GetNewsItems(this.ItemCount, this.CategoryFilter)
            };

            return View("Default", model);
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Gets news items - in a real implementation, this would query Sitefinity's News module
        /// For this example, we're returning sample data
        /// </summary>
        private List<NewsItem> GetNewsItems(int count, string category)
        {
            // Sample data - in production, you would query Sitefinity's News module
            // Example: Use NewsManager to get actual news items
            var allNews = new List<NewsItem>
            {
                new NewsItem 
                { 
                    Headline = "Breaking: New Product Launch Announced", 
                    Summary = "Company announces exciting new product lineup for 2025",
                    PublishedDate = DateTime.Now.AddHours(-2),
                    Category = "Products",
                    Url = "/news/new-product-launch"
                },
                new NewsItem 
                { 
                    Headline = "Industry Report: Market Growth Continues", 
                    Summary = "Latest industry analysis shows continued market expansion",
                    PublishedDate = DateTime.Now.AddHours(-5),
                    Category = "Industry",
                    Url = "/news/market-growth"
                },
                new NewsItem 
                { 
                    Headline = "Customer Success Story: Enterprise Implementation", 
                    Summary = "Major enterprise successfully implements our solution",
                    PublishedDate = DateTime.Now.AddHours(-8),
                    Category = "Success Stories",
                    Url = "/news/customer-success"
                },
                new NewsItem 
                { 
                    Headline = "Tech Update: New Features Released", 
                    Summary = "Latest software update includes highly requested features",
                    PublishedDate = DateTime.Now.AddHours(-12),
                    Category = "Technology",
                    Url = "/news/new-features"
                },
                new NewsItem 
                { 
                    Headline = "Event Announcement: Annual Conference 2025", 
                    Summary = "Save the date for our biggest event of the year",
                    PublishedDate = DateTime.Now.AddHours(-24),
                    Category = "Events",
                    Url = "/news/conference-2025"
                },
                new NewsItem 
                { 
                    Headline = "Partnership: Strategic Alliance Formed", 
                    Summary = "New partnership will bring enhanced capabilities to customers",
                    PublishedDate = DateTime.Now.AddDays(-2),
                    Category = "Partnerships",
                    Url = "/news/strategic-alliance"
                },
                new NewsItem 
                { 
                    Headline = "Award Recognition: Industry Excellence", 
                    Summary = "Company receives prestigious industry award for innovation",
                    PublishedDate = DateTime.Now.AddDays(-3),
                    Category = "Company",
                    Url = "/news/industry-award"
                }
            };

            // Filter by category if specified
            var filteredNews = string.IsNullOrEmpty(category) 
                ? allNews 
                : allNews.Where(n => n.Category.Equals(category, StringComparison.OrdinalIgnoreCase)).ToList();

            // Return the requested number of items
            return filteredNews.Take(count).ToList();
        }

        #endregion
    }
}
