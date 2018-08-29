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
    public class SearchTeachersViewComponent : ViewComponent
    {
        public IUnitOfWorkAsync _unitOfWork;
        protected readonly IMapper _mapper;
        public SearchTeachersViewComponent(IUnitOfWorkAsync unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IViewComponentResult> InvokeAsync(int? page, int pageSize,string keyword = "", long countryId=0, long gradeId=0, long termId=0, long subjectId = 0)
        {
            ViewBag.Keyword = keyword;
            ViewBag.page = page;
            ViewBag.pageSize = pageSize;
            ViewBag.termId = termId;
            ViewBag.gradeId = gradeId;
            ViewBag.countryId = countryId;
            ViewBag.subjectId = subjectId;


            IQueryable<TeacherDto> books = _unitOfWork.TeacherRepository.SearchTeachers(page, pageSize, keyword, countryId, gradeId, termId, subjectId);
            ViewBag.ResultCount = books.Count();
            int result = (books.Count() / pageSize) + (books.Count() % pageSize > 0 ? 1 : 0);
            if (page > 1 && result < page)
            {
                ViewBag.Page = page - 1;
                var list = await PaginatedList<TeacherDto>.CreateAsync(books, page - 1 ?? 1, pageSize);
                return View(list);
            }
            else
            {
                var list = await PaginatedList<TeacherDto>.CreateAsync(books.AsNoTracking(), page ?? 1, pageSize);
                return View(list);
            }
        
    }
    }
}