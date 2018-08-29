using System;
using System.Collections.Generic;
using AutoMapper;
using Drossey.Areas.admin.Models;
using Drossey.Data.Core;
using Drossey.Data.Core.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using MotleyFlash;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using MotleyFlash.Extensions;
using Drossey.Data.Core.Dto;
using Drossey.Areas.admin.Controllers;
using Drossey.Models;

namespace Drossey.Controllers
{
    [Authorize(Roles = "User")]
    [ApiExplorerSettings(IgnoreApi = true)]

    public class MyBooksController : BaseController
    {
        public MyBooksController(IUnitOfWorkAsync unitOfWork, SignInManager<ApplicationUser> signInMgr, UserManager<ApplicationUser> userMgr, IPasswordHasher<ApplicationUser> hasher, IConfiguration config, IMapper mapper, ILogger<BaseController> logger, IMessenger messenger, IHostingEnvironment hostingEnvironment) : base(unitOfWork, signInMgr, userMgr, hasher, config, mapper, logger, messenger, hostingEnvironment)
        {
        }

        public IActionResult Index(SearchMyBookModel model)
        {
            ViewBag.Keyword = model.Keyword;
            ViewBag.page = model.Page;
            ViewBag.pageSize = model.PageSize;
            ViewBag.termId = model.TermId;
            ViewBag.countryId = model.CountryId;
            ViewBag.gradeId = model.GradeId;
            ViewBag.subjectId = model.SubjectId;
            ViewBag.countries = new SelectList(_unitOfWork.CountryRepository.Filter(u=>u.IsPuplished), "Id", "Name");
            //profile navigation
            var user = _unitOfWork.UserRepository.GetOneUser(_userMgr.GetUserId(HttpContext.User));
            ViewData["userName"] = user.FirstName + ' ' + user.LastName;
            ViewData["Balance"] = user.Balance;
            ViewData["PhotoUrl"] = user.PhotoUrl;
            ViewData["Active"] = "myBooks";




            return View();

        }


        [HttpPost]
        public ViewComponentResult Search(SearchMyBookModel model)
        {
            return ViewComponent("SearchMyBooks", new { pageSize = model.PageSize, page = model.Page, keyword = model.Keyword, countryId = model.CountryId, gradeId = model.GradeId, TermId = model.TermId,SubjectId=model.SubjectId });
        }
        [HttpGet]
        public ViewComponentResult Search(int pageSize, int page, string keyword,long countryId ,long gradeId,long termId,long subjectId)
        {
            return ViewComponent("SearchMyBooks", new { pageSize = pageSize, page = page, keyword = keyword, countryId = countryId, gradeId = gradeId, TermId = termId , SubjectId = subjectId });
        }


        
        private void loadPhoto(string photoUrl)
        {
            if (!string.IsNullOrEmpty(photoUrl))
                ViewBag.PhotoUrl = GetFullPath(photoUrl);
            else
                ViewBag.PhotoUrl = "";
        }

        public async Task<ActionResult> Details(long id)
        {
            var userId = (await GetCurrentUser()).Id;
            MySubjectDto subject = GetUserbookbyId(id, userId);
            if (subject == null)
            {
                return NotFound();
            }
            ViewBag.Book = subject;
            return View(subject);
        }

        private MySubjectDto GetUserbookbyId(long id, string userId)
        {
            //edit to select only .IsPuplished == true

            var transaction = _unitOfWork.TransactionDetailsRepository.All()
                .Include(u => u.Transaction)
                .Where(u => u.SubjectId == id && u.Transaction.UserId == userId);


            if (transaction.Any())
            {
               return _unitOfWork.SubjectRepository.GetUserSubjectbyId(id,userId);
            }        
            else
                return null;
        }

        private ICollection<MySubjectQuizDto> GetQuizbyId(long id, string type)
        {
            List<MySubjectQuizDto> result = new List<MySubjectQuizDto>();

            switch (type)
            {
                case "subject":
                    result = _unitOfWork.QuizRepository.Filter(u => u.SubjectId == id).Select(u => new MySubjectQuizDto()
                    {
                        Id = u.Id,
                        Description = u.Description
                    }).ToList();
                    break;

                case "book":
                    result = _unitOfWork.QuizRepository.Filter(u => u.BookId == id).Select(u => new MySubjectQuizDto()
                    {
                        Id = u.Id,
                        Description = u.Description
                    }).ToList();
                    break;

                case "lesson":
                    result = _unitOfWork.QuizRepository.Filter(u => u.LessonId == id).Select(u => new MySubjectQuizDto()
                    {
                        Id = u.Id,
                        Description = u.Description
                    }).ToList();
                    break;
                default:
                    result = null;
                    break;
            }

            return result;
        }

        [HttpPost]
        public IActionResult LoadDrp([FromBody] ItemDto model)
        {
            List<ItemDto> result = new List<ItemDto>();
                
            switch (model.Name)
            {
                case "CountryId":
                    result = _unitOfWork.GradeRepository.Filter(u => u.CountryId == model.Id).Select(u => new ItemDto()
                    {
                        Id = u.Id,
                        Name = u.Name
                    }).ToList();
                    break;
                case "GradeId":
                    result = _unitOfWork.TermRepository.Filter(u => u.GradeId == model.Id).Select(u => new ItemDto()
                    {
                        Id = u.Id,
                        Name = u.Name
                    }).ToList();
                    break;

                case "TermId":
                    result = _unitOfWork.SubjectRepository.Filter(u => u.TermId == model.Id).Select(u => new ItemDto()
                    {
                        Id = u.Id,
                        Name = u.Name
                    }).ToList();
                    break;
                default:
                    break;
            };
            return Json(result);
        }


    }
}
