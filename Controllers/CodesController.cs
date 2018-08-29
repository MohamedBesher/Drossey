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
    [Authorize(Roles = "User")]
    [ApiExplorerSettings(IgnoreApi = true)]

    public class CodesController : BaseController
    {
        private readonly IPinCodeGenerator _pinCodeGenerator;

        public CodesController(IUnitOfWorkAsync unitOfWork, SignInManager<ApplicationUser> signInMgr, UserManager<ApplicationUser> userMgr, IPasswordHasher<ApplicationUser> hasher, IConfiguration config, IMapper mapper, ILogger<BaseController> logger, IMessenger messenger, IHostingEnvironment hostingEnvironment, IPinCodeGenerator pinCodeGenerator) 
            : base(unitOfWork, signInMgr, 
                  userMgr, hasher,
                  config, mapper, 
                  logger, messenger, hostingEnvironment)
        {
            _pinCodeGenerator = pinCodeGenerator;
        }

        public IActionResult Add()
        {
            CodeAddViewModel model = new CodeAddViewModel();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(CodeAddViewModel model)
        {
            var user = await GetCurrentUser();
            if (ModelState.IsValid)
            {
                var random = Convert.ToDouble(model.Code.Substring(0, 10));              
                var code = model.Code.Substring(10, 5);
                var pinCode = _unitOfWork.PinCodeRepository.Filter(u => u.Code == random).FirstOrDefault();
                if (pinCode == null || pinCode.Status!=CodeStatus.IsActive)
                {
                    ModelState.AddModelError("Code", "كود غير صالح  ");
                }
                else
                {
                    string codeStr=_pinCodeGenerator.GetCode(pinCode.Amount, pinCode.Code, pinCode.Vector, pinCode.Key);
                    if (codeStr != model.Code)
                    {
                        ModelState.AddModelError("", "كود غير صالح");
                    }
                    else
                    {
                      user.Balance += pinCode.Amount;
                      await _userMgr.UpdateAsync(user);
                      pinCode.Status = CodeStatus.Shipped;
                      await _unitOfWork.CommitAsync();
                      return RedirectToAction("profile", "Manage");

                    }
                }

            }
            return View(model);
            
        }
    }
}
