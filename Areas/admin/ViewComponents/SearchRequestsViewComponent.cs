using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Drossey.Areas.admin.Dtos;
using Drossey.Data.Core;
using Drossey.Data.Core.Enum;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Extensions.Internal;

namespace Drossey.Areas.admin.ViewComponents
{
    public class SearchRequestsViewComponent : ViewComponent
    {
        public IUnitOfWorkAsync _unitOfWork;
        protected readonly IMapper _mapper;

        public SearchRequestsViewComponent(IUnitOfWorkAsync unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public IViewComponentResult Invoke(int? page, int pageSize, long cityId, long? placeId,
            RequestStatus? status, string userId = "", string keyword = "", long? serviceId = 0)
        {
            ViewBag.UserId = userId;
            ViewBag.Page = page;
            ViewBag.PageSize = pageSize;
            ViewBag.PlaceId = placeId;
            ViewBag.CityId = cityId;
            ViewBag.Status = status;
            ViewBag.Keyword = keyword;
            ViewBag.ServiceId = serviceId;


            var requests = _unitOfWork.RequestRepository.All()
                .Include(r => r.Places)
                .Include(r => r.User)

                .Include(u => u.RequestsDetails).ThenInclude(u => u.Couffier);



            var requestsDto = requests.Select(x => new RequestDto
            {
                FullName = x.User.FullName,
                Id = x.Id,
                UserId = x.UserId,
                PlaceId = x.PlaceId,
                RequestStatus = x.RequestStatus,
                PlaceName = x.Places.NameAr,
                Address = x.Address,
                RequestDate = x.RequestDate,
                Price = x.RequestsDetails.FirstOrDefault(u => u.ResponseStatus == ResponseStatus.Approved) != null ? x.RequestsDetails.FirstOrDefault(u => u.ResponseStatus == ResponseStatus.Approved).Price : (decimal?)null,
                CouffierId = x.RequestsDetails.FirstOrDefault(u => u.ResponseStatus == ResponseStatus.Approved) != null ? x.RequestsDetails.FirstOrDefault(u => u.ResponseStatus == ResponseStatus.Approved).CouffierId : "",
                CouffierName = x.RequestsDetails.FirstOrDefault(u => u.ResponseStatus == ResponseStatus.Approved) != null ? x.RequestsDetails.FirstOrDefault(u => u.ResponseStatus == ResponseStatus.Approved).Couffier.FullName : ""
            });



            var requestWithQuery = requestsDto.Where(x =>
               ((placeId == null || placeId == 0) || x.PlaceId == placeId)
               &&
                ((serviceId == null || placeId == 0) || x.ServiceId == serviceId)
               &&
               (string.IsNullOrEmpty(userId) || x.UserId == userId)
               &&
               (status == null || x.RequestStatus == status)
               &&
               (string.IsNullOrEmpty(keyword) || (x.FullName.Contains(keyword) || x.CouffierName.Contains(keyword)))
             );



            ViewBag.ResultCount = requestWithQuery.Count();
            var result = requestWithQuery.Count() / pageSize + (requestWithQuery.Count() % pageSize > 0 ? 1 : 0);
            if (page > 1 && result < page)
            {
                ViewBag.Page = page - 1;
                var userList = PaginatedList<RequestDto>.Create(requestWithQuery, page ?? 1, pageSize);
                return View(userList);
            }
            else
            {
                var userList = PaginatedList<RequestDto>.Create(requestWithQuery, page ?? 1, pageSize);
                return View(userList);
            }
        }
    }
}