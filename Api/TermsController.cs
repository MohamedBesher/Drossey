using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Drossey.Data.Core;
using Drossey.Data.Core.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Drossey.Data.Core.Dto;

namespace Drossey.Api.Controllers
{
    [Produces("application/json")]
    [Route("api/Terms")]

    public class TermsController : BaseController
    {
        public TermsController(IUnitOfWorkAsync unitOfWork, SignInManager<ApplicationUser> signInMgr, UserManager<ApplicationUser> userMgr, IPasswordHasher<ApplicationUser> hasher, ILogger<AuthController> logger, IConfiguration config, IMapper mapper) : base(unitOfWork, signInMgr, userMgr, hasher, logger, config, mapper)
        {
        }

        [HttpGet("{id}")]
        public IActionResult GetAllTerms(long id)
        {
            var terms = _unitOfWork.TermRepository.All().Where(u=>u.GradeId==id)
                .Select(u => new ItemDto() { Id = u.Id, Name = u.Name }); 
            return Ok(terms);
        }
    }
}