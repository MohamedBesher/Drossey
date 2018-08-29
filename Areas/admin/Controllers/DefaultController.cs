using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Drossey.Areas.admin.Controllers
{
    [ApiExplorerSettings(IgnoreApi = true)]

    public class DefaultController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}