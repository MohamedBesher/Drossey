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
    public class TopAddedViewComponent : ViewComponent
    {
        public IUnitOfWorkAsync _unitOfWork;
        protected readonly IMapper _mapper;
        public readonly UserManager<ApplicationUser> _userMgr;

        public TopAddedViewComponent(IUnitOfWorkAsync unitOfWork, IMapper mapper, UserManager<ApplicationUser> userMgr)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _userMgr = userMgr;


        }

        public async Task<IViewComponentResult> InvokeAsync()
        {

           
            var books = _unitOfWork.BookRepository.GetAllBooksAsync(_userMgr.GetUserId(HttpContext.User),1,6,null,0,0,0,0);           
            return View(books);
           
        
    }
    }
}