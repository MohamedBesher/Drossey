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
    public class SearchTermsViewComponent : ViewComponent
    {
        public IUnitOfWorkAsync _unitOfWork;
        protected readonly IMapper _mapper;
        public SearchTermsViewComponent(IUnitOfWorkAsync unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IViewComponentResult> InvokeAsync(int? page, int pageSize,string keyword = "",long gradeId=0)
        {
            ViewBag.Keyword = keyword;
            ViewBag.page = page;
            ViewBag.pageSize = pageSize;
            ViewBag.gradeId = gradeId;

            var terms = _unitOfWork.TermRepository.All().Include(u=>u.Grade)
                .ThenInclude(u=>u.Country);

            IQueryable<Term> termsList = terms.Where(x => (string.IsNullOrEmpty(keyword) ||
                                          x.Name.Contains(keyword))   && (gradeId == 0 || x.GradeId == gradeId)).OrderByDescending(u => u.CreationDate)
                                          .OrderBy(u => u.GradeId).ThenBy(u => u.Id);

            ViewBag.ResultCount = termsList.Count();
            int result = (termsList.Count() / pageSize) + (termsList.Count() % pageSize > 0 ? 1 : 0);
            if (page > 1 && result < page)
            {
                ViewBag.Page = page - 1;
                var list = await PaginatedList<Term>.CreateAsync(termsList, page - 1 ?? 1, pageSize);
                return View(list);
            }
            else
            {
                var list = await PaginatedList<Term>.CreateAsync(termsList.AsNoTracking(), page ?? 1, pageSize);
                return View(list);
            }
        
    }
    }
}