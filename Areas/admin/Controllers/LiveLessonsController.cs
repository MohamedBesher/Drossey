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
using Drossey.Admin.Services;

namespace Drossey.Areas.admin.Controllers
{
    [Authorize(Roles = "Administrator")]
    [Area("Admin")]
    [ApiExplorerSettings(IgnoreApi = true)]

    public class LiveLessonsController : BaseController
    {
        private readonly IWizIQSender _wizIQSender;
        private readonly ITimeZone _timeZone;

        public LiveLessonsController(IUnitOfWorkAsync unitOfWork, SignInManager<ApplicationUser> signInMgr, UserManager<ApplicationUser> userMgr, IPasswordHasher<ApplicationUser> hasher, IConfiguration config, IMapper mapper, ILogger<BaseController> logger, IMessenger messenger, IHostingEnvironment hostingEnvironment,  IWizIQSender wizIQSender, ITimeZone timeZone) : base(unitOfWork, signInMgr, userMgr, hasher, config, mapper, logger, messenger, hostingEnvironment)
        {
            _wizIQSender = wizIQSender;
            _timeZone = timeZone;
        }

        public IActionResult Index(SearchLiveLessonModel model)
        {
            ViewBag.Keyword = model.Keyword;
            ViewBag.page = model.Page;
            ViewBag.pageSize = model.PageSize;
            ViewBag.termId = model.TermId;
            ViewBag.countryId = model.CountryId;
            ViewBag.gradeId = model.GradeId;
            ViewBag.subjectId = model.SubjectId;
            ViewBag.bookId = model.BookId;
            ViewBag.lessonId = model.LessonId;
            ViewBag.countries = new SelectList(_unitOfWork.CountryRepository.Filter(u=>u.IsPuplished), "Id", "Name");
            return View();

        }


        [HttpPost]
        public ViewComponentResult Search(SearchLiveLessonModel model)
        {
            return ViewComponent("SearchLiveLessons", new { pageSize = model.PageSize, page = model.Page, keyword = model.Keyword, countryId = model.CountryId, gradeId = model.GradeId, TermId = model.TermId,SubjectId=model.SubjectId, BookId = model.BookId,LessonId=model.LessonId });
        }

        [HttpGet]
        public ViewComponentResult Search(int pageSize, int page, string keyword,long countryId ,long gradeId,long termId,long subjectId,long bookId,long lessonId)
        {
            return ViewComponent("SearchLiveLessons", new { pageSize = pageSize, page = page, keyword = keyword, countryId = countryId, gradeId = gradeId, TermId = termId , SubjectId = subjectId, BookId = bookId, LessonId= lessonId });
        }

