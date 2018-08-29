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

    public class ModulesController : BaseController
    {
        public ModulesController(IUnitOfWorkAsync unitOfWork, SignInManager<ApplicationUser> signInMgr, UserManager<ApplicationUser> userMgr, IPasswordHasher<ApplicationUser> hasher, IConfiguration config, IMapper mapper, ILogger<BaseController> logger, IMessenger messenger, IHostingEnvironment hostingEnvironment) : base(unitOfWork, signInMgr, userMgr, hasher, config, mapper, logger, messenger, hostingEnvironment)
        {
        }

        public IActionResult Index(SearchBookModel model)
        {
            ViewBag.Keyword = model.Keyword;
            ViewBag.page = model.Page;
            ViewBag.pageSize = model.PageSize;
            ViewBag.termId = model.TermId;
            ViewBag.countryId = model.CountryId;
            ViewBag.gradeId = model.GradeId;
            ViewBag.subjectId = model.SubjectId;
            ViewBag.countries = new SelectList(_unitOfWork.CountryRepository.Filter(u=>u.IsPuplished), "Id", "Name");
            return View();

        }


        [HttpPost]
        public ViewComponentResult Search(SearchBookModel model)
        {
            return ViewComponent("SearchBooks", new { pageSize = model.PageSize, page = model.Page, keyword = model.Keyword, countryId = model.CountryId, gradeId = model.GradeId, TermId = model.TermId,SubjectId=model.SubjectId });
        }
        [HttpGet]
        public ViewComponentResult Search(int pageSize, int page, string keyword,long countryId ,long gradeId,long termId,long subjectId)
        {
            return ViewComponent("SearchBooks", new { pageSize = pageSize, page = page, keyword = keyword, countryId = countryId, gradeId = gradeId, TermId = termId , SubjectId = subjectId });
        }


        public IActionResult Create(long subjectId=0)
        {
            ViewData["countries"] = new SelectList(_unitOfWork.CountryRepository.Filter(u=>u.IsPuplished), "Id", "Name");

            if (subjectId!=0)
            {
                var subject = _unitOfWork.SubjectRepository.All()
                    .Include(u=>u.Term)
                    .ThenInclude(u=>u.Grade).ThenInclude(u=>u.Country)
                    .FirstOrDefault(u=>u.Id==subjectId);

                if (subject==null)
                    return View("Create", new BookViewModel());
                else
                {
                    LoadCountries();
                    LoadTerms(subject.Term.GradeId);
                    LoadGrades(subject.Term.Grade.CountryId);
                    LoadSubjects(subject.TermId);
                    return View("Create", new BookViewModel() {
                        CountryId = subject.Term.Grade.CountryId,
                        TermId = subject.TermId,
                        GradeId = subject.Term.GradeId ,
                        SubjectId =subject.Id});
                }


            }
            return View("Create", new BookViewModel());
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(long? subjectId, BookViewModel book)
        {
            

            if (_unitOfWork.BookRepository.All().Any(u => u.Name.ToLower() == book.Name.ToLower() && u.SubjectId == book.SubjectId))
            {
                ViewData["countries"] = new SelectList(_unitOfWork.CountryRepository.Filter(u => u.IsPuplished), "Id", "Name");
                ModelState.AddModelError("", "هذة المادة مسجلة من قبل .");
                return View(book);
            }
            if (ModelState.IsValid && !ValidateModel(book))
            {

                var order = _unitOfWork.BookRepository.All()
                    .Where(u => u.SubjectId == book.SubjectId);
                    

                book.Order = order.Any() ? order.Max(u => u.Order) + 1 : 1;

                var model = _mapper.Map<BookViewModel, Book>(book);
                _unitOfWork.BookRepository.Create(model);
                await _unitOfWork.CommitAsync();
                _messenger.Success(
                 title: $"تنبية",
                  text: "تم اضافة الوحدة الدراسية  بنجاح");
                if (subjectId != null)
                    return RedirectToAction(nameof(Details),"Subjects",new { id= subjectId });
                else
                return RedirectToAction(nameof(Index));
            }
           
            return View(book);
        }

        private bool ValidateModel(BookViewModel book)
        {
            #region validate Model

            if (book.GradeId == 0)
            {
                ModelState.AddModelError("", "الصف الدراسى مطللوب");
                LoadCountries();
                LoadGrades(book.CountryId);
                return true;
            }
            if (book.TermId == 0)
            {
                ModelState.AddModelError("", "الترم الدراسى مطللوب");
                LoadCountries();
                LoadGrades(book.CountryId);
                LoadTerms(book.GradeId);
                return true;

            }
            if (book.SubjectId == 0)
            {
                ModelState.AddModelError("", " المادة الدراسية مطلوبة");
                LoadCountries();
               LoadGrades(book.CountryId);
               LoadTerms(book.GradeId);
               LoadSubjects(book.TermId);


                return true;
            }
            //if (!CheckIfFileExists(book.PhotoUrl))
            //{
            //    ModelState.AddModelError("", "يجب اضافة الصورة");
            //    LoadCountries();
            //    LoadGrades(book.CountryId);
            //    LoadTerms(book.GradeId);
            //    LoadSubjects(book.TermId);
            //    return true;

            //}
            return false;
            #endregion
        }

        private void loadPhoto(string photoUrl)
        {
            if (!string.IsNullOrEmpty(photoUrl))
                ViewBag.PhotoUrl = GetFullPath(photoUrl);
            else
                ViewBag.PhotoUrl = "";
        }

        public IActionResult Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }          
            var book = _unitOfWork.BookRepository.Filter(u=>u.Id==id)
                .Include(y => y.Subject)
                .ThenInclude(y=>y.Term)
                .ThenInclude(u=>u.Grade).ThenInclude(u=>u.Country).FirstOrDefault();
            if (book == null)
            {
                return NotFound();
            }

            var model = new BookViewModel()
            {
                Id = book.Id,
                Name=book.Name,
                SubjectId=book.SubjectId,
                TermId = book.Subject.TermId,
                CountryId = book.Subject.Term.Grade.CountryId,
                GradeId = book.Subject.Term.GradeId,
                IsPuplished =book.IsPuplished,
                //PhotoUrl=book.PhotoUrl,
            };
            LoadCountries();
            LoadGrades(model.CountryId);
            LoadTerms(model.GradeId);
            LoadSubjects(model.TermId);
            //if (!string.IsNullOrEmpty(model.PhotoUrl))
            //    ViewBag.PhotoUrl = GetFullPath(model.PhotoUrl);
            //else
            //    ViewBag.PhotoUrl = "";

            return View(model);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(long id, BookViewModel book)
        {

            if (ModelState.IsValid && !ValidateModel(book))
            {


                

                try
                {
                    if (_unitOfWork.BookRepository.All().Any(u => u.Name.ToLower() == book.Name.ToLower() && u.SubjectId == book.SubjectId && u.Id != id))
                    {
                        ModelState.AddModelError("", "هذة المادة الدراسية مسجلة من قبل .");
                        ViewData["countries"] = new SelectList(_unitOfWork.CountryRepository.Filter(u => u.IsPuplished), "Id", "Name");
                        return View(book);
                    }

                    var old = _unitOfWork.BookRepository.Find(book.Id);
                   
                    if (old == null)
                    {
                        return NotFound();

                    }
                    var photoUrl = old.PhotoUrl;

                    old.Update(book.Name, book.SubjectId,book.IsPuplished);
                    _unitOfWork.Commit();
                    //if (_unitOfWork.Commit() && old.PhotoUrl!=book.PhotoUrl)
                    //    DeleteImage(photoUrl);



                }
                catch (DbUpdateConcurrencyException)
                {
                    _messenger.Error(
               title: $"تحذير",
                text: "حدث خطأ أثناء تعديل الوحدة الدراسية .");

                }

                return RedirectToAction(nameof(Index));
            }
          
          

            return View(book);
           
               
                
        }

        [HttpPost]
        public async Task<ActionResult> Delete(SearchBookModel model)
        {

            try
            {
                var sub = _unitOfWork.BookRepository.Find(model.Id);

                if (sub != null)
                {
                   

                    var users = _unitOfWork.LessonRepository.Filter(x => x.ModuleId == model.Id).Any();
                    if (users)
                        return StatusCode(404, "LessonsUsedBooks");


                    _unitOfWork.BookRepository.Delete(sub);
                    await _unitOfWork.CommitAsync();

                    return RedirectToAction("Search", new { Page = model.Page, PageSize = model.PageSize, keyword = model.Keyword,  countryId=model.CountryId,  gradeId=model.GradeId,  termId=model.TermId,subjectId=model.SubjectId });

                }

                else
                {
                    return StatusCode(404, "NotFound");

                }

            }
            catch (Exception)
            {
                _messenger.Error(
                   title: $"تنبية !",
                  text: "هذا الوحدة الدراسية مستخدمة لا يمكن حذفه ");
                return StatusCode(404, "Error");


            }
        }

        public List<BookLessonViewModel> GetLessonsByBookId(long bookId)
        {
            return _unitOfWork.LessonRepository.GetLessonsByBookId(bookId).OrderBy(u => u.Order)
                .Select(u => new BookLessonViewModel()
                {
                    Id = u.Id,
                    Name = u.Name,
                    Order = u.Order,
                    BookId = u.ModuleId

                }).ToList();
        }

        public ActionResult Details(long id)
        {

            var book = _unitOfWork.BookRepository.Filter(u => u.Id == id)
              .Include(y => y.Subject)
              .ThenInclude(y => y.Term)
              .ThenInclude(u => u.Grade)
              .ThenInclude(u => u.Country).FirstOrDefault();
            if (book == null)
            {
                return NotFound();
            }

            var model = new BookDto()
            {
                Id = book.Id,
                Name = book.Name,
                SubjectName = book.Subject.Name,
                TermName = book.Subject.Term.Name,
                CountryName = book.Subject.Term.Grade.Country.Name,
                GradeName = book.Subject.Term.Grade.Name,
                IsPublished = book.IsPuplished,
                PhotoUrl = book.PhotoUrl,
                CreationDate=book.CreationDate
            };
           
            if (!string.IsNullOrEmpty(model.PhotoUrl))
                ViewBag.PhotoUrl = GetFullPath(model.PhotoUrl);
            else
                ViewBag.PhotoUrl = "";
            ViewBag.Book = model;

            var books = GetLessonsByBookId(model.Id);
            return View(books);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Details(List<BookLessonViewModel> list)
        {

            List<int> distinct = list.Select(u => u.Order).Distinct().ToList();
            var count = distinct.Count();
            if (ModelState.IsValid)
            {
                if (count == list.Count)
                {
                    foreach (BookLessonViewModel item in list)
                    {
                        var book = _unitOfWork.LessonRepository.Find(item.Id);
                        if (book != null)
                        {
                            book.Order = item.Order;
                        }
                        _unitOfWork.Commit();
                    }
                    _messenger.Success(
                        title: $"تنبية",
                         text: $"تم تعديل ترتيب الدروس  بنجاح");
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    _messenger.Error(
                    title: $"تحذير",
                     text: "لا يمكن اضافة اكتر من درس بنفس الترتيب .");
                }
            }
            else
            {
                _messenger.Error(
                         title: $"تحذير",
                          text: "حدث خطأ أثناء تعديل ترتيب الدروس  بنجاح .");
            }


            return RedirectToAction(nameof(Details), new { id = list[0].BookId });


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
