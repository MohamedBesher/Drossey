using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Drossey.Areas.admin.Models;
using Drossey.Data.Core;
using Drossey.Data.Core.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace Drossey.Areas.admin.ViewComponents
{
    public class SearchServiceViewComponent : ViewComponent
    {
        public IUnitOfWorkAsync _unitOfWork;
        protected readonly IMapper _mapper;
        public SearchServiceViewComponent(IUnitOfWorkAsync unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IViewComponentResult> InvokeAsync(int? page, int pageSize, long categoryId,bool isActive, string keyword = "" )
        {
            ViewBag.Keyword = keyword;
            ViewBag.page = page;
            ViewBag.pageSize = pageSize;
            ViewBag.CategoryId = categoryId;
            ViewBag.IsActive = isActive;
            var services = _unitOfWork.ServiceRepository.All().Include(c => c.Category).Include(x => x.Levels).Where(x=>x.Category.IsActive == true);

            IQueryable<Service> service =  services.Where(x => (  string.IsNullOrEmpty(keyword) ||
                                                    x.Name.Contains(keyword) ||
                                                    x.NameAr.Contains(keyword) || x.Desp.Contains(keyword)
                                                    || x.DespAr.Contains(keyword))&&
                                                    (categoryId== 0 ||x.CategoryId== categoryId) 
                                                    && (x.IsActive == isActive));

         
            ViewBag.ResultCount = service.Count();
            int result = (service.Count() / pageSize) + (service.Count() % pageSize > 0 ? 1 : 0);
            if (page > 1 && result < page)
            {
                ViewBag.Page = page - 1;
                var serviceList = await PaginatedList<Service>.CreateAsync(service, page ?? 1, pageSize);
                return View(serviceList);
            }
            else
            {
                var serviceList = await PaginatedList<Service>.CreateAsync(service.AsNoTracking(), page ?? 1, pageSize);
                return View(serviceList);
            }
        
    }
    }
}