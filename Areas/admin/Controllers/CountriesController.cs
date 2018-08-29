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
    //[Route("[controller]/[action]")]
    [Authorize(Roles = "Administrator")]
    [Area("Admin")]
    [ApiExplorerSettings(IgnoreApi = true)]

    public class CountriesController : BaseController
    {
        public CountriesController(IUnitOfWorkAsync unitOfWork, SignInManager<ApplicationUser> signInMgr, UserManager<ApplicationUser> userMgr, IPasswordHasher<ApplicationUser> hasher, IConfiguration config, IMapper mapper, ILogger<BaseController> logger, IMessenger messenger, IHostingEnvironment hostingEnvironment) : base(unitOfWork, signInMgr, userMgr, hasher, config, mapper, logger, messenger, hostingEnvironment)
        {
        }

        public ActionResult Index(Pager model)
        {
            try
            {
                ViewBag.Keyword = model.Keyword;
                ViewBag.page = model.Page;
                ViewBag.pageSize = model.PageSize;
                return View();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return View();
            }

        }



        [HttpPost]
        public ViewComponentResult Search(SearchModel model)
        {

            return ViewComponent("SearchCountries", new { pageSize = model.PageSize, page = model.Page, keyword = model.Keyword });
        }
        [HttpGet]
        public ViewComponentResult Search(int pageSize, int page, string keyword)
        {

            return ViewComponent("SearchCountries", new { pageSize = pageSize, page = page, keyword = keyword });
        }


        public ActionResult Create()
        {


            return View();
        }

        public ActionResult Edit(long id)
        {
            var model = _unitOfWork.CountryRepository.Find(id);
            var cityModel = _mapper.Map<Country, CountryViewModel>(model);

            return View(cityModel);
        }
        public ActionResult Details(long id)
        {
            var model = _unitOfWork.CountryRepository.Find(id);
            var cityModel = _mapper.Map<Country, CountryViewModel>(model);
            return View(cityModel);
        }

        [HttpPost]
        public async Task<ActionResult> Create(CountryViewModel country)
        {

            if (_unitOfWork.CountryRepository.All().Any(u => u.Name == country.Name))
            {
                ModelState.AddModelError("", "هذة البلد مسجلة من قبل .");
                return View(country);
            }
            if (country != null && ModelState.IsValid)
            {
                var model = _mapper.Map<CountryViewModel, Country>(country);
                _unitOfWork.CountryRepository.Create(model);
                await _unitOfWork.CommitAsync();
                _messenger.Success(
                   title: $"تنبية !",
                   text: "تم اضافة البلد بنجاح");
                return RedirectToAction(nameof(Index));
            }

            return View(country);
        }


        [HttpPost]
        public async Task<ActionResult> Edit(CountryViewModel country)
        {
            if (country != null && ModelState.IsValid)
            {
                var model = _mapper.Map<CountryViewModel, Country>(country);
                _unitOfWork.CountryRepository.Update(model);
                await _unitOfWork.CommitAsync();
                _messenger.Success(
                   title: $"تنبية !",
                       text: "تم تعديل البلد بنجاح");
                return RedirectToAction(nameof(Index));
            }

            return View(country);
        }

        //[HttpGet]
        //public async Task<ActionResult> Delete(long id)
        //{

        //    try
        //    {
        //        var model = _unitOfWork.CountryRepository.Find(id);

        //        if (model != null)
        //        {
        //            _unitOfWork.CountryRepository.Delete(model);
        //            await _unitOfWork.CommitAsync();
        //            _messenger.Success(
        //           title: $"تنبية !",
        //           text: "تم حذف البلد بنجاح");
        //               return RedirectToAction("Search", new { Page = 1, PageSize = 10 });

        //        }
        //        return View();

        //    }
        //    catch (Exception ex)
        //    {
        //        _logger.LogError(ex.Message);
        //        _messenger.Error(
        //           title: $"تنبية !",
        //          text: "هذة البلد مستخدمة لا يمكن حذفها ");
        //        return StatusCode(404, "Error");
        //        // return RedirectToAction("Search", new { Page = 1, PageSize = 10 });

        //    }
        //}

        [HttpPost]
        public ActionResult Delete(Pager search)
        {
            try
            {


                var path = _unitOfWork.CountryRepository.Find(search.Id);
                if (path == null)
                    return StatusCode(404, "NotFound");
                var users = _unitOfWork.UserRepository.Filter(x => x.CountryId == search.Id).Any();
                if (users)
                    return StatusCode(404, "UserUsedCity");
                _unitOfWork.CountryRepository.Delete(path);
                _unitOfWork.Commit();
                return RedirectToAction("Search", new { Page = search.Page, PageSize = search.PageSize });
            }
            catch (Exception e)
            {
                _logger.LogError($"Error in delete country {e}");
                return StatusCode(404, "Error");
            }

        }
    }

}

