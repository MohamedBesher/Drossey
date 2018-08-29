using System;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Drossey.Data.Core;
using Drossey.Data.Core.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Drossey.Admin.Services;
using Drossey.Models;
using MotleyFlash;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Drossey.Data.Core.Enum;

namespace Drossey.Api.Controllers
{

    [Route("api/Codes")]
   [Area("API")]
  [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
   

    public class CodeController : BaseController
    {
        private readonly IPinCodeGenerator _pinCodeGenerator;

       
        public CodeController(IUnitOfWorkAsync unitOfWork,  UserManager<ApplicationUser> userMgr, IPasswordHasher<ApplicationUser> hasher, ILogger<AuthController> logger, IConfiguration config, IMapper mapper, IPinCodeGenerator pinCodeGenerator) 
            : base(unitOfWork, userMgr, hasher, logger, config, mapper)
        {
            _pinCodeGenerator = pinCodeGenerator;
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody]CodeAddViewModel model)
        {

            try
            {
                if (ModelState.IsValid)
                {
                    var userId = await GetUserId();
                    var user = await _userMgr.FindByIdAsync(userId);

                    var random = Convert.ToDouble(model.Code.Substring(0, 10));
                    var code = model.Code.Substring(10, 5);
                    var pinCode = _unitOfWork.PinCodeRepository.Filter(u => u.Code == random).FirstOrDefault();

                    if (pinCode == null)
                        return StatusCode(400, "InValidCode");

                    if (pinCode.Status != CodeStatus.IsActive)   
                           return StatusCode(400, "Used");
                    
                    else
                    {
                        string codeStr = _pinCodeGenerator.GetCode(pinCode.Amount, pinCode.Code,pinCode.Vector, pinCode.Key);
                        if (codeStr != model.Code)
                        {
                            return StatusCode(400, "InValidCode");
                        }
                        else
                        {
                            user.Balance += pinCode.Amount;
                            await _userMgr.UpdateAsync(user);
                            pinCode.Status = CodeStatus.Shipped;
                            await _unitOfWork.CommitAsync();
                            return Ok();

                        }
                    }

                }
                return StatusCode(400, ModelState);
            }
            catch (Exception e)
            {

                return StatusCode(500, e.Message);
            }
           
            

        }
    }

}