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
using System.IO;
using System.Diagnostics;
using System.Net.Http;
using System.Net;

namespace Drossey.Areas.admin.Controllers
{
    [Authorize(Roles = "Administrator")]
    [Area("Admin")]
    [ApiExplorerSettings(IgnoreApi = true)]

    public class LessonsController : BaseController
    {
        public LessonsController(IUnitOfWorkAsync unitOfWork, SignInManager<ApplicationUser> signInMgr, UserManager<ApplicationUser> userMgr, IPasswordHasher<ApplicationUser> hasher, IConfiguration config, IMapper mapper, ILogger<BaseController> logger, IMessenger messenger, IHostingEnvironment hostingEnvironment) : base(unitOfWork, signInMgr, userMgr, hasher, config, mapper, logger, messenger, hostingEnvironment)
        {
        }

        public IActionResult Index(SearchLessonModel model)
        {
            ViewBag.Keyword = model.Keyword;
            ViewBag.page = model.Page;
            ViewBag.pageSize = model.PageSize;
            ViewBag.termId = model.TermId;
            ViewBag.countryId = model.CountryId;
            ViewBag.gradeId = model.GradeId;
            ViewBag.subjectId = model.SubjectId;
            ViewBag.bookId = model.BookId;
            ViewBag.countries = new SelectList(_unitOfWork.CountryRepository.Filter(u=>u.IsPuplished), "Id", "Name");
            return View();

        }


        [HttpPost]
        public ViewComponentResult Search(SearchLessonModel model)
        {
            return ViewComponent("SearchLessons", new { pageSize = model.PageSize, page = model.Page, keyword = model.Keyword, countryId = model.CountryId, gradeId = model.GradeId, TermId = model.TermId,SubjectId=model.SubjectId, BookId = model.BookId });
        }

        [HttpGet]
        public ViewComponentResult Search(int pageSize, int page, string keyword,long countryId ,long gradeId,long termId,long subjectId,long bookId)
        {
            return ViewComponent("SearchLessons", new { pageSize = pageSize, page = page, keyword = keyword, countryId = countryId, gradeId = gradeId, TermId = termId , SubjectId = subjectId, BookId = bookId });
        }


