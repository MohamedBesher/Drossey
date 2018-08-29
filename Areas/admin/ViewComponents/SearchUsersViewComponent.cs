using System.Linq;
using System.Threading.Tasks;
using Drossey.Areas.admin.Dtos;
using Drossey.Data.Core.Enum;
using Drossey.Data.Core.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Drossey.Admin;
using Drossey.Data.Core;
using Drossey.Data.Core.Dto;

namespace Drossey.Areas.admin.ViewComponents
{
    public class SearchUsersViewComponent : ViewComponent
    {
        public readonly UserManager<ApplicationUser> _userMgr;

        public IUnitOfWorkAsync _unitOfWork;

        public SearchUsersViewComponent(UserManager<ApplicationUser> userMgr, IUnitOfWorkAsync unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _userMgr = userMgr;

        }

        public async Task<IViewComponentResult> InvokeAsync(int? page, int pageSize, long countryId,
            bool? suspended, string keyword = "")
        {
            ViewBag.Keyword = keyword;
            ViewBag.Page = page;
            ViewBag.PageSize = pageSize;
            ViewBag.CountryId = countryId;
            ViewBag.Suspended = suspended;
            var users =_unitOfWork.UserRepository.GetAllUsers(EnumRoles.User.ToString());
            var selectedUsers = users.Where(x => (
                                                string.IsNullOrEmpty(keyword) ||
                                                  x.FirstName.Contains(keyword) ||
                                                    x.FirstName.Contains(keyword) ||
                                                  x.PhoneNumber.Contains(keyword) ||
                                                  x.Email.Contains(keyword)
                                                  )
                                                 &&
                                                 (countryId == 0 || x.CountryId == countryId) &&        
                                                 (suspended == null || x.IsSuspended == suspended.Value)
                                                 );
            var usersS= selectedUsers;

            ViewBag.ResultCount = usersS.Count();
            var result = usersS.Count() / pageSize + (usersS.Count() % pageSize > 0 ? 1 : 0);
            if (page > 1 && result < page)
            {
                ViewBag.Page = page - 1;
                var userList =await PaginatedList<UserDto>.CreateAsync(usersS, page - 1 ?? 1, pageSize);
                return View(userList);
            }
            else
            {
                var userList = await PaginatedList<UserDto>.CreateAsync(usersS, page ?? 1, pageSize);
                return View(userList);
            }
        }
    }
}