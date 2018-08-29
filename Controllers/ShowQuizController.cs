using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Drossey.Areas.admin.Controllers;
using Drossey.Data.Core;
using Drossey.Data.Core.Dto;
using Drossey.Data.Core.Enum;
using Drossey.Data.Core.Models;
using Drossey.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using MotleyFlash;


namespace Drossey.Controllers
{
    [Authorize(Roles = "User,Administrator")]
    [ApiExplorerSettings(IgnoreApi = true)]

    public class ShowQuizController : BaseController
    {
        public ShowQuizController(IUnitOfWorkAsync unitOfWork,
                                 SignInManager<ApplicationUser> signInMgr,
                                 UserManager<ApplicationUser> userMgr,
                                 IPasswordHasher<ApplicationUser> hasher,
                                 IConfiguration config, IMapper mapper,
                                 ILogger<BaseController> logger, IMessenger messenger, IHostingEnvironment hostingEnvironment)
            : base(unitOfWork, signInMgr, userMgr, hasher, config, mapper, logger, messenger, hostingEnvironment)
        {
        }
        

        public  List<T> Randamize<T>(List<T> list)
        {
            Random rng = new Random();
            int n = list.Count;
            while (n > 1)
            {
                n--;
                int k = rng.Next(n + 1);
                T value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
            return list;
        }

        [Authorize(Roles = "User")]
        public async Task<IActionResult> Index(long QuizId)
        {
            
            var QuizData = _unitOfWork.QuizRepository.FindQuizType(QuizId);
            List<QuestionDto> Question = new List<QuestionDto>();
            var userId = _userMgr.GetUserId(HttpContext.User);
            var user =await _userMgr.FindByIdAsync(userId);
            //var InUserRole = await _userMgr.IsInRoleAsync(user, "User");
            var InAdminRole = await _userMgr.IsInRoleAsync(user, "Administrator");

            if (QuizData.LessonId != null)
            {
            
              

                Question = _unitOfWork.QuizRepository.GetQuizLesson(QuizData.LessonId.Value,userId,InAdminRole);
            }
            else if (QuizData.BookId != null)
            {
                Question = _unitOfWork.QuizRepository.GetQuizModule(QuizData.BookId.Value, userId, InAdminRole);
            }
            else
            {
                Question = _unitOfWork.QuizRepository.GetQuizSubject(QuizData.SubjectId.Value, userId, InAdminRole);

            }


            var AllQuestion = (Question.Where(q => q.type == QuestionType.Choose).Take(QuizData.ChooseCount)
                .Union(Question.Where(q => q.type == QuestionType.Complete).Take(QuizData.CompeleteCount))
                .Union(Question.Where(q => q.type == QuestionType.TrueFalse).Take(QuizData.TrueFalseCount))).ToList();

            Question = Randamize(AllQuestion);

            //var answers = quiz.Questions[0].Answers;
            ViewBag.Question = Question;

            return View();
        }


        [Authorize(Roles = "Administrator")]

        public async Task<IActionResult> GetQuestions(long id)
        {

            var QuizData = _unitOfWork.QuizRepository.All().Where(u=>u.LessonId==id).FirstOrDefault();
            List<QuestionDto> Question = new List<QuestionDto>();
            var userId = _userMgr.GetUserId(HttpContext.User);
            var user = await _userMgr.FindByIdAsync(userId);
            //var InUserRole = await _userMgr.IsInRoleAsync(user, "User");
            var InAdminRole = await _userMgr.IsInRoleAsync(user, "Administrator");

            if (QuizData.LessonId != null)
            {



                Question = _unitOfWork.QuizRepository.GetQuizLesson(QuizData.LessonId.Value, userId, InAdminRole);
            }
            else if (QuizData.BookId != null)
            {
                Question = _unitOfWork.QuizRepository.GetQuizModule(QuizData.BookId.Value, userId, InAdminRole);
            }
            else
            {
                Question = _unitOfWork.QuizRepository.GetQuizSubject(QuizData.SubjectId.Value, userId, InAdminRole);

            }


            var AllQuestion = (Question.Where(q => q.type == QuestionType.Choose).Take(QuizData.ChooseCount)
                .Union(Question.Where(q => q.type == QuestionType.Complete).Take(QuizData.CompeleteCount))
                .Union(Question.Where(q => q.type == QuestionType.TrueFalse).Take(QuizData.TrueFalseCount))).ToList();

            Question = Randamize(AllQuestion);

            //var answers = quiz.Questions[0].Answers;
            ViewBag.Question = Question;

            return View("Index", AllQuestion);
        }
    }
}