        public IActionResult Create(long moduleId=0)
        {
            LoadCountries();

            if (moduleId != 0)
            {
                var book = _unitOfWork.BookRepository.All()
                    .Include(u => u.Subject)
                    .ThenInclude(u => u.Term)
                    .ThenInclude(u => u.Grade).ThenInclude(u => u.Country)
                    .FirstOrDefault(u => u.Id == moduleId);

                if (book == null)
                    return View("Create", new LessonViewModel());
                else
                {
                   
                    LoadTerms(book.Subject.Term.GradeId);
                    LoadGrades(book.Subject.Term.Grade.CountryId);
                    LoadSubjects(book.Subject.TermId);
                    LoadBooks(book.SubjectId);
                    return View("Create", new LessonViewModel()
                    {
                        CountryId = book.Subject.Term.Grade.CountryId,
                        TermId = book.Subject.TermId,
                        GradeId = book.Subject.Term.GradeId,
                        SubjectId = book.SubjectId,
                        ModuleId=book.Id
                    });
                }


             }
               
            return View("Create", new LessonViewModel());
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(long? moduleId, LessonViewModel lesson)
        {


            if (ModelState.IsValid && !ValidateModel(lesson))
            {
                if (_unitOfWork.LessonRepository.All().Any(u => u.Name.ToLower() == lesson.Name.ToLower() && u.ModuleId == lesson.ModuleId))
                {
                    LoadCountries();
                    LoadGrades(lesson.CountryId);
                    LoadTerms(lesson.GradeId);
                    LoadSubjects(lesson.TermId);
                    LoadBooks(lesson.SubjectId);
                    ModelState.AddModelError("", "هذة الدرس مسجلة من قبل .");
                    return View(lesson);
                }

                var model = _mapper.Map<LessonViewModel, Lesson>(lesson);
                var order = _unitOfWork.LessonRepository.All()
                   .Where(u => u.ModuleId == lesson.ModuleId);

                model.Order = order.Any() ? order.Max(u => u.Order) + 1 : 1;

                _unitOfWork.LessonRepository.Create(model);

                await _unitOfWork.CommitAsync();

                if(lesson.HasLink) CreateFolder(model.Id, 0);
                if (lesson.HasQuizLink) CreateFolder(model.Id, 1);


                _messenger.Success(
                 title: $"تنبية",
                  text: "تم اضافة الدرس  بنجاح");

                if (moduleId != null)
                    return RedirectToAction(nameof(Details),"Modules", new { id = moduleId });
                else
                   return RedirectToAction(nameof(Index));
            }
            LoadCountries();
            LoadGrades(lesson.CountryId);
            LoadTerms(lesson.GradeId);
            LoadSubjects(lesson.TermId);
            LoadBooks(lesson.SubjectId);
            return View(lesson);
        }

        private bool ValidateModel(LessonViewModel lesson)
        {
            #region validate Model

           

            if (lesson.GradeId == 0)
            {
                ModelState.AddModelError("", "الصف الدراسى مطللوب");
                LoadCountries();
                LoadGrades(lesson.CountryId);
                return true;
            }
            if (lesson.TermId == 0)
            {
                ModelState.AddModelError("", "الترم الدراسى مطللوب");
                LoadCountries();
                LoadGrades(lesson.CountryId);
                LoadTerms(lesson.GradeId);
                return true;

            }
            if (lesson.SubjectId == 0)
            {
                ModelState.AddModelError("", " المادة الدراسية مطلوبة");
                LoadCountries();
                LoadGrades(lesson.CountryId);
                LoadTerms(lesson.GradeId);
                LoadSubjects(lesson.TermId);
                return true;
            }
            if (lesson.ModuleId == 0)
            {
                ModelState.AddModelError("", " الوحدة الدراسية مطلوبة");
                LoadCountries();
                LoadGrades(lesson.CountryId);
                LoadTerms(lesson.GradeId);
                LoadSubjects(lesson.TermId);
                LoadBooks(lesson.SubjectId);
                return true;
            }
            //if (!CheckIfFileExists(lesson.PhotoUrl))
            //{
            //    ModelState.AddModelError("", "يجب اضافة الصورة");
            //    LoadCountries();
            //    LoadGrades(lesson.CountryId);
            //    LoadTerms(lesson.GradeId);
            //    LoadSubjects(lesson.TermId);
            //    LoadBooks(lesson.SubjectId);

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
            var lesson = _unitOfWork.LessonRepository.Filter(u=>u.Id==id)
                .Include(y => y.Module)
                .ThenInclude(y => y.Subject)
                .ThenInclude(y=>y.Term)
                .ThenInclude(u=>u.Grade)
                .ThenInclude(u=>u.Country).FirstOrDefault();

            if (lesson == null)
            {
                return NotFound();
            }
            var model = new LessonViewModel()
            {
                Id = lesson.Id,
                Name=lesson.Name,
                ModuleId = lesson.ModuleId,
                SubjectId = lesson.Module.SubjectId,
                TermId = lesson.Module.Subject.TermId,
                CountryId = lesson.Module.Subject.Term.Grade.CountryId,
                GradeId = lesson.Module.Subject.Term.GradeId,
                IsPuplished =lesson.IsPuplished,
                MeetingLiveLink=lesson.MeetingLiveLink,
                MeetingRecoredLink = lesson.MeetingRecoredLink,
                //QuizLink=lesson.QuizLink,
                //Link= lesson.Link,
                //PhotoUrl =lesson.PhotoUrl,

                CreationDate=lesson.CreationDate       
            };
            LoadCountries();
            LoadGrades(model.CountryId);
            LoadTerms(model.GradeId);
            LoadSubjects(model.TermId);
            LoadBooks(model.SubjectId);
            //if (!string.IsNullOrEmpty(model.PhotoUrl))
            //    ViewBag.PhotoUrl = GetFullPath(model.PhotoUrl);
            //else
            //    ViewBag.PhotoUrl = "";

            return View(model);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(long id, LessonViewModel lesson)
        {

            if (ModelState.IsValid && !ValidateModel(lesson))
            {        
                try
                {
                    if (_unitOfWork.LessonRepository.All().Any(u => u.Name.ToLower() == lesson.Name.ToLower() && u.ModuleId == lesson.ModuleId && u.Id != id))
                    {
                        ModelState.AddModelError("", "هذة الدرس مسجل من قبل .");
                        LoadCountries();
                        LoadGrades(lesson.CountryId);
                        LoadTerms(lesson.GradeId);
                        LoadSubjects(lesson.TermId);
                        LoadBooks(lesson.SubjectId);
                        //loadPhoto(lesson.PhotoUrl);
                        return View(lesson);
                    }

                    var old = _unitOfWork.LessonRepository.Find(lesson.Id);
                    if (old == null)
                    {
                        return NotFound();
                    }
                    //var photoUrl = old.PhotoUrl;

                    old.Update(lesson.Name, lesson.ModuleId,lesson.MeetingLiveLink,lesson.MeetingRecoredLink ,lesson.IsPuplished);
                    _unitOfWork.Commit();
                    //if (_unitOfWork.Commit() && old.PhotoUrl!=lesson.PhotoUrl)
                    //    DeleteImage(photoUrl);

                    if (!lesson.HasLink)
                        DeleteFolder(lesson.Id,0);
                    if (!lesson.HasQuizLink)
                        DeleteFolder(lesson.Id,1);



                    _messenger.Success(
                     title: $"تنبية",
                      text: "تم تعديل الدرس  بنجاح");
                }
                catch (DbUpdateConcurrencyException)
                {
                    _messenger.Error(
               title: $"تحذير",
                text: "حدث خطأ أثناء تعديل الدرس .");

                }

                return RedirectToAction(nameof(Index));
            }
            LoadCountries();
            LoadGrades(lesson.CountryId);
            LoadTerms(lesson.GradeId);
            LoadSubjects(lesson.TermId);
            //loadPhoto(lesson.PhotoUrl);
            return View(lesson);
           
                            
        }

        [HttpPost]
        public async Task<ActionResult> Delete(SearchLessonModel model)
        {

            try
            {
                var sub = _unitOfWork.LessonRepository.Find(model.Id);

                if (sub != null)
                {
                    

                    //var users = _unitOfWork.CartRepository.Filter(x => x.BookId == sub.BookId).Any();
                    //if (users)
                    //    return StatusCode(404, "OrdersUsedLessons");


                    _unitOfWork.LessonRepository.Delete(sub);
                    await _unitOfWork.CommitAsync();
                    return RedirectToAction("Search", new { Page = model.Page, PageSize = model.PageSize, keyword = model.Keyword,  countryId=model.CountryId,  gradeId=model.GradeId,  termId=model.TermId,subjectId=model.SubjectId,bookId=model.BookId });

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
                  text: "هذاالدرس مستخدم لا يمكن حذفه ");
                return StatusCode(404, "Error");


            }
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

            return View(model);
        }

        [HttpPost]
        public IActionResult LoadDrp([FromBody] ItemDto model)
        {
            List<ItemDto> result = new List<ItemDto>();
            List<ItemIdDto> result1 = new List<ItemIdDto>();

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

                case "SubjectId":
                    result = _unitOfWork.BookRepository.Filter(u => u.SubjectId == model.Id)
                        .Select(u => new ItemDto()
                        {
                            Id = u.Id,
                            Name = u.Name
                        }).ToList();
                    break;

                case "Subject":
                    result = _unitOfWork.BookRepository.Filter(u => u.SubjectId == model.Id)
                        .Select(u => new ItemDto()
                    {
                        Id = u.Id,
                        Name = u.Name
                    }).ToList();

                    var teacherIds = _unitOfWork.TeacherSubjectRepository.All()
                        .Where(u => u.SubjectId == model.Id)
                        .Select(u=>u.TeacherId);
                    result1 = _unitOfWork.TeacherRepository.Filter(u => (!teacherIds.Any() || teacherIds.Contains(u.Id)))
                        .Select(u => new ItemIdDto()
                    {
                        Id = u.Email,
                        Name = u.Name
                    }).ToList();
                    break;

                case "BookId":
                    result = _unitOfWork.LessonRepository.Filter(u => u.ModuleId == model.Id).Select(u => new ItemDto()
                    {
                        Id = u.Id,
                        Name = u.Name
                    }).ToList();
                    break;

                default:
                    break;
            };

            if (model.Name== "Subject")
                return Json(new { list = result, list2 = result1 });

            return Json(result);
        }


        [Authorize]
        public IActionResult File(long id)
        {
            return PhysicalFile(Path.Combine(_hostingEnvironment.ContentRootPath, "Lessons/G05_T01_U01_CH01_L01/lesson_player/index.html"), "text/html");
        }

        //[Authorize]
        //[Route("admin/Lessons/css/{name}")]
        //[Route("admin/Lessons/css/{path}/{name}")]
        //[Route("admin/Lessons/css/{path}/{subpath}/{name}")]
        //public IActionResult CssFile(string name, string path = "", string subpath = "")
        //{



        //    if (!string.IsNullOrEmpty(path) && string.IsNullOrEmpty(subpath))
        //    {
        //        if(name.ToLower().Contains("png"))
        //        return PhysicalFile(Path.Combine(_hostingEnvironment.ContentRootPath, "Lessons/G05_T01_U01_CH01_L01/lesson_player/css/" + path), "image/png");

        //            else
        //            return PhysicalFile(Path.Combine(_hostingEnvironment.ContentRootPath, "Lessons/G05_T01_U01_CH01_L01/lesson_player/css/"+path, name), "text/css");

        //    }

        //    //if (!string.IsNullOrEmpty(subpath))
        //    //    return PhysicalFile(Path.Combine(_hostingEnvironment.ContentRootPath, "Lessons/G05_T01_U01_CH01_L01/lesson_player/css/" + path +"/"+ subpath, name), "image/png");



        //    return PhysicalFile(Path.Combine(_hostingEnvironment.ContentRootPath, "Lessons/G05_T01_U01_CH01_L01/lesson_player/css",name), "text/css");
        //}
        //[Route("admin/Lessons/js/{name}")]
        //public IActionResult jsFile(string name)
        //{
        //    return PhysicalFile(Path.Combine(_hostingEnvironment.ContentRootPath, "Lessons/G05_T01_U01_CH01_L01/lesson_player/js", name), "text/javascript");
        //}

        //[Route("admin/Lessons/fonts/{name}")]
        //public IActionResult fontFile(string name)
        //{
        //    return PhysicalFile(Path.Combine(_hostingEnvironment.ContentRootPath, "Lessons/G05_T01_U01_CH01_L01/lesson_player/fonts", name), "application/octet-stream");
        //}






        [Route("admin/Lessons/{id}/{name}/{level1}")]
        [Route("admin/Lessons/{id}/{name}/{level1}/{level2}")]
        [Route("admin/Lessons/{id}/{name}/{level1}/{level2}/{level3}")]
        [Route("admin/Lessons/{id}/{name}/{level1}/{level2}/{level3}/{level4}")]
        [Route("admin/Lessons/{id}/{name}/{level1}/{level2}/{level3}/{level4}/{level5}")]
        [Route("admin/Lessons/{id}/{name}/{level1}/{level2}/{level3}/{level4}/{level5}/{level6}")]
        [Route("admin/Lessons/{id}/{name}/{level1}/{level2}/{level3}/{level4}/{level5}/{level6}/{level7}")]
        [Route("admin/Lessons/{id}/{name}/{level1}/{level2}/{level3}/{level4}/{level5}/{level6}/{level7}/{level8}")]

        public IActionResult AllFile(long id,string name, string level1, string level2 = "",
            string level3="", string level4 = "" , string level5="", string level6 = "",
            string level7 = "", string level8 = "", string level9 = "")
        {
            List<string> parameters = new List<string>() { level1, level2, level3, level4, level5, level6, level7, level8, level9 };
            int count=parameters.Count(u => string.IsNullOrEmpty(u));
            var path = name;
            var end = ((parameters.Count) - count);
            string extension = "";
            GetParameters(parameters, ref path, end, ref extension);
            var MIMExtentsion =GetMIMEtype(extension);
          if (MIMExtentsion== "mp3")
            {
                var stream = new System.IO.FileStream(Path.Combine(_hostingEnvironment.ContentRootPath, "Lessons/G05_T01_U01_CH01_L01/lesson_player/", path), System.IO.FileMode.Open);
                var returStream = new StreamContent(stream);
                return File(stream, "application/octet-stream");
            }
            else
            {
               return PhysicalFile(Path.Combine(_hostingEnvironment.ContentRootPath, "Lessons/G05_T01_U01_CH01_L01/lesson_player/", path), MIMExtentsion);

            }


        }

       


    }
}
