using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Drossey.Data.Core;
using Drossey.Data.Core.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Drossey.Admin;
using Drossey.Data.Core.Dto;
using Drossey.Admin.Services;
using Drossey.Data.Core.Enum;

namespace Drossey.Areas.admin.ViewComponents
{
    public class SearchCodesViewComponent : ViewComponent
    {
        public IUnitOfWorkAsync _unitOfWork;
        protected readonly IMapper _mapper;
        protected readonly IPinCodeGenerator _pinCodeGenerator;

        public SearchCodesViewComponent(IUnitOfWorkAsync unitOfWork, IMapper mapper,IPinCodeGenerator pinCodeGenerator) 
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _pinCodeGenerator = pinCodeGenerator;
        }
        //
        public async Task<IViewComponentResult> InvokeAsync(int? page, int pageSize,string keyword = "", int? status = 0, long sellerId = 0)
        {
            ViewBag.Keyword = keyword;
            ViewBag.page = page;
            ViewBag.pageSize = pageSize;
            ViewBag.status = status;
            ViewBag.sellerId = sellerId;
            var codes = _unitOfWork.PinCodeRepository.All().Include(u=>u.Seller);
            //

            IQueryable<PinCodeDto> codesList = codes.Where(x =>
            ((string.IsNullOrEmpty(keyword) || x.Amount.ToString().Contains(keyword))
            && (status == 0 || x.Status.GetHashCode() == status)
             && (sellerId == 0 || x.SellerId == sellerId)
            ))
                                          .Select(u => new PinCodeDto
                                          {
                                              Id = u.Id,
                                              Amount = u.Amount,
                                              PinCode = _pinCodeGenerator.GetCode(u.Amount, u.Code, u.Vector, u.Key),
                                              Price = u.Price,
                                              Status = u.Status,
                                              SellerName=u.Seller.Name,
                                              SellerId = u.SellerId
                                          }
                                              );
            ViewBag.ResultCount = codesList.Count();
            int result = (codesList.Count() / pageSize) + (codesList.Count() % pageSize > 0 ? 1 : 0);
            if (page > 1 && result < page)
            {
                ViewBag.Page = page - 1;
                var list = await PaginatedList<PinCodeDto>.CreateAsync(codesList, page-1 ?? 1, pageSize);
                return View(list);
            }
            else
            {
                var list = await PaginatedList<PinCodeDto>.CreateAsync(codesList.AsNoTracking(), page ?? 1, pageSize);
                return View(list);
            }
        
    }
    }
}