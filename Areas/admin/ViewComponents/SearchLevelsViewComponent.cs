using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Drossey.Data.Core;
using Drossey.Data.Core.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace Drossey.Areas.admin.ViewComponents
{
    public class SearchLevelsViewComponent : ViewComponent
    {
        public IUnitOfWorkAsync _unitOfWork;
        protected readonly IMapper _mapper;
        public SearchLevelsViewComponent(IUnitOfWorkAsync unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IViewComponentResult> InvokeAsync(long? serviceId ,int ? page, int pageSize , string actionName , string controllerName , string keyword = "")
        {
            ViewBag.Keyword = keyword;
            ViewBag.page = page;
            ViewBag.pageSize = pageSize;
            ViewBag.serviceId = serviceId;
            ViewBag.actionName =actionName;
            ViewBag.controllerName = controllerName;

            var levels =_unitOfWork.LevelRepository.All().Include(x => x.Service).Where(x =>(!serviceId.HasValue || x.ServiceId == serviceId )&& (string.IsNullOrEmpty(keyword) ||
                                                                                       x.Name.Contains(keyword) ||
                                                                                       x.NameAr.Contains(keyword) || x.Service.Name.Contains(keyword)));
           
            
            ViewBag.ResultCount = levels.Count();
            int result = (levels.Count() / pageSize) + (levels.Count() % pageSize > 0 ? 1 : 0);
            if (page > 1 && result < page)
            {
                ViewBag.Page = page - 1;
                var levelList = await PaginatedList<Level>.CreateAsync(levels, page ?? 1, pageSize);
                return View(levelList);
            }
            else
            {
                var levelList = await PaginatedList<Level>.CreateAsync(levels, page ?? 1, pageSize);
                return View(levelList);
            }
        
    }
    }
}