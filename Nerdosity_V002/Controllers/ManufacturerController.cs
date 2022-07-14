using Microsoft.AspNetCore.Mvc;
using Nerdosity_V002.Models;

namespace Nerdosity_V002.Controllers
{
    public class ManufacturerController : Controller
    {
        private Repository<Manufacturer> data { get; set; }
        public ManufacturerController(NerdosityContext ctx) => data = new Repository<Manufacturer>(ctx);

        public IActionResult Index() => RedirectToAction("List");

        public ViewResult List(GridDTO vals)
        {
            string defaultSort = nameof(Manufacturer.Name);
            var builder = new GridBuilder(HttpContext.Session, vals, defaultSort);
            builder.SaveRouteSegments();

            var options = new QueryOptions<Manufacturer>
            {
                Include = "ProductManufacturers.Product",
                PageNumber = builder.CurrentRoute.PageNumber,
                PageSize = builder.CurrentRoute.PageSize,
                OrderByDirection = builder.CurrentRoute.SortDirection
            };
            if (builder.CurrentRoute.SortField.EqualsNoCase(defaultSort))
                options.OrderBy = m => m.Name;
            else
                options.OrderBy = m => m.Address;

            var vm = new ManufacturerListViewModel
            {
                Manufacturers = data.List(options),
                CurrentRoute = builder.CurrentRoute,
                TotalPages = builder.GetTotalPages(data.Count)
            };

            return View(vm);
        }

        public IActionResult Details(int id)
        {
            var manufacturer = data.Get(new QueryOptions<Manufacturer>
            {
                Include = "ProductManufacturers.Product",
                Where = m => m.ManufacturerId == id
            });
            return View(manufacturer);
        }
    }
}
