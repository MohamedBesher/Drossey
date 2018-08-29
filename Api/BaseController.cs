using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using Drossey.Data.Core;
using Drossey.Data.Core.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Hosting;
using System.IO;

namespace Drossey.Api.Controllers
{
    public class BaseController : Controller
    {
        public readonly ILogger<AuthController> _logger;
        public readonly SignInManager<ApplicationUser> _signInMgr;
        public readonly UserManager<ApplicationUser> _userMgr;
        public readonly IPasswordHasher<ApplicationUser> _hasher;
        public readonly IConfiguration _config;
        public IUnitOfWorkAsync _unitOfWork;
        protected readonly IMapper _mapper;

        public BaseController(IUnitOfWorkAsync unitOfWork,
           SignInManager<ApplicationUser> signInMgr,
            UserManager<ApplicationUser> userMgr,
            IPasswordHasher<ApplicationUser> hasher,
            ILogger<AuthController> logger,
            IConfiguration config,
            IMapper mapper

            )
        {
            _unitOfWork = unitOfWork;
            _signInMgr = signInMgr;
            _logger = logger;
            _userMgr = userMgr;
            _hasher = hasher;
            _config = config;
            _mapper = mapper;


        }

        public BaseController(IUnitOfWorkAsync unitOfWork,
           UserManager<ApplicationUser> userMgr,
           IPasswordHasher<ApplicationUser> hasher,
           ILogger<AuthController> logger,
           IConfiguration config,
           IMapper mapper
           )
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
            _userMgr = userMgr;
            _hasher = hasher;
            _config = config;
            _mapper = mapper;


        }

        protected async Task<string> GetUserId()
        {
            try
            {
                var userName = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;
                if (!string.IsNullOrEmpty(userName))
                {
                    var user = await _userMgr.FindByNameAsync(userName);
                    if (user != null)
                        return user.Id;
                    else
                        return null;
                }
            }
            catch (Exception)
            {

                return null;
            }

            return null;
        }


        
    }
}
