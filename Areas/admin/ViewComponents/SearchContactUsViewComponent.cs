using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Drossey.Data.Core;
using Drossey.Data.Core.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Drossey.Areas.admin.ViewComponents
{
    public class SearchContactUsViewComponent : ViewComponent
    {
        public IUnitOfWorkAsync _unitOfWork;
        protected readonly IMapper _mapper;
        public SearchContactUsViewComponent(IUnitOfWorkAsync unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IViewComponentResult> InvokeAsync(int? page, int pageSize, string keyword = "")
        {
            ViewBag.Keyword = keyword;
            ViewBag.page = page;
            ViewBag.pageSize = pageSize;
            var contactUs = _unitOfWork.ContactUsRepository.All();

            IQueryable<ContactUs> contactUsList = contactUs.Where(x => string.IsNullOrEmpty(keyword) ||
                                          x.Name.ToLower().Contains(keyword.ToLower())).OrderByDescending(u => u.Id);
            ViewBag.ResultCount = contactUsList.Count();
            int result = (contactUsList.Count() / pageSize) + (contactUsList.Count() % pageSize > 0 ? 1 : 0);
            if (page > 1 && result < page)
            {
                ViewBag.Page = page - 1;
                var List = await PaginatedList<ContactUs>.CreateAsync(contactUsList, page - 1 ?? 1, pageSize);
                return View(List);
            }
            else
            {
                var List = await PaginatedList<ContactUs>.CreateAsync(contactUsList.AsNoTracking(), page ?? 1, pageSize);
                return View(List);
            }

        }
    }

}