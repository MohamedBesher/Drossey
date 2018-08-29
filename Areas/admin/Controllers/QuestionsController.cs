using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Drossey.Areas.admin.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Drossey.Data.Core.Models;
using AutoMapper;
using Drossey.Data.Core;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using MotleyFlash;
using MotleyFlash.Extensions;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Text.RegularExpressions;
using Drossey.Models;
using Newtonsoft.Json;
using Drossey.Data.Core.Enum;

namespace Drossey.Areas.admin.Controllers
{
    [Authorize(Roles = "Administrator")]
    [Area("Admin")]
    [ApiExplorerSettings(IgnoreApi = true)]

    public class QuestionsController : BaseController
    {
        public QuestionsController(IUnitOfWorkAsync unitOfWork, SignInManager<ApplicationUser> signInMgr, UserManager<ApplicationUser> userMgr, IPasswordHasher<ApplicationUser> hasher, IConfiguration config, IMapper mapper, ILogger<BaseController> logger, IMessenger messenger, IHostingEnvironment hostingEnvironment) : base(unitOfWork, signInMgr, userMgr, hasher, config, mapper, logger, messenger, hostingEnvironment)
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
                                                .Filter(u => u.IsPuplished), "Id", "Name");
            return View();
        }

        [HttpGet]
        public ViewComponentResult SearchQuestions(int pageSize, int page, string keyword)
        {
            return ViewComponent("SearchQuestions", new
            {
                pageSize = pageSize,
                page = page,
                keyword = keyword,

            });
        }


        [HttpPost]
        public ViewComponentResult SearchQuestions(SearchLiveLessonModel model)
        {

            return ViewComponent("SearchQuestions",
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

        public IActionResult Create(Data.Core.Enum.QuestionType? QuestionType = null, long CountryId = 0, long GradeId = 0, long TermId = 0, long SubjectId = 0, long BookId = 0, long LessonId = 0)
        {
            var model = new QuestionViewModel();

            ViewData["countries"] = new SelectList(_unitOfWork.CountryRepository.Filter(u => u.IsPuplished), "Id", "Name");
            if (CountryId != 0)
            {
                model.CountryId = CountryId;
                LoadGrades(CountryId);
            }
            if (GradeId != 0)
            {
                model.GradeId = GradeId;
                LoadTerms(GradeId);
            }
            if (TermId != 0)
            {
                model.TermId = TermId;
                LoadSubjects(TermId);
            }
            if (SubjectId != 0)
            {
                model.SubjectId = SubjectId;
                LoadBooks(SubjectId);
            }
            if (BookId != 0)
            {
                model.BookId = BookId;
                LoadLessons(BookId);
            }
            if (LessonId != 0)
            {
                model.LessonId = LessonId;
            }

            if (QuestionType != null)
            {
                model.IsAjax = true;
                model.Type = QuestionType.Value;

            }
            else
                model.Type = Data.Core.Enum.QuestionType.Choose;
            return View(model);
        }



        public IActionResult CreateQuestions()
        {
            var model = new QuestionsViewModel();
            ViewData["countries"] = new SelectList(_unitOfWork.CountryRepository.Filter(u => u.IsPuplished), "Id", "Name");
            return View(model);
        }

        [HttpPost]
        public IActionResult CreateQuestions(QuestionsViewModel model)
        {


            if (ModelState.IsValid)
            {


                if (string.IsNullOrEmpty(model.TextJson))
                {
                    ModelState.AddModelError("", "نص الملف غير صالح");
                    return View(model);
                }
                else
                {
                    var result = AddQuestions(model.LessonId, model.TextJson);
                    if (!result)
                    {
                        ModelState.AddModelError("", "نص الملف غير صالح");
                        return View(model);
                    }
                }


                _messenger.Success(
                   title: $"تنبية",
                   text: "تم اضافة الأسئلة بنجاح");
                return RedirectToAction(nameof(Index));


            }

            return View(model);
        }

        private bool AddQuestions(long lessonId, string textData)
        {

            try
            {


                //string noComments = "";


                // var json = JsonConvert.SerializeObject(model.TextData);
                //var ggg = JsonConvert.DeserializeObject<QuizJson>(json);


                //#region Remove Commented Lines
                //var blockComments = @"/\*(.*?)\*/";
                //var lineComments = @"//(.*?)\r?\n";
                //var strings = @"""((\\[^\n]|[^""\n])*)""";
                //var verbatimStrings = @"@(""[^""]*"")+";

                //noComments = Regex.Replace(textData,
                //   blockComments + "|" + lineComments + "|" + strings + "|" + verbatimStrings,
                //           me =>
                //           {
                //               if (me.Value.StartsWith("/*") || me.Value.StartsWith("//"))
                //                   return me.Value.StartsWith("//") ? Environment.NewLine : "";
                //           // Keep the literal strings
                //           return me.Value;
                //           }, RegexOptions.Singleline);

                //#endregion

                //#region replace ' by "   
                //// ":'',or :''}"
                //string c1 = "(:)"; // Any Single Character 1
                //string c2 = "(\\s+)";  // White Space 1
                //string c3 = "(')"; // Any Single Character 2
                //                   //noComments = noComments.Replace($"\'", $"\"");
                //noComments = Regex.Replace(noComments, c1 + c2 + c3, ":\"", RegexOptions.Multiline | RegexOptions.IgnoreCase);



                //string c4 = "(')"; // Any Single Character 1
                //string c5 = "(\\s+)";  // White Space 1
                //string c6 = "(,)";  // Any Single Character 2
                //noComments = Regex.Replace(noComments, c4 + c5 + c6, "\",", RegexOptions.Multiline | RegexOptions.IgnoreCase);


                //string c7 = "(')"; // Any Single Character 1
                //string c8 = "(\\s+)";  // White Space 1
                //string c9 = "(})";  // Any Single Character 2
                //noComments = Regex.Replace(noComments, c7 + c8 + c9, "\"}", RegexOptions.Multiline | RegexOptions.IgnoreCase);







                //// not working 
                //string c10 = "({)";   // Any Single Character 1
                //string c11 = "(\\s+)";  // White Space 1
                //string c12 = "(')"; // Any Single Character 2
                //noComments = Regex.Replace(noComments, c10 + c11 + c12, "{\"", RegexOptions.Multiline | RegexOptions.IgnoreCase);

                //string c210 = "({)";   // Any Single Character 1
                //string c212 = "(')"; // Any Single Character 2
                //noComments = Regex.Replace(noComments, c210 + c212, "{\"", RegexOptions.Multiline | RegexOptions.IgnoreCase);



                //string re13 = "(')"; // Any Single Character 1
                //string re14 = "(\\s+)";  // White Space 1
                //string re15 = "(:)";    // Any Single Character 2

                //noComments = Regex.Replace(noComments, re13 + re14 + re15, "\":", RegexOptions.Multiline | RegexOptions.IgnoreCase);

                //string re33 = "(':)"; // Any Single Character 1


                //noComments = Regex.Replace(noComments, re33, "\":", RegexOptions.Multiline | RegexOptions.IgnoreCase);



                //string re16 = "(,)"; // Any Single Character 1
                //string re17 = "(\\s+)";  // White Space 1
                //string re18 = "(')"; // Any Single Character 2
                //noComments = Regex.Replace(noComments, re16 + re17 + re18, ",\"", RegexOptions.Multiline | RegexOptions.IgnoreCase);

                //#endregion

                //#region replace_quiz
                //string quiz1 = "(var\\s+quiz\\s+=\\s+)"; // Any Single Word Character (Not Whitespace) 1
                ////string quiz2 = "()"; // Any Single Word Character (Not Whitespace) 2
                ////string quiz3 = "()"; // Any Single Word Character (Not Whitespace) 3
                ////string quiz4 = "()";  // White Space 1
                ////string quiz5 = "()"; // Any Single Word Character (Not Whitespace) 4
                ////string quiz6 = "()"; // Any Single Word Character (Not Whitespace) 5
                ////string quiz7 = "()"; // Any Single Word Character (Not Whitespace) 6
                ////string quiz8 = "()"; // Any Single Word Character (Not Whitespace) 7
                ////string quiz9 = "()";  // White Space 2
                ////string quiz10 = "()";    // Any Single Character 1
                ////string quiz11 = "()"; // White Space 3
                //noComments = Regex.Replace(noComments, quiz1, "  ");
                //#endregion

                //#region Replace(},])with(},])
                //string re1 = "(\\})";   // Command Seperated Values 1
                //string re2 = "(,)"; // Any Single Character 1
                //string re3 = "(\\s+)";  // White Space 1
                //string re4 = "(\\])";   // Any Single Character 2
                //noComments = Regex.Replace(noComments, re1 + re2 + re3 + re4, "}]");

                //#endregion

                //#region Replace ; end of string
                //noComments = System.Text.RegularExpressions.Regex.Replace(noComments, ";$", "");
                //#endregion


                var model = JsonConvert.DeserializeObject<QuizJson>(textData);

                var questions = new List<Data.Core.Models.Question>();
                int trueFalse = model.report.trueFalse;
                int singleChoiceTxt = model.report.singleChoiceTxt;
                int multiChoiceTxt = model.report.multiChoiceTxt;
                //int singleChoiceImg = model.report.singleChoiceImg;
                int essay = 0;

                // int multiChoiceImg = model.report.multiChoiceImg;
                int fillBlanK = model.report.fillBlanK;
                // int fillDropDown = model.report.fillDropDown;
                //  int fillByDragDrop = model.report.fillByDragDrop;

                foreach (var question in model.questions)
                {
                    var item = new Data.Core.Models.Question();
                    item.LessonId = lessonId;
                    switch (question.type.ToLower())
                    {
                        case "fillblank":
                            item.Type = Data.Core.Enum.QuestionType.Complete;
                            item.Answers = new List<Answers>() {
                                 new Answers(){
                                     Answer = question.blankwords.FirstOrDefault().correct,
                                     IsCorrect=true }
                            };
                            item.grade = fillBlanK;
                            break;

                        case "truefalse":
                            item.Type = Data.Core.Enum.QuestionType.TrueFalse;
                            item.Answers = new List<Answers>() {
                                 new Answers(){
                                     Answer = null,
                                     IsCorrect=true }
                            };
                            item.grade = trueFalse;

                            break;

                        case "singlechoicetxt":
                            item.Type = Data.Core.Enum.QuestionType.Choose;
                            item.Answers = new List<Answers>() {
                                new Answers(){Answer = question.co_answer.ToString(),IsCorrect=true },
                            };
                            foreach (var wrong in question.wro_answer)
                            {
                                item.Answers.Add(new Answers() { Answer = wrong.wrong.ToString(), IsCorrect = false });
                            }
                            item.grade = singleChoiceTxt;
                            break;

                        //case "multiChoiceTxt":
                        //    item.Type = Data.Core.Enum.QuestionType.Choose;
                        //    item.Answers = new List<Answers>();

                        //    co_answer[] answers = (co_answer[])question.co_answer;
                        //    if (answers.Length > 0)
                        //    {
                        //        foreach (var answer in answers)
                        //        {
                        //            item.Answers.Add(new Answers() { Answer = answer.ToString(), IsCorrect = true });
                        //        }
                        //    }

                        //    foreach (var wrong in question.wro_answer)
                        //    {
                        //        item.Answers.Add(new Answers() { Answer = wrong.wrong.ToString(), IsCorrect = false });
                        //    }

                        //    item.grade = singleChoiceTxt;
                        //    break;

                        //case "essay":
                        //    item.Type = Data.Core.Enum.QuestionType.Complete;
                        //    item.Answers = new List<Answers>() {
                        //        new Answers(){Answer = question.rightfeedback.ToString(),IsCorrect=true },
                        //    };
                        //    item.grade = essay;
                        //    break;
                        default:
                            break;
                    }
                    item.Body = question.body;
                    questions.Add(item);


                }

                _unitOfWork.QuestionRepository.CreateRange(questions);
                _unitOfWork.Commit();

                return true;

                //Quiz quiz = new Quiz() {LessonId=lessonId,Description="اختبار", ChooseCount=20 ,CompeleteCount=20,TrueFalseCount=20 };

                //_unitOfWork.QuizRepository.Create(quiz);



            }
            catch (Exception )
            {

                return false;
            }
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(QuestionViewModel model)
        {

            try
            {
                if (ModelState.IsValid)
                {
                    if (!ValidateModel(model))
                    {


                        if (model.Type == Data.Core.Enum.QuestionType.Choose)
                        {
                            if (model.Answers.Count >= 3)
                            {
                                var question = _mapper.Map<QuestionViewModel, Data.Core.Models.Question>(model);
                                _unitOfWork.QuestionRepository.Create(question);
                            }
                            else
                            {
                                ModelState.AddModelError("", "اضف اجوبة لهذا السؤال");
                                return View(model);
                            }

                        }
                        else if (model.Type == Data.Core.Enum.QuestionType.TrueFalse)
                        {
                            var answer = new Answers() { IsCorrect = model.IsCorrect  };
                            var question = _mapper.Map<QuestionViewModel, Data.Core.Models.Question>(model);
                            question.Answers = new List<Answers>() { answer };
                            _unitOfWork.QuestionRepository.Create(question);



                        }
                        else if (model.Type == Data.Core.Enum.QuestionType.Complete)
                        {
                            var answer = new Answers() { IsCorrect = true, Answer = model.Answer };
                            var question = _mapper.Map<QuestionViewModel, Data.Core.Models.Question>(model);
                            question.Answers = new List<Answers>() { answer };
                            _unitOfWork.QuestionRepository.Create(question);


                        }

                        await _unitOfWork.CommitAsync();

                        _messenger.Success(
                        title: $"تنبية",
                        text: "تم اضافة السؤال  بنجاح");
                        return RedirectToAction(nameof(Index));
                    }
                }
                else
                {
                    ValidateModel(model);
                }
                return View(model);


            }
            catch (Exception)
            {
                return View(model);

                throw;
            }
        }

        public IActionResult Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var question = _unitOfWork.QuestionRepository.All()
                .Include(u => u.Lesson).ThenInclude(u => u.Module)
                .ThenInclude(u => u.Subject).ThenInclude(u => u.Term).ThenInclude(u => u.Grade).ThenInclude(u => u.Country)

                .Select(u => new QuestionViewModel()
                {
                    Id = u.Id,
                    Body = u.Body,
                    Type = u.Type,
                    LessonId = u.LessonId,
                    BookId = u.Lesson.ModuleId,
                    SubjectId = u.Lesson.Module.SubjectId,
                    TermId = u.Lesson.Module.Subject.TermId,
                    GradeId = u.Lesson.Module.Subject.Term.GradeId,
                    CountryId = u.Lesson.Module.Subject.Term.Grade.CountryId,
                    grade = u.grade,
                    Answers = u.Answers.Select(a => new AnswerViewModel()
                    {
                        Answer = a.Answer,
                        IsCorrect = a.IsCorrect

                    }).ToList()


                }).FirstOrDefault(u => u.Id == id);
            if(question.Type == QuestionType.TrueFalse)
            {
                question.IsCorrect = question.Answers[0].IsCorrect;
            }
            if (question == null)
            {
                return NotFound();
            }

            ViewBag.countries = new SelectList(_unitOfWork.CountryRepository.All(), "Id", "Name");
            ViewBag.grades = new SelectList(_unitOfWork.GradeRepository.Filter(u => u.CountryId == question.CountryId), "Id", "Name");
            ViewBag.terms = new SelectList(_unitOfWork.TermRepository.Filter(u => u.GradeId == question.GradeId), "Id", "Name");
            ViewBag.subjects = new SelectList(_unitOfWork.SubjectRepository.Filter(u => u.TermId == question.TermId), "Id", "Name");
            ViewBag.books = new SelectList(_unitOfWork.BookRepository.Filter(u => u.SubjectId == question.SubjectId), "Id", "Name");
            ViewBag.lessons = new SelectList(_unitOfWork.LessonRepository.Filter(u => u.ModuleId == question.BookId), "Id", "Name");

            return View(question);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(long id, QuestionViewModel question)
        {

            if (ModelState.IsValid)
            {
                if (!ValidateModel(question))
                {

                    try
                    {

                        var old = _unitOfWork.QuestionRepository.Find(id);

                        if (old == null)
                        {
                            return NotFound();

                        }

                        old.Body = question.Body;
                        old.grade = question.grade;
                        old.LessonId = question.LessonId;
                        if (question.Type == QuestionType.Choose)
                        {
                            _unitOfWork.AnswersRepository.DeleteAnswers(id);

                            var AnswerList = new List<Answers>();
                            foreach (AnswerViewModel answer in question.Answers)
                            {
                                var newAnswer = new Answers()
                                {
                                    Answer = answer.Answer,
                                    IsCorrect = answer.IsCorrect,
                                    QuestionId = id


                                };

                                AnswerList.Add(newAnswer);

                            }
                            _unitOfWork.AnswersRepository.CreateRange(AnswerList);
                            _unitOfWork.CommitAsync();
                        }
                        else if (question.Type == QuestionType.TrueFalse)
                        {
                            var oldAnswer = _unitOfWork.AnswersRepository.Filter(a => a.QuestionId == id).FirstOrDefault();
                            oldAnswer.IsCorrect = question.IsCorrect;
                            _unitOfWork.CommitAsync();
                        }
                        else if (question.Type == QuestionType.Complete)
                        {
                            var oldAnswer = _unitOfWork.AnswersRepository.Filter(a => a.QuestionId == id).FirstOrDefault();
                            oldAnswer.Answer = question.Answers[0].Answer;
                            _unitOfWork.CommitAsync();
                        }
                        _messenger.Success(
                        title: $"تنبية",
                         text: "تم تعديل السؤال  بنجاح");


                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        _messenger.Error(
                           title: $"تحذير",
                            text: "حدث خطأ أثناء تعديل السؤال .");

                    }

                    return RedirectToAction("Index", "Questions", new { area = "Admin" });
                }

            }

            return View(question);



        }




        [HttpPost]
        public async Task<ActionResult> Delete(SearchLiveLessonModel model)
        {
            try
            {
                var question = _unitOfWork.QuestionRepository.Find(model.Id);
                if (question != null)
                {
                    _unitOfWork.QuestionRepository.Delete(question);
                    await _unitOfWork.CommitAsync();
                    return RedirectToAction("SearchQuestions", new
                    {
                        Page = model.Page,
                        PageSize = model.PageSize,
                        keyword = model.Keyword,

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
        public ActionResult ChangeQuestionType(int QuestionType, long CountryId, long GradeId, long TermId, long SubjectId, long BookId, long LessonId)
        {
            Data.Core.Enum.QuestionType Type = Data.Core.Enum.QuestionType.Choose;
            switch (QuestionType)
            {
                case 2:
                    Type = Data.Core.Enum.QuestionType.TrueFalse;
                    break;
                case 3:
                    Type = Data.Core.Enum.QuestionType.Complete;
                    break;
                default:
                    Type = Data.Core.Enum.QuestionType.Choose;
                    break;
            }
            return RedirectToAction(nameof(Create), new { QuestionType = Type, CountryId = CountryId, GradeId = GradeId, TermId = TermId, SubjectId = SubjectId, BookId = BookId, LessonId = LessonId });




        }
        private bool ValidateModel(QuestionViewModel question)
        {
            #region validate Model

            int count = 0;

            if (question.GradeId == 0)
            {
                ModelState.AddModelError("", "الصف الدراسى مطلوب");
                LoadCountries();
                LoadGrades(question.CountryId);
                return true;
            }
            if (question.TermId == 0)
            {
                ModelState.AddModelError("", "الترم الدراسى مطلوب");
                LoadCountries();
                LoadGrades(question.CountryId);
                LoadTerms(question.GradeId);
                return true;

            }
            if (question.SubjectId == 0)
            {
                ModelState.AddModelError("", " المادة الدراسية مطلوبة");
                LoadCountries();
                LoadGrades(question.CountryId);
                LoadTerms(question.GradeId);
                LoadSubjects(question.TermId);
                return true;
            }
            if (question.BookId == 0)
            {
                ModelState.AddModelError("", " الوحدة الدراسية مطلوبة");
                LoadCountries();
                LoadGrades(question.CountryId);
                LoadTerms(question.GradeId);
                LoadSubjects(question.TermId);
                LoadBooks(question.SubjectId);
                return true;
            }
            if (question.LessonId == 0)
            {
                ModelState.AddModelError("", " الدرس مطلوب");
                LoadCountries();
                LoadData(question);
                return true;
            }
            if (question.grade < 0 || question.Body == null)
            {
                LoadData(question);
                return true;

            }
            if (question.Type == QuestionType.Choose)
            {
                if (question.Answers.Count != 4)
                {
                    ModelState.AddModelError("", " الأجابة  مطلوبة");
                    LoadData(question);
                    return true;
                }
                for (int i = 0; i < 4; i++)
                {
                    if (question.Answers[i].IsCorrect)
                        count++;
                }
                if (count != 1)
                {
                    ModelState.AddModelError("", "اختار اجابة واحده صحيحه");
                    LoadData(question);
                    return true;
                }
            }
            if (question.Type == QuestionType.Complete)
            {
                if (question.Body.IndexOf(" ===== ") == -1)
                {
                    ModelState.AddModelError("", "السؤال لابد ان يحتوى على ' ===== '");
                    LoadData(question);
                    return true;
                }
                if (question.Answer == null && question.Answers== null)
                {
                    ModelState.AddModelError("", " الأجابة  مطلوبة");
                    LoadData(question);
                    return true;
                }

            }
            return false;
            #endregion
        }

        private void LoadData(QuestionViewModel question)
        {
            LoadCountries();
            LoadGrades(question.CountryId);
            LoadTerms(question.GradeId);
            LoadSubjects(question.TermId);
            LoadBooks(question.SubjectId);
            LoadLessons(question.BookId);

        }
    }

}