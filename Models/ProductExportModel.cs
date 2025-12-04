using System;
using System.Collections.Generic;

namespace SitefinityWebApp.Mvc.Models
{
    public class ProductExportModel
    {
        public string Title { get; set; }
        public string ButtonText { get; set; }
        public string ButtonCssClass { get; set; }
        public string ExportFileName { get; set; }
        public int TotalProductsCount { get; set; }
        public string Message { get; set; }
        public bool IncludeHeaders { get; set; }
    }
}
