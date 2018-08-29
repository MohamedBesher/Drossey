using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Drossey.Models;
using Drossey.Areas.admin.Models;
using Microsoft.AspNetCore.Identity;
using Drossey.Data.Core.Models;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Net.NetworkInformation;

namespace Drossey.Controllers
{
    [ApiExplorerSettings(IgnoreApi = true)]
    [MvcExceptionFilter]

    public class HomeController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        public HomeController( UserManager<ApplicationUser> userManager)
        {
           _userManager = userManager;
        }
        public IActionResult Index()
        {
            string sss=GetMACAddress();
            ViewBag.main = "active";
             ViewBag.user = _userManager.GetUserId(HttpContext.User);
            return View();
        }



        static public string GetMACAddress()
        {
            NetworkInterface[] nics = NetworkInterface.GetAllNetworkInterfaces();
            String sMacAddress = string.Empty;
            foreach (NetworkInterface adapter in nics)
            {
                if (sMacAddress == String.Empty)// only return MAC Address from first card  
                {
                    IPInterfaceProperties properties = adapter.GetIPProperties();
                    sMacAddress = adapter.GetPhysicalAddress().ToString();
                }
            }
            return sMacAddress;
        }
    }


    // For mvc
    public class MvcExceptionFilterAttribute : ExceptionFilterAttribute
    {
        public override void OnException(ExceptionContext context)
        {
            return;
            // send view result
        }
    }

}
