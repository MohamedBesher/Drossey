using AutoMapper;
using Drossey.Data.Core;
using Drossey.Data.Core.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace Drossey.Api.Controllers
{
    //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Produces("application/json")]
    [Area("API")]

    [Route("api/ContactUs")]

    public class ContactUsController : BaseController
    {
       

        public ContactUsController(IUnitOfWorkAsync unitOfWork, SignInManager<ApplicationUser> signInMgr, UserManager<ApplicationUser> userMgr, IPasswordHasher<ApplicationUser> hasher, ILogger<AuthController> logger, IConfiguration config, IMapper mapper) : base(unitOfWork, signInMgr, userMgr, hasher, logger, config, mapper)
        {
        }

        [HttpPost("user")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]

        public async Task<IActionResult> index([FromBody] Models.ContactUsModel model)
        {
            try
            {
                var userId = await GetUserId();


                if (userId == null)
                    return StatusCode(401, "NotUser");

                var user = await _userMgr.FindByIdAsync(userId);
                if (user==null)
                    return StatusCode(401, "NotUser");

                if (ModelState.IsValid)
                {
                    var contactUs = new ContactUs()
                    {
                     Email = user.Email,
                     Name = $"{user.FirstName} {user.LastName}",
                     Phone = user.PhoneNumber,
                     UserId = user.Id,
                     Message=model.Message,
                };
                    _unitOfWork.ContactUsRepository.Create(contactUs);
                    await _unitOfWork.CommitAsync();
                    return Ok();
                }
                else
                {
                    return StatusCode(400,ModelState);

                }
            }
            catch (Exception e)
            {

                return StatusCode(400,e.Message );
            }


        }



        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> add([FromBody] Models.ContactUsViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var contactUs = _mapper.Map<Models.ContactUsViewModel, ContactUs>(model);
                    _unitOfWork.ContactUsRepository.Create(contactUs);
                    await _unitOfWork.CommitAsync();
                    return Ok();
                }
                else
                {
                    return StatusCode(400, ModelState);

                }
            }
            catch (Exception e)
            {

                return StatusCode(400, e.Message);
            }


        }
    }

}