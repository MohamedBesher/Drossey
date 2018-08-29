using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Drossey.Data.Core;
using Drossey.Data.Core.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Drossey.Admin;
using Drossey.Data.Core.Dto;
using Microsoft.AspNetCore.Identity;
using Drossey.Data.Core.Enum;

namespace Drossey.ViewComponents
{
    public class WishListViewComponent : ViewComponent
    {
        public IUnitOfWorkAsync _unitOfWork;
        protected readonly IMapper _mapper;
        protected readonly UserManager<ApplicationUser> _userMgr;

        
        public WishListViewComponent(IUnitOfWorkAsync unitOfWork, IMapper mapper, UserManager<ApplicationUser>  userMgr)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _userMgr = userMgr;
        }

        public async Task<IViewComponentResult> InvokeAsync(int? page)
        {
           
            var User = _userMgr.GetUserId(HttpContext.User);

           

            var model = _unitOfWork.FavoriteRepository.GetAllFavourite(User);
            int pageSize = 10;



            ViewBag.page = page ?? 1;
            ViewBag.pageSize = pageSize;

            ViewBag.ResultCount = model.Any() ? model.Count() : 0;
         

            var Favorite =await PaginatedList<SubjectDto>.CreateAsync(model, page ?? 1, pageSize);
            return View(Favorite);
           
        
    }
    }
}