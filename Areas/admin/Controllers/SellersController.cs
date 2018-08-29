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
using Drossey.Data.Core.Enum;

namespace Drossey.Areas.admin.Controllers
{
    [Authorize(Roles = "Administrator")]
    [Area("Admin")]
    [ApiExplorerSettings(IgnoreApi = true)]

    public class SellersController : BaseController
    {
        public SellersController(IUnitOfWorkAsync unitOfWork, SignInManager<ApplicationUser> signInMgr, UserManager<ApplicationUser> userMgr, IPasswordHasher<ApplicationUser> hasher, IConfiguration config, IMapper mapper, ILogger<BaseController> logger, IMessenger messenger, IHostingEnvironment hostingEnvironment) : base(unitOfWork, signInMgr, userMgr, hasher, config, mapper, logger, messenger, hostingEnvironment)
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

            return ViewComponent("SearchSellers", new { pageSize = model.PageSize, page = model.Page, keyword = model.Keyword });
        }
        [HttpGet]
        public ViewComponentResult Search(int pageSize, int page, string keyword)
        {

            return ViewComponent("SearchSellers", new { pageSize = pageSize, page = page, keyword = keyword });
        }


        public ActionResult Create()
        {


            return View();
        }
        [HttpPost]
        public async Task<ActionResult> Create(SellerViewModel seller)
        {

            if (_unitOfWork.SellerRepository.All().Any(u => u.Name == seller.Name))
            {
                ModelState.AddModelError("", "هذة الموزع مسجلة من قبل .");
                return View(seller);
            }
            if (seller != null && ModelState.IsValid)
            {
                var model = _mapper.Map<SellerViewModel, Seller>(seller);
                _unitOfWork.SellerRepository.Create(model);
                await _unitOfWork.CommitAsync();

                _messenger.Success(
                   title: $"تنبية !",
                   text: "تم اضافة الموزع بنجاح");
                return RedirectToAction(nameof(Index));
            }

            return View(seller);
        }

        public ActionResult Edit(long id)
        {
            var model = _unitOfWork.SellerRepository.Find(id);
            var seller = _mapper.Map<Seller, SellerViewModel>(model);

            return View(seller);
        }


        [HttpPost]
        public async Task<ActionResult> Edit(SellerViewModel seller)
        {
            if (seller != null && ModelState.IsValid)
            {
                var model = _mapper.Map<SellerViewModel, Seller>(seller);

                _unitOfWork.SellerRepository.Update(model);
                await _unitOfWork.CommitAsync();
                _messenger.Success(
                   title: $"تنبية !",
                       text: "تم تعديل الموزع بنجاح");
                return RedirectToAction(nameof(Index));
            }

            return View(seller);
        }


        public ActionResult Details(long id)
        {
            var model = _unitOfWork.CountryRepository.Find(id);
            var cityModel = _mapper.Map<Country, CountryViewModel>(model);
            return View(cityModel);
        }




        [HttpPost]
        public ActionResult Approve(Pager search)
        {
            try
            {
                var path = _unitOfWork.SellerRepository.Find(search.Id);
                if (path == null)
                    return StatusCode(404, "NotFound");

                path.IsActive = search.IsActive;
                CodeStatus status = search.IsActive ? CodeStatus.IsActive : CodeStatus.Suspended;
                List<PinCode> codes = _unitOfWork.PinCodeRepository.All()
                    .Where(u => u.SellerId == search.Id && u.Status==CodeStatus.Suspended).ToList();
                if (codes.Any())
                {
                    codes.ForEach(item =>
                      {
                          item.Status = search.IsActive ? Data.Core.Enum.CodeStatus.IsActive : Data.Core.Enum.CodeStatus.Suspended;
                      });
                }
                _unitOfWork.Commit();
                return RedirectToAction("Search", new { Page = search.Page, PageSize = search.PageSize });
            }
            catch (Exception e)
            {
                _logger.LogError($"Error in delete country {e}");
                return StatusCode(404, "Error");
            }

        }


        [HttpPost]
        public ActionResult Delete(Pager search)
        {
            try
            {
                var seller = _unitOfWork.SellerRepository.Find(search.Id);
                if (seller == null)
                    return StatusCode(404, "NotFound");


                CodeStatus status = search.IsActive ? CodeStatus.IsActive : CodeStatus.Suspended;
                List<PinCode> codes = _unitOfWork.PinCodeRepository.All()
                    .Where(u => u.SellerId == search.Id).ToList();

                _unitOfWork.SellerRepository.Delete(seller);
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
