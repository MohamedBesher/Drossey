using System.Linq;
using AutoMapper;
using Drossey.Data.Core;
using Drossey.Data.Core.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Drossey.Data.Core.Dto;

namespace Drossey.Api.Controllers
{
    [Produces("application/json")]
    [Route("api/Subjects")]

    public class SubjectsController : BaseController
    {
        public SubjectsController(IUnitOfWorkAsync unitOfWork, SignInManager<ApplicationUser> signInMgr, UserManager<ApplicationUser> userMgr, IPasswordHasher<ApplicationUser> hasher, ILogger<AuthController> logger, IConfiguration config, IMapper mapper) : base(unitOfWork, signInMgr, userMgr, hasher, logger, config, mapper)
        {
        }

        [HttpGet("{id}")]
        public IActionResult GetAll(long id)
        {
            var subjects = _unitOfWork.SubjectRepository.All().Where(u => u.TermId == id)
                .Select(u => new ItemDto() { Id = u.Id, Name = u.Name });
            return Ok(subjects);
        }
    }
}