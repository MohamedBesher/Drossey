using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Drossey.Data.Core;
using Drossey.Data.Core.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace Drossey.Areas.admin.ViewComponents
{
    public class SearchPlacesViewComponent : ViewComponent
    {
        public IUnitOfWorkAsync _unitOfWork;
        protected readonly IMapper _mapper;
        public SearchPlacesViewComponent(IUnitOfWorkAsync unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IViewComponentResult> InvokeAsync(int? page, int pageSize,string keyword = "")
        {
            ViewBag.Keyword = keyword;
            ViewBag.page = page;
            ViewBag.pageSize = pageSize;
            var places = _unitOfWork.PlaceRepository.All();

            IQueryable<Place> place = places.Where(x => string.IsNullOrEmpty(keyword) ||
                                          x.Name.Contains(keyword) ||
                                          x.NameAr.Contains(keyword));
            ViewBag.ResultCount = place.Count();
            int result = (place.Count() / pageSize) + (place.Count() % pageSize > 0 ? 1 : 0);
            if (page > 1 && result < page)
            {
                ViewBag.Page = page - 1;
                var placeList = await PaginatedList<Place>.CreateAsync(place, page ?? 1, pageSize);
                return View(placeList);
            }
            else
            {
                var placeList = await PaginatedList<Place>.CreateAsync(place.AsNoTracking(), page ?? 1, pageSize);
                return View(placeList);
            }
        
    }
    }
}