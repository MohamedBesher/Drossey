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

namespace Drossey.Areas.admin.ViewComponents
{
    public class SearchQuizViewComponent : ViewComponent
    {
        public IUnitOfWorkAsync _unitOfWork;
        public SearchQuizViewComponent(IUnitOfWorkAsync unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IViewComponentResult> InvokeAsync(int? page, int pageSize,string keyword = "", long countryId=0, long gradeId=0, long termId=0, long subjectId = 0, long bookId=0,long lessonId=0)
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
            List<long> subjects = new List<long>() {  };
            List<long> modules = new List<long>() { };
            List<long> lessons = new List<long>() { };


         //   bookId != 0 && subjectId != 0 && termId!= 0 && gradeId!= 0 && countryId != 0
            if (lessonId == 0)
            {
                lessons = _unitOfWork.LessonRepository.All()
            .Include(u => u.Module)
            .ThenInclude(u => u.Subject)
            .ThenInclude(u => u.Term)
            .ThenInclude(u => u.Grade)
            .ThenInclude(u => u.Country)
                            .Where(x =>   (bookId == 0 || x.ModuleId == bookId) &&
                             (subjectId == 0 || x.Module.SubjectId == subjectId) &&
                            (termId == 0 || x.Module.Subject.TermId == termId) &&
                            (gradeId == 0 || x.Module.Subject.Term.GradeId == gradeId) &&
                            (countryId == 0 || x.Module.Subject.Term.Grade.CountryId == countryId))
                            .Select(u => u.Id).ToList();

                if (!lessons.Any())
                    lessons = new List<long>() { 0 };
          }
           if (lessonId == 0 &&  bookId == 0 )
            {

                 modules = _unitOfWork.BookRepository.All()
           .Include(u => u.Subject)
           .ThenInclude(u => u.Term)
           .ThenInclude(u => u.Grade)
           .ThenInclude(u => u.Country)
                           .Where(x => 
                            (subjectId == 0 || x.SubjectId == subjectId) &&
                           (termId == 0 || x.Subject.TermId == termId) &&
                           (gradeId == 0 || x.Subject.Term.GradeId == gradeId) &&
                           (countryId == 0 || x.Subject.Term.Grade.CountryId == countryId))
                           .Select(u => u.Id).ToList();



                if (!modules.Any())
                    modules = new List<long>() { 0 };
              

           } 
            if (lessonId == 0 && bookId == 0 && subjectId == 0)
           {
              subjects = _unitOfWork.SubjectRepository.All()    
             .Include(u => u.Term)
             .ThenInclude(u => u.Grade)
             .ThenInclude(u => u.Country)
                             .Where(x =>                              
                             (termId == 0 || x.TermId == termId) &&
                             (gradeId == 0 || x.Term.GradeId == gradeId) &&
                             (countryId == 0 || x.Term.Grade.CountryId == countryId))
                             .Select(u => u.Id).ToList();
                if (!subjects.Any())
                    subjects = new List<long>() { 0 };
            }

           
        
           

            IQueryable<Quiz> quiz = _unitOfWork
.QuizRepository
.All().Include(u=>u.Lesson).Include(u=>u.Book)
.Include(u=>u.Subject)
.Where(x =>
(
(string.IsNullOrEmpty(keyword) || x.Description.ToString().Contains(keyword))
&&
(
( x.LessonId!=null && ( lessons.Contains(x.LessonId.Value)) )
|| ( (x.BookId != null && (modules.Contains(x.BookId.Value))))
|| ((x.SubjectId != null && (subjects.Contains(x.SubjectId.Value)))) )  
            
&& (lessonId==0 || x.LessonId==lessonId)
            )
            );

            ViewBag.ResultCount = quiz.Count();
            int result = (quiz.Count() / pageSize) + (quiz.Count() % pageSize > 0 ? 1 : 0);
            if (page > 1 && result < page)
            {
                ViewBag.Page = page - 1;
                var list = await PaginatedList<Quiz>.CreateAsync(quiz.AsNoTracking(), page - 1 ?? 1, pageSize);
                return View(list);
            }
            else
            {
                var list = await PaginatedList<Quiz>.CreateAsync(quiz.AsNoTracking(), page ?? 1, pageSize);
                return View(list);
            }
        
    }
    }
}