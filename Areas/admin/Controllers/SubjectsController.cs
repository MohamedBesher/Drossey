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

namespace Drossey.Areas.admin.Controllers
{
    [Authorize(Roles = "Administrator")]
    [Area("Admin")]
    [ApiExplorerSettings(IgnoreApi = true)]

    public class SubjectsController : BaseController
    {
        public SubjectsController(IUnitOfWorkAsync unitOfWork, SignInManager<ApplicationUser> signInMgr, UserManager<ApplicationUser> userMgr, IPasswordHasher<ApplicationUser> hasher, IConfiguration config, IMapper mapper, ILogger<BaseController> logger, IMessenger messenger, IHostingEnvironment hostingEnvironment) : base(unitOfWork, signInMgr, userMgr, hasher, config, mapper, logger, messenger, hostingEnvironment)
        {
        }

        public IActionResult Index(SearchSubjectModel model)
        {
            ViewBag.Keyword = model.Keyword;
            ViewBag.page = model.Page;
            ViewBag.pageSize = model.PageSize;
            ViewBag.termId = model.TermId;
            ViewBag.countryId = model.CountryId;
            ViewBag.gradeId = model.GradeId;
            ViewBag.countries = new SelectList(_unitOfWork.CountryRepository.Filter(u => u.IsPuplished), "Id", "Name");
            return View();

        }


        [HttpPost]
        public ViewComponentResult Search(SearchSubjectModel model)
        {
            return ViewComponent("SearchSubjects", new { pageSize = model.PageSize, page = model.Page, keyword = model.Keyword, countryId = model.CountryId, gradeId = model.GradeId, TermId = model.TermId });
        }

        [HttpGet]
        public ViewComponentResult Search(int pageSize, int page, string keyword, long countryId, long gradeId, long termId)
        {
            return ViewComponent("SearchSubjects", new { pageSize = pageSize, page = page, keyword = keyword, countryId = countryId, gradeId = gradeId, TermId = termId });
        }


