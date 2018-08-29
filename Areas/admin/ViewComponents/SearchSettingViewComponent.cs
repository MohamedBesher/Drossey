using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Drossey.Data.Core;
using Drossey.Data.Core.Enum;
using Drossey.Data.Core.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace Drossey.Areas.admin.ViewComponents
{
    public class SearchSettingViewComponent : ViewComponent
    {
        public IUnitOfWorkAsync _unitOfWork;
        protected readonly IMapper _mapper;
        public SearchSettingViewComponent(IUnitOfWorkAsync unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IViewComponentResult> InvokeAsync(int? page, int pageSize,string keyword = "")
        {
            ViewBag.Keyword = keyword;
            ViewBag.page = page;
            ViewBag.pageSize = pageSize;
            var settings = _unitOfWork.SettingRepository.All().Where(u=>u.key!=SettingEnum.Rate.ToString());

            IQueryable<Setting> setting = settings.Where(x => string.IsNullOrEmpty(keyword) ||
                                          x.Name.Contains(keyword) ||
                                          x.NameAr.Contains(keyword));
            ViewBag.ResultCount = setting.Count();
            int result = (setting.Count() / pageSize) + (setting.Count() % pageSize > 0 ? 1 : 0);
            if (page > 1 && result < page)
            {
                ViewBag.Page = page - 1;
                var settingList = await PaginatedList<Setting>.CreateAsync(setting, page ?? 1, pageSize);
                return View(settingList);
            }
            else
            {
                var settingList = await PaginatedList<Setting>.CreateAsync(setting.AsNoTracking(), page ?? 1, pageSize);
                return View(settingList);
            }
        
    }
    }
}