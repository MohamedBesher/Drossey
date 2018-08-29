using AutoMapper;
using Drossey.Areas.admin.Controllers;
using Drossey.Data.Core;
using Drossey.Data.Core.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using MotleyFlash;

namespace Drossey.Controllers
{
    [Authorize(Roles = "User")]
    [ApiExplorerSettings(IgnoreApi = true)]

    public class MessageController : BaseController
    {
        public MessageController(IUnitOfWorkAsync unitOfWork, SignInManager<ApplicationUser> signInMgr,
            UserManager<ApplicationUser> userMgr,
            IPasswordHasher<ApplicationUser> hasher,
            IConfiguration config, IMapper mapper, ILogger<BaseController> logger, IMessenger messenger,
            IHostingEnvironment hostingEnvironment)
            : base(unitOfWork, signInMgr, userMgr, hasher, config, mapper, logger, messenger, hostingEnvironment)
        {
        }

        public IActionResult Index()
        {
           
            return View();
        }




    }
}