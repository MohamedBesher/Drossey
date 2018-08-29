using AutoMapper;
using Drossey.Data.Core;
using Drossey.Data.Core.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Drossey.Api.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Produces("application/json")]
    [Route("api/Lessons")]
    public class LessonsController : BaseController
    {
        private readonly IHostingEnvironment _hostingEnvironment;

       
        public LessonsController(IUnitOfWorkAsync unitOfWork, SignInManager<ApplicationUser> signInMgr, UserManager<ApplicationUser> userMgr, IPasswordHasher<ApplicationUser> hasher, ILogger<AuthController> logger, IConfiguration config, IMapper mapper, IHostingEnvironment hostingEnvironment) 
            : base(unitOfWork, signInMgr, userMgr, hasher, logger, config, mapper)
        {
            _hostingEnvironment = hostingEnvironment;
        }

        [HttpGet]
        [Route("{id}")]
        [DisableRequestSizeLimit]
        public async Task<IActionResult> GetLesson(long id)
        {

            try
            {
                var userId = await GetUserId();
                ApplicationUser usr = await _userMgr.FindByIdAsync(userId);
                var InUserRole = await _userMgr.IsInRoleAsync(usr, "User");
                var lesson = _unitOfWork.LessonRepository.All().Include(u => u.Module)
                            .FirstOrDefault(u => u.Id == id);

                    if (lesson == null)
                        return NotFound();

                    if (InUserRole)
                    {
                        bool paid = _unitOfWork.TransactionRepository.CheckIfUserPaid(lesson.Module.SubjectId, usr.Id);
                        if (paid)
                        {
                            var path = $"{id}.zip";
                            return PhysicalFile(Path.Combine(_hostingEnvironment.ContentRootPath, $"Lessons/{id}/", path), "application/octet-stream");
                        }
                        else
                            return NotFound();
                    }
            
                    else
                        return NotFound();
            }
            catch (Exception e)
            {

                return StatusCode(500, e.Message);
            }
          


        }

    



    }


}