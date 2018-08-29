using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Drossey.Models.Api.Models;
using Drossey.Data.Core;
using Drossey.Data.Core.Models;
using AutoMapper;
using Drossey.Data.Core.Enum;
using Drossey.Admin.Services;
using Drossey.Extensions;
using Drossey.Admin.Extensions;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using Microsoft.EntityFrameworkCore;

namespace Drossey.Api.Controllers
{
    public class AuthController : Drossey.Api.Controllers.BaseController
    {
        private readonly IEmailSender _emailSender;
        private readonly IHostingEnvironment _hostingEnvironment;


        public AuthController(IUnitOfWorkAsync unitOfWork, SignInManager<ApplicationUser> signInMgr,
            UserManager<ApplicationUser> userMgr, 
            IPasswordHasher<ApplicationUser> hasher, 
            ILogger<AuthController> logger,
            IConfiguration config, IMapper mapper, 
            IHostingEnvironment hostingEnvironment, IEmailSender emailSender
)
            : base(unitOfWork, signInMgr, userMgr, hasher, logger, config, mapper)
        {
            _hostingEnvironment = hostingEnvironment;
            _emailSender = emailSender;


        }

        [HttpPost("api/auth/Login")]
        public async Task<IActionResult> Login([FromBody]LoginViewModel model)
        {
           
            if (ModelState.IsValid)
            {
                // Require the user to have a confirmed email before they can log on.
                var user = await _userMgr.FindByEmailAsync(model.Email);
                if (user != null)
                {
                    //check if user
                    if (!await _userMgr.IsInRoleAsync(user, EnumRoles.User.ToString()))
                    {
                        return BadRequest("NotUser");
                    }
                    if (!await _userMgr.IsEmailConfirmedAsync(user))
                    {
                        return BadRequest("NotConfirmedEmail");
                    }

                    var result = await _signInMgr.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, lockoutOnFailure: false);
                    if (result.Succeeded)
                    {
                        return await GetToken(user);
                    }
                    else
                        return BadRequest("WrongPasswordOrEmail");


                }
                else
                {
                    
                    return BadRequest("NotUser");
                }

            }

            return BadRequest("WrongPasswordOrEmail");
        }
        private async Task<IActionResult> GetToken(ApplicationUser user)
        {
            try
            {


                var userClaims = await _userMgr.GetClaimsAsync(user);
                var userRoles = await _userMgr.GetRolesAsync(user);
                var claims = new[]
                {
                  new Claim(JwtRegisteredClaimNames.Sub, user.Email),
                  new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                };

                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Tokens:Key"]));
                var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
                

                var token = new JwtSecurityToken(
                    _config["Tokens:Issuer"],
                    _config["Tokens:Audience"],
                    claims,
                    expires: DateTime.UtcNow.AddHours(27),
                    signingCredentials: creds
                );

                return Ok(new
                {
                    token = new JwtSecurityTokenHandler().WriteToken(token),
                    expiration = token.ValidTo,
                    userRoles
                });
            }
            catch (Exception ex)
            {
                _logger.LogError($"Exception thrown while creating JWT: {ex}");
                return BadRequest(ex.ToString());

            }


        }
        [HttpPost("api/auth/Register")]
        public async Task<IActionResult> RegisterUser([FromBody] RegisterViewModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);


                //Check if email already exist
                var exists = await _userMgr.FindByEmailAsync(model.Email);
                if (exists != null)
                {
                    ModelState.AddModelError("EmailAlreadyExists", "EmailAlreadyExists");
                    return BadRequest(ModelState);
                }

                var user = new ApplicationUser()
                {
                    UserName = model.Email,
                    Email = model.Email,
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    CountryId = model.CountryId,
                    PhoneNumber = model.PhoneNumber,
                    Gender = model.Gender,
                    GradeId = model.GradeId

                };
                var result = await _userMgr.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    _logger.LogInformation("User created a new account with password.");
                    //add role to user
                    await _userMgr.AddToRoleAsync(user, EnumRoles.User.ToString());
                    var claimResult = await _userMgr.AddClaimAsync(user, new Claim("UserClaim", "True"));
                    var code = await _userMgr.GenerateEmailConfirmationTokenAsync(user);
                    var callbackUrl = Url.EmailConfirmationLink(user.Id, code, Request.Scheme);
                    await _emailSender.SendEmailConfirmationAsync(model.Email, callbackUrl);


