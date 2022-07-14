using Microsoft.AspNetCore.Mvc;
using Nerdosity_V002.Models;

namespace Nerdosity_V002.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ValidationController : Controller
    {
        private Repository<Manufacturer> manufacturerData { get; set; }
        private Repository<Category> categoryData { get; set; }

        public ValidationController(NerdosityContext ctx)
        {
            manufacturerData = new Repository<Manufacturer>(ctx);
            categoryData = new Repository<Category>(ctx);
        }

        public JsonResult CheckCategory(string categoryId)
        {
            var validate = new Validate(TempData);
            validate.CheckCategory(categoryId, categoryData);
            if (validate.IsValid)
            {
                validate.MarkCategoryChecked();
                return Json(true);
            }
            else
            {
                return Json(validate.ErrorMessage);
            }
        }

        public JsonResult CheckManufacturer(string name, string address, string operation)
        {
            var validate = new Validate(TempData);
            validate.CheckManufacturer(name, address, operation, manufacturerData);
            if (validate.IsValid)
            {
                validate.MarkManufacturerChecked();
                return Json(true);
            }
            else
            {
                return Json(validate.ErrorMessage);
            }
        }
    }
}
