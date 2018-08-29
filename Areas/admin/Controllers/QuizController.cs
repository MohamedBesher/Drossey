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

    public class QuizController : BaseController
    {
        public QuizController(IUnitOfWorkAsync unitOfWork, SignInManager<ApplicationUser> signInMgr, UserManager<ApplicationUser> userMgr, IPasswordHasher<ApplicationUser> hasher, IConfiguration config, IMapper mapper, ILogger<BaseController> logger, IMessenger messenger, IHostingEnvironment hostingEnvironment) : base(unitOfWork, signInMgr, userMgr, hasher, config, mapper, logger, messenger, hostingEnvironment)
        {
        }

        public IActionResult Index(SearchLiveLessonModel model)
        {
            ViewBag.Keyword = model.Keyword;
            ViewBag.page = model.Page;
            ViewBag.pageSize = model.PageSize;
            ViewBag.countryId = model.CountryId;
            ViewBag.gradeId = model.GradeId;
            ViewBag.subjectId = model.SubjectId;
            ViewBag.termId = model.TermId;
            ViewBag.bookId = model.BookId;
            ViewBag.lessonId = model.LessonId;
            ViewBag.countries = new SelectList(_unitOfWork.CountryRepository
                                                .Filter(u=>u.IsPuplished), "Id", "Name");
            return View();

        }


        [HttpPost]
        public ViewComponentResult Search(SearchLiveLessonModel model)
        {
            return ViewComponent("SearchQuiz",
                new {
                    pageSize = model.PageSize,
                    page = model.Page,
                    keyword = model.Keyword,
                    countryId = model.CountryId,
                    gradeId = model.GradeId,
                    TermId = model.TermId,
                    SubjectId =model.SubjectId,
                    BookId = model.BookId,
                    LessonId = model.LessonId
                });
        }
        [HttpGet]
        public ViewComponentResult Search(int pageSize, int page, string keyword,long countryId ,long gradeId,long termId,long subjectId)
        {
            return ViewComponent("SearchQuiz", new { pageSize = pageSize,
                                                    page = page,
                                                    keyword = keyword,
                                                    countryId = countryId,
                                                    gradeId = gradeId,
                                                    TermId = termId ,
                                                    SubjectId = subjectId });
        }


        public IActionResult Create(long SubjectId=0,long BookId=0,long LessonId=0)
        {
           
            ViewData["countries"] = new SelectList(_unitOfWork.CountryRepository.Filter(u=>u.IsPuplished), "Id", "Name");
            ViewBag.type = 3;

            if (SubjectId != 0 && BookId == 0 && LessonId == 0)
                ViewBag.type = 1;

            if (SubjectId == 0 && BookId != 0 && LessonId == 0)
                ViewBag.type = 2;


            return View("Create", new QuizViewModel());
        }






        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(long? subjectId, QuizViewModel book)
        {
            if (ModelState.IsValid && !ValidateModel(book))
            {

                if (_unitOfWork.QuizRepository.All().Any(u => u.Description.ToLower() == book.Description.ToLower()
                && (book.SubjectId == null || u.SubjectId == book.SubjectId)
                && (book.BookId == null || u.BookId == book.BookId)
                && (book.LessonId == null || u.LessonId == book.LessonId)
                ))
                {
                    LoadCountries();
                    LoadGrades(book.CountryId);
                    LoadTerms(book.GradeId);
                    LoadSubjects(book.TermId);
                    ModelState.AddModelError("", "هذة الاختبار مسجل من قبل .");
                    return View(book);
                }
                if(book.LessonId != null)
                {
                    book.SubjectId = null;
                    book.BookId = null;
                }
                else if (book.BookId != null)
                {
                    book.SubjectId = null;
                    
                }
                var model = _mapper.Map<QuizViewModel, Quiz>(book);
                _unitOfWork.QuizRepository.Create(model);
                await _unitOfWork.CommitAsync();
                _messenger.Success(
                 title: $"تنبية",
                  text: "تم اضافة الاختبار   بنجاح");
                if (subjectId != null)
                    return RedirectToAction(nameof(Details), "Quiz", new { id =  model.Id});
                else
                    return RedirectToAction(nameof(Index));
            }

            return View(book);
        }

        private bool ValidateModel(QuizViewModel book)
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
            
            return false;
            #endregion
        }

        


        public IActionResult Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }
          
            var quiz = _unitOfWork.QuizRepository.All().FirstOrDefault(u => u.Id == id);
            if (quiz == null)
            {
                return NotFound();
            }

            if (quiz.LessonId != null)
            {
                ViewBag.type = 3;
                quiz = _unitOfWork.QuizRepository.All()
                    .Include(u => u.Lesson)
                    .ThenInclude(u => u.Module)
                    .ThenInclude(u => u.Subject)
                    .ThenInclude(u => u.Term)
                     .ThenInclude(u => u.Grade)
                      .ThenInclude(u => u.Country)
                    .FirstOrDefault(u => u.Id == id);
            }
            else if (quiz.BookId != null)
            {
                ViewBag.type = 2;
                quiz = _unitOfWork.QuizRepository.All()
                    .Include(u => u.Book)
                 .ThenInclude(u => u.Subject)
                  .ThenInclude(u => u.Term)
                     .ThenInclude(u => u.Grade)
                      .ThenInclude(u => u.Country)
                 .FirstOrDefault(u => u.Id == id);
            }
            else if (quiz.SubjectId != null)
            {
                ViewBag.type = 1;
                quiz = _unitOfWork.QuizRepository.All()
                 .Include(u => u.Subject)
                  .ThenInclude(u => u.Term)
                     .ThenInclude(u => u.Grade)
                      .ThenInclude(u => u.Country)
                 .FirstOrDefault(u => u.Id == id);
            }



            var model = new QuizViewModel()
            {
                Id = quiz.Id,
                Description = quiz.Description,

                LessonId = quiz.Lesson != null ? quiz.Lesson.Id : -1,
                BookId = quiz.Lesson != null ? quiz.Lesson.Module.Id : quiz.Book != null ? quiz.Book.Id : -1,
                SubjectId = quiz.Lesson != null ? quiz.Lesson.Module.Subject.Id : quiz.Book != null ? quiz.Book.Subject.Id : quiz.Subject != null ? quiz.Subject.Id : -1,

                TermId= quiz.Lesson != null ? quiz.Lesson.Module.Subject.Term.Id : quiz.Book != null ? quiz.Book.Subject.Term.Id : quiz.Subject != null ? quiz.Subject.Term.Id : -1,
                GradeId = quiz.Lesson != null ? quiz.Lesson.Module.Subject.Term.Grade.Id : quiz.Book != null ? quiz.Book.Subject.Term.Grade.Id : quiz.Subject != null ? quiz.Subject.Term.Grade.Id :-1,
                CountryId = quiz.Lesson != null ? quiz.Lesson.Module.Subject.Term.Grade.Country.Id : quiz.Book != null ? quiz.Book.Subject.Term.Grade.Country.Id : quiz.SubjectId != null ? quiz.Subject.Term.Grade.Country.Id :-1,
                ChooseCount = quiz.ChooseCount,
                TrueFalseCount = quiz.TrueFalseCount,
                CompeleteCount = quiz.CompeleteCount
            };
            ViewBag.countries = new SelectList(_unitOfWork.CountryRepository.All(), "Id", "Name");
            ViewBag.grades = new SelectList(_unitOfWork.GradeRepository.Filter(u => u.CountryId == model.CountryId), "Id", "Name");
            ViewBag.terms = new SelectList(_unitOfWork.TermRepository.Filter(u =>  u.GradeId == model.GradeId), "Id", "Name");
            ViewBag.subjects = new SelectList(_unitOfWork.SubjectRepository.Filter(u => u.TermId == model.TermId), "Id", "Name");
            ViewBag.books = new SelectList(_unitOfWork.BookRepository.Filter(u =>  u.SubjectId == model.SubjectId), "Id", "Name");
            ViewBag.lessons = new SelectList(_unitOfWork.LessonRepository.Filter(u => u.ModuleId == model.BookId), "Id", "Name");

            return View(model);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(long id, QuizViewModel quiz)
        {

            if (ModelState.IsValid && !ValidateModel(quiz))
            {

                if (_unitOfWork.QuizRepository.All().Any(u => u.Description.ToLower() == quiz.Description.ToLower()
                && (quiz.SubjectId == null || u.SubjectId == quiz.SubjectId)
                && (quiz.BookId == null || u.BookId == quiz.BookId)
                && (quiz.LessonId == null || u.LessonId == quiz.LessonId)
                && (u.Id != quiz.Id)
                ))
                {
                    LoadCountries();
                    LoadGrades(quiz.CountryId);
                    LoadTerms(quiz.GradeId);
                    LoadSubjects(quiz.TermId);
                    ModelState.AddModelError("", "هذة الاختبار مسجل من قبل .");
                    return View(quiz);
                }

                try
                {

                    var old = _unitOfWork.QuizRepository.Find(id);

                    if (old == null)
                    {
                        return NotFound();

                    }

                    old.Description = quiz.Description;
                    old.ChooseCount = quiz.ChooseCount;
                    old.TrueFalseCount = quiz.TrueFalseCount;
                    old.CompeleteCount = quiz.CompeleteCount;
                    old.LessonId = quiz.LessonId;
                    old.BookId = quiz.BookId;
                    old.SubjectId = quiz.SubjectId;
                    _unitOfWork.CommitAsync();

                    _messenger.Success(
                    title: $"تنبية",
                     text: "تم تعديل الأختبار  بنجاح");


                }
                catch (DbUpdateConcurrencyException)
                {
                    _messenger.Error(
                       title: $"تحذير",
                        text: "حدث خطأ أثناء تعديل الأختبار .");

                }

                return RedirectToAction("Index","Quiz",new {area="Admin"});


            }

            return View(quiz);



        }


        public ActionResult Details(long id)
        {

            var quiz = _unitOfWork.QuizRepository.All().FirstOrDefault(u => u.Id == id);
            if (quiz == null)
            {
                return NotFound();
            }

            if(quiz.LessonId!=null)
            {
                quiz= _unitOfWork.QuizRepository.All()
                    .Include(u => u.Lesson)
                    .ThenInclude(u => u.Module)
                    .ThenInclude(u => u.Subject)
                    .ThenInclude(u=>u.Term)
                     .ThenInclude(u => u.Grade)
                      .ThenInclude(u => u.Country)
                    .FirstOrDefault(u => u.Id == id);
            }
            else if(quiz.BookId != null)
            {
                quiz = _unitOfWork.QuizRepository.All()
                    .Include(u=>u.Book)
                 .ThenInclude(u => u.Subject)
                  .ThenInclude(u => u.Term)
                     .ThenInclude(u => u.Grade)
                      .ThenInclude(u => u.Country)
                 .FirstOrDefault(u => u.Id == id);
            }
            else if (quiz.SubjectId != null)
            {
                quiz = _unitOfWork.QuizRepository.All()
                 .Include(u => u.Subject)
                  .ThenInclude(u => u.Term)
                     .ThenInclude(u => u.Grade)
                      .ThenInclude(u => u.Country)
                 .FirstOrDefault(u => u.Id == id);
            }
     


            var model = new QuizDetailsDto()
            {
                Id = quiz.Id,
                Description = quiz.Description,
                LessonName= quiz.Lesson!=null ? quiz.Lesson.Name :string.Empty,
                BookName= quiz.Lesson != null ? quiz.Lesson.Module.Name : quiz.Book!=null ? quiz.Book.Name : string.Empty,
                SubjectName = quiz.Lesson != null ? quiz.Lesson.Module.Subject.Name : quiz.Book != null ? quiz.Book.Subject.Name : quiz.Subject != null ? quiz.Subject.Name : string.Empty,
                TermName = quiz.Lesson != null ? quiz.Lesson.Module.Subject.Term.Name : quiz.Book != null ? quiz.Book.Subject.Term.Name : quiz.Subject != null ? quiz.Subject.Term.Name : string.Empty,
                GradeName = quiz.Lesson != null ? quiz.Lesson.Module.Subject.Term.Grade.Name : quiz.Book != null ? quiz.Book.Subject.Term.Grade.Name : quiz.Subject != null ? quiz.Subject.Term.Grade.Name : string.Empty,
                CountryName = quiz.Lesson != null ? quiz.Lesson.Module.Subject.Term.Grade.Country.Name : quiz.Book != null ? quiz.Book.Subject.Term.Grade.Country.Name : quiz.Subject != null ? quiz.Subject.Term.Grade.Country.Name : string.Empty,
                CreationDate= quiz.CreationDate
            };

            ViewBag.Keyword = "";
            ViewBag.page = 1;
            ViewBag.pageSize = 10;
            ViewBag.quizId = model.Id;
            ViewBag.Quiz = model;

            // var questions = GetQuestionsByQuizId(model.Id);
            //return View(questions);
            return View();
        }




        [HttpPost]
        public async Task<ActionResult> Delete(SearchLiveLessonModel model)
        {
            try
            {
                var OldQuiz = _unitOfWork.QuizRepository.Find(model.Id);
                if (OldQuiz != null)
                {
                    _unitOfWork.QuizRepository.Delete(OldQuiz);
                    await _unitOfWork.CommitAsync();
                  
                    return ViewComponent("SearchQuiz",
                 new
                 {
                     pageSize = model.PageSize,
                     page = model.Page,
                     keyword = model.Keyword,
                     countryId = model.CountryId,
                    gradeId = model.GradeId,
                     TermId = model.TermId,
                     SubjectId = model.SubjectId,
                     BookId = model.BookId,
                     LessonId = model.LessonId
                 });
                }
                else
                    return StatusCode(404, "NotFound");          
            }
            catch (Exception)
            {
                return StatusCode(404, "Error");
            }
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


        //public List<QuestionDto> GetQuestionsByQuizId(long quizId)
        //{
        //    return _unitOfWork.QuestionRepository
        //        .All()
        //        .Where(u=>u.QuizId== quizId)            
        //        .Select(u => new QuestionDto()
        //        {
        //           Id=u.Id,
        //           Body=u.Body,
        //           type=u.Type
        //        })
        //        .OrderBy(u => u.type).ToList();
        //}

    }
}
