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
    public class SearchSellersViewComponent : ViewComponent
    {
        public IUnitOfWorkAsync _unitOfWork;
        protected readonly IMapper _mapper;
        public SearchSellersViewComponent(IUnitOfWorkAsync unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IViewComponentResult> InvokeAsync(int? page, int pageSize,string keyword = "")
        {
            ViewBag.Keyword = keyword;
            ViewBag.page = page;
            ViewBag.pageSize = pageSize;
            var sellers = _unitOfWork.SellerRepository.All();

            IQueryable<Seller> sellerList = sellers.Where(x => string.IsNullOrEmpty(keyword) ||
                                          x.Name.Contains(keyword))
                                          .OrderByDescending(u=>u.CreationDate);
            ViewBag.ResultCount = sellerList.Count();
            int result = (sellerList.Count() / pageSize) + (sellerList.Count() % pageSize > 0 ? 1 : 0);
            if (page > 1 && result < page)
            {
                ViewBag.Page = page - 1;
                var list = await PaginatedList<Seller>.CreateAsync(sellerList, page - 1 ?? 1, pageSize);
                return View(list);
            }
            else
            {
                var list = await PaginatedList<Seller>.CreateAsync(sellerList.AsNoTracking(), page ?? 1, pageSize);
                return View(list);
            }
        
    }
    }
}