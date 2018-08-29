using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Drossey.Areas.admin.Controllers;
using Drossey.Data.Core;
using Drossey.Data.Core.Dto;
using Drossey.Data.Core.Models;
using Drossey.Models.AccountViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using MotleyFlash;

namespace Drossey.Controllers
{
    [ApiExplorerSettings(IgnoreApi = true)]

    [Authorize(Roles = "User")]
    public class UserController : BaseController
    {
        public UserController(IUnitOfWorkAsync unitOfWork, SignInManager<ApplicationUser> signInMgr, UserManager<ApplicationUser> userMgr,
           IPasswordHasher<ApplicationUser> hasher,
           IConfiguration config, IMapper mapper, ILogger<BaseController> logger, IMessenger messenger, IHostingEnvironment hostingEnvironment)
           : base(unitOfWork, signInMgr, userMgr, hasher, config, mapper, logger, messenger, hostingEnvironment)
        {
        }
        public IActionResult Index()
        {
            var User =  _userMgr.GetUserId(HttpContext.User);
            var UserProfile = _unitOfWork.UserRepository.GetOneUser(User);
            return View(UserProfile);
        }



        // GET: Movies/Edit/5
        public IActionResult Edit()
        {
            var User = _userMgr.GetUserId(HttpContext.User);
            var user =_unitOfWork.UserRepository.GetOneUser(User);
            var countries = _unitOfWork.CountryRepository.All().Select(x => new { Id = x.Id, Value = x.Name });
            user.CountryList = new SelectList(countries, "Id", "Value");
            if (user == null)
            {
                return NotFound();
            }
            return View(user);
        }

        // POST: Movies/Edit/5
     
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(UserProfileDto model)
        {

            if (ModelState.IsValid)
            {
                var user = await _userMgr.GetUserAsync(HttpContext.User);

                user.UserName = model.Email;
                user.Email = model.Email;
                user.FirstName = model.FirstName;
                user.LastName = model.LastName;
                user.CountryId = model.CountryId;
                user.PhoneNumber = model.PhoneNumber;
                user.Gender = model.Gender;
                user.Address = model.Address;
                
                var result =await  _userMgr.UpdateAsync(user);

            }
            return RedirectToAction("Index");
        }
    }
}