using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Text;
using System.Web.Mvc;
using SitefinityWebApp.Mvc.Models;
using Telerik.Sitefinity.Mvc;

namespace SitefinityWebApp.Mvc.Controllers
{
    /// <summary>
    /// Product Export Widget - Generates CSV file with products and provides as ZIP download
    /// </summary>
    [ControllerToolboxItem(
        Name = "ProductExport",
        Title = "Product Export",
        SectionName = "MvcWidgets",
        CssClass = "sfMvcIcn")]
    public class ProductExportController : Controller
    {
        #region Properties

        [Category("Content")]
        [DisplayName("Widget Title")]
        [Description("Title displayed above the export button")]
        public string Title { get; set; }

        [Category("Content")]
        [DisplayName("Button Text")]
        [Description("Text displayed on the export button")]
        public string ButtonText { get; set; }

        [Category("Content")]
        [DisplayName("Message")]
        [Description("Additional message or instructions for users")]
        public string Message { get; set; }

        [Category("Settings")]
        [DisplayName("Export File Name")]
        [Description("Name of the CSV file (without extension)")]
        public string ExportFileName { get; set; }

        [Category("Settings")]
        [DisplayName("Include CSV Headers")]
        [Description("Include column headers in the CSV file")]
        public bool IncludeHeaders { get; set; }

        [Category("Advanced")]
        [DisplayName("Button CSS Class")]
        [Description("Custom CSS class for the export button")]
        public string ButtonCssClass { get; set; }

        #endregion

        #region Constructor

        public ProductExportController()
        {
            this.Title = "Export Products";
            this.ButtonText = "Download Products (CSV/ZIP)";
            this.Message = "Click the button below to download all products as a CSV file in a ZIP archive.";
            this.ExportFileName = "products";
            this.IncludeHeaders = true;
            this.ButtonCssClass = "btn btn-primary btn-lg";
        }

        #endregion

        #region Actions

        /// <summary>
        /// Default action - displays the export widget UI
        /// </summary>
        public ActionResult Index()
        {
            var products = GetAllProducts();

            var model = new ProductExportModel
            {
                Title = this.Title,
                ButtonText = this.ButtonText,
                ButtonCssClass = this.ButtonCssClass,
                ExportFileName = this.ExportFileName,
                Message = this.Message,
                IncludeHeaders = this.IncludeHeaders,
                TotalProductsCount = products.Count
            };

            return View("Default", model);
        }

