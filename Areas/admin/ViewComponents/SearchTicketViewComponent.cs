using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Drossey.Data.Core;
using Drossey.Data.Core.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace Drossey.Areas.admin.ViewComponents
{
    public class SearchTicketViewComponent : ViewComponent
    {
        public IUnitOfWorkAsync _unitOfWork;
        protected readonly IMapper _mapper;
        public SearchTicketViewComponent(IUnitOfWorkAsync unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IViewComponentResult> InvokeAsync(int? page, int pageSize,string keyword = "")
        {
            ViewBag.Keyword = keyword;
            ViewBag.page = page;
            ViewBag.pageSize = pageSize;
            var tickts = _unitOfWork.TicketRepository.All().Include(t => t.User);
            var message = "";
            if (tickts.Any())
            {
               var replayes =
                    _unitOfWork.TicketReplyRepository.All()
                        .OrderBy(x => x.Id)
                        .FirstOrDefault(x => x.TicketId == tickts.FirstOrDefault().Id);
                   message = replayes != null? replayes.Message: "";

            }
            ViewBag.ShortMessage = message;

            IQueryable<Ticket> setting = tickts.Where(x => string.IsNullOrEmpty(keyword) ||
                                          x.User.FullName.Contains(keyword) ||x.RequestId.ToString().Contains(keyword) 
                                         );
            ViewBag.ResultCount = setting.Count();
            int result = (setting.Count() / pageSize) + (setting.Count() % pageSize > 0 ? 1 : 0);
            if (page > 1 && result < page)
            {
                ViewBag.Page = page - 1;
                var settingList = await PaginatedList<Ticket>.CreateAsync(setting, page ?? 1, pageSize);
                return View(settingList);
            }
            else
            {
                var settingList = await PaginatedList<Ticket>.CreateAsync(setting.AsNoTracking(), page ?? 1, pageSize);
                return View(settingList);
            }
        
    }
    }
}