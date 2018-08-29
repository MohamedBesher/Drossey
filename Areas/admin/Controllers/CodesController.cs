using AutoMapper;
using Drossey.Admin.Services;
using Drossey.Areas.admin.Models;
using Drossey.Data.Core;
using Drossey.Data.Core.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using MotleyFlash;
using MotleyFlash.Extensions;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Drossey.Areas.admin.Controllers
{
    [Authorize(Roles = "Administrator")]
    [Area("Admin")]
    [ApiExplorerSettings(IgnoreApi = true)]

    public class CodesController : BaseController
    {
        private readonly IPinCodeGenerator _pinCodeGenerator; 
        public CodesController(IUnitOfWorkAsync unitOfWork, SignInManager<ApplicationUser> signInMgr, UserManager<ApplicationUser> userMgr, IPasswordHasher<ApplicationUser> hasher, IConfiguration config, IMapper mapper, ILogger<BaseController> logger, IMessenger messenger, IHostingEnvironment hostingEnvironment, IPinCodeGenerator pinCodeGenerator) : base(unitOfWork, signInMgr, userMgr, hasher, config, mapper, logger, messenger, hostingEnvironment)
        {
            _pinCodeGenerator = pinCodeGenerator;
        }
        public IActionResult Index(CodeSearchModel model)
        {
            LoadSellers();
            ViewBag.Keyword = model.Keyword;
            ViewBag.page = model.Page;
            ViewBag.pageSize = model.PageSize;
            ViewBag.status = model.Status;
            ViewBag.sellerId = model.SellerId;
            return View();
        }
        [HttpPost]
        public ViewComponentResult Search(CodeSearchModel model)
        {
            
            return ViewComponent("SearchCodes", new { pageSize = model.PageSize, page = model.Page, keyword = model.Keyword,status = model.Status,sellerId = model.SellerId});
            }
        [HttpGet]
        public ViewComponentResult Search(int pageSize, int page, string keyword,int? status,long? sellerId)
        {
            
            return ViewComponent("SearchCodes", new { pageSize = pageSize, page = page, keyword = keyword, status = status, sellerId = sellerId });
        }

     
        public IActionResult Create()
        {
            LoadSellers();
            return View("Create", new CodeViewModel());
        }

      
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CodeViewModel code)
        {
            if (ModelState.IsValid)
            {
                /// <returns> inilization Vector - key - digits - random </returns>           
                    for (int i = 0; i < code.Count; i++)
                    {
                    Tuple<string, string, string,double> tuple = _pinCodeGenerator.Encrypt(code.Amount);
                    _unitOfWork.PinCodeRepository.Create(new PinCode()
                    {
                        Amount=code.Amount,
                        Price = code.Price,
                        Vector = tuple.Item1,
                        Key=tuple.Item2,
                        Code =tuple.Item4,
                        SellerId=code.SellerId,
                        Status= code.Status

                    });
                    }              
                await _unitOfWork.CommitAsync();
                _messenger.Success(
                 title: $"تنبية",
                  text: "تم اضافة الكارت  بنجاح");
                return RedirectToAction(nameof(Index));
            }
            LoadSellers();
            return View(code);
        }
      




        //public IActionResult Edit(long? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var Term = _unitOfWork.TermRepository.Find(id);
        //    if (Term == null)
        //    {
        //        return NotFound();
        //    }
        //    var model = _mapper.Map<Term, TermViewModel>(Term);
        //    ViewData["grades"] = new SelectList(_unitOfWork.GradeRepository.GetAllGrades(), "Id", "Name");

        //    return View(model);
        //}


        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public IActionResult Edit(long id, TermViewModel Term)
        //{

        //    if (ModelState.IsValid)
        //    {
        //        try
        //        {
        //            if (_unitOfWork.TermRepository.All().Any(u => u.Name.ToLower() == Term.Name.ToLower() && u.GradeId == Term.GradeId && u.Id!=id))
        //            {
        //                ModelState.AddModelError("", "هذا الترم مسجل من قبل .");
        //                ViewData["grades"] = new SelectList(_unitOfWork.GradeRepository.GetAllGrades(), "Id", "Name");

        //                return View(Term);
        //            }

        //            var old=_unitOfWork.TermRepository.Find(Term.Id);
        //            if (old == null)
        //            {
        //                return NotFound();

        //            }
        //            old.Update(Term.Name,  Term.GradeId,Term.IsPuplished);
        //            _unitOfWork.Commit();


        //        }
        //        catch (DbUpdateConcurrencyException)
        //        {
        //            _messenger.Error(
        //       title: $"تحذير",
        //        text: "حدث خطأ أثناء تعديل الترم .");

        //        }

        //        return RedirectToAction(nameof(Index));
        //    }
        //    return View(Term);
        //}

        [HttpPost]
        public async Task<ActionResult> Delete(CodeSearchModel model)
        {

            try
            {
                var code = _unitOfWork.PinCodeRepository.Find(model.Id);

                if (code != null)
                {
                    _unitOfWork.PinCodeRepository.Delete(code);
                    await _unitOfWork.CommitAsync();

                    return RedirectToAction("Search", new
                    { Page = model.Page, PageSize = model.PageSize, keyword = model.Keyword, status = model.Status, sellerId = model.SellerId });
                }

                else
                {
                    return StatusCode(404, "NotFound");

                }
            }
            catch (Exception)
            {
                _messenger.Error(
                   title: $"تنبية !",
                  text: "حدث خطأ أثناء حذف الكارت .");
                return StatusCode(404, "Error");


            }
        }



        [HttpPost]
        public ActionResult DeletebyIds(string[] ids)
        {
            try
            {
                var list = ids.Select(long.Parse).ToList();
                var codes = _unitOfWork.PinCodeRepository.All().Where(u => list.Contains(u.Id));
                if(codes.Any())
                {
                 _unitOfWork.PinCodeRepository.DeleteRange(codes);
                _unitOfWork.Commit();
                 return Json("OK");
                }

                return Json("Error");
            }
            catch (Exception)
            {
                return Json("Error" + ids[0]);
            }
           
        }



    }
}
