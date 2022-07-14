using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Nerdosity_V002.Models;

namespace Nerdosity_V002.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    [Area("Admin")]
    public class ManufacturerController : Controller
    {
        private Repository<Manufacturer> data { get; set; }
        public ManufacturerController(NerdosityContext ctx) => data = new Repository<Manufacturer>(ctx);
        public ViewResult Index()
        {
            var manufacturers = data.List(new QueryOptions<Manufacturer>
            {
                OrderBy = m => m.Name
            });
            return View(manufacturers);
        }
        public RedirectToActionResult Select(int id, string operation)
        {
            switch (operation.ToLower())
            {
                case "viewproducts":
                    return RedirectToAction("ViewProducts", new { id });
                case "edit":
                    return RedirectToAction("Edit", new { id });
                case "delete":
                    return RedirectToAction("Delete", new { id });
                default:
                    return RedirectToAction("Index");
            }
        }

        [HttpGet]
        public ViewResult Add() => View("Manufacturer", new Manufacturer());

        [HttpPost]
        public IActionResult Add(Manufacturer manufacturer, string operation)
        {
            var validate = new Validate(TempData);
            if (!validate.IsManufacturerChecked)
            {
                validate.CheckManufacturer(manufacturer.Name, manufacturer.Address, operation, data);
                if (!validate.IsValid)
                {
                    ModelState.AddModelError(nameof(manufacturer.Address), validate.ErrorMessage);
                }
            }

            if (ModelState.IsValid)
            {
                data.Insert(manufacturer);
                data.Save();
                validate.ClearManufacturer();
                TempData["message"] = $"{manufacturer.Name} added to Manufacturers.";
                return RedirectToAction("Index");
            }
            else
            {
                return View("Manufacturer", manufacturer);
            }
        }

        [HttpGet]
        public ViewResult Edit(int id) => View("Manufacturer", data.Get(id));

        [HttpPost]
        public IActionResult Edit(Manufacturer manufacturer)
        {
            if (ModelState.IsValid)
            {
                data.Update(manufacturer);
                data.Save();
                TempData["message"] = $"{manufacturer.Name} updated.";
                return RedirectToAction("Index");
            }
            else
            {
                return View("Manufacturer", manufacturer);
            }
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            var manufacturer = data.Get(new QueryOptions<Manufacturer>
            {
                Include = "ProductManufacturers",
                Where = m => m.ManufacturerId == id
            });

            if (manufacturer.ProductManufacturers.Count > 0)
            {
                TempData["message"] = $"Can't delete manufacturer {manufacturer.Name} because they are associated with these products.";
                return GoToManufacturerSearch(manufacturer);
            }
            else
            {
                return View("Manufacturer", manufacturer);
            }
        }

        [HttpPost]
        public RedirectToActionResult Delete(Manufacturer manufacturer)
        {
            data.Delete(manufacturer);
            data.Save();
            TempData["message"] = $"{manufacturer.Name} removed from Manufacturers.";
            return RedirectToAction("Index");
        }

        public RedirectToActionResult ViewProducts(int id)
        {
            var manufacturer = data.Get(id);
            return GoToManufacturerSearch(manufacturer);
        }

        private RedirectToActionResult GoToManufacturerSearch(Manufacturer manufacturer)
        {
            var search = new SearchData(TempData)
            {
                SearchTerm = manufacturer.Name,
                Type = "manufacturer"
            };
            return RedirectToAction("Search", "Product");
        }
    }
}
