using System;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using System.IO;
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
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using MotleyFlash;
using MotleyFlash.Extensions;

namespace Drossey.Areas.admin.Controllers
{
    [ApiExplorerSettings(IgnoreApi = true)]

    [Authorize(Roles = "Administrator")]
    [Area("Admin")]

    public class GradesController : BaseController
    {
        public GradesController(IUnitOfWorkAsync unitOfWork, SignInManager<ApplicationUser> signInMgr, UserManager<ApplicationUser> userMgr, IPasswordHasher<ApplicationUser> hasher, IConfiguration config, IMapper mapper, ILogger<BaseController> logger, IMessenger messenger, IHostingEnvironment hostingEnvironment)
            : base(unitOfWork, signInMgr, userMgr, hasher, config, mapper, logger, messenger, hostingEnvironment)
        {
        }

        public IActionResult Index(SearchGradeModel model)
        {
            ViewBag.Keyword = model.Keyword;
            ViewBag.page = model.Page;
            ViewBag.pageSize = model.PageSize;
            ViewBag.countryId = model.CountryId;

            ViewBag.countries = new SelectList(_unitOfWork.CountryRepository.Filter(u=>u.IsPuplished).AsNoTracking(), "Id", "Name");

            return View();
 
        }


        [HttpPost]
        public ViewComponentResult Search(SearchGradeModel model)
        {

            return ViewComponent("SearchGrades", new { pageSize = model.PageSize, page = model.Page, keyword = model.Keyword, countryId=model.CountryId });
        }
        [HttpGet]
        public ViewComponentResult Search(int pageSize, int page, string keyword,long countryId)
        {

            return ViewComponent("SearchGrades", new { pageSize = pageSize, page = page, keyword = keyword,countryId= countryId });
        }

        public IActionResult Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var category = _unitOfWork.GradeRepository.Find(id);
            if (category == null)
            {
                return NotFound();
            }

            return View(category);
        }

        public IActionResult Create()
        {
            ViewData["CountryId"] = new SelectList(_unitOfWork.CountryRepository.Filter(u => u.IsPuplished), "Id", "Name");

            return View("Create", new GradeViewModel());
        }

      
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(GradeViewModel grade)
        {

            if (_unitOfWork.GradeRepository.All().Any(u => u.Name.Trim().ToLower() == grade.Name.Trim().ToLower() 
            && u.CountryId==grade.CountryId))
            {
                ViewData["CountryId"] = new SelectList(_unitOfWork.CountryRepository.Filter(u => u.IsPuplished), "Id", "Name");

                ModelState.AddModelError("", "هذا الصف مسجل من قبل .");
                return View(grade);
            }
            if (ModelState.IsValid)
            {
                var model = _mapper.Map<GradeViewModel, Grade>(grade);
               

                _unitOfWork.GradeRepository.Create(model);
                await _unitOfWork.CommitAsync();
                _messenger.Success(
                 title: $"تنبية",
                  text: "تم اضافة الصف  بنجاح");
                return RedirectToAction(nameof(Index));
            }
            return View(grade);
        }

        public IActionResult Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var grade = _unitOfWork.GradeRepository.Find(id);
            if (grade == null)
            {
                return NotFound();
            }
            var model = _mapper.Map<Grade, GradeViewModel>(grade);
            ViewData["CountryId"] = new SelectList(_unitOfWork.CountryRepository.Filter(u => u.IsPuplished), "Id", "Name");

            return View(model);
        }

       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(long id, GradeViewModel grade)
        {
            
            if (ModelState.IsValid)
            {
                try
                {
                    if (_unitOfWork.GradeRepository.All().Any(u => u.Name.Trim().ToLower() == grade.Name.Trim().ToLower() && u.CountryId == grade.CountryId && u.Id!=id))
                    {
                        ModelState.AddModelError("", "هذا الصف مسجل من قبل .");
                        ViewData["CountryId"] = new SelectList(_unitOfWork.CountryRepository.Filter(u => u.IsPuplished), "Id", "Name");

                        return View(grade);
                    }

                    var old=_unitOfWork.GradeRepository.Find(grade.Id);
                    if (old == null)
                    {
                        return NotFound();

                    }
                    old.Update(grade.Name,  grade.CountryId,grade.IsPuplished);
                    _unitOfWork.Commit();
                     

                }
                catch (DbUpdateConcurrencyException)
                {
                    _messenger.Error(
               title: $"تحذير",
                text: "حدث خطأ أثناء تعديل الصف .");

                }

                return RedirectToAction(nameof(Index));
            }
            return View(grade);
        }

        [HttpPost]
        public async Task<ActionResult> Delete(SearchGradeModel model)
        {

            try
            {
                var cat = _unitOfWork.GradeRepository.Find(model.Id);

                if (cat != null)
                {
                    _unitOfWork.GradeRepository.Delete(cat);
                    await _unitOfWork.CommitAsync();


                    var users = _unitOfWork.TermRepository.Filter(x => x.GradeId == model.Id).Any();
                    if (users)
                        return StatusCode(404, "TermsUsedGrades");
                    return RedirectToAction("Search", new { Page = model.Page, PageSize = model.PageSize , keyword =model.Keyword,CountryId=model.CountryId});

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
                  text: "هذا التصنيف مستخدم لا يمكن حذفه ");
                return StatusCode(404, "Error");
           

            }
        }



       
    }
}
