using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Drossey.Areas.admin.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Drossey.Data;
using Drossey.Data.Core;
using Drossey.Data.Core.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using MotleyFlash;
using MotleyFlash.Extensions;

namespace Drossey.Areas.admin.Controllers
{
    [Authorize(Roles = "Administrator")]
    [ApiExplorerSettings(IgnoreApi = true)]

    public class TicketsController : BaseController
    {
       

        public TicketsController(IUnitOfWorkAsync unitOfWork, SignInManager<ApplicationUser> signInMgr, UserManager<ApplicationUser> userMgr, IPasswordHasher<ApplicationUser> hasher, IConfiguration config, IMapper mapper, ILogger<BaseController> logger, IMessenger messenger, IHostingEnvironment hostingEnvironment) : base(unitOfWork, signInMgr, userMgr, hasher, config, mapper, logger, messenger, hostingEnvironment)
        {
        }

        public IActionResult Index(Pager model)
        {

            ViewBag.Keyword = model.Keyword;
            ViewBag.page = model.Page;
            ViewBag.pageSize = model.PageSize;

            return View();
        }


        [HttpPost]
        public ViewComponentResult Search(SearchModel model)
        {

            return ViewComponent("SearchTicket", new { pageSize = model.PageSize, page = model.Page, keyword = model.Keyword });
        }

        [HttpGet]
        public ViewComponentResult Search(int pageSize, int page, string keyword)
        {

            return ViewComponent("SearchTicket", new { pageSize, page , keyword  });
        }
        [HttpPost]
        public ViewComponentResult SearchReplay(SearchTicketsModel model)
        {

            return ViewComponent("SearchTicketReplay", new { ticketId=model.TicketId, pageSize = model.PageSize, page = model.Page, keyword = model.Keyword });
        }

        [HttpGet]
        public ViewComponentResult SearchReplay(long ticketId ,int pageSize, int page, string keyword="")
        {

            return ViewComponent("SearchTicketReplay", new { ticketId , pageSize, page , keyword });
        }
        public  IActionResult Details(long? ticketId, SearchTicketsModel model)
        {
            ViewBag.Keyword = model.Keyword;
            ViewBag.page = model.Page;
            ViewBag.pageSize = model.PageSize;
            ViewBag.TicketId = ticketId;

            return View();
        }
        public IActionResult Create(long ticketId)
        {
          
            ViewBag.TicketId = ticketId;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(TicketReply model)
        {
            model.UserId= GetCurrentUser().Result.Id;
            if (ModelState.IsValid)
            {
                _unitOfWork.TicketReplyRepository.Create(model);
                await _unitOfWork.CommitAsync();

                _messenger.Success(
               title: $"تنبية !",
               text: "تم اضافة الرد بنجاح");
                return RedirectToAction("Details","Tickets",new { ticketId=model.TicketId});
            }
       
            return View(model);
        }

     
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ticket = await _unitOfWork.TicketRepository.All().SingleOrDefaultAsync(m => m.Id == id);
            if (ticket == null)
            {
                return NotFound();
            }
            ViewData["UserId"] = new SelectList(_unitOfWork.UserRepository.All(), "Id", "Id", ticket.UserId);
            return View(ticket);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("Id,UserId,RequestId")] Ticket ticket)
        {
            if (id != ticket.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _unitOfWork.TicketRepository.Update(ticket);
                    await _unitOfWork.CommitAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TicketExists(ticket.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["UserId"] = new SelectList(_unitOfWork.UserRepository.All(), "Id", "Id", ticket.UserId);
            return View(ticket);
        }

      
        public async Task<IActionResult> Delete(long id)
        {
            var ticket = await _unitOfWork.TicketRepository.All().SingleOrDefaultAsync(m => m.Id == id);
            if (ticket == null)
                return StatusCode(404, "NotFound");
            _unitOfWork.TicketRepository.Delete(ticket);
            await _unitOfWork.CommitAsync();
            _messenger.Success(
                    title: $"تنبية !",
                    text: "تم حذف الشكوى بنجاح");
            return RedirectToAction("Search", new { Page = 1, PageSize = 10 });
        }

        public async Task<IActionResult> DeleteReplay(long id)
        {
            var ticket = await _unitOfWork.TicketReplyRepository.All().SingleOrDefaultAsync(m => m.Id == id);
            if (ticket == null)
                return StatusCode(404, "NotFound");
            _unitOfWork.TicketReplyRepository.Delete(ticket);
            await _unitOfWork.CommitAsync();
            _messenger.Success(
                 title: $"تنبية !",
                 text: "تم حذف الرد بنجاح");
            return RedirectToAction("SearchReplay", new  { TicketId= ticket.TicketId, Page = 1, PageSize = 10 });
        }
        private bool TicketExists(long id)
        {
            return _unitOfWork.TicketRepository.All().Any(e => e.Id == id);
        }
    }
}
