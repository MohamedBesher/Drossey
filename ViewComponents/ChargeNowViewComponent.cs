using AutoMapper;
using Drossey.Data.Core;
using Drossey.Data.Core.Models;
using Drossey.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Drossey.ViewComponents
{
    public class ChargeNowViewComponent: ViewComponent
    {
        public IUnitOfWorkAsync _unitOfWork;
        protected readonly IMapper _mapper;
        protected readonly UserManager<ApplicationUser> _userMgr;
        public ChargeNowViewComponent(IUnitOfWorkAsync unitOfWork, IMapper mapper, UserManager<ApplicationUser> userMgr)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _userMgr = userMgr;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            string userId = _userMgr.GetUserId(Request.HttpContext.User);
            var user = await _userMgr.GetUserAsync(Request.HttpContext.User);
            ViewBag.InUserRole = await _userMgr.IsInRoleAsync(user, "User");
            return View();
        }

    
    }
}
