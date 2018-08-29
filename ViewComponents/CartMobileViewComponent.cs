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
using System;

namespace Drossey.ViewComponents
{
    public class CartMobileViewComponent : ViewComponent
    {
        public IUnitOfWorkAsync _unitOfWork;
        protected readonly IMapper _mapper;
        protected readonly UserManager<ApplicationUser> _userMgr;

        
        public CartMobileViewComponent(IUnitOfWorkAsync unitOfWork, IMapper mapper, UserManager<ApplicationUser>  userMgr)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _userMgr = userMgr;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            int itemCount = 0;
            int favoriteCount = 0;
            decimal total = 0;
            string userId = _userMgr.GetUserId(Request.HttpContext.User);
            var user =await _userMgr.GetUserAsync(Request.HttpContext.User);
            if (user != null)
                ViewBag.InUserRole = await _userMgr.IsInRoleAsync(user, "User");
            else
                ViewBag.InUserRole = false;

            if (!string.IsNullOrEmpty(userId))
            {

                var carts = _unitOfWork.CartRepository.GetAllCarts(userId);
                var favorites = _unitOfWork.FavoriteRepository.GetAllFavourite(userId);
                itemCount = carts.Count();
                favoriteCount = favorites.Count();

            total = Convert.ToInt32(carts.Sum(u => (u.Price - ((u.Price * u.DiscountPercentage) / 100))));

            }
            ViewData["cart-items-count"] = itemCount;
            ViewData["favorite-items-count"] = favoriteCount;
            ViewBag.total = total;
            return View();
           
        
    }
    }
}