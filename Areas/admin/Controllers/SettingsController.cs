using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Drossey.Areas.admin.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Drossey.Data;
using Drossey.Data.Core;
using Drossey.Data.Core.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using MotleyFlash;
using MotleyFlash.Extensions;

namespace Drossey.Areas.admin.Controllers
{
    [Authorize(Roles = "Administrator")]
    [ApiExplorerSettings(IgnoreApi = true)]

    public class SettingsController : BaseController
    {
       

        // GET: Settings
        public SettingsController(IUnitOfWorkAsync unitOfWork, SignInManager<ApplicationUser> signInMgr, UserManager<ApplicationUser> userMgr, IPasswordHasher<ApplicationUser> hasher, IConfiguration config, IMapper mapper, ILogger<BaseController> logger, IMessenger messenger, IHostingEnvironment hostingEnvironment) : base(unitOfWork, signInMgr, userMgr, hasher, config, mapper, logger, messenger, hostingEnvironment)
        {
        }

        public  IActionResult Index(Pager model)
        {
            ViewBag.Keyword = model.Keyword;
            ViewBag.page = model.Page;
            ViewBag.pageSize = model.PageSize;
          
           
           
            return View();
        }


        [HttpPost]
        public ViewComponentResult Search(SearchModel model)
        {

            return ViewComponent("SearchSetting", new { pageSize = model.PageSize, page = model.Page, keyword = model.Keyword });
        }
        //[HttpGet]
        //public ViewComponentResult Search(int pageSize, int page, string keyword)
        //{

        //    return ViewComponent("SearchSetting", new { pageSize = pageSize, page = page, keyword = keyword });
        //}

        // GET: Settings/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var setting = await _unitOfWork.SettingRepository.All()
                .SingleOrDefaultAsync(m => m.Id == id);
            if (setting == null)
            {
                return NotFound();
            }

            return View(setting);
        }

        // GET: Settings/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Settings/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,key,Value,Name,NameAr")] SettingViewModel setting)
        {
            if (ModelState.IsValid)
            {
                var model = _mapper.Map<SettingViewModel, Setting>(setting);
                _unitOfWork.SettingRepository.Create(model);
                await _unitOfWork.CommitAsync();
                _messenger.Success(
                  title: $"تنبية !",
                  text: "تم الاضافة بنجاح");
                return RedirectToAction(nameof(Index));
            }
            return View(setting);
        }

        // GET: Settings/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var setting = await _unitOfWork.SettingRepository.All().SingleOrDefaultAsync(m => m.Id == id);
            var model = _mapper.Map<Setting, SettingViewModel>(setting);
            if (model == null)
            {
                return NotFound();
            }
            return View(model);
        }

        // POST: Settings/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, SettingViewModel setting)
        {
            if (id != setting.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var model = _mapper.Map<SettingViewModel, Setting>(setting);
                    _unitOfWork.SettingRepository.Update(model);
                    await _unitOfWork.CommitAsync();
                    _messenger.Success(
               title: $"تنبية !",
               text: "تم التعديل  بنجاح");
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SettingExists(setting.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(setting);
        }





        private bool SettingExists(long id)
        {
            return _unitOfWork.SettingRepository.All().Any(e => e.Id == id);
        }
    }
}
