using Microsoft.AspNetCore.Mvc;
using Nerdosity_V002.Models;


namespace Nerdosity_V002.Controllers
{
    public class ProductController : Controller
    {
        private NerdosityUnitOfWork data { get; set; }
        public ProductController(NerdosityContext ctx) => data = new NerdosityUnitOfWork(ctx);

        public RedirectToActionResult Index() => RedirectToAction("List");

        public ViewResult List(ProductsGridDTO values)
        {
            var builder = new ProductsGridBuilder(HttpContext.Session, values,
                defaultSortField: nameof(Product.Name));

            var options = new ProductQueryOptions
            {
                Include = "ProductManufacturers.Manufacturer, Category",
                OrderByDirection = builder.CurrentRoute.SortDirection,
                PageNumber = builder.CurrentRoute.PageNumber,
                PageSize = builder.CurrentRoute.PageSize
            };
            options.SortFilter(builder);

            var vm = new ProductListViewModel
            {
                Products = data.Products.List(options),
                Manufacturers = data.Manufacturers.List(new QueryOptions<Manufacturer> {
                    OrderBy = m => m.Name }),
                Categories = data.Categories.List(new QueryOptions<Category> {
                    OrderBy = c => c.Name}),
                CurrentRoute = builder.CurrentRoute,
                TotalPages = builder.GetTotalPages(data.Products.Count)
            };

            return View(vm);
        }

        public ViewResult Details(int id)
        {
            var product = data.Products.Get(new QueryOptions<Product>
            {
                Include = "ProductManufacturers.Manufacturer, Category",
                Where = p => p.ProductId == id
            });
            return View(product);
        }

        [HttpPost]
        public RedirectToActionResult Filter(string[] filter, bool clear = false)
        {
            var builder = new ProductsGridBuilder(HttpContext.Session);

            if (clear)
            {
                builder.ClearFilterSegments();
            }
            else
            {
                var manufacturer = data.Manufacturers.Get(filter[0].ToInt());
                builder.LoadFilterSegments(filter, manufacturer);
            }

            builder.SaveRouteSegments();
            return RedirectToAction("List", builder.CurrentRoute);
        }
    }
}
