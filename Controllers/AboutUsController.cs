using System;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Drossey.Areas.admin.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Drossey.Data;
using Drossey.Data.Core;
using Drossey.Data.Core.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using MotleyFlash;
using MotleyFlash.Extensions;
using Drossey.Admin.Services;
using Drossey.Areas.admin.Controllers;
using Drossey.Models;
using Drossey.Data.Core.Enum;

namespace Drossey.Controllers
{

    [ApiExplorerSettings(IgnoreApi = true)]

    public class AboutUsController : BaseController
    {

        public AboutUsController(IUnitOfWorkAsync unitOfWork, SignInManager<ApplicationUser> signInMgr, UserManager<ApplicationUser> userMgr, IPasswordHasher<ApplicationUser> hasher, IConfiguration config, IMapper mapper, ILogger<BaseController> logger, IMessenger messenger, IHostingEnvironment hostingEnvironment) 
            : base(unitOfWork, signInMgr, 
                  userMgr, hasher,
                  config, mapper, 
                  logger, messenger, hostingEnvironment)
        {
        }

        [HttpGet]
        public IActionResult index()
        {
            ViewBag.AboutUs = "active";
            return View();
        }

        [Route("FAQ")]
        [HttpGet]
        public IActionResult FAQ()
        {
            return View();
        }

        [HttpGet]
        [Route("Privacy")]
        public IActionResult Privacy()
        {
            return View();
        }


    }
}
