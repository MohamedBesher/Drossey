using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Drossey.Data.Core;
using Drossey.Data.Core.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Drossey.Admin;
using Microsoft.AspNetCore.Identity;
using Drossey.Data.Core.Dto;

namespace Drossey.ViewComponents
{
    public class SearchLibraryViewComponent : ViewComponent
    {
        public IUnitOfWorkAsync _unitOfWork;
        protected readonly IMapper _mapper;
        public readonly UserManager<ApplicationUser> _userMgr;

        public SearchLibraryViewComponent(IUnitOfWorkAsync unitOfWork, IMapper mapper, UserManager<ApplicationUser> userMgr)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _userMgr = userMgr;


        }

        public async Task<IViewComponentResult> InvokeAsync
           (int pageSize = 10, 
            int page = 0,
            string keyword = "",
            long countryId=0, 
            long gradeId=0, 
            long termId=0,
            long subjectId=0)
        {
            ViewBag.user = _userMgr.GetUserId(HttpContext.User);
            ViewBag.Keyword = keyword;
            ViewBag.page = page;
            ViewBag.pageSize = pageSize;
            ViewBag.termId = termId;
            ViewBag.gradeId = gradeId;
            ViewBag.countryId = countryId;
            ViewBag.subjectId = subjectId;

            ViewBag.user = _userMgr.GetUserId(HttpContext.User);
            var books = _unitOfWork.BookRepository.GetAllBooksAsync(_userMgr.GetUserId(HttpContext.User),page,pageSize,keyword,countryId,gradeId,termId,subjectId);
            ViewBag.ResultCount = books.Any() ? books[0].OverallCount : 0;
            var list =  PaginatedList<SubjectDto>.Create(books, page, pageSize);
            return View(list);
           
        
    }
    }
}