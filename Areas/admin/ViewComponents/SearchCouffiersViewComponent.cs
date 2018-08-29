using System.Linq;
using System.Threading.Tasks;
using Drossey.Areas.admin.Dtos;
using Drossey.Data.Core.Enum;
using Drossey.Data.Core.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Drossey.Areas.admin.ViewComponents
{
    public class SearchCouffiersViewComponent : ViewComponent
    {
        public readonly UserManager<ApplicationUser> _userMgr;


        public SearchCouffiersViewComponent(UserManager<ApplicationUser> userMgr)
        {
            _userMgr = userMgr;
        }

        public async Task<IViewComponentResult> InvokeAsync(int? page, int pageSize, long cityId, long placeId,
            bool? suspended, string keyword = "")
        {
            ViewBag.Keyword = keyword;
            ViewBag.Page = page;
            ViewBag.PageSize = pageSize;
            ViewBag.PlaceId = placeId;
            ViewBag.CityId = cityId;
            ViewBag.Suspended = suspended;

            var users = await _userMgr.GetUsersInRoleAsync(EnumRoles.Couffier.ToString());

            var selectedUsers = users.Where(x => (
                                                string.IsNullOrEmpty(keyword) ||
                                                  x.FullName.Contains(keyword) ||
                                                  
                                                  x.PhoneNumber.Contains(keyword) ||
                                                  x.Email.Contains(keyword)
                                                  )
                                                 &&
                                                 (cityId == 0 || x.CityId == cityId)
                                                &&
                                                 (placeId == 0 || x.PlaceId == placeId)
                                                 &&
                                                 (suspended == null || x.IsSuspended == suspended.Value)
                                                 );
            var usersS= selectedUsers.Select(x => new UserDto
                {
                  
                    FullName = x.FullName,
                    PhoneNumber = x.PhoneNumber,
                    PhotoUrl = x.PhotoUrl,
                    IsSuspended = x.IsSuspended,
                    Email=x.Email,
                    Id=x.Id
                }).ToList();

            ViewBag.ResultCount = usersS.Count();
            var result = usersS.Count() / pageSize + (usersS.Count() % pageSize > 0 ? 1 : 0);
            if (page > 1 && result < page)
            {
                ViewBag.Page = page - 1;
                var userList = await PaginatedList<UserDto>.CreateAsync(usersS, page ?? 1, pageSize);
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