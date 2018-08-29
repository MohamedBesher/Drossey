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

    public class ContactUsController : BaseController
    {

        public ContactUsController(IUnitOfWorkAsync unitOfWork, SignInManager<ApplicationUser> signInMgr, UserManager<ApplicationUser> userMgr, IPasswordHasher<ApplicationUser> hasher, IConfiguration config, IMapper mapper, ILogger<BaseController> logger, IMessenger messenger, IHostingEnvironment hostingEnvironment) 
            : base(unitOfWork, signInMgr, 
                  userMgr, hasher,
                  config, mapper, 
                  logger, messenger, hostingEnvironment)
        {
        }

        public async Task<IActionResult> index()
        {
            ViewBag.ContactUs = "active";

            var user = await GetCurrentUser();
            var model = new Models.ContactUsViewModel();
            if (user != null)
            {
                model.Email = user.Email;
                model.Name = $"{user.FirstName} {user.LastName}";
                model.Phone = user.PhoneNumber;
            }
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> index(Models.ContactUsViewModel model)
        {
            try
            {
                var user = await GetCurrentUser();
                if (user != null)
                {
                    model.Email = user.Email;
                    model.Name = $"{user.FirstName} {user.LastName}";
                    model.Phone = user.PhoneNumber;
                }
                if (ModelState.IsValid)
                {
                    var contactUs = _mapper.Map<Models.ContactUsViewModel, ContactUs>(model);
                    _unitOfWork.ContactUsRepository.Create(contactUs);
                    await _unitOfWork.CommitAsync();
                    return RedirectToAction("Index", "Message");
                }
                return View(model);
            }
            catch (Exception)
            {

                return View(model);
            }
           
            
        }
    }
}
