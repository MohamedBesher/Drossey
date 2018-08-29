using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Drossey.Data.Core;
using Drossey.Data.Core.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Drossey.Areas.admin.ViewComponents
{
    public class SearchTransactionViewComponent : ViewComponent
    {
        public IUnitOfWorkAsync _unitOfWork;
        protected readonly IMapper _mapper;
        public SearchTransactionViewComponent(IUnitOfWorkAsync unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IViewComponentResult> InvokeAsync(string userId,int? page, int pageSize, string keyword = "")
        {
            ViewBag.Keyword = keyword;
            ViewBag.page = page;
            ViewBag.pageSize = pageSize;
            var transactions = _unitOfWork.TransactionRepository.All()
                
                .Include(x => x.User);
                
            if (!string.IsNullOrEmpty(userId))
            {
                transactions= _unitOfWork.TransactionRepository
                    .Filter(x => x.UserId == userId)
                 
                .Include(x => x.User);
            }
            

            IQueryable<Transaction> transaction = transactions
                .Where(x => string.IsNullOrEmpty(keyword) ||
                 x.User.FirstName.ToLower().Contains(keyword.ToLower()) || x.User.LastName.ToLower().Contains(keyword.ToLower())
            );
            ViewBag.ResultCount = transaction.Count();
            int result = (transaction.Count() / pageSize) + (transaction.Count() % pageSize > 0 ? 1 : 0);
            if (page > 1 && result < page)
            {
                ViewBag.Page = page - 1;
                var transactionsist = await PaginatedList<Transaction>.CreateAsync(transaction, page - 1 ?? 1, pageSize);
                return View(transactionsist);
            }
            else
            {
                var transactionsist = await PaginatedList<Transaction>.CreateAsync(transaction.AsNoTracking(), page ?? 1, pageSize);
                return View(transactionsist);
            }

        }
    }
}