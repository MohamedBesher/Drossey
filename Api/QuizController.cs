using AutoMapper;
using Drossey.Data.Core;
using Drossey.Data.Core.Dto;
using Drossey.Data.Core.Enum;
using Drossey.Data.Core.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Drossey.Api.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Produces("application/json")]
    [Route("api/Quiz")]

    public class QuizController : BaseController
    {
        public QuizController(IUnitOfWorkAsync unitOfWork, SignInManager<ApplicationUser> signInMgr, UserManager<ApplicationUser> userMgr, IPasswordHasher<ApplicationUser> hasher, ILogger<AuthController> logger, IConfiguration config, IMapper mapper) : base(unitOfWork, signInMgr, userMgr, hasher, logger, config, mapper)
        {
        }

        private List<T> Randamize<T>(List<T> list)
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

        [HttpGet("{id}")]
        public async Task<IActionResult> Index(long id)
        {
            try
            {
                var QuizData = _unitOfWork.QuizRepository.FindQuizType(id);
                List<QuestionDto> Question = new List<QuestionDto>();
                var userId = await GetUserId();
                if (QuizData.LessonId != null)
                    Question = _unitOfWork.QuizRepository.GetQuizLesson(QuizData.LessonId.Value, userId);
                else if (QuizData.BookId != null)
                    Question = _unitOfWork.QuizRepository.GetQuizModule(QuizData.BookId.Value, userId);
                else
                    Question = _unitOfWork.QuizRepository.GetQuizSubject(QuizData.SubjectId.Value, userId);


                if (Question.Any())
                {
                    var AllQuestion = (Question.Where(q => q.type == QuestionType.Choose).Take(QuizData.ChooseCount)
                        .Union(Question.Where(q => q.type == QuestionType.Complete).Take(QuizData.CompeleteCount))
                        .Union(Question.Where(q => q.type == QuestionType.TrueFalse).Take(QuizData.TrueFalseCount))).ToList();
                    Question = Randamize(AllQuestion);
                    return Ok(Question);
                }
                else
                    return NotFound();
               
            }
            catch (Exception e)
            {

                return StatusCode(500,e.Message);
            }
           
           
        }
    }


}