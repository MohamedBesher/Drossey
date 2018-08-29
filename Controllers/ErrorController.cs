using AutoMapper;
using Drossey.Areas.admin.Controllers;
using Drossey.Areas.admin.Models;
using Drossey.Data.Core;
using Drossey.Data.Core.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using MotleyFlash;
using System.Diagnostics;


namespace Drossey.Controllers
{
    [ApiExplorerSettings(IgnoreApi = true)]

    public class ErrorController : BaseController
    {

        public ErrorController(IUnitOfWorkAsync unitOfWork, SignInManager<ApplicationUser> signInMgr, UserManager<ApplicationUser> userMgr, IPasswordHasher<ApplicationUser> hasher, IConfiguration config, IMapper mapper, ILogger<BaseController> logger, IMessenger messenger, IHostingEnvironment hostingEnvironment) 
            : base(unitOfWork, signInMgr, 
                  userMgr, hasher,
                  config, mapper, 
                  logger, messenger, hostingEnvironment)
        {
        }
        [HttpGet("/error/{id?}")]

        public IActionResult index(int id=0)
        {

            return View(id);

            // return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }



    }
}