        public IActionResult Create()
        {
            loadcountries();
            loadtimeZones();
            loadLanguages();
            return View("Create", new LessonLiveViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(LessonLiveViewModel lesson)
        {           
            if (ModelState.IsValid)
            {
                if (ValidateModel(lesson))
                {
                    loadtimeZones();
                    loadLanguages();
                    return View(lesson);
                }
                var teacher = _unitOfWork.TeacherRepository.All()
                    .FirstOrDefault(u => u.Email == lesson.Presenter_email);

                if(string.IsNullOrEmpty(teacher.teacher_id))
                {
                    var teacherId =_wizIQSender.AddTeacher(teacher.Name,teacher.Email,teacher.Password,
                        teacher.Phone_number,teacher.Mobile_number,teacher.Time_zone,teacher.About_the_teacher,Convert.ToInt32(teacher.Can_schedule_class).ToString(), Convert.ToInt32(teacher.Is_active).ToString(),"");

                   if (teacherId.Item2 == "ok")
                       teacher.teacher_id = teacherId.Item1;
                   else
                   {
                        ModelState.AddModelError("", teacherId.Item1);
                        loadcountries();
                        loadGrades(lesson.CountryId);
                        loadTerms(lesson.GradeId);
                        loadsubjects(lesson.TermId);
                        loadbooks(lesson.Subject);
                        loadLessons(lesson.Subject);
                        loadteachers(lesson.Subject);
                        loadtimeZones();
                        loadLanguages();
                        return View(lesson);
                    }
                }


                var result = _wizIQSender.Create(lesson.Start_time.ToString("MM/dd/yyyy HH:mm"), lesson.Presenter_email, lesson.Title,
                               lesson.Time_zone, lesson.Attendee_limit.ToString(), lesson.Duration.ToString(), lesson.Create_recording.ToString(),
                               lesson.Language_culture_name);

                if (result.Item2 == "ok")
                    lesson.ClassId = result.Item1;
                else
                {
                    ModelState.AddModelError("", result.Item1);
                    loadcountries();
                    loadGrades(lesson.CountryId);
                    loadTerms(lesson.GradeId);
                    loadsubjects(lesson.TermId);
                    loadbooks(lesson.Subject);
                    loadLessons(lesson.Subject);
                    loadteachers(lesson.Subject);
                    loadtimeZones();
                    loadLanguages();
                    return View(lesson);
                }
                //var teacher = _unitOfWork.TeacherRepository.All()
                //    .FirstOrDefault(u => u.Email == lesson.Presenter_email);
               
                var model = _mapper.Map<LessonLiveViewModel, LiveLesson>(lesson);
                if (teacher != null)
                    model.TeacherId = teacher.Id;

                _unitOfWork.LiveLessonRepository.Create(model);

                await _unitOfWork.CommitAsync();        
                _messenger.Success(
                 title: $"تنبية",
                  text: "تم اضافة الدرس  بنجاح");
                return RedirectToAction(nameof(Index));
            }
            loadcountries();
            loadGrades(lesson.CountryId);
            loadTerms(lesson.GradeId);
            loadsubjects(lesson.TermId);
            loadbooks(lesson.Subject);
            loadLessons(lesson.BookId);
            loadteachers(lesson.Subject);
            loadtimeZones();
            loadLanguages();
            return View(lesson);
        }
        private bool ValidateModel(LessonLiveViewModel lesson)
        {
            #region validate Model

           
            if (lesson.GradeId == 0)
            {
                ModelState.AddModelError("", "الصف الدراسى مطللوب");
                loadcountries();
                loadGrades(lesson.CountryId);
                return true;
            }
            if (lesson.TermId == 0)
            {
                ModelState.AddModelError("", "الترم الدراسى مطللوب");
                loadcountries();
                loadGrades(lesson.CountryId);
                loadTerms(lesson.GradeId);
                return true;

            }
            if (lesson.Subject == 0)
            {
                ModelState.AddModelError("", " المادة الدراسية مطلوبة");
                loadcountries();
                loadGrades(lesson.CountryId);
                loadTerms(lesson.GradeId);
                loadsubjects(lesson.TermId);
                return true;
            }
            if (lesson.BookId == 0)
            {
                ModelState.AddModelError("", " الوحدة الدراسية مطلوبة");
                loadcountries();
                loadGrades(lesson.CountryId);
                loadTerms(lesson.GradeId);
                loadsubjects(lesson.TermId);
                loadbooks(lesson.Subject);
                return true;
            }
          
            return false;
            #endregion
        }
        private void loadsubjects(long termId)
        {
            ViewData["subjects"] = new SelectList(_unitOfWork.SubjectRepository.Filter(u => u.IsPuplished && u.TermId ==termId), "Id", "Name");
        }
        private void loadbooks(long subjectId)
        {
            ViewData["books"] = new SelectList(_unitOfWork.BookRepository
                .Filter(u => u.IsPuplished && u.SubjectId == subjectId), "Id", "Name");
        }

        private void loadLessons(long bookId)
        {
            ViewData["lessons"] = new SelectList(_unitOfWork.LessonRepository.Filter
                (u => u.IsPuplished && u.ModuleId == bookId), "Id", "Name");
        }
        private void loadLanguages()
        {
            ViewData["languages"] = new SelectList(_timeZone.GetLanguages(), "Value", "Name");
        }
        private void loadTerms(long gradeId)
        {
            ViewData["terms"] = new SelectList(_unitOfWork.TermRepository.Filter(u => u.IsPuplished && u.GradeId == gradeId), "Id", "Name");
        }

        private void loadGrades(long countryId)
        {
            ViewData["grades"] = new SelectList(_unitOfWork.GradeRepository.Filter(u => u.IsPuplished && u.CountryId == countryId), "Id", "Name");

        }

        private void loadcountries()
        {
            ViewData["countries"] = new SelectList(_unitOfWork.CountryRepository.Filter(u => u.IsPuplished), "Id", "Name");

        }

        private void loadtimeZones()
        {
            ViewData["timeZones"] = new SelectList(_timeZone.GetTimesZones(), "Value", "Name");

        }
        private void loadteachers(long subjectId)
        {
            var teacherIds = _unitOfWork.TeacherSubjectRepository.All()
                        .Where(u => u.SubjectId == subjectId)
                        .Select(u => u.TeacherId);

            ViewData["teachers"] = new SelectList(_unitOfWork.TeacherRepository.Filter(u => (!teacherIds.Any() || teacherIds.Contains(u.Id))), "Email", "Name");    
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
            var liveLesson = _unitOfWork.LiveLessonRepository.Filter(u=>u.Id==id)
                .Include(u=>u.Teacher)
                .Include(y => y.Lesson)
                .ThenInclude(y => y.Module)
                .ThenInclude(y => y.Subject)
                .ThenInclude(y=>y.Term)
                .ThenInclude(u=>u.Grade)
                .ThenInclude(u=>u.Country).FirstOrDefault();

            if (liveLesson == null)
            {
                return NotFound();
            }
            var model = new LessonLiveViewModel()
            {
                Id = liveLesson.Id,
                Title = liveLesson.Title,
                BookId = liveLesson.Lesson.ModuleId,
                Subject = liveLesson.Lesson.Module.SubjectId,
                TermId = liveLesson.Lesson.Module.Subject.TermId,
                CountryId = liveLesson.Lesson.Module.Subject.Term.Grade.CountryId,
                GradeId = liveLesson.Lesson.Module.Subject.Term.GradeId,
                LessonId = liveLesson.LessonId,
                Start_time = liveLesson.Start_time,
                Time_zone = liveLesson.Time_zone,
                Presenter_email = liveLesson.Teacher.Email,
                Attendee_limit = int.Parse(liveLesson.Attendee_limit),
                Duration = liveLesson.Duration,
                Create_recording = liveLesson.Create_recording,
                Language_culture_name = liveLesson.Language_culture_name,
                ClassId = liveLesson.Class_id.ToString(),
                TeacherId=liveLesson.TeacherId,
               
            };
            loadcountries();
            loadGrades(model.CountryId);
            loadTerms(model.GradeId);
            loadsubjects(model.TermId);
            loadbooks(model.Subject);
            loadLessons(model.BookId);
            loadteachers(model.Subject);
            loadtimeZones();
            loadLanguages();

            return View(model);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, LessonLiveViewModel lesson)
        {

            if (ModelState.IsValid)
            {
                if (ValidateModel(lesson))
                {
                    loadtimeZones();
                    loadLanguages();
                    return View(lesson);
                }
                var old = _unitOfWork.LiveLessonRepository.Find(id);
                if (old==null)
                {
                    _messenger.Error(
                     title: $"تحذير",
                      text: "هذا الدرس غير موجود");
                    return RedirectToAction(nameof(Index));
                }
               
                var result = _wizIQSender.Modify(lesson.ClassId,lesson.Start_time.ToString("MM/dd/yyyy HH:mm"), lesson.Presenter_email, lesson.Title,
                                lesson.Time_zone, lesson.Attendee_limit.ToString(), lesson.Duration.ToString(), lesson.Create_recording.ToString(),
                                lesson.Language_culture_name);

                if (result.Item2 == "ok")
                    lesson.ClassId = result.Item1;
                else
                {
                    ModelState.AddModelError("", result.Item1);
                    loadcountries();
                    loadGrades(lesson.CountryId);
                    loadTerms(lesson.GradeId);
                    loadsubjects(lesson.TermId);
                    loadbooks(lesson.Subject);
                    loadLessons(lesson.BookId);
                    loadteachers(lesson.Subject);
                    loadtimeZones();
                    loadLanguages();
                    return View(lesson);
                }

                var teacher = _unitOfWork.TeacherRepository.All().FirstOrDefault(u => u.Email == lesson.Presenter_email);

                if (teacher != null)

                old.Update(lesson.Start_time, lesson.Presenter_email, lesson.Title, lesson.Time_zone, lesson.Attendee_limit.ToString(), lesson.Duration,
                    "", "", lesson.Create_recording, "", "", lesson.Language_culture_name, teacher.Id);


                await _unitOfWork.CommitAsync();

                _messenger.Success(
                 title: $"تنبية",
                  text: "تم اضافة الدرس  بنجاح");
                return RedirectToAction(nameof(Index));
            }
            loadcountries();
            loadGrades(lesson.CountryId);
            loadTerms(lesson.GradeId);
            loadsubjects(lesson.TermId);
            loadbooks(lesson.Subject);
            loadLessons(lesson.BookId);
            loadteachers(lesson.Subject);
            loadtimeZones();
            loadLanguages();
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
                    result = _unitOfWork.BookRepository.Filter(u => u.SubjectId == model.Id).Select(u => new ItemDto()
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


        [AcceptVerbs("Get", "Post")]
        public IActionResult VerifyDate(DateTime start_time)
        {
            try
            {
                if (DateTime.Compare(start_time, DateTime.Now) <= 0)
                {
                    return Json($"التاريخ يجب ان يكون اكبر من اليوم .");
                }
                return Json(true);
            }
            catch (Exception)
            {
                return Json(false);
            }
           
        }





    }
}
