using System;
using System.Collections.Generic;

namespace MonitorsArena.Widgets.Models
{
    /// <summary>
    /// Model for the News Ticker widget
    /// </summary>
    public class NewsTickerModel
    {
        public string Title { get; set; }
        public List<NewsItem> NewsItems { get; set; }
        public string CssClass { get; set; }
        public int RefreshInterval { get; set; }
        public bool AutoScroll { get; set; }
        public string TemplateName { get; set; }
    }

    /// <summary>
    /// Represents a single news item
    /// </summary>
    public class NewsItem
    {
        public string Headline { get; set; }
        public string Summary { get; set; }
        public DateTime PublishedDate { get; set; }
        public string Url { get; set; }
        public string Category { get; set; }
    }
}
