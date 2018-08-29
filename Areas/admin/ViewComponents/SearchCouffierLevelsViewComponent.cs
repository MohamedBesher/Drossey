using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Drossey.Data.Core;
using Drossey.Data.Core.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Drossey.Areas.admin.ViewComponents
{
    public class SearchCouffierLevelsViewComponent : ViewComponent
    {
        public IUnitOfWorkAsync _unitOfWork;
        protected readonly IMapper _mapper;
        public SearchCouffierLevelsViewComponent(IUnitOfWorkAsync unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IViewComponentResult> InvokeAsync(long? serviceId, int? page, int pageSize, string keyword = "",string userId = "",string actionName="" ,string controllerName="")
        {
            ViewBag.Keyword = keyword;
            ViewBag.page = page;
            ViewBag.pageSize = pageSize;
            ViewBag.serviceId = serviceId;
            ViewBag.UserId = userId;
            ViewBag.actionName = "SearchLevels";
            ViewBag.controllerName = "Couffiers";

            IQueryable<CoiffeurServiceLevel> levels = _unitOfWork.CoiffeurServiceLevelsRepository.All()
                .Include(u => u.Level)
                .Include(x => x.Service)             
                .Where(x => x.UserId==userId);
                        
            ViewBag.ResultCount = levels.Count();
            int result = (levels.Count() / pageSize) + (levels.Count() % pageSize > 0 ? 1 : 0);
            if (page > 1 && result < page)
            {
                ViewBag.Page = page - 1;
                var levelList = await PaginatedList<CoiffeurServiceLevel>.CreateAsync(levels, page ?? 1, pageSize);
                return View(levelList);
            }
            else
            {
                var levelList = await PaginatedList<CoiffeurServiceLevel>.CreateAsync(levels, page ?? 1, pageSize);
                return View(levelList);
            }

        }
    }
}