        /// <summary>
        /// Generates and downloads the CSV file as a ZIP archive
        /// </summary>
        [HttpGet]
        public ActionResult Download()
        {
            try
            {
                var products = GetAllProducts();
                var csvContent = GenerateCsvContent(products, this.IncludeHeaders);
                var zipBytes = CreateZipArchive(csvContent, this.ExportFileName);

                var zipFileName = $"{this.ExportFileName}_{DateTime.Now:yyyyMMdd_HHmmss}.zip";

                return File(zipBytes, "application/zip", zipFileName);
            }
            catch (Exception ex)
            {
                // Log the error (in production, use proper logging)
                System.Diagnostics.Debug.WriteLine($"Error exporting products: {ex.Message}");

                // Return error message
                TempData["ErrorMessage"] = "An error occurred while generating the export file. Please try again.";
                return RedirectToAction("Index");
            }
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Gets all products - in production, this would query your actual data source
        /// </summary>
        private List<Product> GetAllProducts()
        {
            // Sample data - replace with actual data retrieval
            // In production, you might query from:
            // - Sitefinity Dynamic Content
            // - Database
            // - External API
            // - Custom data source

            return new List<Product>
            {
                new Product
                {
                    Name = "Monitor 27\" 4K UHD",
                    Description = "Professional 4K monitor with IPS panel and HDR support",
                    Price = 499.99m,
                    ImageUrl = "/images/monitor-27-4k.jpg"
                },
                new Product
                {
                    Name = "Gaming Monitor 32\" Curved",
                    Description = "High refresh rate curved gaming monitor with G-Sync",
                    Price = 599.99m,
                    ImageUrl = "/images/monitor-32-curved.jpg"
                },
                new Product
                {
                    Name = "Ultrawide Monitor 34\"",
                    Description = "Ultrawide 21:9 monitor perfect for productivity",
                    Price = 699.99m,
                    ImageUrl = "/images/monitor-34-ultrawide.jpg"
                },
                new Product
                {
                    Name = "Portable Monitor 15.6\"",
                    Description = "USB-C portable monitor for on-the-go professionals",
                    Price = 249.99m,
                    ImageUrl = "/images/monitor-portable.jpg"
                },
                new Product
                {
                    Name = "Professional Monitor 32\" 4K",
                    Description = "Color-accurate monitor for creative professionals",
                    Price = 899.99m,
                    ImageUrl = "/images/monitor-pro-32.jpg"
                },
                new Product
                {
                    Name = "Budget Monitor 24\" FHD",
                    Description = "Affordable full HD monitor for everyday use",
                    Price = 149.99m,
                    ImageUrl = "/images/monitor-24-fhd.jpg"
                },
                new Product
                {
                    Name = "Touch Screen Monitor 24\"",
                    Description = "Interactive touchscreen monitor with 10-point touch",
                    Price = 349.99m,
                    ImageUrl = "/images/monitor-touch-24.jpg"
                },
                new Product
                {
                    Name = "Vertical Monitor 27\"",
                    Description = "Pivoting monitor ideal for coding and document editing",
                    Price = 299.99m,
                    ImageUrl = "/images/monitor-vertical-27.jpg"
                },
                new Product
                {
                    Name = "Gaming Monitor 24\" 240Hz",
                    Description = "High refresh rate esports monitor with 1ms response time",
                    Price = 399.99m,
                    ImageUrl = "/images/monitor-gaming-240hz.jpg"
                },
                new Product
                {
                    Name = "Designer Monitor 27\" 5K",
                    Description = "Retina 5K display with P3 wide color gamut",
                    Price = 1299.99m,
                    ImageUrl = "/images/monitor-designer-5k.jpg"
                }
            };
        }

        /// <summary>
        /// Generates CSV content from product list
        /// </summary>
        private string GenerateCsvContent(List<Product> products, bool includeHeaders)
        {
            var csv = new StringBuilder();

            // Add headers if requested
            if (includeHeaders)
            {
                csv.AppendLine("Name,Description,Price,Image URL");
            }

            // Add product rows
            foreach (var product in products)
            {
                // Escape fields that contain commas, quotes, or line breaks
                var name = EscapeCsvField(product.Name);
                var description = EscapeCsvField(product.Description);
                var price = product.Price.ToString("F2");
                var imageUrl = EscapeCsvField(product.ImageUrl);

                csv.AppendLine($"{name},{description},{price},{imageUrl}");
            }

            return csv.ToString();
        }

        /// <summary>
        /// Escapes CSV field by wrapping in quotes if it contains special characters
        /// </summary>
        private string EscapeCsvField(string field)
        {
            if (string.IsNullOrEmpty(field))
                return string.Empty;

            // If field contains comma, quote, or newline, wrap in quotes and escape internal quotes
            if (field.Contains(",") || field.Contains("\"") || field.Contains("\n") || field.Contains("\r"))
            {
                return $"\"{field.Replace("\"", "\"\"")}\"";
            }

            return field;
        }

        /// <summary>
        /// Creates a ZIP archive containing the CSV file
        /// Uses SharpZipLib library which is included in Sitefinity
        /// </summary>
        private byte[] CreateZipArchive(string csvContent, string fileName)
        {
            using (var inputStream = new MemoryStream())
            using (var outputStream = new MemoryStream())
            {
                // Create a new entry for the CSV file
                var csvFileName = $"{fileName}.csv";
                var entry = new Telerik.Sitefinity.Utilities.Zip.ZipFile(csvFileName);
                //entry.DateTime = DateTime.Now;
                entry.AddFileStream(csvFileName, string.Empty, inputStream);

                // Write CSV content to the zip entry
                var contentBytes = Encoding.UTF8.GetBytes(csvContent);
                inputStream.Write(contentBytes, 0, contentBytes.Length);
                entry.Save(outputStream);
                return outputStream.ToArray();
            }
        }
    }

    #endregion

}
