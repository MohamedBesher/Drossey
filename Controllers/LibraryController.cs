using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.Extensions.Configuration;
using Drossey.Areas.admin.Controllers;
using Drossey.Data.Core;
using Drossey.Data.Core.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MotleyFlash;
using Drossey.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using Drossey.Data.Core.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;

namespace Drossey.Controllers
{
    // [Authorize(Roles = "User")]
    [ApiExplorerSettings(IgnoreApi = true)]

    public class LibraryController : BaseController
    {
        public LibraryController(IUnitOfWorkAsync unitOfWork,
                                 SignInManager<ApplicationUser> signInMgr,
                                 UserManager<ApplicationUser> userMgr,
                                 IPasswordHasher<ApplicationUser> hasher,
                                 IConfiguration config, IMapper mapper,
                                 ILogger<BaseController> logger, IMessenger messenger, IHostingEnvironment hostingEnvironment) 
            : base(unitOfWork, signInMgr, userMgr, hasher, config, mapper, logger, messenger, hostingEnvironment)
        {
        }

        [ResponseCache(NoStore = true, Location = ResponseCacheLocation.None)]

        public IActionResult Index(LibrarySearchModel model)
        {
            ViewBag.library = "active";
            //ViewBag.countries = new SelectList(_unitOfWork.CountryRepository.All(), "Id", "Name");
        //    ViewBag.grades = new SelectList(_unitOfWork.GradeRepository.All(), "Id", "Name");
          //  ViewBag.terms = new SelectList(_unitOfWork.TermRepository.All(), "Id", "Name");
           // ViewBag.subjects = new SelectList(_unitOfWork.SubjectRepository.All(), "Id", "Name");
            ViewBag.Keyword = model.Keyword;
            ViewBag.page = model.Page;
            ViewBag.pageSize = model.PageSize;
            ViewBag.termId = model.TermId;
            ViewBag.countryId = model.CountryId;
            ViewBag.gradeId = model.GradeId;
            ViewBag.subjectId = model.SubjectId;

            ViewBag.user = _userMgr.GetUserId(HttpContext.User);
            ViewBag.countries = new SelectList(_unitOfWork.CountryRepository.Filter(u => u.IsPuplished), "Id", "Name");
            return View();

        }


        [HttpPost]
        public ViewComponentResult Search(LibrarySearchModel model)
        {
            return ViewComponent("SearchLibrary", new {
                pageSize = model.PageSize,
                page = model.Page,
                keyword = model.Keyword,
                countryId = model.CountryId,
                gradeId = model.GradeId,
                TermId = model.TermId,
                SubjectId=model.SubjectId
            });
        }

        [HttpGet]
        public ViewComponentResult Search(int pageSize, 
            int page,
            string keyword,
            long countryId,
            long gradeId, 
            long termId,
            long subjectId)
        {
            return ViewComponent("SearchLibrary", new {
                pageSize = pageSize,
                page = page,
                keyword = keyword,
                countryId = countryId,
                gradeId = gradeId,
                TermId = termId,
                SubjectId = subjectId
            });
        }








        [HttpPost]
        public IActionResult LoadDrp([FromBody] ItemDto model)
        {
            List<ItemDto> result = new List<ItemDto>();
            List<ItemIdDto> result1 = new List<ItemIdDto>();

            switch (model.Name)
            {
                case "CountryId":
                    result = _unitOfWork.GradeRepository.Filter(u => u.CountryId == model.Id && u.IsPuplished == true).OrderBy(u=>u.Id).Select(u => new ItemDto()
                    {
                        Id = u.Id,
                        Name = u.Name
                    }).ToList();
                    break;
                case "GradeId":
                    result = _unitOfWork.TermRepository.Filter(u => u.GradeId == model.Id && u.IsPuplished == true).OrderBy(u => u.Id).Select(u => new ItemDto()
                    {
                        Id = u.Id,
                        Name = u.Name
                    }).ToList();
                    break;

                case "TermId":
                    result = _unitOfWork.SubjectRepository.Filter(u => u.TermId == model.Id && u.IsPuplished == true).OrderBy(u => u.Id).Select(u => new ItemDto()
                    {
                        Id = u.Id,
                        Name = u.Name
                    }).ToList();
                    break;


                default:
                    break;
            };

            //if (model.Name == "Subject")
            //    return Json(new { list = result, list2 = result1 });

            return Json(result);
        }


        [HttpGet]
        [Route("Library/FilterSelect/{id}/{value}")]
        public IActionResult FilterSelect(string Id, string value)
        {
            List<ItemDto> result = new List<ItemDto>();
            if (Id == "country")
            {
                result = _unitOfWork.GradeRepository.Filter(u => u.CountryId == int.Parse(value)).Select(u => new ItemDto()
                {
                    Id = u.Id,
                    Name = u.Name
                }).ToList();


            }

            else if (Id == "grade")
            {
                result = _unitOfWork.TermRepository.Filter(u => u.GradeId == int.Parse(value)).Select(u => new ItemDto()
                {
                    Id = u.Id,
                    Name = u.Name
                }).ToList();


            }
            else if (Id == "term")
            {
                result = _unitOfWork.SubjectRepository.Filter(u => u.TermId == int.Parse(value)).Select(u => new ItemDto()
                {
                    Id = u.Id,
                    Name = u.Name
                }).ToList();


            }

            return Json(result);

        }


        //public IActionResult  Details(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var subject = _unitOfWork.BookRepository.GetOneSubject(id);
        //   // var BookModel = _mapper.Map<Subject, SubjectsViewModel>(subject);
        //    if (subject == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(subject);
        //}

        public ActionResult Details(long id)
        {
            //var userId = (await GetCurrentUser()).Id;
         return  GetbookbyId(id);
        }

        private ActionResult GetbookbyId(long id)
        {
            var subject = _unitOfWork.SubjectRepository.GetSubjectbyId(id);
                if (subject == null)
                    return null;

                return View(subject);
        }



    }

}
