using System;
using System.Linq;
using AutoMapper;
using Drossey.Areas.admin.Models;
using Microsoft.AspNetCore.Mvc;
using Drossey.Data.Core;
using Drossey.Data.Core.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using MotleyFlash;

namespace Drossey.Areas.admin.Controllers
{
    [ApiExplorerSettings(IgnoreApi = true)]

    [Authorize(Roles = "Administrator")]
    [Area("Admin")]

    public class ContactUsController : BaseController
    {
        public ContactUsController(IUnitOfWorkAsync unitOfWork, SignInManager<ApplicationUser> signInMgr, UserManager<ApplicationUser> userMgr, IPasswordHasher<ApplicationUser> hasher, IConfiguration config, IMapper mapper, ILogger<BaseController> logger, IMessenger messenger, IHostingEnvironment hostingEnvironment) : base(unitOfWork, signInMgr, userMgr, hasher, config, mapper, logger, messenger, hostingEnvironment)
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

            return ViewComponent("SearchContactUs", new { pageSize = model.PageSize, page = model.Page, keyword = model.Keyword });
        }
        [HttpGet]
        public ViewComponentResult Search(int pageSize, int page, string keyword)
        {

            return ViewComponent("SearchContactUs", new { pageSize = pageSize, page = page, keyword = keyword });
        }



        public ActionResult Details(long id)
        {  
            var model = _unitOfWork.ContactUsRepository.Find(id); 
            if(model==null)
            {
                return NotFound();
            }
            return View(model);
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


                var contactUs = _unitOfWork.ContactUsRepository.Find(search.Id);
                if (contactUs == null)
                    return StatusCode(404, "NotFound");
             
                _unitOfWork.ContactUsRepository.Delete(contactUs);
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
