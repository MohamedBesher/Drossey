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
    public class SearchCountriesViewComponent : ViewComponent
    {
        public IUnitOfWorkAsync _unitOfWork;
        protected readonly IMapper _mapper;
        public SearchCountriesViewComponent(IUnitOfWorkAsync unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IViewComponentResult> InvokeAsync(int? page, int pageSize,string keyword = "")
        {
            ViewBag.Keyword = keyword;
            ViewBag.page = page;
            ViewBag.pageSize = pageSize;
            var countries = _unitOfWork.CountryRepository.All();

            IQueryable<Country> countriesList = countries.Where(x => string.IsNullOrEmpty(keyword) ||
                                          x.Name.Contains(keyword)).OrderByDescending(u=>u.CreationDate);
            ViewBag.ResultCount = countriesList.Count();
            int result = (countriesList.Count() / pageSize) + (countriesList.Count() % pageSize > 0 ? 1 : 0);
            if (page > 1 && result < page)
            {
                ViewBag.Page = page - 1;
                var cityList = await PaginatedList<Country>.CreateAsync(countriesList, page-1 ?? 1, pageSize);
                return View(cityList);
            }
            else
            {
                var cityList = await PaginatedList<Country>.CreateAsync(countriesList.AsNoTracking(), page ?? 1, pageSize);
                return View(cityList);
            }
        
    }
    }

}