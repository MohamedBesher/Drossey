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
    public class ProfileNavViewComponent : ViewComponent
    {
        public IUnitOfWorkAsync _unitOfWork;
        protected readonly IMapper _mapper;
        protected readonly UserManager<ApplicationUser> _userMgr;

        
        public ProfileNavViewComponent(IUnitOfWorkAsync unitOfWork, IMapper mapper, UserManager<ApplicationUser>  userMgr)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _userMgr = userMgr;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
           
            string userId = _userMgr.GetUserId(Request.HttpContext.User);
            var user =await _userMgr.GetUserAsync(Request.HttpContext.User);
            if (user!=null)
            {
            ViewData["userName"] = user.FirstName + ' ' + user.LastName;
            ViewData["Balance"] = user.Balance;
            ViewData["PhotoUrl"] = user.PhotoUrl;
            }
            else
            {
                ViewData["userName"] ="";
                ViewData["Balance"] = "0";
                ViewData["PhotoUrl"] = "";
            }
            return View();
           
        
    }
    }
}