                    _logger.LogInformation("User created a new account with password.");
                    return Ok(user.Id);
                }
                return BadRequest(GetErrorResult(result));
            }
            catch (Exception e)
            {

                return StatusCode(500, e.Message);
            }
          


        }

        [HttpGet("api/auth/ConfirmEmail")]
        public async Task<IActionResult> ConfirmEmail(string userId, string code)
        {
            if (userId == null || code == null)
            {
                return BadRequest();
            }
            var user = await _userMgr.FindByIdAsync(userId);
            if (user == null)
            {
                return BadRequest("NotUser");
                //throw new ApplicationException($"Unable to load user with ID '{userId}'.");
            }
            var result = await _userMgr.ConfirmEmailAsync(user, code);
            if (result.Succeeded)
                return Ok();
            return BadRequest();
            // return View(result.Succeeded ? "ConfirmEmail" : "Error");
        }


        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpPost("api/auth/ChangePassword")]

        public async Task<IActionResult> ChangePassword([FromBody]ChangePasswordViewModel changePasswordViewModel)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var userId = await GetUserId();
                var u = await _userMgr.FindByIdAsync(userId);

                var result = await _userMgr.ChangePasswordAsync(u, changePasswordViewModel.OldPassword,
                    changePasswordViewModel.NewPassword);

                var errorResult = GetErrorResult(result);
                if (errorResult != null)
                    return errorResult;
                return Ok();
            }
            catch (Exception ex)
            {
                var msg = ex.Message;
                return BadRequest("ChangePassword --- " + msg);
            }
        }


        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpGet("api/auth/balance")]

        public async Task<IActionResult> GetUserBalance()
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var userId = await GetUserId();
                if (string.IsNullOrEmpty(userId))
                    return Unauthorized();

                var u = await _userMgr.FindByIdAsync(userId);      
                
                return Ok(u.Balance);
            }
            catch (Exception ex)
            {
                var msg = ex.Message;
                return BadRequest("GetUserBalance --- " + msg);
            }
        }



      

        [Route("api/auth/ForgetPassword")]
        [HttpPost]
        public async Task<IActionResult> ForgetPassword([FromBody] ForgotPasswordViewModel forgotPasswordViewModel)
        {
            if (!ModelState.IsValid || forgotPasswordViewModel == null)
                return BadRequest(ModelState);
            try
            {
   
                var user = await _userMgr.FindByEmailAsync(forgotPasswordViewModel.Email);
                if (user == null || !(await _userMgr.IsEmailConfirmedAsync(user)))
                {
                    ModelState.AddModelError("", "Not Found");
                    return BadRequest(ModelState);
                }

                var code = await _userMgr.GeneratePasswordResetTokenAsync(user);
                var callbackUrl = Url.ResetPasswordCallbackLink(user.Id, code, Request.Scheme);
                //await _emailSender.SendEmailAsync(forgotPasswordViewModel.Email, "Reset Password",
                //   $"Please reset your password by clicking here: <a href='{callbackUrl}'>link</a>");
                return Ok(code);
            }
            catch (Exception ex)
            {
                var msg = ex.Message;
                return BadRequest("ForgetPassword --- " + msg);
            }
        }

        [HttpPost("api/auth/ResetPassword")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]

        public async Task<IActionResult> ResetPassword([FromBody]ResetPasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var user = await _userMgr.FindByEmailAsync(model.Email);
            if (user == null)
            {
                ModelState.AddModelError("", "Not Found");
                return BadRequest(ModelState);
            }
            var result = await _userMgr.ResetPasswordAsync(user, model.Code, model.Password);
            if (result.Succeeded)
            {
                return Ok();
            }
            var errorResult = GetErrorResult(result);
                return errorResult;

        }

        [HttpPost("api/auth/updateUser")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> updateUser([FromBody] UpdateViewModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var userId = await GetUserId();
                var user = await _userMgr.FindByIdAsync(userId);

                //user.UserName = model.Email;
                //user.Email = model.Email;
                user.FirstName = model.FirstName;
                user.LastName = model.LastName;
                user.CountryId = model.CountryId;
                user.PhoneNumber = model.PhoneNumber;
                user.Address = model.Address;

                user.Gender = model.Gender;
                user.GradeId = model.GradeId;
                if (string.IsNullOrEmpty(model.PhotoUrl) && !string.IsNullOrEmpty(model.Base64))
                    user.PhotoUrl = $"/uploads/{SavePicture(model.Base64)}";

                var result = await _userMgr.UpdateAsync(user);
                var errorResult = GetErrorResult(result);
                if (errorResult != null)
                    return errorResult;


                return Ok();


            }
            catch (Exception ex)
            {
                _logger.LogError($"Exception thrown while creating JWT: {ex}");
                return BadRequest(ex.ToString());

            }


        }



        [HttpGet("api/auth/getuserData")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> getUserData()
        {
           // var user = await _userMgr.GetUserAsync(User);
            var userId = await GetUserId();
            if (string.IsNullOrEmpty(userId))
                return Unauthorized();

           var user= _unitOfWork.UserRepository.All().Include(u=>u.Grade).Include(u=>u.Country).FirstOrDefault(u => u.Id == userId);

            if (user == null)
            {
                return NotFound();
            }
            var model = new UpdateViewModel
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                Address = user.Address,
                Gender = user.Gender.Value,
                PhoneNumber = user.PhoneNumber,
                CountryId = user.CountryId,
                CountryName = user.Country.Name,
                GradeName = user.Grade.Name,
                GradeId = user.GradeId.Value,
                PhotoUrl = user.PhotoUrl,
                Email=user.Email
            };
            return Ok(model);
        }

        protected IActionResult GetErrorResult(IdentityResult result)
        {


            if (!result.Succeeded)
            {
                if (result.Errors != null)
                {
                    foreach (IdentityError error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                }

                if (ModelState.IsValid)
                {
                    // No ModelState errors are available to send, so just return an empty BadRequest.
                    return BadRequest();
                }

                return BadRequest(ModelState);
            }

            return null;
        }



        public string SavePicture(string base64string)
        {
            try
            {


                var base64array = Convert.FromBase64String(base64string);
                string imageName = Guid.NewGuid().ToString() + ".png";
                var filePath = Path.Combine($"{_hostingEnvironment.ContentRootPath}/wwwroot/uploads/{imageName}");
                System.IO.File.WriteAllBytes(filePath, base64array);
                return imageName;
            }
            catch (Exception e)
            {

                return "";
            }


        }


    }




}
