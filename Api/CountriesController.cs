using System.Collections.Generic;
using AutoMapper;
using Drossey.Data.Core;
using Drossey.Data.Core.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace Drossey.Api.Controllers
{
    //[Produces("application/json")]
    [Route("api/Countries")]

    public class CountriesController : BaseController
    {
        public CountriesController(IUnitOfWorkAsync unitOfWork, SignInManager<ApplicationUser> signInMgr, UserManager<ApplicationUser> userMgr, IPasswordHasher<ApplicationUser> hasher, ILogger<AuthController> logger, IConfiguration config, IMapper mapper) : base(unitOfWork, signInMgr, userMgr, hasher, logger, config, mapper)
        {
        }

        [HttpGet("GetAll")]
        public IActionResult GetAllCountries()
        {
            var countries = _unitOfWork.CountryRepository.Get();
            return Ok(countries);
        }
    }

}