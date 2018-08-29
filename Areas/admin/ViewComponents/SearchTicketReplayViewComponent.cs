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
    public class SearchTicketReplayViewComponent : ViewComponent
    {
        public IUnitOfWorkAsync _unitOfWork;
        protected readonly IMapper _mapper;
        public SearchTicketReplayViewComponent(IUnitOfWorkAsync unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IViewComponentResult> InvokeAsync(long ticketId ,int? page, int pageSize,string keyword = "")
        {
            ViewBag.Keyword = keyword;
            ViewBag.page = page;
            ViewBag.pageSize = pageSize;
            ViewBag.TicketId = ticketId;
            var ticket = await _unitOfWork.TicketReplyRepository.All()
                .Include(t => t.User)
                .Where(m => m.TicketId == ticketId).ToListAsync();

            IEnumerable<TicketReply> tickets = ticket.Where(x => string.IsNullOrEmpty(keyword) ||
                                          x.User.FullName.Contains(keyword) ||x.Message.Contains(keyword)
                                         ).ToList(); 

            ViewBag.ResultCount = tickets.Count();
            int result = (tickets.Count() / pageSize) + (tickets.Count() % pageSize > 0 ? 1 : 0);
            if (page > 1 && result < page)
            {
                ViewBag.Page = page - 1;
                var settingList = await PaginatedList<TicketReply>.CreateAsync(tickets, page ?? 1, pageSize);
                return View(settingList);
            }
            else
            {
                var settingList = await PaginatedList<TicketReply>.CreateAsync(tickets, page ?? 1, pageSize);
                return View(settingList);
            }
        
    }
    }
}