using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.Extensions.Configuration;
using Drossey.Areas.admin.Controllers;
using Drossey.Data.Core;
using Drossey.Data.Core.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MotleyFlash;
using Microsoft.AspNetCore.Authorization;
using Drossey.Models;
using Drossey.Data.Core.Dto;

namespace Drossey.Controllers
{
    [ApiExplorerSettings(IgnoreApi = true)]

    [Authorize(Roles = "User")]
    public class MyProfileController : BaseController
    {
        public MyProfileController(IUnitOfWorkAsync unitOfWork, SignInManager<ApplicationUser> signInMgr, UserManager<ApplicationUser> userMgr,
            IPasswordHasher<ApplicationUser> hasher,
            IConfiguration config, IMapper mapper, ILogger<BaseController> logger, IMessenger messenger, IHostingEnvironment hostingEnvironment)
            : base(unitOfWork, signInMgr, userMgr, hasher, config, mapper, logger, messenger, hostingEnvironment)
        {
        }

        public IActionResult Index()
        {
           
            return View();
        }
  
    }
}