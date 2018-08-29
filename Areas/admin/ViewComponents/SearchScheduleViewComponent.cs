using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Drossey.Data.Core;
using Drossey.Data.Core.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Drossey.Areas.admin.ViewComponents
{
    public class SearchScheduleViewComponent : ViewComponent
    {
        public IUnitOfWorkAsync _unitOfWork;
        protected readonly IMapper _mapper;
        public SearchScheduleViewComponent(IUnitOfWorkAsync unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IViewComponentResult> InvokeAsync(string couffierId, int? page, int pageSize, string keyword = "")
        {
            ViewBag.Keyword = keyword;
            ViewBag.page = page;
            ViewBag.pageSize = pageSize;
            ViewBag.couffierId = couffierId;
            var schedules = _unitOfWork.ScheduleRepository.Filter(x=>x.CoiffeurId == couffierId).Include(t => t.Coiffeur);
         

            IQueryable<Schedule> schedule = schedules.Where(x =>   string.IsNullOrEmpty(keyword) ||
                                                           x.Coiffeur.FullName.Contains(keyword) 
            );
            ViewBag.ResultCount = schedule.Count();
            int result = (schedule.Count() / pageSize) + (schedule.Count() % pageSize > 0 ? 1 : 0);
            if (page > 1 && result < page)
            {
                ViewBag.Page = page - 1;
                var schedulelist = await PaginatedList<Schedule>.CreateAsync(schedule, page ?? 1, pageSize);
                return View(schedulelist);
            }
            else
            {
                var schedulelist = await PaginatedList<Schedule>.CreateAsync(schedule.AsNoTracking(), page ?? 1, pageSize);
                return View(schedulelist);
            }

        }
    }
}