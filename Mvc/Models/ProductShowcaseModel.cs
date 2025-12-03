using System;
using System.Collections.Generic;

namespace SitefinityWebApp.Mvc.Models
{
    public class ProductShowcaseModel
    {
        public string Title { get; set; }
        public List<Product> Products { get; set; }
        public string TemplateName { get; set; }
        public string CssClass { get; set; }
    }

    public class Product
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string ImageUrl { get; set; }
    }
}
