using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Nerdosity_V002.Models;


namespace Nerdosity_V002.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    [Area("Admin")]
    public class CategoryController : Controller
    {
        private Repository<Category> data { get; set; }
        public CategoryController(NerdosityContext ctx) => data = new Repository<Category>(ctx);
        public ViewResult Index()
        {
            var search = new SearchData(TempData);
            search.Clear();

            var categories = data.List(new QueryOptions<Category>
            {
                OrderBy = c => c.Name
            });
            return View(categories);
        }
        [HttpGet]
        public ViewResult Add() => View("Category", new Category());

        [HttpPost]
        public IActionResult Add(Category category)
        {
            var validate = new Validate(TempData);
            if (!validate.IsCategoryChecked)
            {
                validate.CheckCategory(category.CategoryId, data);
                if (!validate.IsValid)
                {
                    ModelState.AddModelError(nameof(category.CategoryId), validate.ErrorMessage);
                }
            }

            if (ModelState.IsValid)
            {
                data.Insert(category);
                data.Save();
                validate.ClearCategory();
                TempData["message"] = $"{category.Name} added to Categories.";
                return RedirectToAction("Index");
            }
            else
            {
                return View("Category", category);
            }
        }

        [HttpGet]
        public ViewResult Edit(string id) => View("Category", data.Get(id));

        [HttpPost]
        public IActionResult Edit(Category category)
        {
            if (ModelState.IsValid)
            {
                data.Update(category);
                data.Save();
                TempData["message"] = $"{category.Name} updated.";
                return RedirectToAction("Index");
            }
            else
            {
                return View("Category", category);
            }
        }

        [HttpGet]
        public IActionResult Delete(string id)
        {
            var category = data.Get(new QueryOptions<Category>
            {
                Include = "Products",
                Where = c => c.CategoryId == id
            });

            if (category.Products.Count > 0)
            {
                TempData["message"] = $"Can't delete category {category.Name} "
                                    + "because it's associated with these products.";
                return GoToProductSearchResults(id);
            }
            else
            {
                return View("Category", category);
            }
        }

        [HttpPost]
        public IActionResult Delete(Category category)
        {
            data.Delete(category);
            data.Save();
            TempData["message"] = $"{category.Name} removed from Categories.";
            return RedirectToAction("Index");
        }

        public RedirectToActionResult ViewProductss(string id) => GoToProductSearchResults(id);

        private RedirectToActionResult GoToProductSearchResults(string id)
        {
            var search = new SearchData(TempData)
            {
                SearchTerm = id,
                Type = "category"
            };
            return RedirectToAction("Search", "Product");
        }

    }
}
