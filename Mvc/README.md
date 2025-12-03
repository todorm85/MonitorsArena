# Custom Sitefinity MVC Widgets

This directory contains custom MVC widgets created following the [Sitefinity MVC Widget Documentation](https://www.progress.com/documentation/sitefinity-cms/create-widgets-mvc).

## Structure

```
Mvc/
??? Controllers/
?   ??? HelloWorldController.cs
?   ??? ProductShowcaseController.cs
??? Models/
?   ??? HelloWorldModel.cs
?   ??? ProductShowcaseModel.cs
??? Views/
    ??? HelloWorld/
    ?   ??? Default.cshtml
    ??? ProductShowcase/
        ??? List.ProductGrid.cshtml
        ??? List.ProductList.cshtml
```

## Widgets Included

### 1. Hello World Widget

A simple widget demonstrating the basic structure of a Sitefinity MVC widget.

**Features:**
- Customizable message property
- CSS class support for styling
- Simple default view template

**Properties:**
- `Message` (string): The message to display
- `CssClass` (string): CSS class for styling

**Controller:** `HelloWorldController.cs`
**Model:** `HelloWorldModel.cs`
**View:** `Default.cshtml`

### 2. Product Showcase Widget

A more advanced widget demonstrating multiple templates and property categories.

**Features:**
- Displays a configurable number of products
- Two view templates (Grid and List)
- Property categories (Content, Settings, Advanced)
- Bootstrap 4/5 compatible styling

**Properties:**
- `Title` (string): Widget title
- `ProductCount` (int): Number of products to display
- `CssClass` (string): CSS class for styling

**Controller:** `ProductShowcaseController.cs`
**Model:** `ProductShowcaseModel.cs`
**Views:**
- `List.ProductGrid.cshtml` - Grid layout with cards
- `List.ProductList.cshtml` - List layout

## How to Use

### Step 1: Build the Project

After adding these files, build your Sitefinity project to register the widgets.

### Step 2: Access Widgets in Sitefinity

1. Log in to Sitefinity Backend
2. Navigate to a page and enter Edit mode
3. Look for your widgets in the widget selector under "MvcWidgets" section
4. Drag and drop the widget onto the page

### Step 3: Configure Widget Properties

1. Click on the widget on the page
2. Click "Edit" to open the widget designer
3. Configure the properties:
   - For Hello World: Set custom message and CSS class
   - For Product Showcase: Set title, number of products, and CSS class
4. Save and publish the page

### Step 4: Switch Templates (Product Showcase)

1. In widget designer, look for "Template" dropdown
2. Select between:
   - `List.ProductGrid` - Card-based grid layout
   - `List.ProductList` - Simple list layout

## Key Concepts

### Controller Attributes

```csharp
[ControllerToolboxItem(
    Name = "WidgetName",           // Internal name
    Title = "Display Title",        // Display name in widget selector
    SectionName = "MvcWidgets",     // Toolbox section
    CssClass = "sfMvcIcn")]         // CSS class for icon
```

### Property Attributes

```csharp
[Category("Settings")]              // Groups properties in designer
[DisplayName("Property Label")]     // Label shown in designer
[Description("Help text")]          // Tooltip description
public string PropertyName { get; set; }
```

### View Naming Convention

- Default view: `Default.cshtml`
- List views: `List.TemplateName.cshtml`
- Detail views: `Detail.TemplateName.cshtml`

## Creating Your Own Widget

### Step 1: Create the Model

```csharp
// Mvc/Models/YourWidgetModel.cs
namespace SitefinityWebApp.Mvc.Models
{
    public class YourWidgetModel
    {
        // Add properties for your widget data
        public string Title { get; set; }
        public string Content { get; set; }
    }
}
```

### Step 2: Create the Controller

```csharp
// Mvc/Controllers/YourWidgetController.cs
using System.Web.Mvc;
using SitefinityWebApp.Mvc.Models;
using Telerik.Sitefinity.Mvc;

namespace SitefinityWebApp.Mvc.Controllers
{
    [ControllerToolboxItem(
        Name = "YourWidget",
        Title = "Your Widget",
        SectionName = "MvcWidgets",
        CssClass = "sfMvcIcn")]
    public class YourWidgetController : Controller
    {
        // Add configurable properties here
        public string Title { get; set; }
        
        public ActionResult Index()
        {
            var model = new YourWidgetModel
            {
                Title = this.Title
            };
            
            return View("Default", model);
        }
    }
}
```

### Step 3: Create the View

```razor
@* Mvc/Views/YourWidget/Default.cshtml *@
@model SitefinityWebApp.Mvc.Models.YourWidgetModel

<div class="your-widget">
    <h2>@Model.Title</h2>
    <!-- Add your HTML markup here -->
</div>
```

### Step 4: Build and Test

1. Build the project
2. Restart the application if necessary
3. Go to Sitefinity Backend and find your widget

## Additional Resources

- [Sitefinity MVC Widget Documentation](https://www.progress.com/documentation/sitefinity-cms/create-widgets-mvc)
- [Widget Designer Documentation](https://www.progress.com/documentation/sitefinity-cms/designer-mvc-widgets)
- [Widget Templates Documentation](https://www.progress.com/documentation/sitefinity-cms/widget-templates-mvc)

## Notes

- Widgets are automatically discovered by Sitefinity when the project is built
- Widget properties with `[Category]` attributes are grouped in the designer
- Multiple view templates allow users to choose different layouts
- Follow the naming convention for views to ensure they appear in template selector
- Use Bootstrap classes for consistency with Sitefinity's default styling
