using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Drossey.Data.Core;
using Drossey.Data.Core.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Drossey.Admin;

namespace Drossey.Areas.admin.ViewComponents
{
    public class SearchGradesViewComponent : ViewComponent
    {
        public IUnitOfWorkAsync _unitOfWork;
        protected readonly IMapper _mapper;
        public SearchGradesViewComponent(IUnitOfWorkAsync unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IViewComponentResult> InvokeAsync(int? page, int pageSize,string keyword = "",long countryId=0)
        {
            ViewBag.Keyword = keyword;
            ViewBag.page = page;
            ViewBag.pageSize = pageSize;
            ViewBag.countryId = countryId;

            var grades = _unitOfWork.GradeRepository.All().Include(u=>u.Country);

            IQueryable<Grade> gradesList = grades.Where(x => (string.IsNullOrEmpty(keyword) ||
                                          x.Name.Contains(keyword))   && (countryId == 0 || x.CountryId == countryId)).OrderBy(u=>u.CountryId).ThenBy(u=>u.Name);
            ViewBag.ResultCount = gradesList.Count();
            int result = (gradesList.Count() / pageSize) + (gradesList.Count() % pageSize > 0 ? 1 : 0);
            if (page > 1 && result < page)
            {
                ViewBag.Page = page - 1;
                var categoryList = await PaginatedList<Grade>.CreateAsync(gradesList, page - 1 ?? 1, pageSize);
                return View(categoryList);
            }
            else
            {
                var categoryList = await PaginatedList<Grade>.CreateAsync(gradesList.AsNoTracking(), page ?? 1, pageSize);
                return View(categoryList);
            }
        
    }
    }
}