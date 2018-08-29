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
using Drossey.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using Drossey.Data.Core.Dto;
using Microsoft.AspNetCore.Authorization;
using System.IO;
using System.Net.Http;
using Microsoft.EntityFrameworkCore;
using Drossey.Data.Core.Enum;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;

namespace Drossey.Controllers
{
    [Authorize(Roles = "User,Administrator")]
    [ApiExplorerSettings(IgnoreApi = true)]

    [Route("MyLessons")]
    public class MyLessonsController : BaseController
    {
        public MyLessonsController(IUnitOfWorkAsync unitOfWork, SignInManager<ApplicationUser> signInMgr,
            UserManager<ApplicationUser> userMgr, IPasswordHasher<ApplicationUser> hasher,
            IConfiguration config, IMapper mapper, ILogger<BaseController> logger,
            IMessenger messenger, IHostingEnvironment hostingEnvironment) 
            : base(unitOfWork, signInMgr, userMgr, hasher, config, mapper, logger, messenger, hostingEnvironment)
        {
        }
        //[Route("")]
        //public async Task<IActionResult> index()
        //{
        //    ApplicationUser usr = await GetCurrentUserAsync();
        //    return Ok(usr?.Id);

        //}
        [HttpGet]
        [Route("{id}")]
        [DisableRequestSizeLimit]
        public async Task<IActionResult> index(long id)
        {
           
            ApplicationUser usr = await GetCurrentUserAsync();
            var InUserRole=await _userMgr.IsInRoleAsync(usr,"User");
            
            var lesson = _unitOfWork.LessonRepository.All().Include(u=>u.Module).FirstOrDefault(u=>u.Id==id);
            if (lesson == null)
                return NotFound();

            if (InUserRole)
            {
                bool paid =_unitOfWork.TransactionRepository.CheckIfUserPaid(lesson.Module.SubjectId, usr.Id);
                            if (paid)
                            {     
                    
                                return PhysicalFile(Path.Combine(_hostingEnvironment.ContentRootPath, $"Lessons/{id}/index.html"), "text/html");
                            }
                            else                          
                                return NotFound();                            
            }
            else if(await _userMgr.IsInRoleAsync(usr, "Administrator"))           
                return PhysicalFile(Path.Combine(_hostingEnvironment.ContentRootPath, $"Lessons/{id}/index.html"), "text/html");
            else        
                return NotFound();
           

        }


        [HttpGet]
        [Route("{id}/{email}/{passWord}")]
        [AllowAnonymous]
        public async Task<IActionResult> RequestAccess(long id, string email, string passWord)
        {
            // Require the user to have a confirmed email before they can log on.
            var user = await _userMgr.FindByEmailAsync(email);
            if (user != null)
            {
                //check if user
                if (!await _userMgr.IsInRoleAsync(user, EnumRoles.User.ToString()))
                    return RedirectToAction("Login", "Account", new { returnUrl = $"/MyLessons/{id}" });

                if (!await _userMgr.IsEmailConfirmedAsync(user))
                    return RedirectToAction("Login", "Account", new { returnUrl = $"/MyLessons/{id}" });




                var result = await _signInMgr.PasswordSignInAsync(email, passWord, true, lockoutOnFailure: false);
                if (result.Succeeded)
                {
                    await AddTokensToCookie(user,passWord);

                   
                    return RedirectToAction(nameof(index),new { id=id});
                }
                if (result.RequiresTwoFactor)
                {
                    return RedirectToAction("Login", "Account", new { returnUrl = $"/MyLessons/{id}" });
                }
                if (result.IsLockedOut)
                {
                    return RedirectToAction("Login", "Account", new { returnUrl = $"/MyLessons/{id}" });
                }
                else
                {
                    return RedirectToAction("Login", "Account", new { returnUrl = $"/MyLessons/{id}" });

                }
            }

            return RedirectToAction("Login", "Account", new { returnUrl = $"/MyLessons/{id}" });

        }

        private async Task AddTokensToCookie(ApplicationUser user, string password)
            {
                // create claims
                List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.FirstName),
                new Claim(ClaimTypes.Email, user.Email)
            };

                // create identity
                ClaimsIdentity identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                // create principal
                ClaimsPrincipal principal = new ClaimsPrincipal(identity);

                //await AuthenticationHttpContextExtensions.SignInAsync(this.HttpContext, CookieAuthenticationDefaults.AuthenticationScheme, principal);
                await AuthenticationHttpContextExtensions.SignInAsync(this.HttpContext, "UserPanel", principal);

                // sign-in
                //await HttpContext.SignInAsync(
                //        scheme: "AdminAuth",
                //        principal: principal);
            }


        [Route("{id}/{name}/{level1}")]
        [Route("{id}/{name}/{level1}/{level2}")]
        [Route("{id}/{name}/{level1}/{level2}/{level3}")]
        [Route("{id}/{name}/{level1}/{level2}/{level3}/{level4}")]
        [Route("{id}/{name}/{level1}/{level2}/{level3}/{level4}/{level5}")]
        [Route("{id}/{name}/{level1}/{level2}/{level3}/{level4}/{level5}/{level6}")]
        [Route("{id}/{name}/{level1}/{level2}/{level3}/{level4}/{level5}/{level6}/{level7}")]
        [Route("{id}/{name}/{level1}/{level2}/{level3}/{level4}/{level5}/{level6}/{level7}/{level8}")]

        public IActionResult AllFile(long id, string name, string level1, string level2 = "",
            string level3 = "", string level4 = "", string level5 = "", string level6 = "",
            string level7 = "", string level8 = "", string level9 = "")
        {

            try
            {

                List<string> parameters = new List<string>() { level1, level2, level3, level4, level5, level6, level7, level8, level9 };
                int count = parameters.Count(u => string.IsNullOrEmpty(u));
                var path = name;
                var end = ((parameters.Count) - count);
                string extension = "";
                GetParameters(parameters, ref path, end, ref extension);
                var MIMExtentsion = GetMIMEtype(extension);
                if (MIMExtentsion == "mp3")
                {
                    var stream = new System.IO.FileStream(Path.Combine(_hostingEnvironment.ContentRootPath, $"Lessons/{id}/", path), System.IO.FileMode.Open);
                    var returStream = new StreamContent(stream);
                    return File(stream, "application/octet-stream");
                }
                else
                {
                    return PhysicalFile(Path.Combine(_hostingEnvironment.ContentRootPath, $"Lessons/{id}/", path), MIMExtentsion);

                }
            }
            catch (Exception )
            {

                throw;
            }

           


        }

       

    }
}