using System.Collections.Generic;
using System.ComponentModel;
using System.Web.Mvc;
using SitefinityWebApp.Mvc.Models;
using Telerik.Sitefinity.Mvc;

namespace SitefinityWebApp.Mvc.Controllers
{
    [ControllerToolboxItem(
        Name = "ProductShowcase",
        Title = "Product Showcase",
        SectionName = "Custom Widgets",
        CssClass = "sfMvcIcn")]
    public class ProductShowcaseController : Controller
    {
        [Category("Content")]
        [DisplayName("Widget Title")]
        [Description("The title displayed above the products")]
        public string Title { get; set; }

        [Category("Settings")]
        [DisplayName("Number of Products")]
        [Description("Number of products to display")]
        public int ProductCount { get; set; }

        [Category("Advanced")]
        [DisplayName("CSS Class")]
        [Description("CSS class to apply to the widget container")]
        public string CssClass { get; set; }

        public ProductShowcaseController()
        {
            this.ProductCount = 3;
            this.Title = "Featured Products";
        }

        public ActionResult Index()
        {
            var model = new ProductShowcaseModel
            {
                Title = this.Title,
                CssClass = this.CssClass,
                Products = GetSampleProducts(this.ProductCount)
            };

            return View("List.ProductGrid", model);
        }

        private List<Product> GetSampleProducts(int count)
        {
            var products = new List<Product>
            {
                new Product { Name = "Product 1", Description = "Description for product 1", Price = 29.99m, ImageUrl = "/images/product1.jpg" },
                new Product { Name = "Product 2", Description = "Description for product 2", Price = 49.99m, ImageUrl = "/images/product2.jpg" },
                new Product { Name = "Product 3", Description = "Description for product 3", Price = 19.99m, ImageUrl = "/images/product3.jpg" },
                new Product { Name = "Product 4", Description = "Description for product 4", Price = 39.99m, ImageUrl = "/images/product4.jpg" },
                new Product { Name = "Product 5", Description = "Description for product 5", Price = 59.99m, ImageUrl = "/images/product5.jpg" }
            };

            return products.GetRange(0, System.Math.Min(count, products.Count));
        }
    }
}
