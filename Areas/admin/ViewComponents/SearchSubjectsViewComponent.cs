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
    public class SearchSubjectsViewComponent : ViewComponent
    {
        public IUnitOfWorkAsync _unitOfWork;
        protected readonly IMapper _mapper;
        public SearchSubjectsViewComponent(IUnitOfWorkAsync unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IViewComponentResult> InvokeAsync(int? page, int pageSize,string keyword = "", long countryId=0, long gradeId=0, long termId=0)
        {
            ViewBag.Keyword = keyword;
            ViewBag.page = page;
            ViewBag.pageSize = pageSize;
            ViewBag.termId = termId;
            ViewBag.gradeId = gradeId;
            ViewBag.countryId = countryId;
            var subjects = _unitOfWork.SubjectRepository.All().Include(u=>u.Term).ThenInclude(u=>u.Grade).ThenInclude(u=>u.Country).OrderByDescending(u => u.CreationDate);

            IQueryable<Subject> subjectsList = subjects.Where(x => 
                                (string.IsNullOrEmpty(keyword) || x.Name.Contains(keyword))&&
                                (termId == 0 || x.TermId == termId) &&
                                (gradeId == 0 || x.Term.GradeId == gradeId) &&
                                (countryId == 0 || x.Term.Grade.CountryId == countryId));

            ViewBag.ResultCount = subjectsList.Count();
            int result = (subjectsList.Count() / pageSize) + (subjectsList.Count() % pageSize > 0 ? 1 : 0);
            if (page > 1 && result < page)
            {
                ViewBag.Page = page - 1;
                var list = await PaginatedList<Subject>.CreateAsync(subjectsList, page - 1 ?? 1, pageSize);
                return View(list);
            }
            else
            {
                var list = await PaginatedList<Subject>.CreateAsync(subjectsList.AsNoTracking(), page ?? 1, pageSize);
                return View(list);
            }
        
    }
    }
}