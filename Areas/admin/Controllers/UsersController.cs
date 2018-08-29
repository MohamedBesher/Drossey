using System;
using System.Threading.Tasks;
using AutoMapper;
using Drossey.Areas.admin.Models;
using Drossey.Data.Core;
using Drossey.Data.Core.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using MotleyFlash;

namespace Drossey.Areas.admin.Controllers
{
    [Authorize(Roles = "Administrator")]
    [Route("Admin/[controller]/[action]")]
    [Area("Admin")]
    [ApiExplorerSettings(IgnoreApi = true)]

    public class UsersController : BaseController
    {
        public UsersController(IUnitOfWorkAsync unitOfWork, SignInManager<ApplicationUser> signInMgr,
            UserManager<ApplicationUser> userMgr, IPasswordHasher<ApplicationUser> hasher, IConfiguration config,
            IMapper mapper, ILogger<BaseController> logger, IMessenger messenger, IHostingEnvironment hostingEnvironment)
            : base(unitOfWork, signInMgr, userMgr, hasher, config, mapper, logger, messenger, hostingEnvironment)
        {
        }


        public IActionResult Index(UserSearchModel model)

        {
            ViewBag.countries = _unitOfWork.CountryRepository.All();
            ViewBag.Keyword = model.Keyword;
            ViewBag.Page = model.Page;
            ViewBag.PageSize = model.PageSize;
            ViewBag.CountryId = model.CountryId;
            ViewBag.Suspended = model.IsSuspended;
            return View();
        }

        [HttpPost]
        public ViewComponentResult Search(UserSearchModel model)
        {
            return ViewComponent("SearchUsers",
                new
                {
                    pageSize = model.PageSize,
                    page = model.Page,
                    keyword = model.Keyword,
                    countryId = model.CountryId,
                    suspended = model.IsSuspended
                });
        }

        [HttpGet]
        [Route("{id}/{isActive}")]
        public async Task<ActionResult> ApproveUser(string id, bool isActive)
        {
            try
            {
                var oldUser = await _userMgr.FindByIdAsync(id);
                if (oldUser != null)
                {
                    oldUser.IsSuspended = isActive;
                    oldUser.EmailConfirmed = !isActive;

                    var result = await _userMgr.UpdateAsync(oldUser);
                    if (result.Succeeded)
                        return Json("OK");

                    return Json("NotFound");
                }

                return Json("NotFound");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return Json("Error");
            }
        }


        [HttpGet]
        [Route("Activate/{id}/{isActive}")]
        public async Task<ActionResult> Activate(string id, bool isActive)
        {
            try
            {
                var oldUser = await _userMgr.FindByIdAsync(id);
                if (oldUser != null)
                {
                    oldUser.IsSuspended = isActive;
                    oldUser.EmailConfirmed = isActive;

                    var result = await _userMgr.UpdateAsync(oldUser);
                    if (result.Succeeded)
                        return Json("OK");

                    return Json("NotFound");
                }

                return Json("NotFound");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return Json("Error");
            }
        }

        // GET: Users/Details/5
        public async Task<IActionResult> Details(string id)
        {
            ViewData["Uploads"] = _config.GetSection("WebApiUploadPath").Value;
            var request = await _unitOfWork.UserRepository.All()
               .Include(r => r.Country) 
                .SingleOrDefaultAsync(m => m.Id == id);

            if (request == null)
            {
                return NotFound();
            }

            return View(request);
        }







    }
}