        public IActionResult Create()
        {
           
            ViewData["countries"] = new SelectList(_unitOfWork.CountryRepository.Filter(u => u.IsPuplished), "Id", "Name");
            return View("Create", new SubjectViewModel());
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(SubjectViewModel subject)
        {
            if (_unitOfWork.SubjectRepository.All().Any(u => u.Name.ToLower() == subject.Name.ToLower() && u.TermId == subject.TermId))
            {
                ViewData["countries"] = new SelectList(_unitOfWork.CountryRepository.Filter(u => u.IsPuplished), "Id", "Name");
                ModelState.AddModelError("", "هذة المادة مسجلة من قبل .");
                return View(subject);
            }
            if (!ValidateModel(subject))
            {
               
                if (ModelState.IsValid)
                {
                    var model = _mapper.Map<SubjectViewModel, Subject>(subject);
                    _unitOfWork.SubjectRepository.Create(model);
                    await _unitOfWork.CommitAsync();
                    _messenger.Success(
                     title: $"تنبية",
                      text: "تم اضافة المادة  بنجاح");
                    return RedirectToAction(nameof(Index));
                }
               
            }
            return View(subject);
        }

        private bool ValidateModel(SubjectViewModel subject)
        {
            if (subject.GradeId == 0)
            {
                ModelState.AddModelError("", "الصف الدراسى مطللوب");
                LoadCountries();
                LoadGrades(subject.CountryId);
                return true;

            }
            if (subject.TermId == 0)
            {
                ModelState.AddModelError("", "الترم الدراسى مطللوب");
                LoadCountries();
                LoadGrades(subject.CountryId);
                LoadTerms(subject.GradeId);
                return true;

            }

            if (!CheckIfFileExists(subject.PhotoUrl))
            {
                ModelState.AddModelError("", "يجب اضافة الصورة");
                LoadCountries();
                LoadGrades(subject.CountryId);
                LoadTerms(subject.GradeId);
                return true;

            }
            if (subject.Description == null)
            {
                //ModelState.AddModelError("", "الوصف مطلوب");
                LoadCountries();
                LoadGrades(subject.CountryId);
                LoadTerms(subject.GradeId);
                return true;

            }


            return false;
        }

        public IActionResult Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var subject = _unitOfWork.SubjectRepository.Filter(u => u.Id == id).Include(y => y.Term).ThenInclude(u => u.Grade).FirstOrDefault();
            if (subject == null)
            {
                return NotFound();
            }

            var model = new SubjectViewModel()
            {
                Id = subject.Id,
                Name = subject.Name,
                TermId = subject.TermId,
                CountryId = subject.Term.Grade.CountryId,
                GradeId = subject.Term.GradeId,
                IsPuplished = subject.IsPuplished,
                Price = subject.Price,
                DiscountPercentage = subject.DiscountPercentage,
                Description = subject.Description,
                PhotoUrl = subject.PhotoUrl
            };
            if (!string.IsNullOrEmpty(model.PhotoUrl))
                ViewBag.PhotoUrl = GetFullPath(model.PhotoUrl);
            else
                ViewBag.PhotoUrl = "";

            LoadCountries();
            LoadGrades(model.CountryId);
            LoadTerms(model.GradeId);
            return View(model);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(long id, SubjectViewModel subject)
        {

            if (ModelState.IsValid && !ValidateModel(subject))
            {
                try
                {
                    if (_unitOfWork.SubjectRepository.All().Any(u => u.Name.ToLower() == subject.Name.ToLower() && u.TermId == subject.TermId && u.Id != id))
                    {
                        ModelState.AddModelError("", "هذة المادة الدراسية مسجلة من قبل .");
                        ViewData["countries"] = new SelectList(_unitOfWork.CountryRepository.Filter(u => u.IsPuplished), "Id", "Name");
                        return View(subject);
                    }

                    var old = _unitOfWork.SubjectRepository.Find(subject.Id);
                    if (old == null)
                    {
                        return NotFound();

                    }
                    var photoUrl = old.PhotoUrl;
                    old.Update(subject.Name, subject.TermId, subject.IsPuplished, subject.Price, subject.DiscountPercentage,subject.Description,subject.PhotoUrl);
                    if (_unitOfWork.Commit() && old.PhotoUrl != subject.PhotoUrl)
                        DeleteImage(photoUrl);

                    _messenger.Success(
                 title: $"تنبية",
                  text: "تم تعديل المادة  بنجاح");

                }
                catch (DbUpdateConcurrencyException)
                {
                    _messenger.Error(
               title: $"تحذير",
                text: "حدث خطأ أثناء تعديل الترم .");

                }

                return RedirectToAction(nameof(Index));
            }
            return View(subject);



        }

        [HttpPost]
        public async Task<ActionResult> Delete(SearchSubjectModel model)
        {

            try
            {
                var sub = _unitOfWork.SubjectRepository.Find(model.Id);

                if (sub != null)
                {
                    

                    var users = _unitOfWork.BookRepository.Filter(x => x.SubjectId == model.Id).Any();
                    if (users)
                        return StatusCode(404, "BooksUsedSubjects");



                    _unitOfWork.SubjectRepository.Delete(sub);
                    await _unitOfWork.CommitAsync();







                    return RedirectToAction("Search", new { Page = model.Page, PageSize = model.PageSize, keyword = model.Keyword, countryId = model.CountryId, gradeId = model.GradeId, termId = model.TermId });

                }

                else
                {
                    return StatusCode(404, "NotFound");

                }

            }
            catch (Exception )
            {
                _messenger.Error(
                   title: $"تنبية !",
                  text: "هذا المادة الدراسية مستخدمة لا يمكن حذفه ");
                return StatusCode(404, "Error");


            }
        }


        public IActionResult Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var subject = _unitOfWork.SubjectRepository.Filter(u => u.Id == id)
                .Include(y => y.Term)
                .ThenInclude(u => u.Grade).ThenInclude(u=>u.Country).FirstOrDefault();
            if (subject == null)
            {
                return NotFound();
            }

            
            if (!string.IsNullOrEmpty(subject.PhotoUrl))
                ViewBag.PhotoUrl = GetFullPath(subject.PhotoUrl);
            else
                ViewBag.PhotoUrl = "";




            var books=GetBookById(subject.Id);
            ViewBag.Subject = subject;
            return View(books);
        }


        public List<SubjectBookViewModel> GetBookById(long subject)
        {
            return _unitOfWork.BookRepository.GetBooksbyId(subject).OrderBy(u=>u.Order)
                .Select(u => new SubjectBookViewModel()
            {
                Id = u.Id,
                Name = u.Name,            
                Order=u.Order,
                SubjectId=u.SubjectId

            }).ToList();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Details(List<SubjectBookViewModel> list)
        {

            List<int> distinct = list.Select(u => u.Order).Distinct().ToList();
            var count = distinct.Count();
            if (ModelState.IsValid)
            {
                if (count == list.Count)
                {
                    foreach (SubjectBookViewModel item in list)
                    {
                        var book = _unitOfWork.BookRepository.Find(item.Id);
                        if (book != null)
                        {
                            book.Order = item.Order;
                        }
                        _unitOfWork.Commit();
                    }
                    _messenger.Success(
                        title: $"تنبية",
                         text: $"تم تعديل ترتيب الوحدات  بنجاح");
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    _messenger.Error(
                    title: $"تحذير",
                     text: "لا يمكن اضافة اكتر من وحدة بنفس الترتيب .");
                }
            }
            else
            {
                _messenger.Error(
                         title: $"تحذير",
                          text: "حدث خطأ أثناء تعديل ترتيب الوحدات  بنجاح .");
            }


            return RedirectToAction(nameof(Details), new { id = list[0].SubjectId });


        }

        //public ActionResult ChangeLevels(int count)
        //{

        //    return RedirectToAction(nameof(Create), 
        //        new { count = count, isAjax = true);
        //}



    }
}
