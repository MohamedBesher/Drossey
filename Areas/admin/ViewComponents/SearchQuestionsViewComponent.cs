using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Drossey.Data.Core;
using Drossey.Data.Core.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Drossey.Admin;
using Drossey.Data.Core.Dto;
using System.Collections.Generic;
using Drossey.Areas.admin.Models;

namespace Drossey.Areas.admin.ViewComponents
{
    public class SearchQuestionsViewComponent : ViewComponent
    {
        public IUnitOfWorkAsync _unitOfWork;
        public SearchQuestionsViewComponent(IUnitOfWorkAsync unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IViewComponentResult> InvokeAsync(int? page, int pageSize, string keyword = "", long countryId = 0, long gradeId = 0,
            long termId = 0, long subjectId = 0, long bookId = 0, long lessonId = 0)
        {


            ViewBag.Keyword = keyword;
            ViewBag.page = page;
            ViewBag.pageSize = pageSize;
            ViewBag.termId = termId;
            ViewBag.gradeId = gradeId;
            ViewBag.countryId = countryId;
            ViewBag.subjectId = subjectId;
            ViewBag.bookId = bookId;
            ViewBag.lessonId = lessonId;
            var question = _unitOfWork.QuestionRepository.All().Include(u => u.Lesson).ThenInclude(u => u.Module)
            .ThenInclude(u => u.Subject).ThenInclude(u => u.Term).ThenInclude(u => u.Grade).ThenInclude(u => u.Country); 

            var questions = _unitOfWork.QuestionRepository.All().Include(u => u.Lesson).ThenInclude(u => u.Module)
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
               Answers = u.Answers.Select(a => new AnswerViewModel()
               {
                   Answer = a.Answer,
                   IsCorrect = a.IsCorrect

               }).ToList()
            })
            .Where(x =>
                                (string.IsNullOrEmpty(keyword) || x.Body.Contains(keyword)) &&
                                (lessonId == 0 || x.LessonId == lessonId) &&
                                (bookId == 0 || x.BookId == bookId) &&
                                (subjectId == 0 || x.SubjectId == subjectId) &&
                                (termId == 0 || x.TermId == termId) &&
                                (gradeId == 0 || x.GradeId == gradeId) &&
                                (countryId == 0 || x.CountryId == countryId));
         
            ViewBag.ResultCount = questions.Count();

            int result = (questions.Count() / pageSize) + (questions.Count() % pageSize > 0 ? 1 : 0);
            if (page > 1 && result < page)
            {
                page = page - 1;
                ViewBag.Page = page;
                var list = await PaginatedList<QuestionViewModel>.CreateAsync(questions, page - 1 ?? 1, pageSize);
                return View(list);
            }
            else
            {
                var list = await PaginatedList<QuestionViewModel>.CreateAsync(questions.AsNoTracking(), page ?? 1, pageSize);
                return View(list);
            }
        }
        public IQueryable<QuestionDto> GetQuestions()
        {
            return _unitOfWork.QuestionRepository
                .All()
                //.Where(u => u.LessonId == quizId)
                .Select(u => new QuestionDto()
                {
                    Id = u.Id,
                    Body = u.Body,
                    type = u.Type
                })
                .OrderBy(u => u.type);
        }
    }



}