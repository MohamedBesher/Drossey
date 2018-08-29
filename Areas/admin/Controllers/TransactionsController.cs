using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Drossey.Data;
using Drossey.Data.Core;
using Drossey.Data.Core.Enum;
using Drossey.Data.Core.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using MotleyFlash;
using Drossey.Areas.admin.Models;

namespace Drossey.Areas.admin.Controllers
{
    [Authorize(Roles = "Administrator")]
    [Area("Admin")]
    [ApiExplorerSettings(IgnoreApi = true)]

    public class TransactionsController : BaseController
    {
       

 
        public TransactionsController(IUnitOfWorkAsync unitOfWork, SignInManager<ApplicationUser> signInMgr, UserManager<ApplicationUser> userMgr, IPasswordHasher<ApplicationUser> hasher, IConfiguration config, IMapper mapper, ILogger<BaseController> logger, IMessenger messenger, IHostingEnvironment hostingEnvironment) : base(unitOfWork, signInMgr, userMgr, hasher, config, mapper, logger, messenger, hostingEnvironment)
        {
        }
        
        public IActionResult Index(Pager model)
        {
            ViewBag.Keyword = model.Keyword;
            ViewBag.page = model.Page;
            ViewBag.pageSize = model.PageSize;
            ViewBag.userId = model.UserId;
            return View();
        }


        [HttpPost]
        public ViewComponentResult Search(SearchModel model)
        {

            return ViewComponent("SearchTransaction", new { userId=model.UserId, pageSize = model.PageSize, page = model.Page, keyword = model.Keyword });
        }

        [HttpGet]
        public ViewComponentResult Search(string userId, int pageSize, int page, string keyword)
        {

            return ViewComponent("SearchTransaction", new { userId, pageSize, page, keyword });
        }

        // GET: Transactions/Details/5
        public IActionResult Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var transactions = _unitOfWork.TransactionDetailsRepository
               .All()
               .Include(u=>u.Transaction)
               .ThenInclude(u=>u.User)
                .Include(t => t.Subject)
                .Where(u=>u.TransactionId==id)      
                .ToList();
            if (transactions == null || transactions.Count==0)
            {
                return NotFound();
            }

            return View(transactions);
        }

     
        // GET: Transactions/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var transaction = await _unitOfWork.TransactionRepository.All()
                .Include(t => t.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (transaction == null)
            {
                return NotFound();
            }

            return View(transaction);
        }

        // POST: Transactions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var transaction = await  _unitOfWork.TransactionRepository.All().SingleOrDefaultAsync(m => m.Id == id);
             _unitOfWork.TransactionRepository.Delete(transaction);
            await _unitOfWork.CommitAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TransactionExists(long id)
        {
            return  _unitOfWork.TransactionRepository.All().Any(e => e.Id == id);
        }

       
    }
}
