using System.ComponentModel;
using System.Web.Mvc;
using SitefinityWebApp.Mvc.Models;
using Telerik.Sitefinity.Mvc;

namespace SitefinityWebApp.Mvc.Controllers
{
    [ControllerToolboxItem(
        Name = "HelloWorld",
        Title = "Hello World",
        SectionName = "MvcWidgets",
        CssClass = "sfMvcIcn")]
    public class HelloWorldController : Controller
    {
        [Category("Settings")]
        [DisplayName("Message")]
        [Description("Enter the message to display")]
        public string Message { get; set; }

        [Category("Advanced")]
        [DisplayName("CSS Class")]
        [Description("CSS class to apply to the widget")]
        public string CssClass { get; set; }

        public ActionResult Index()
        {
            var model = new HelloWorldModel
            {
                Message = !string.IsNullOrEmpty(this.Message) ? this.Message : "Hello from Sitefinity MVC Widget!",
                CssClass = this.CssClass
            };

            return View("Default", model);
        }
    }
}
