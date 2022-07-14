using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Nerdosity_V002.Models;


namespace Nerdosity_V002.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    [Area("Admin")]
    public class ProductController : Controller
    {
        private NerdosityUnitOfWork data { get; set; }
        public ProductController(NerdosityContext ctx) => data = new NerdosityUnitOfWork(ctx);
        public ViewResult Index()
        {
            var search = new SearchData(TempData);
            search.Clear();

            return View();
        }

        [HttpPost]
        public RedirectToActionResult Search(SearchViewModel vm)
        {
            if (ModelState.IsValid)
            {
                var search = new SearchData(TempData)
                {
                    SearchTerm = vm.SearchTerm,
                    Type = vm.Type
                };
                return RedirectToAction("Search");
            }
            else
            {
                return RedirectToAction("Index");
            }
        }

        [HttpGet]
        public ViewResult Search()
        {
            var search = new SearchData(TempData);

            if (search.HasSearchTerm)
            {
                var vm = new SearchViewModel
                {
                    SearchTerm = search.SearchTerm
                };

                var options = new QueryOptions<Product>
                {
                    Include = "Category, ProductManufacturers.Manufacturer"
                };
                if (search.IsProduct)
                {
                    options.Where = p => p.Name.Contains(vm.SearchTerm);
                    vm.Header = $"Search results for Product name '{vm.SearchTerm}'";
                }
                if (search.IsManufacturer)
                {
                    int index = vm.SearchTerm.LastIndexOf(' ');
                    if (index == -1)
                    {
                        options.Where = p => p.ProductManufacturers.Any(
                            pm => pm.Manufacturer.Name.Contains(vm.SearchTerm) ||
                            pm.Manufacturer.Address.Contains(vm.SearchTerm));
                    }
                    else
                    {
                        string first = vm.SearchTerm.Substring(0, index);
                        string last = vm.SearchTerm.Substring(index + 1);
                        options.Where = p => p.ProductManufacturers.Any(
                            pm => pm.Manufacturer.Name.Contains(first) &&
                            pm.Manufacturer.Address.Contains(last));
                    }
                    vm.Header = $"Search results for manufacturer '{vm.SearchTerm}'";
                }
                if (search.IsCategory)
                {
                    options.Where = p => p.CategoryId.Contains(vm.SearchTerm);
                    vm.Header = $"Search results for category ID '{vm.SearchTerm}'";
                }
                vm.Products = data.Products.List(options);
                return View("SearchResults", vm);
            }
            else
            {
                return View("Index");
            }
        }
        [HttpGet]
        public ViewResult Add(int id) => GetProduct(id, "Add");

        [HttpPost]
        public IActionResult Add(ProductViewModel vm)
        {
            if (ModelState.IsValid)
            {
                data.LoadNewProductManufacturers(vm.Product, vm.SelectedManufacturers);
                data.Products.Insert(vm.Product);
                data.Save();

                TempData["message"] = $"{vm.Product.Name} added to Products.";
                return RedirectToAction("Index");
            }
            else
            {
                Load(vm, "Add");
                return View("Product", vm);
            }
        }

        [HttpGet]
        public ViewResult Edit(int id) => GetProduct(id, "Edit");

        [HttpPost]
        public IActionResult Edit(ProductViewModel vm)
        {
            if (ModelState.IsValid)
            {
                data.DeleteCurrentProductManufacturers(vm.Product);
                data.LoadNewProductManufacturers(vm.Product, vm.SelectedManufacturers);
                data.Products.Update(vm.Product);
                data.Save();

                TempData["message"] = $"{vm.Product.Name} updated.";
                return RedirectToAction("Search");
            }
            else
            {
                Load(vm, "Edit");
                return View("Product", vm);
            }
        }

        [HttpGet]
        public ViewResult Delete(int id) => GetProduct(id, "Delete");

        [HttpPost]
        public IActionResult Delete(ProductViewModel vm)
        {
            data.Products.Delete(vm.Product);
            data.Save();
            TempData["message"] = $"{vm.Product.Name} removed from Products.";
            return RedirectToAction("Search");
        }

        private ViewResult GetProduct(int id, string operation)
        {
            var product = new ProductViewModel();
            Load(product, operation, id);
            return View("Product", product);
        }
        private void Load(ProductViewModel vm, string op, int? id = null)
        {
            if (Operation.IsAdd(op))
            {
                vm.Product = new Product();
            }
            else
            {
                vm.Product = data.Products.Get(new QueryOptions<Product>
                {
                    Include = "ProductManufacturers.Manufacturer, Category",
                    Where = p => p.ProductId == (id ?? vm.Product.ProductId)
                });
            }

            vm.SelectedManufacturers = vm.Product.ProductManufacturers?.Select(
                pm => pm.Manufacturer.ManufacturerId).ToArray();
            vm.Manufacturers = data.Manufacturers.List(new QueryOptions<Manufacturer>
            {
                OrderBy = m => m.Name
            });
            vm.Categories = data.Categories.List(new QueryOptions<Category>
            {
                OrderBy = c => c.Name
            });
        }
    }
}
