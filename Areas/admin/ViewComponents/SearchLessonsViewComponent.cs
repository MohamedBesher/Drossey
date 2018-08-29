using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Drossey.Data.Core;
using Drossey.Data.Core.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Drossey.Admin;
using Drossey.Data.Core.Dto;

namespace Drossey.Areas.admin.ViewComponents
{
    public class SearchLessonsViewComponent : ViewComponent
    {
        public IUnitOfWorkAsync _unitOfWork;
        protected readonly IMapper _mapper;
        public SearchLessonsViewComponent(IUnitOfWorkAsync unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IViewComponentResult> InvokeAsync(int? page, int pageSize,string keyword = "", long countryId=0, long gradeId=0, long termId=0, long subjectId = 0, long bookId=0)
        {
            ViewBag.Keyword = keyword;
            ViewBag.page = page;
            ViewBag.pageSize = pageSize;
            ViewBag.termId = termId;
            ViewBag.gradeId = gradeId;
            ViewBag.countryId = countryId;
            ViewBag.subjectId = subjectId;
            ViewBag.bookId = bookId;


            IQueryable<LessonDto> lessons = _unitOfWork.LessonRepository.SearchLessons(page, pageSize, keyword, countryId, gradeId, termId, subjectId,bookId);
            ViewBag.ResultCount = lessons.Count();
            int result = (lessons.Count() / pageSize) + (lessons.Count() % pageSize > 0 ? 1 : 0);
            if (page > 1 && result < page)
            {
                ViewBag.Page = page - 1;
                var list = await PaginatedList<LessonDto>.CreateAsync(lessons, page - 1 ?? 1, pageSize);
                return View(list);
            }
            else
            {
                var list = await PaginatedList<LessonDto>.CreateAsync(lessons.AsNoTracking(), page ?? 1, pageSize);
                return View(list);
            }
        
    }
    }
}