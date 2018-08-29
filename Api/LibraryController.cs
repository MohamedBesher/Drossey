using AutoMapper;
using Drossey.Data.Core;
using Drossey.Data.Core.Models;
using Drossey.Models;
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
    [Produces("application/json")]
    [Area("API")]
    [Route("api/Library")]
    public class LibraryController : BaseController
    {
        public LibraryController(IUnitOfWorkAsync unitOfWork, SignInManager<ApplicationUser> signInMgr, UserManager<ApplicationUser> userMgr, IPasswordHasher<ApplicationUser> hasher, ILogger<AuthController> logger, IConfiguration config, IMapper mapper) : base(unitOfWork, signInMgr, userMgr, hasher, logger, config, mapper)
        {
        }

        [HttpPost]
        [AllowAnonymous]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> All([FromBody] Models.Api.Models.LibrarySearchModel search)
        {
            try
            {
                var userId = await GetUserId();
                var books = _unitOfWork.BookRepository.GetAllBooksAsync(userId,
                                                                        search.Page,
                                                                        search.PageSize,
                                                                        search.Keyword,
                                                                        search.CountryId,
                                                                        search.GradeId,
                                                                        search.TermId,
                                                                        search.SubjectId);
                return Ok(books);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [HttpGet("{id}")]
        [AllowAnonymous]
        public IActionResult GetBookById(long id)
        {
            var subject = _unitOfWork.SubjectRepository.GetSubjectbyId(id);
            if (subject == null)
                return NotFound();

            return Ok(subject);
        }

        [HttpGet("users/{id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> GetBookByIdAsync(long id)
        {
            var userId = await GetUserId();

            var subject = _unitOfWork.SubjectRepository.GetUserSubjectbyId(id, userId);
            if (subject == null)
                return NotFound();

            return Ok(subject);
        }

        [HttpPost("users/GetUserBooks")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> GetUserBooks([FromBody] SearchMyBookModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var userId = await GetUserId();
                // var subjects = _unitOfWork.BookRepository.GetUserBooks(userId,model.Page,model.PageSize,"", 0, 0, 0, 0);
                var subjects = _unitOfWork.BookRepository.GetAllUserBooks(userId, model.Page, model.PageSize, "", 0, 0, 0, 0);

                return Ok(subjects);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [AllowAnonymous]
        [HttpGet("Suggestions")]
        public async Task<IActionResult> GetSuggested()
        {
            try
            {
                var userId = await GetUserId();
                if (string.IsNullOrEmpty(userId))
                {
                    var books = _unitOfWork.BookRepository.GetAllBooksAsync("", 1, 6, null, 0, 0, 0, 0);
                    return Ok(books);
                }
                else
                {
                    var books = _unitOfWork.BookRepository.GetSuggestions(3, userId);
                    return Ok(books);

                }
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }
    }
}