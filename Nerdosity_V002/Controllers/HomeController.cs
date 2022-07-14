using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Nerdosity_V002.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Nerdosity_V002.Controllers
{
    public class HomeController : Controller
    {
        private Repository<Product> data { get; set; }
        public HomeController(NerdosityContext ctx) => data = new Repository<Product>(ctx);

        public ViewResult Index()
        {
            var random = data.Get(new QueryOptions<Product>
            {
                OrderBy = p => Guid.NewGuid()
            });

            return View(random);
        }

        public IActionResult About()
        {
            return View();
        }
    }
}
